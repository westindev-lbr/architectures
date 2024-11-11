namespace movie_flow_api.Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int Year { get; set; }
    public IEnumerable<Category>? Categories { get; set; }
    public string? ImageUrl { get; set; }
    public double Rating { get; set; }
    public IEnumerable<string> Directors { get; set; } = null!;
}
