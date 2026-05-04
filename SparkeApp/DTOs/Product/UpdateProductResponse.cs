namespace SparkeApp.DTOs.Product;

public class UpdateProductResponse
{
    public object Id { get; internal set; } = default!;
    public object Name { get; internal set; } = default!;
    public object Price { get; internal set; } = default!;
    public object Description { get; internal set; } = default!;
    public object Quantity { get; internal set; } = default!;
    public string Message { get; internal set; } = default!;
}
