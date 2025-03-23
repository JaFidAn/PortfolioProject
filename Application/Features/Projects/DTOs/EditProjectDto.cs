namespace Application.Features.Projects.DTOs;

public class EditProjectDto
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Link { get; set; }
    public List<string> TechnologyIds { get; set; } = new();
}
