namespace AdventOfCode.Y23.D14;

public record Rock(int ZeroBasedRow, int ZeroBasedColumn)
{
    public override string ToString()
    {
        return $"({ZeroBasedRow}, {ZeroBasedColumn})";
    }
}
