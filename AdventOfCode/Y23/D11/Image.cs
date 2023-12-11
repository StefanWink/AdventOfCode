namespace AdventOfCode.Y23.D11;

public class Image
{
    private readonly string input;
    private readonly char[][] map;

    public Galaxy[] Galaxies { get; }

    private IEnumerable<string> GetLines()
    {
        foreach (char[] row in map)
            yield return new string(row);
    }

    public Image(string input)
        : this(input.Split(Environment.NewLine))
    {
    }

    public Image(string[] lines)
    {
        input = string.Join(Environment.NewLine, lines);
        map = new char[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            map[i] = line.ToCharArray();
        }

        Galaxies = GetGalaxies().ToArray();
    }

    public Image GetExpandedImage()
    {
        List<string> expandedLines = new();

        foreach (string line in GetLines())
        {
            expandedLines.Add(line);

            if (line.All(c => c == '.'))
                expandedLines.Add(line);
        }

        int columns = map[0].Length;

        HashSet<int> columnsToExpand = Enumerable.Range(0, columns)
            .Where(IsColumnEmpty)
            .ToHashSet();

        string[] lines = expandedLines
            .Select(line => ExpandLine(line, columnsToExpand).ToArray())
            .Select(chars => new string(chars))
            .ToArray();

        return new(lines);
    }

    public int CalculateDistance(int galaxy1, int galaxy2)
    {
        return CalculateDistance(Galaxies[galaxy1], Galaxies[galaxy2]);
    }

    public static int CalculateDistance(Galaxy galaxy1, Galaxy galaxy2)
    {
        return Math.Abs(galaxy1.Row - galaxy2.Row)
            + Math.Abs(galaxy1.Column - galaxy2.Column);
    }

    public int CalculateShortestPath()
    {
        return Day11.GetPairs(Galaxies.Length)
            .Select(pair => CalculateDistance(pair.Item1, pair.Item2))
            .Sum();
    }

    private IEnumerable<Galaxy> GetGalaxies()
    {
        for (int i = 0; i < map.Length; i++)
            for (int j = 0; j < map[i].Length; j++)
                if (map[i][j] == '#')
                    yield return new Galaxy(i, j);
    }

    private bool IsColumnEmpty(int index)
    {
        foreach (char[] row in map)
            if (row[index] != '.')
                return false;

        return true;
    }

    private static IEnumerable<char> ExpandLine(string line, ISet<int> indexes)
    {
        for (int i = 0; i < line.Length; i++)
        {
            yield return line[i];

            if (indexes.Contains(i))
                yield return line[i];
        }
    }

    public override string ToString()
    {
        return input;
    }
}
