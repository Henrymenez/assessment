using Movie.Core.Utility;

namespace Movie.Core.Dtos.Response;

public record MovieInfoResponseDto : BaseRecord
{
    public string Title { get; set; } = null!;
    public string Year { get; set; } = null!;
    public string Rated { get; set; } = null!;
    public string Released { get; set; } = null!;
    public string Runtime { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public string Director { get; set; } = null!;
    public string Writer { get; set; } = null!;
    public string Actors { get; set; } = null!;
    public string Plot { get; set; } = null!;
    public string Language { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Awards { get; set; } = null!;
    public string Poster { get; set; } = null!;
    public List<Ratings> Ratings { get; set; } = new List<Ratings>();
    public string Metascore { get; set; } = string.Empty;
    public string imdbRating { get; set; } = string.Empty;
    public string imdbVotes { get; set; } = string.Empty;
    public string imdbID { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string DVD { get; set; } = string.Empty;
    public string BoxOffice { get; set; } = string.Empty;
    public string Production { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
}

public class Ratings
{
    public string Source { get; set; } = null!;
    public string Value { get; set; } = null!;
}
