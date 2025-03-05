using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models.DTO
{
    public class ProductDTO
    {
        public string ProductName { get; set; } = null!;
        public string ProductClass { get; set; } = null!;
    }
}
