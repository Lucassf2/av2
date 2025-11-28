using System.Collections.Generic;

namespace HamburgueriaBlazor.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public double OrderTotal { get; set; }
        public List<OrderDetailDTO> Items { get; set; } = new List<OrderDetailDTO>();
    }

    public class OrderDetailDTO
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
