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
                    case "[":
                        {
                            int depth = 1;
                            int j = index + 1;

                            while (j < tokens.Length && depth > 0)
                            {
                                if (tokens[j] == "[") depth++;
                                else if (tokens[j] == "]") depth--;
                                j++;
                            }

                            var subTokens = tokens[(index + 1)..(j - 1)];
                            var subInput = string.Join(" ", subTokens);

                            // recursively parse nested list
                            yield return new Sequence(ParseInput(subInput));

                            index = j - 1; // jump ahead
                        }
                        break;
                    default: yield return new Push(word); break;
                }
            }
            else yield return new Push(n);
            index++;
        }
    }
}