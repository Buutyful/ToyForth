using ToyForth;

string[] inputs =
    [
        "10 20 sum dup -60 sum",
        "10 [ 10 10 sum ] dup sum",
        "3 [ 5 [ 2 dup sum ] sum ] dup"
    ];
var parsed = new TfProgram(TfParser.ParseInput(inputs[2]));

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


program.Exec();
program2.Exec();
parsed.Exec();

Console.WriteLine(program.ProgramResult + ", " + parsed.ProgramResult);
Console.WriteLine(program2.ProgramResult);

