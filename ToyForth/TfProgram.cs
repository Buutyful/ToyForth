namespace ToyForth;

public class TfProgram(IEnumerable<ITfOperation> ops)
{
    private readonly Stack<object> _stack = [];

    public object ProgramResult => _stack.Count > 0 ? _stack.Peek() : "Error empty result";
    public IEnumerable<ITfOperation> Operations { get; } = ops;

    public void Exec()
    {
        foreach (ITfOperation op in Operations) op.Exec(_stack);
    }
}