namespace Zip.WebAPI.Models.Responses
{
    public class Response<T> : UserResponse
    {
        public T Data { get; set; }
    }
}
