namespace movie_flow_api.Domain.Common;

/// <summary>
/// Generic response object
/// </summary>
public record ActionResponse<T> : IActionResponse<T>
{
    /// <summary>
    /// Response status code
    /// </summary>
    public int ResultCode { get; set; }

    /// <summary>
    /// Response description message
    /// </summary>
    public IEnumerable<string>? Message { get; set; }

    /// <summary>
    /// Exception thrown
    /// </summary>
    public IEnumerable<IAppException> Exception { get; set; }

    /// <summary>
    /// Result of the request
    /// </summary>
    public T? Result { get; set; }
}

public interface IActionResponse<T>
{
    /// <summary>
    /// Response status code
    /// </summary>
    public int ResultCode { get; set; }

    /// <summary>
    /// Response description message
    /// </summary>
    public IEnumerable<string>? Message { get; set; }

    /// <summary>
    /// Exception thrown
    /// </summary>
    public IEnumerable<IAppException> Exception { get; set; }

    /// <summary>
    ///  Result of the request
    /// </summary>
    public T? Result { get; set; }
}

public class AppException : Exception, IAppException
{
    public string MessageException
    {
        get { return Message; }
    }

    public AppException() : base() { }

    public AppException(string? message) : base(message) { }

    public AppException(string? message, Exception? innerException) : base(message, innerException)
    { }
}

public interface IAppException
{
    public string MessageException { get; }
}
