namespace Application.DTOs.Projects;

public class ProjectResponseDto
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? Link { get; set; }
    public List<string> Technologies { get; set; } = new();
}
