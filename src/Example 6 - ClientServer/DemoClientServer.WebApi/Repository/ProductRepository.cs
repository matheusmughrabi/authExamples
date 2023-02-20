namespace DemoClientServer.WebApi.Repository
{
    public class ProductRepository
    {
        private List<Product> GetProductsMock()
        {
            return new List<Product>()
            {
                new Product(){ Id = 1, OwnerId = 1, Name = "Notebook", Price = 1000M },
                new Product(){ Id = 2, OwnerId = 1, Name = "Headset", Price = 100M },
                new Product(){ Id = 3, OwnerId = 1, Name = "Car", Price = 300000M },
                new Product(){ Id = 4, OwnerId = 2, Name = "House", Price = 1000000M },
                new Product(){ Id = 5, OwnerId = 2, Name = "Cake", Price = 5M },
                new Product(){ Id = 6, OwnerId = 3, Name = "Bed", Price = 120M },
                new Product(){ Id = 7, OwnerId = 3, Name = "Shoes", Price = 50M }
            };
        }

        public Product GetProductById(int id)
        {
            return GetProductsMock().FirstOrDefault(c => c.Id == id);
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
