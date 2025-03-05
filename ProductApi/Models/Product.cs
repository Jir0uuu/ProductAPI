using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{
    public class Product
    {
        [Key]
        public Guid ID { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductClass { get; set; } = null!;
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeUpdated { get; set; }
    }
}
