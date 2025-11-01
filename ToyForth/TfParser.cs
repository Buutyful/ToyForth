namespace ToyForth;

public static class TfParser
{
    public static IEnumerable<ITfOperation> ParseInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) yield break;
        foreach (var word in input.Split(" "))
        {
            if (!int.TryParse(word, out var n))
            {
                switch (word)
                {
                    case "sum": yield return new Sum(); break;
                    case "dup": yield return new Dup(); break;
                    default: yield return new Push(word); break;
                }
            }
            else yield return new Push(n);
        }

    }
}