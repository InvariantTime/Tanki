namespace Tanki.Domain
{
    public interface IResult
    {
        bool IsSuccess { get; }
        
        string Error { get; }
    }

    public interface IResult<T> : IResult
    {
        T? Value { get; }
    }
}