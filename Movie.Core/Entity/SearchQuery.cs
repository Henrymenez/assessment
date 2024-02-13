namespace Movie.Core.Entity;

public class SearchQuery
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdateAt { get; set; }

}
