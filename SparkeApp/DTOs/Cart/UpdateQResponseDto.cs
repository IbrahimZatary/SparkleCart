namespace SparkeApp.Services.Interfaces;

public class UpdateQResponseDto
{
    public int CartItemId { get; internal set; }
    public int NewQuantity { get; internal set; }
    public string? Message { get; internal set; }
}