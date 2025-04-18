namespace levanhieu_2122110522.Model
{
    using System.Text.Json.Serialization; // nhớ using cái này

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }

        public int CategoryId { get; set; }

        [JsonIgnore] // ✅ chặn deserialize khi POST
        public Category? Category { get; set; }
    }



}
