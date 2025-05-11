namespace Products.Domain.Entities
{
    public class Product
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int StockAvailable { get; set; }
    }
}