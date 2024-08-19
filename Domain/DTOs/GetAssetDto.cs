using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class GetAssetDto
    {
        public decimal Price { get; set; }
        public int AssetTypeId { get; set; }
    }
}
