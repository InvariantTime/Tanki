namespace Tanki.Infrastructure.Hubs
{
    public record CodeResult
    {
        public bool IsSuccess { get; init; }

        public string Error { get; init; } = string.Empty;

        public static CodeResult Success()
        {
            return new()
            {
                IsSuccess = true
            };
        }

        public static CodeResult Failure(string code)
        {
            return new()
            {
                IsSuccess = false,
                Error = code
            };
        }
    }
}
