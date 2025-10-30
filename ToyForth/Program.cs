using ToyForth;

var program = new TfProgram(
[
    new Push(10),
    new Push(20),
    new Sum(),
    new Dup(),
    new Push(-60),
    new Sum()
]);

var program2 = new TfProgram(
[
    new Push("Hello"),
    new Push("World!"),
    new Sum(),
    new Dup(),
    new Push(2025),
    new Sum()
]);

var stack = new Stack<object>();
var stack2 = new Stack<object>();
program.Exec(stack);
program2.Exec(stack2);

Console.WriteLine(stack.Peek());
Console.WriteLine(stack2.Peek());