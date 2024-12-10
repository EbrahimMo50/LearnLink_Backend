namespace LearnLink_Backend.DTOs
{
    public class ResponseAPI
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = "Normalized";
        public object? Data { get; set; } = null;
    }
}
