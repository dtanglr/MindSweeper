namespace SchneiderElectric.MindSweeper.Domain;

public interface IResult
{
    ResultStatus Status { get; }

    List<string> Errors { get; }

    List<ValidationIssue> ValidationIssues { get; }

    OperationIssue OperationIssue { get; }

    bool HasOperationIssue { get; }

    bool IsSuccess { get; }
}
