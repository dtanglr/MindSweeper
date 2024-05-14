namespace SchneiderElectric.MindSweeper.Domain;

public interface IResultValue
{
    Type ValueType { get; }

    object? GetValue();
}
