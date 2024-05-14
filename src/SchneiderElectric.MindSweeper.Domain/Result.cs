namespace SchneiderElectric.MindSweeper.Domain;

public class Result : IResult
{
    private Result(ResultStatus status)
    {
        Status = status;
    }

    internal Result()
    {
    }

    public ResultStatus Status { get; internal set; } = ResultStatus.Ok;

    public bool IsSuccess => Status == ResultStatus.Ok || Status == ResultStatus.NoContent || Status == ResultStatus.Accepted;

    public string SuccessMessage { get; internal set; } = string.Empty;

    public IEnumerable<string> Errors { get; internal set; } = [];

    public List<ValidationIssue> ValidationIssues { get; internal set; } = [];

    public OperationIssue OperationIssue { get; internal set; }

    public bool HasOperationIssue { get; internal set; }

    /// <summary>
    /// Converts existing result into a new version with a different value type of <typeparamref name="TOutput"/>
    /// </summary>
    /// <param name="value">Sets the Value property</param>
    /// <returns>A Result</returns>
    /// <remarks>
    /// This is handy for bubbling up a the outcome from a failed operation into another that expects a result of a different type.
    /// </remarks>
    public Result ToResult() => new()
    {
        Status = Status,
        SuccessMessage = SuccessMessage,
        Errors = Errors,
        ValidationIssues = ValidationIssues,
        OperationIssue = OperationIssue,
        HasOperationIssue = HasOperationIssue
    };

    /// <summary>
    /// Converts existing result into a new version with a different value type of <typeparamref name="TOutput"/>
    /// </summary>
    /// <param name="value">Sets the Value property</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    /// <remarks>
    /// This is handy for bubbling up a the outcome from a failed operation into another that expects a result of a different type.
    /// </remarks>
    public Result<TOutput> ToResult<TOutput>() => new()
    {
        Status = Status,
        SuccessMessage = SuccessMessage,
        Errors = Errors,
        ValidationIssues = ValidationIssues,
        OperationIssue = OperationIssue,
        HasOperationIssue = HasOperationIssue
    };

    /// <summary>
    /// Converts existing result into a new version with a different value of type <typeparamref name="TOutput"/>
    /// </summary>
    /// <param name="value">Sets the Value property</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    /// <remarks>
    /// This is handy for taking the a result and transforming into a result of another type
    /// </remarks>
    public Result<TOutput> ToResult<TOutput>(TOutput value) => new(value)
    {
        Status = Status,
        SuccessMessage = SuccessMessage,
        Errors = Errors,
        ValidationIssues = ValidationIssues,
        OperationIssue = OperationIssue,
        HasOperationIssue = HasOperationIssue
    };

    /// <summary>
    /// Represents a accepted operation and accepts a values as the result of the operation
    /// </summary>
    /// <returns>A Result</returns>
    public static Result Accepted() => new(ResultStatus.Accepted);

    /// <summary>
    /// Represents a nocontent operation and as the result of the operation
    /// </summary>
    /// <returns>A Result</returns>
    public static Result NoContent() => new(ResultStatus.NoContent);

    /// <summary>
    /// Represents an error that occurred during the execution of the service.
    /// Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errorMessages">A list of string error messages.</param>
    /// <returns>A Result</returns>
    public static Result Error(params string[] errorMessages) => new(ResultStatus.Error)
    {
        Errors = errorMessages
    };

    /// <summary>
    /// Represents validation issues that prevent the underlying service from completing.
    /// </summary>
    /// <param name="validationIssues">A list of encountered validation issues</param>
    /// <returns>A Result</returns>
    public static Result Invalid(List<ValidationIssue> validationIssues) => new(ResultStatus.Invalid)
    {
        ValidationIssues = validationIssues
    };

    /// <summary>
    /// Represents the operation issue that has prevented a dependent service from completing.
    /// </summary>
    /// <param name="operationIssue">An encountered operation issue</param>
    /// <returns>A Result</returns>
    public static Result Unprocessable(OperationIssue operationIssue) => new(ResultStatus.Unprocessable)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };

    /// <summary>
    /// Represents the operation issue that has prevented the dependent service from completing.
    /// Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errorMessages">A list of string error messages.</param>
    /// <returns>A Result</returns>
    public static Result Unprocessable(params string[] errorMessages) => new(ResultStatus.Unprocessable)
    {
        Errors = errorMessages
    };

    /// <summary>
    /// Represents the situation where a service was unable to find a requested resource.
    /// </summary>
    /// <returns>A Result</returns>
    public static Result NotFound() => new(ResultStatus.NotFound);

    /// <summary>
    /// Represents the situation where a service was unable to find a requested resource on a dependent service
    /// </summary>
    /// <param name="operationIssue">An encountered operation issue</param>
    /// <returns>A Result</returns>
    public static Result NotFound(OperationIssue operationIssue) => new(ResultStatus.NotFound)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };

    /// <summary>
    /// The parameters to the call were correct, but the user does not have permission to perform some action.
    /// See also HTTP 403 Forbidden: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
    /// </summary>
    /// <returns>A Result</returns>
    public static Result Forbidden() => new(ResultStatus.Forbidden);

    /// <summary>
    /// The parameters to the call were correct, but the user does not have permission to perform some action.
    /// </summary>
    /// <param name="operationIssue">An encountered operation issue</param>
    /// <returns>A Result</returns>
    public static Result Forbidden(OperationIssue operationIssue) => new(ResultStatus.Forbidden)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };

    /// <summary>
    /// This is similar to Forbidden, but should be used when the user has not authenticated or has attempted to authenticate but failed.
    /// See also HTTP 401 Unauthorized: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
    /// </summary>
    /// <returns>A Result</returns>
    public static Result Unauthorized() => new(ResultStatus.Unauthorized);

    /// <summary>
    /// This is similar to Forbidden, but should be used when the user has not authenticated or has attempted to authenticate but failed on a dependent service.
    /// </summary>
    /// <param name="operationIssue">An encountered operation issue</param>
    /// <returns>A Result</returns>
    public static Result Unauthorized(OperationIssue operationIssue) => new(ResultStatus.Unauthorized)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };

    /// <summary>
    /// Represents a conflict with the current state of the target resource.
    /// </summary>
    /// <returns>A Result</returns>
    public static Result Conflict() => new(ResultStatus.Conflict)
    {
    };

    /// <summary>
    /// Indicates that the user has sent too many requests in a given amount of time (rate limiting).
    /// </summary>
    /// <returns>A Result</returns>
    public static Result TooManyRequests() => new(ResultStatus.TooManyRequests)
    {
    };
}
