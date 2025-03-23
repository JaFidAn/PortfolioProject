namespace Application.Features.Projects.DTOs;

public class CreateProjectDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Link { get; set; }
    public required List<string> TechnologyIds { get; set; } = new();
}
