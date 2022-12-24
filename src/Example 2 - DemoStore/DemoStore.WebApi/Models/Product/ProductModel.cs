namespace DemoStore.WebApi.Models.Product
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductRepositoryMock
    {
        private static List<ProductModel> _products = new List<ProductModel>()
        {
            new ProductModel { Id = Guid.NewGuid(), Name = "Shoe", Price = 70M },
            new ProductModel { Id = Guid.NewGuid(), Name = "T-Shirt", Price = 30M },
            new ProductModel { Id = Guid.NewGuid(), Name = "Shorts", Price = 25M }
        };

        public IEnumerable<ProductModel> GetProducts()
        {
            return _products;
        }

        public void DeleteProductById(Guid id)
        {
            var productToRemove = _products.First(p => p.Id == id);
            _products.Remove(productToRemove);
        }
    }
}
