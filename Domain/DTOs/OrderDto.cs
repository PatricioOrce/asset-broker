using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CreateOrderDto
    {
        public int OrderId { get; set; }
        public int AccountId { get; set; }
        public int AssetId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public char Operation { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
