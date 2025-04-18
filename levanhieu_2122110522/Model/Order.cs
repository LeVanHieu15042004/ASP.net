using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace levanhieu_2122110522.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore] // tránh vòng lặp khi serialize
        public User? User { get; set; }

        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public double Total { get; set; }

        public int Trash { get; set; }
        public string Status { get; set; }
    }
}
