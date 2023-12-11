namespace AdventOfCode.Y23.D11;

public record Pair(int Item1, int Item2)
{
    public override string ToString()
    {
        return $"({Item1}, {Item2})";
    }
}
