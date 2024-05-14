namespace SchneiderElectric.MindSweeper.Domain;

public interface IResult<out T> : IResult, IResultValue
{
    T? Value { get; }
}
