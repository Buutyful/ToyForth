namespace ToyForth;

public static class TfParser
{
    public static IEnumerable<ITfOperation> ParseInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) yield break;
        var tokens = input.Trim().Split(' ');
        var index = 0;

        while (index < tokens.Length)
        {
            var word = tokens[index];
            if (!int.TryParse(word, out var n))
            {
                switch (word)
                {
                    case "sum": yield return new Sum(); break;
                    case "dup": yield return new Dup(); break;
                    case "if": yield return new If(); break;
                    case "<" or ">" or "==": yield return new Condition(word); break;
                    case "[":
                        {
                            var (ops, newIndex) = ParseBlock(tokens, index);
                            yield return new Sequence(ops);
                            index = newIndex;
                        } break;
                    default: yield return new Push(word); break;
                }
            }
            else yield return new Push(n);
            index++;
        }
    }

    private static (IEnumerable<ITfOperation> ops, int newIndex) ParseBlock(string[] tokens, int startIndex)
    {
        int depth = 1;
        int j = startIndex + 1;

        while (j < tokens.Length && depth > 0)
        {
            if (tokens[j] == "[") depth++;
            else if (tokens[j] == "]") depth--;
            j++;
        }

        if (depth != 0)
            throw new InvalidOperationException("block not closed correctly");

        var innerTokens = tokens[(startIndex + 1)..(j - 1)];
        var innerInput = string.Join(" ", innerTokens);

        return (TfParser.ParseInput(innerInput), j - 1);
    }
}