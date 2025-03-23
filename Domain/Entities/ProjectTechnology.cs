namespace Domain.Entities;

public class ProjectTechnology
{
    public string ProjectId { get; set; } = null!;
    public Project Project { get; set; } = null!;

    public string TechnologyId { get; set; } = null!;
    public Technology Technology { get; set; } = null!;
}
