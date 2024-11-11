namespace movie_flow_api.Application.Dto;

public record MovieDto(
    int Id,
    string Title,
    int Year,
    string ImageUrl,
    double Rating,
    IEnumerable<string> Directors,
    IEnumerable<string> Categories
);
