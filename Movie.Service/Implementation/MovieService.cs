using Microsoft.EntityFrameworkCore;
using Movie.Core.AppDbContext;
using Movie.Core.Dtos.Request;
using Movie.Core.Dtos.Response;
using Movie.Core.Entity;
using Movie.Service.Interface;
using Movie.Service.Utility;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace Movie.Service.Implementation;

public class MovieService : IMovieService
{
    private readonly HttpClient _httpClient;
    private readonly OmdConfig _omdConfig;
    private readonly ApplicationDbContext _dbContext;

    public MovieService(HttpClient httpClient, OmdConfig omdConfig, ApplicationDbContext dbContext)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _omdConfig = omdConfig ?? throw new ArgumentNullException(nameof(omdConfig));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }



    public async Task<ServiceResponse<MovieInfoResponseDto>> SearchMovie(MovieSearchRequestDto model)
    {
        try
        {
            _httpClient.BaseAddress = new Uri(_omdConfig.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("apikey", $"{_omdConfig.ApiKey}");
            string apiUrl = $"{_omdConfig.BaseUrl}/?t={model.MovieTitle}&apikey={_omdConfig.ApiKey}";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<MovieInfoResponseDto>(content);


                await _dbContext.SearchQueries.AddAsync(new SearchQuery
                {
                    Title = model.MovieTitle,
                    CreatedAt = DateTime.Now
                });

                await _dbContext.SaveChangesAsync();

                if (result.Title == null)
                {
                    return new ServiceResponse<MovieInfoResponseDto>
                    {
                        Message = $"Invalid Movie Name/ Unable to find information on movie title {model.MovieTitle}",
                        StatusCode = HttpStatusCode.BadRequest,
                        Success = false
                    };
                }


                return new ServiceResponse<MovieInfoResponseDto>
                {
                    Data = result,
                    Message = "Success",
                    StatusCode = HttpStatusCode.OK,
                    Success = true
                };
            }

            return new ServiceResponse<MovieInfoResponseDto>
            {

                Message = "Failed",
                StatusCode = HttpStatusCode.BadRequest,
                Success = false
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<MovieInfoResponseDto>
            {

                Message = $"{ex.Message}",
                StatusCode = HttpStatusCode.BadRequest,
                Success = false
            };

        }
    }

    public async Task<ServiceResponse<IEnumerable<string>>> LastFiveSearchedMovie()
    {
        var movies = await _dbContext.SearchQueries.Where(s => s.IsActive == true).OrderByDescending(d => d.CreatedAt).Take(5).ToListAsync();
        IEnumerable<string> movieTitles = movies.Select(movie => movie.Title);
        return new ServiceResponse<IEnumerable<string>>
        {
            Data = movieTitles,
            Message = "Success",
            StatusCode = HttpStatusCode.OK,
            Success = true
        };
    }
}
