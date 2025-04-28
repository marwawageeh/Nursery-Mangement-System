namespace mvc_project.Models
{
    public class ProductBL
    {

        public List<Product> prod;
        public ProductBL()
        {
            prod = new List<Product>();

            prod.Add(new Product() { ID = 1, Name = "clothes", Description = "this is", Price = 10000, ImageURL = "2.jpg" });

            prod.Add(new Product() { ID = 2, Name = "t shirt", Description = "this is", Price = 20000, ImageURL = "2.jpg" });

            prod.Add(new Product() { ID = 3, Name = "shoes", Description = "this is", Price = 30000, ImageURL = "m.jpg" });

            prod.Add(new Product() { ID = 4, Name = "clothes", Description = "this is", Price = 40000, ImageURL = "m.jpg" });
        }

        public List<Product> GetAll()
        {
            return prod;
        }
        public Product GetById(int id)
        {
            return prod.FirstOrDefault(s => s.ID == id);
        }



    }

}
