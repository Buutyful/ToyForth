using ToyForth;

string[] inputs =
[
    "10 20 sum dup -60 sum",                     // 0
    "10 [ 10 10 sum ] dup sum",                  // 50
    "3 [ 5 [ 2 dup sum ] sum ] dup",             // 24
    "10 5 > [ \"greater\" ] [ \"smaller\" ] if", // greater
    "3 3 == [ 100 ] [ -100 ] if",                // 100
    "Hi there sum",                     // "Hi there"
    "Yo dup",                                // "YoYo"
    "A B sum C sum",                 // "ABC"
    "5 10 < [ 3 3 == [ true1 ] [ false ] if ] [ true2 ] if", //true1
    "1 2 > [ 5 5 sum ] [ 10 10 sum dup ] if",               // 40
];


foreach (var input in inputs)
{
    var program = new TfProgram(TfParser.ParseInput(input));
    program.Exec();
    Console.WriteLine(program.ProgramResult);
}

