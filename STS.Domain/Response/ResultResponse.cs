namespace STS.Domain.Response
{
    public class ResultResponse <T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
