
namespace E_Commerce_API.DTOs
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public Object? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}