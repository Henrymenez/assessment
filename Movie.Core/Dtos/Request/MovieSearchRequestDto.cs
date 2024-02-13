namespace Movie.Core.Dtos.Request;

public record MovieSearchRequestDto
{
    public string MovieTitle { get; set; } = null!;
}
