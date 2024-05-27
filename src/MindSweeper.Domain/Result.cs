namespace MindSweeper.Domain;

/// <summary>
/// Represents the result of an operation.
/// </summary>
public class Result : IResult
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    internal Result()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class with the specified status.
    /// </summary>
    /// <param name="status">The status of the result.</param>
    private Result(ResultStatus status)
    {
        Status = status;
    }

    /// <summary>
    /// Gets or sets the list of errors.
    /// </summary>
    public List<string> Errors { get; internal set; } = [];

    /// <summary>
    /// Gets or sets a value indicating whether the result has an operation issue.
    /// </summary>
    public bool HasOperationIssue { get; internal set; }

    /// <summary>
    /// Gets a value indicating whether the result is a success.
    /// </summary>
    public bool IsSuccess => Status == ResultStatus.Ok || Status == ResultStatus.NoContent || Status == ResultStatus.Accepted;

    /// <summary>
    /// Gets or sets the operation issue.
    /// </summary>
    public OperationIssue OperationIssue { get; internal set; }

    /// <summary>
    /// Gets or sets the status of the result.
    /// </summary>
    public ResultStatus Status { get; internal set; } = ResultStatus.Ok;

    /// <summary>
    /// Gets or sets the success message.
    /// </summary>
    public string SuccessMessage { get; internal set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of validation issues.
    /// </summary>
    public List<ValidationIssue> ValidationIssues { get; internal set; } = [];

    /// <summary>
    /// Represents an accepted operation and accepts a value as the result of the operation.
    /// </summary>
    /// <returns>A result indicating the accepted operation.</returns>
    public static Result Accepted() => new(ResultStatus.Accepted);

    /// <summary>
    /// Represents a conflict with the current state of the target resource.
    /// </summary>
    /// <returns>A result indicating the conflict situation.</returns>
    public static Result Conflict() => new(ResultStatus.Conflict);

    /// <summary>
    /// Represents an error that occurred during the execution of the service.
    /// Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errorMessages">The error messages.</param>
    /// <returns>A result indicating the error.</returns>
    public static Result Error(params string[] errorMessages) => new(ResultStatus.Error)
    {
        Errors = new List<string>(errorMessages)
    };

    /// <summary>
    /// Represents the situation where the user does not have permission to perform some action.
    /// </summary>
    /// <returns>A result indicating the forbidden situation.</returns>
    public static Result Forbidden() => new(ResultStatus.Forbidden);

    /// <summary>
    /// Represents the situation where the user does not have permission to perform some action.
    /// </summary>
    /// <param name="operationIssue">The operation issue.</param>
    /// <returns>A result indicating the forbidden situation.</returns>
    public static Result Forbidden(OperationIssue operationIssue) => new(ResultStatus.Forbidden)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };

    /// <summary>
    /// Represents validation issues that prevent the underlying service from completing.
    /// </summary>
    /// <param name="validationIssues">The validation issues.</param>
    /// <returns>A result indicating the validation issues.</returns>
    public static Result Invalid(List<ValidationIssue> validationIssues) => new(ResultStatus.Invalid)
    {
        ValidationIssues = validationIssues
    };

    /// <summary>
    /// Represents a no content operation as the result of the operation.
    /// </summary>
    /// <returns>A result indicating the no content operation.</returns>
    public static Result NoContent() => new(ResultStatus.NoContent);

    /// <summary>
    /// Represents the situation where a service was unable to find a requested resource.
    /// </summary>
    /// <returns>A result indicating the not found situation.</returns>
    public static Result NotFound() => new(ResultStatus.NotFound);

    /// <summary>
    /// Represents the situation where a service was unable to find a requested resource on a dependent service.
    /// </summary>
    /// <param name="operationIssue">The operation issue.</param>
    /// <returns>A result indicating the not found situation.</returns>
    public static Result NotFound(OperationIssue operationIssue) => new(ResultStatus.NotFound)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };

    /// <summary>
    /// Indicates that the user has sent too many requests in a given amount of time (rate limiting).
    /// </summary>
    /// <returns>A result indicating the too many requests situation.</returns>
    public static Result TooManyRequests() => new(ResultStatus.TooManyRequests);

    /// <summary>
    /// Represents the situation where the user has not authenticated or has attempted to authenticate but failed.
    /// </summary>
    /// <returns>A result indicating the unauthorized situation.</returns>
    public static Result Unauthorized() => new(ResultStatus.Unauthorized);

    /// <summary>
    /// Represents the situation where the user has not authenticated or has attempted to authenticate but failed on a dependent service.
    /// </summary>
    /// <param name="operationIssue">The operation issue.</param>
    /// <returns>A result indicating the unauthorized situation.</returns>
    public static Result Unauthorized(OperationIssue operationIssue) => new(ResultStatus.Unauthorized)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };

    /// <summary>
    /// Represents the operation issue that has prevented a dependent service from completing.
    /// </summary>
    /// <param name="operationIssue">The operation issue.</param>
    /// <returns>A result indicating the operation issue.</returns>
    public static Result Unprocessable(OperationIssue operationIssue) => new(ResultStatus.Unprocessable)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };

    /// <summary>
    /// Represents the operation issue that has prevented the dependent service from completing.
    /// Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errorMessages">The error messages.</param>
    /// <returns>A result indicating the operation issue.</returns>
    public static Result Unprocessable(params string[] errorMessages) => new(ResultStatus.Unprocessable)
    {
        Errors = new List<string>(errorMessages)
    };

    /// <summary>
    /// Converts the existing result into a new version with a different value type of <typeparamref name="TOutput"/>.
    /// </summary>
    /// <typeparam name="TOutput">The type of the new result value.</typeparam>
    /// <returns>A new result with the specified value type.</returns>
    /// <remarks>
    /// This is handy for bubbling up the outcome from a failed operation into another that expects a result of a different type.
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
    /// Converts the existing result into a new version with a different value type of <typeparamref name="TOutput"/>.
    /// </summary>
    /// <typeparam name="TOutput">The type of the new result value.</typeparam>
    /// <returns>A new result with the specified value type.</returns>
    /// <remarks>
    /// This is handy for bubbling up the outcome from a failed operation into another that expects a result of a different type.
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
    /// Converts the existing result into a new version with a different value of type <typeparamref name="TOutput"/>.
    /// </summary>
    /// <typeparam name="TOutput">The type of the new result value.</typeparam>
    /// <param name="value">The new value for the result.</param>
    /// <returns>A new result with the specified value type and value.</returns>
    /// <remarks>
    /// This is handy for taking a result and transforming it into a result of another type.
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
}
