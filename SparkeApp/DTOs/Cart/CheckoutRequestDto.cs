namespace SparkeApp.DTOs.Cart;

public class CheckoutRequestDto
{
    public int UserId { get; set; }
    public string PaymentMethod { get; set; } = "Credit Card"; // as default
    public string ShippingAddress { get; set; } = string.Empty;
}
