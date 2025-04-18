using System.Collections.Generic; // nhớ using thêm cái này nha bro

namespace levanhieu_2122110522.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public ICollection<Product> Products { get; set; }
    }

}
