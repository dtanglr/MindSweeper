namespace MindSweeper.Domain;

/// <summary>
/// Represents the status of a result.
/// </summary>
public enum ResultStatus
{
    /// <summary>
    /// The operation completed successfully.
    /// </summary>
    Ok,

    /// <summary>
    /// The request has been accepted for processing.
    /// </summary>
    Accepted,

    /// <summary>
    /// The request was successful, but there is no content to return.
    /// </summary>
    NoContent,

    /// <summary>
    /// An error occurred during the operation.
    /// </summary>
    Error,

    /// <summary>
    /// The request is forbidden.
    /// </summary>
    Forbidden,

    /// <summary>
    /// The request requires authentication.
    /// </summary>
    Unauthorized,

    /// <summary>
    /// The request is invalid.
    /// </summary>
    Invalid,

    /// <summary>
    /// The requested resource was not found.
    /// </summary>
    NotFound,

    /// <summary>
    /// The request cannot be processed due to validation errors.
    /// </summary>
    Unprocessable,

    /// <summary>
    /// The request conflicts with the current state of the resource.
    /// </summary>
    Conflict,

    /// <summary>
    /// Too many requests have been sent in a given amount of time.
    /// </summary>
    TooManyRequests
}
