using System.ComponentModel.DataAnnotations;

namespace ProductApi.Models
{
    public class Users
    {
        [Key]
        public string username { get; set; } = null!;
        public string password { get; set; } = null!;
        public string? token { get; set; }

    }
}
