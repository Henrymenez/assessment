using Microsoft.EntityFrameworkCore.Metadata;
using Movie.Core.Dtos.Request;
using Movie.Core.Dtos.Response;
using Movie.Core.Utility;
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

    public MovieService(HttpClient httpClient, OmdConfig omdConfig)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _omdConfig = omdConfig ?? throw new ArgumentNullException(nameof(omdConfig));
    }

 

    public async Task<ServiceResponse<MovieInfoResponseDto>> SearchMovie(MovieSearchRequestDto model)
    {
        try
        {
            _httpClient.BaseAddress = new Uri(_omdConfig.BaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("apikey", $"{_omdConfig.ApiKey}");
            string apiUrl = $"http://www.omdbapi.com/?t={model.MovieTitle}&apikey={_omdConfig.ApiKey}";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<MovieInfoResponseDto>(content);

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

   public async Task<ServiceResponse<PaginationResponse<MovieInfoResponseDto>>> LastFiveSearchedMovie()
    {
        throw new NotImplementedException();
    }
}
