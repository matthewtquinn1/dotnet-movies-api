using MovieReviews.Application.Movies.Commands;
using MovieReviews.Application.Movies.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MovieReviews.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMediator _mediator;

        public MoviesController(
            ILogger<MoviesController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // TODO: Replace try catch with a middleware that catches exceptions and returns a proper response.
            try
            {
                return Ok(await _mediator.Send(new GetMoviesQuery()));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get movies");
                throw;
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            // TODO: Replace try catch with a middleware that catches exceptions and returns a proper response.
            try
            {
                var movie = await _mediator.Send(new GetMovieByIdQuery(id));

                return movie is null ? NotFound() : Ok(movie);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to find movie");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMovieCommand command)
        {
            // TODO: Replace try catch with a middleware that catches exceptions and returns a proper response.
            try
            {
                var result = await _mediator.Send(command);

                return CreatedAtAction(nameof(Post), new { id = result.Id }, result);
            } 
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create movie");
                throw;
            }
        }
    }
}
