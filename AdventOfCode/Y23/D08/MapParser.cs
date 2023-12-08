namespace AdventOfCode.Y23.D08;

public static class MapParser
{
    public static Map Parse(string[] lines)
    {
        return new Map()
        {
            Instructions = lines[0],
            Nodes = lines.Skip(2)
                .Select(ParseNode)
                .ToList()
        };
    }

    private static Node ParseNode(string line)
    {
        return new()
        {
            Name = line[0..3],
            Left = line[7..10],
            Right = line[12..15]
        };
    }
}
