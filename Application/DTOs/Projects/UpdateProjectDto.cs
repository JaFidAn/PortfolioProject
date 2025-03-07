namespace Application.DTOs.Projects;

public class UpdateProjectDto
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? Link { get; set; }
}
