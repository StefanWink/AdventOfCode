namespace AdventOfCode.Y23.D11;

public class Image
{
    private readonly string input;
    private readonly char[][] map;
    private readonly HashSet<int> expandedRows;
    private readonly HashSet<int> expandedColumns;

    public Galaxy[] Galaxies { get; }

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

        expandedRows = Enumerable.Range(0, map.Length)
            .Where(IsRowEmpty)
            .ToHashSet();

        expandedColumns = Enumerable.Range(0, map[0].Length)
            .Where(IsColumnEmpty)
            .ToHashSet();

        Galaxies = GetGalaxies().ToArray();
    }

    public int CalculateDistance(int galaxy1, int galaxy2, int expansionRate)
    {
        return CalculateDistance(Galaxies[galaxy1], Galaxies[galaxy2], expansionRate);
    }

    public int CalculateDistance(Galaxy galaxy1, Galaxy galaxy2, int expansionRate)
    {
        int[] rowSpan = new[] { galaxy1.Row, galaxy2.Row }.OrderBy(x => x).ToArray();
        int[] colSpan = new[] { galaxy1.Column, galaxy2.Column }.OrderBy(x => x).ToArray();

        int diffRows = rowSpan[1] - rowSpan[0];
        int diffCols = colSpan[1] - colSpan[0];

        int result = diffRows + diffCols;

        for (int i = rowSpan[0]; i < rowSpan[1]; i++)
            if (expandedRows.Contains(i))
                result += (expansionRate - 1);

        for (int i = colSpan[0]; i < colSpan[1]; i++)
            if (expandedColumns.Contains(i))
                result += (expansionRate - 1);

        return result;
    }

    public long CalculateShortestPath(int expansionRate)
    {
        return Day11.GetPairs(Galaxies.Length)
            .Select(pair => (long)CalculateDistance(pair.Item1, pair.Item2, expansionRate))
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

    private bool IsRowEmpty(int index)
    {
        return map[index].All(c => c == '.');
    }

    public override string ToString()
    {
        return input;
    }
}
