namespace SchneiderElectric.MindSweeper.Domain;

public enum ResultStatus
{
    Ok,
    Accepted,
    NoContent,
    Error,
    Forbidden,
    Unauthorized,
    Invalid,
    NotFound,
    Unprocessable,
    Conflict,
    TooManyRequests
}
