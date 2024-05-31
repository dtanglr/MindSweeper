namespace MindSweeper.Domain;

/// <summary>
/// Represents the result of an operation with a value of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public class Result<T> : IResult<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class with the specified value.
    /// </summary>
    /// <param name="value">The value of the result.</param>
    public Result(T value)
    {
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class with the specified value and status.
    /// </summary>
    /// <param name="value">The value of the result.</param>
    /// <param name="status">The status of the result.</param>
    internal Result(T value, ResultStatus status)
    {
        Value = value;
        Status = status;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class with the specified status.
    /// </summary>
    /// <param name="status">The status of the result.</param>
    internal Result(ResultStatus status)
    {
        Status = status;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    internal Result()
    {
    }

    /// <summary>
    /// Gets or sets the list of error messages.
    /// </summary>
    public List<string> Errors { get; init; } = [];

    /// <summary>
    /// Gets or sets a value indicating whether the result has an operation issue.
    /// </summary>
    public bool HasOperationIssue { get; init; }

    /// <summary>
    /// Gets a value indicating whether the result is a success.
    /// </summary>
    public bool IsSuccess => Status == ResultStatus.Ok || Status == ResultStatus.Accepted;

    /// <summary>
    /// Gets or sets the operation issue.
    /// </summary>
    public OperationIssue OperationIssue { get; init; }

    /// <summary>
    /// Gets or sets the status of the result.
    /// </summary>
    public ResultStatus Status { get; init; } = ResultStatus.Ok;

    /// <summary>
    /// Gets or sets the success message.
    /// </summary>
    public string SuccessMessage { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of validation issues.
    /// </summary>
    public List<ValidationIssue> ValidationIssues { get; init; } = [];

    /// <summary>
    /// Gets the value of the result.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// Gets the type of the value.
    /// </summary>
    public Type ValueType => typeof(T);

    /// <summary>
    /// Represents an accepted operation and accepts a value as the result of the operation.
    /// </summary>
    /// <param name="value">The value of the result.</param>
    /// <returns>A new instance of the <see cref="Result{T}"/> class with the specified value and status <see cref="ResultStatus.Accepted"/>.</returns>
    public static Result<T> Accepted(T value) => new(value, ResultStatus.Accepted);

    /// <summary>
    /// Represents a conflict with the current state of the target resource.
    /// </summary>
    /// <returns>A result indicating the conflict situation.</returns>
    public static Result<T> Conflict() => new(ResultStatus.Conflict);

    /// <summary>
    /// Represents an error that occurred during the execution of the service.
    /// Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errorMessages">The list of error messages.</param>
    /// <returns>A new instance of the <see cref="Result{T}"/> class with the status <see cref="ResultStatus.Error"/>.</returns>
    public static Result<T> Error(params string[] errorMessages) => new(ResultStatus.Error)
    {
        Errors = new(errorMessages)
    };

    /// <summary>
    /// Represents a forbidden operation.
    /// </summary>
    /// <returns>A new instance of the <see cref="Result{T}"/> class with the status <see cref="ResultStatus.Forbidden"/>.</returns>
    public static Result<T> Forbidden() => new(ResultStatus.Forbidden);

    /// <summary>
    /// Represents a forbidden operation with an operation issue.
    /// </summary>
    /// <param name="operationIssue">The operation issue.</param>
    /// <returns>A new instance of the <see cref="Result{T}"/> class with the status <see cref="ResultStatus.Forbidden"/>.</returns>
    public static Result<T> Forbidden(OperationIssue operationIssue) => new(ResultStatus.Forbidden)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };

    /// <summary>
    /// Implicitly converts a value to a <see cref="Result{T}"/> with the value as the result.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Result<T>(T value) => Success(value);

    /// <summary>
    /// Implicitly converts a <see cref="Result{T}"/> to its value.
    /// </summary>
    /// <param name="result">The result to convert.</param>
    public static implicit operator T?(Result<T> result) => result.Value;

    /// <summary>
    /// Represents validation issues that prevent the underlying service from completing.
    /// </summary>
    /// <param name="validationIssues">The list of validation issues.</param>
    /// <returns>A new instance of the <see cref="Result{T}"/> class with the status <see cref="ResultStatus.Invalid"/>.</returns>
    public static Result<T> Invalid(List<ValidationIssue> validationIssues) => new(ResultStatus.Invalid)
    {
        ValidationIssues = validationIssues
    };

    /// <summary>
    /// Represents the situation where a service was unable to find a requested resource.
    /// </summary>
    /// <returns>A new instance of the <see cref="Result{T}"/> class with the status <see cref="ResultStatus.NotFound"/>.</returns>
    public static Result<T> NotFound() => new(ResultStatus.NotFound);

    /// <summary>
    /// Represents the situation where a service was unable to find a requested resource on a dependent service.
    /// </summary>
    /// <param name="operationIssue">The operation issue.</param>
    /// <returns>A new instance of the <see cref="Result{T}"/> class with the status <see cref="ResultStatus.NotFound"/>.</returns>
    public static Result<T> NotFound(OperationIssue operationIssue) => new(ResultStatus.NotFound)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };

    /// <summary>
    /// Represents a successful operation and accepts a value as the result of the operation.
    /// </summary>
    /// <param name="value">The value of the result.</param>
    /// <returns>A new instance of the <see cref="Result{T}"/> class with the specified value.</returns>
    public static Result<T> Success(T value) => new(value);

    /// <summary>
    /// Represents a successful operation and accepts a value as the result of the operation.
    /// Sets the SuccessMessage property to the provided value.
    /// </summary>
    /// <param name="value">The value of the result.</param>
    /// <param name="successMessage">The success message.</param>
    /// <returns>A new instance of the <see cref="Result{T}"/> class with the specified value and success message.</returns>
    public static Result<T> Success(T value, string successMessage) => new(value)
    {
        SuccessMessage = successMessage
    };

    /// <summary>
    /// Represents an unauthorized operation.
    /// </summary>
    /// <returns>A new instance of the <see cref="Result{T}"/> class with the status <see cref="ResultStatus.Unauthorized"/>.</returns>
    public static Result<T> Unauthorized() => new(ResultStatus.Unauthorized);

    /// <summary>
    /// Represents an unauthorized operation with an operation issue.
    /// </summary>
    /// <param name="operationIssue">The operation issue.</param>
    /// <returns>A new instance of the <see cref="Result{T}"/> class with the status <see cref="ResultStatus.Unauthorized"/>.</returns>
    public static Result<T> Unauthorized(OperationIssue operationIssue) => new(ResultStatus.Unauthorized)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };

    /// <summary>
    /// Represents an unprocessable operation with an operation issue.
    /// </summary>
    /// <param name="operationIssue">The operation issue.</param>
    /// <returns>A new instance of the <see cref="Result{T}"/> class with the status <see cref="ResultStatus.Unprocessable"/>.</returns>
    public static Result<T> Unprocessable(OperationIssue operationIssue) => new(ResultStatus.Unprocessable)
    {
        OperationIssue = operationIssue,
        HasOperationIssue = true
    };

    /// <summary>
    /// Represents an unprocessable operation with error messages.
    /// </summary>
    /// <param name="errorMessages">The list of error messages.</param>
    /// <returns>A new instance of the <see cref="Result{T}"/> class with the status <see cref="ResultStatus.Unprocessable"/>.</returns>
    public static Result<T> Unprocessable(params string[] errorMessages) => new(ResultStatus.Unprocessable)
    {
        Errors = new(errorMessages)
    };

    /// <summary>
    /// Returns the current value.
    /// </summary>
    /// <returns>The value of the result.</returns>
    public object? GetValue() => Value;

    /// <summary>
    /// Converts the existing result into a new version of a result without a value type.
    /// </summary>
    /// <returns>A new instance of the <see cref="Result"/> class with the same properties as the current result.</returns>
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
    /// Converts the existing result into a new version with a different value type.
    /// </summary>
    /// <typeparam name="TOutput">The type of the new value.</typeparam>
    /// <returns>A new instance of the <see cref="Result{TOutput}"/> class with the same properties as the current result.</returns>
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
    /// Converts the existing result into a new version with a different value.
    /// </summary>
    /// <typeparam name="TOutput">The type of the new value.</typeparam>
    /// <param name="value">The new value.</param>
    /// <returns>A new instance of the <see cref="Result{TOutput}"/> class with the specified value and the same properties as the current result.</returns>
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
