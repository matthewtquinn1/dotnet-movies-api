using MovieReviews.Application.Movies.Commands;
using MovieReviews.Application.Movies.Queries;
using MovieReviews.Domain;
using MovieReviews.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MovieReviews.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly Mediator _mediator;

        public MoviesController(
            ILogger<MoviesController> logger,
            Mediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieDto>> Get()
        {
            // TODO: Replace try catch with a middleware that catches exceptions and returns a proper response.
            try
            {
                return await _mediator.Send(new GetMoviesQuery());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get movies");
                throw;
            }
        }

        [HttpPost]
        public async Task<MovieDto> Post([FromBody] CreateMovieCommand command)
        {
            // TODO: Use a proper validation library to validate the command.
            // TODO: Replace try catch with a middleware that catches exceptions and returns a proper response.
            try
            {
                return await _mediator.Send(command);
            } 
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create movie");
                throw;
            }
        }
    }
}
