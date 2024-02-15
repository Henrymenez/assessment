using Moq;
using Movie.Core.AppDbContext;
using Movie.Core.Dtos.Request;
using Movie.Service.Implementation;
using Movie.Service.Interface;
using Movie.Service.Utility;
using System.Net;

namespace Movie.Tests.ServiceTests;

public class MovieServiceTest
{
    private readonly Mock<IMovieService> _movieServiceMock;
    public MovieServiceTest()
    {
        _movieServiceMock = new Mock<IMovieService>();
    }

    [Fact]
    public async Task SearchMovie_ReturnBookDto()
    {
        // Arrange
        /*  
          var omdConfig = new Mock<OmdConfig>();
          // Replace with the actual name of your DbContext
          var MovieService = new MovieService(,omdConfig.Object, );

          // Assuming the model and response are predefined for testing
         
          var responseContent = MovieResponse();
          var successResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseContent) };
          httpClientMock.Setup(client => client.GetAsync(It.IsAny<string>())).ReturnsAsync(successResponse);*/
        var httpClientMock = new Mock<HttpClient>();
        var movieApiConfigMock = new Mock<OmdConfig>();
        var dbContextMock = new Mock<ApplicationDbContext>();
        // Set up any specific behavior you need for the MovieApiConfig mock
        movieApiConfigMock.Setup(config => config.BaseUrl).Returns("http://www.omdbapi.com/");
        movieApiConfigMock.Setup(config => config.ApiKey).Returns("88a396d6");

        var movieService = new MovieService(httpClientMock.Object,movieApiConfigMock.Object, dbContextMock.Object);
        var responseContent = MovieResponse();
        var successResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseContent) };
        httpClientMock.Setup(client => client.GetAsync(It.IsAny<string>())).ReturnsAsync(successResponse);
        var model = new MovieSearchRequestDto { MovieTitle = "Terminator" };
     //   _movieServiceMock.Setup(repo => repo.SearchMovie(model).ReturnsAsync(returnValue);

        // Act
        var result = await movieService.SearchMovie(model);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal("Success", result.Message);
        Assert.NotNull(result.Data);

        // Add more assertions based on your expectations
    }

    private string MovieResponse()
    {
        return "{\r\n  \"isSuccessful\": true,\r\n  \"message\": \"Success\",\r\n  \"data\": " +
            "{\r\n    \"title\": \"Terminator\",\r\n    \"year\": \"1991\",\r\n    \"rated\": \"N/A\",\r\n    \"released\": \"N/A\",\r\n  " +
            "  \"runtime\": \"39 min\",\r\n    \"genre\": \"Short, Action, Sci-Fi\",\r\n    \"director\": \"Ben Hernandez\",\r\n    \"writer\":" +
            " \"James Cameron, Ben Hernandez\",\r\n    \"actors\": \"Loris Basso, James Callahan, Debbie Medows\",\r\n    \"plot\":" +
            " \"A cyborg comes from the future, to kill a girl named Sarah Lee.\",\r\n    \"language\": \"English\",\r\n   " +
            " \"country\": \"United States\",\r\n    \"awards\": \"N/A\",\r\n    \"poster\": \"N/A\",\r\n    \"ratings\": [\r\n     " +
            " {\r\n        \"source\": \"Internet Movie Database\",\r\n        \"value\": \"6.1/10\"\r\n      }\r\n    ],\r\n   " +
            " \"metascore\": \"N/A\",\r\n    \"imdbRating\": \"6.1\",\r\n    \"imdbVotes\": \"41\",\r\n    \"imdbID\": \"tt5817168\",\r\n  " +
            "  \"type\": \"movie\",\r\n    \"dvd\": \"N/A\",\r\n    \"boxOffice\": \"N/A\",\r\n    \"production\": \"N/A\",\r\n    \"website\": \"N/A\",\r\n   " +
            " \"response\": \"True\"\r\n  }\r\n}";
    }
}
