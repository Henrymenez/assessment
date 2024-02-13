using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.API.Controllers.Shared;
using Movie.Core.Dtos.Request;
using Movie.Core.Dtos.Response;
using Movie.Core.Utility;
using Movie.Service.Interface;

namespace Movie.API.Controllers;


[Route("api/[Controller]")]
[ApiController]
public class MovieController : BaseController
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
    }

    [HttpGet("search")]
    [ProducesResponseType(200, Type = typeof(ApiRecordResponse<MovieInfoResponseDto>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> SearchMovie([FromQuery] string MovieTitle)
    {
        var response = await _movieService.SearchMovie(new MovieSearchRequestDto { MovieTitle = MovieTitle });
        return ComputeApiResponse(response);
    }

    [HttpGet("last-five-entries")]
    [ProducesResponseType(200, Type = typeof(ApiResponse<PaginationResponse<MovieInfoResponseDto>>))]
    [ProducesResponseType(404, Type = typeof(ApiResponse))]
    [ProducesResponseType(400, Type = typeof(ApiResponse))]
    public async Task<IActionResult> LastFiveMovies()
    {
        var response = await _movieService.LastFiveSearchedMovie();
        return Ok(response);
    }

}
