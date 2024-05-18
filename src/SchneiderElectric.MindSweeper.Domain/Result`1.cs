namespace SchneiderElectric.MindSweeper.Domain;

public class Result<T> : IResult<T>
{
    public Result(T value)
    {
        Value = value;
    }

    internal Result(T value, ResultStatus status)
    {
        Value = value;
        Status = status;
    }

    internal Result(ResultStatus status)
    {
        Status = status;
    }

    internal Result()
    {
    }

    public static implicit operator T?(Result<T> result) => result.Value;

    public static implicit operator Result<T>(T value) => Success(value);

    public T? Value { get; }

    public Type ValueType => typeof(T);

    public ResultStatus Status { get; init; } = ResultStatus.Ok;

    public bool IsSuccess => Status == ResultStatus.Ok || Status == ResultStatus.Accepted;

    public string SuccessMessage { get; init; } = string.Empty;

    public List<string> Errors { get; init; } = [];

    public List<ValidationIssue> ValidationIssues { get; init; } = [];

    public OperationIssue OperationIssue { get; init; }

    public bool HasOperationIssue { get; init; }

    /// <summary>
    /// Returns the current value.
    /// </summary>
    /// <returns></returns>
    public object? GetValue() => Value;

    /// <summary>
    /// Converts existing result into a new version of a result without a value type
    /// </summary>
    /// <returns></returns>
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
    /// <returns></returns>
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
    /// Represents a successful operation and accepts a values as the result of the operation
    /// </summary>
    /// <param name="value">Sets the Value property</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static Result<T> Success(T value) => new(value);

    /// <summary>
    /// Represents a successful operation and accepts a values as the result of the operation
    /// Sets the SuccessMessage property to the provided value
    /// </summary>
    /// <param name="value">Sets the Value property</param>
    /// <param name="successMessage">Sets the SuccessMessage property</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static Result<T> Success(T value, string successMessage) => new(value)
    {
        SuccessMessage = successMessage
    };

    /// <summary>
    /// Represents a accepted operation and accepts a values as the result of the operation
    /// </summary>
    /// <param name="value">Sets the Value property</param>
    public static Result<T> Accepted(T value) => new(value, ResultStatus.Accepted);

    /// <summary>
    /// Represents an error that occurred during the execution of the service.
    /// Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errorMessages">A list of string error messages.</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static Result<T> Error(params string[] errorMessages) => new(ResultStatus.Error)
    {
        Errors = new(errorMessages)
    };

    /// <summary>
    /// Represents validation issues that prevent the underlying service from completing.
    /// </summary>
    /// <param name="validationIssues">A list of encountered validation issues</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static Result<T> Invalid(List<ValidationIssue> validationIssues) => new(ResultStatus.Invalid)
    {
        ValidationIssues = validationIssues
    };

    /// <summary>
    /// Represents the operation issue that has prevented the dependent service from completing.
    /// </summary>
    /// <param name="operationIssue">An encountered operation issue</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static Result<T> Unprocessable(OperationIssue operationIssue) => new(ResultStatus.Unprocessable)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };

    /// <summary>
    /// Represents the operation issue that has prevented the dependent service from completing.
    /// Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errorMessages">A list of string error messages.</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static Result<T> Unprocessable(params string[] errorMessages) => new(ResultStatus.Unprocessable)
    {
        Errors = new(errorMessages)
    };

    /// <summary>
    /// Represents the situation where a service was unable to find a requested resource.
    /// </summary>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static Result<T> NotFound() => new(ResultStatus.NotFound);

    /// <summary>
    /// Represents the situation where a service was unable to find a requested resource on a dependent service
    /// </summary>
    /// <param name="operationIssue">An encountered operation issue</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static Result<T> NotFound(OperationIssue operationIssue) => new(ResultStatus.NotFound)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };

    /// <summary>
    /// The parameters to the call were correct, but the user does not have permission to perform some action.
    /// See also HTTP 403 Forbidden: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
    /// </summary>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static Result<T> Forbidden() => new(ResultStatus.Forbidden);

    /// <summary>
    /// The parameters to the call were correct, but the user does not have permission to perform some action.
    /// </summary>
    /// <param name="operationIssue">An encountered operation issue</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static Result<T> Forbidden(OperationIssue operationIssue) => new(ResultStatus.Forbidden)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };

    /// <summary>
    /// This is similar to Forbidden, but should be used when the user has not authenticated or has attempted to authenticate but failed.
    /// See also HTTP 401 Unauthorized: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
    /// </summary>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static Result<T> Unauthorized() => new(ResultStatus.Unauthorized);

    /// <summary>
    /// This is similar to Forbidden, but should be used when the user has not authenticated or has attempted to authenticate but failed on a dependent service.
    /// </summary>
    /// <param name="operationIssue">An encountered operation issue</param>
    /// <returns>A Result<typeparamref name="T"/></returns>
    public static Result<T> Unauthorized(OperationIssue operationIssue) => new(ResultStatus.Unauthorized)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };
}
