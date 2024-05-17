namespace Tennis.Middlewares
{
    public class ErrorDetails
    {
        public int StatusCode { get; internal set; }
        public string Message { get; set; }
        public string? StackTrace { get; internal set; }
    }
}
