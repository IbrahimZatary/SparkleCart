namespace SparkeApp.DTOs.Category;

public class UpdateCreateResponseDto
{
    public required string Name { get; set; }
    public string Message { get; set; } = string.Empty;
}
