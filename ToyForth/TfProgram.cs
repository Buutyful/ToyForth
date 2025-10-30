namespace ToyForth;

public class TfProgram(IEnumerable<ITfOperation> ops)
{
    public IEnumerable<ITfOperation> Operations { get; } = ops;

    public void Exec(Stack<Object> callStack)
    {
       foreach (ITfOperation op in Operations) op.Exec(callStack);
    }
}