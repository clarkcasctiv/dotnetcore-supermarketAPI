namespace Supermarket.API.Resources
{
    public class ProductResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int UnitOfMeasurement { get; set; }
        public CategoryResource Category { get; set; }
    }
}