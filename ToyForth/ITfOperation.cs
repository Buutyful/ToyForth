namespace ToyForth;
public interface ITfOperation
{
    void Exec(Stack<object> callStack);
}
public sealed record Sum : ITfOperation
{
    public void Exec(Stack<object> callStack)
    {
        if (callStack is null || callStack.Count < 2) throw new InvalidOperationException();

        (object a, object b) = (callStack.Pop(), callStack.Pop()); 
        object res = (b, a) switch
        {
            (string s1, string s2) => s1 + s2,
            (string s, int i) => s + i,
            (int i, string s) => i + s,
            (int i1, int i2) => i1 + i2,
            _ => throw new InvalidOperationException()
        };
        callStack.Push(res);
    }
}


public sealed record Dup() : ITfOperation
{
    public void Exec(Stack<object> callStack)
    {
        if (callStack is null || callStack.Count == 0) throw new InvalidOperationException();
        object res = callStack.Pop() switch
        {
            string s1 => s1 + s1,
            int i => i * 2,
            _ => throw new InvalidOperationException()
        };
        callStack.Push(res);
    }        
}

public sealed record Push(object Value) : ITfOperation
{
    public void Exec(Stack<object> callStack) => callStack.Push(Value);
}