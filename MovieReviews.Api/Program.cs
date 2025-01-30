using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MovieReviews.Application;
using MovieReviews.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication()
.AddJwtBearer(jwtOptions =>
{
    jwtOptions.Authority = builder.Configuration["Auth:Authority"];
    jwtOptions.Audience = builder.Configuration["Auth:Audience"];
    jwtOptions.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    };

    jwtOptions.MapInboundClaims = false;
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Description = "OpenID Connect",
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(builder.Configuration["Auth:AuthorizeUrl"]!),
                TokenUrl = new Uri(builder.Configuration["Auth:TokenUrl"]!),
                // TODO: Add scopes.
            }
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "oauth2",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddApplicationServices();

builder.Services.AddPersistence(builder.Configuration.GetConnectionString("MoviesDb")!);
builder.Services.AddScoped<IApplicationContext>(serviceProvider => serviceProvider.GetRequiredService<MovieReviewsDbContext>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.OAuthClientId(builder.Configuration["Auth:ClientId"]);
        c.OAuthUsePkce();
        c.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
        {
            ["audience"] = builder.Configuration["Auth:Audience"]!
        });
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
