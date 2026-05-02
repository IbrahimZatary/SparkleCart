namespace SparkeApp.DTOs.Product
{
    public class DeleteProductDto
    {
        public int Id { get; set; }


        // what will be affected
        public int CartItemsCount { get; set; }
        public int OrderItemsCount { get; set; }

        public string ConfirmationMessage { get; set; } = default!;

    }
}
