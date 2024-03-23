using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage ="Symbol can not be over 10 character")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MaxLength(20, ErrorMessage ="company name can not be over 20 character")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(1,1000000000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(10,ErrorMessage ="industry name can not be more tan 10 character")]
        public string Industry { get; set; } = string.Empty;
        
        [Required]
        [Range(1,1000000000)]
        public long MarketCap { get; set; }

    }
}