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
public sealed record Sequence(IEnumerable<ITfOperation> Operations) : ITfOperation
{
    public void Exec(Stack<object> callStack)
    {
        foreach (ITfOperation op in Operations) op.Exec(callStack);
    }
}

public sealed record If() : ITfOperation
{
    public void Exec(Stack<object> stack)
    {
        if (stack.Count < 3)
            throw new InvalidOperationException("[condition] [then] [else]");

        var elseOpResult = stack.Pop();
        var thenOpResult = stack.Pop();
        var condition = stack.Pop();

        bool cond = condition switch
        {
            bool b => b,
            _ => throw new InvalidOperationException("A boolean condition before the sequences when if operation")
        };

        var chosen = cond ? thenOpResult : elseOpResult;
        stack.Push(chosen);
    }
}

public sealed record Condition(string Operator) : ITfOperation
{
    public void Exec(Stack<object> stack)
    {
        if (stack.Count < 2)
            throw new InvalidOperationException("Condition needs two values on the stack");

        var o2 = stack.Pop();
        var o1 = stack.Pop();

        if (o1 is not int i1 || o2 is not int i2)
            throw new NotSupportedException("Condition operators only support ints");

        bool result = Operator switch
        {
            ">" => i1 > i2,
            "<" => i1 < i2,
            "==" => i1 == i2,
            _ => throw new NotSupportedException()
        };

        stack.Push(result);
    }
}


public sealed record Push(object Value) : ITfOperation
{
    public void Exec(Stack<object> callStack) => callStack.Push(Value);
}