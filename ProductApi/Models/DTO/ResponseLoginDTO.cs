namespace ProductApi.Models.DTO
{
    public class ResponseLoginDTO
    {
        public string? token { get; set; }
        public string message { get; set; } = null!;
    }
}
