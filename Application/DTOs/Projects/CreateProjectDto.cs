namespace Application.DTOs.Projects;

public class CreateProjectDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? Link { get; set; }
}
