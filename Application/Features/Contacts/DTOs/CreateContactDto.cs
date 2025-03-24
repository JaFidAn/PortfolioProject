namespace Application.Features.Contacts.DTOs;

public class CreateContactDto
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Message { get; set; } = null!;
}
