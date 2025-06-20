namespace OptimizedDemoApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }  
        
        // Foreign key to Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}