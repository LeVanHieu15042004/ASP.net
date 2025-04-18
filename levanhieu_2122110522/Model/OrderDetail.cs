namespace levanhieu_2122110522.Model
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public int Trash { get; set; } // thêm mới
        public string Status { get; set; } // thêm mới
    }
}
