
namespace Tanki.Domain
{
    public readonly struct Result : IResult
    {
        private static readonly Result _success = new(true, string.Empty);

        public bool IsSuccess { get; }

        public string Error { get; }

        private Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        {
            return _success;
        }

        public static Result Failure(string error)
        {
            return new(false, error);
        }

        public static Result<T> Success<T>(T value)
        {
            return new(true, value, string.Empty);
        }

        public static Result<T> Failure<T>(string error)
        {
            return new(false, default, error);
        }
    }

    public readonly struct Result<T> : IResult<T>
    {
        public T? Value { get; }

        public bool IsSuccess { get; }

        public string Error { get; }

        public Result(bool isSuccess, T? value, string error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public Result()
        {
            Error = string.Empty;
        }
    }
}