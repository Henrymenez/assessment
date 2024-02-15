using Movie.Core.Dtos.Request;
using Movie.Core.Dtos.Response;
using Movie.Core.Utility;
using Movie.Service.Utility;

namespace Movie.Service.Interface;

public interface IMovieService
{
    Task<ServiceResponse<MovieInfoResponseDto>> SearchMovie(MovieSearchRequestDto model);
    Task<ServiceResponse<IEnumerable<string>>> LastFiveSearchedMovie();
}
