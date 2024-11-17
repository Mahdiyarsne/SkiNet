namespace API.Errors
{
	public class ApiErrorRequst (int statusCode ,string message , string? detalis)
	{
        public int StatusCode { get; set; } = statusCode;

        public string Message { get; set; } = message;

        public string? Detalis { get; set; } = detalis;
    }
}
