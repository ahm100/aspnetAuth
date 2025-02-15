namespace Shared
{
    public class ResponseModel<T>
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }

}