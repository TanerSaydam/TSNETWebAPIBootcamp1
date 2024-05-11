namespace eCommerceHomeWork.Utilities;

public sealed record Result
{
    private Result(string message)
    {
        Message = message;
    }
    public string Message { get; set; } = string.Empty;

    public static Result Failed(string message)
    {
        return new(message);
    }

    public static Result Succeed(string message)
    {
        return new(message);
    }
}
