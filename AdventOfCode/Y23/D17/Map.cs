namespace AdventOfCode.Y23.D17;

public class Map
{
    private readonly int[][] map;
    
    public CityBlock Destination { get; }

    public int Size { get; }

    public Map(string[] lines)
    {
        map = new int[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
            map[i] = lines[i].Select(Parse).ToArray();

        Destination = new(map.Length - 1, map.Length - 1);
        Size = map.Length;
    }

    private static int Parse(char c)
    {
        return c - '0';
    }

    public int GetHeatLoss(CityBlock cityBlock)
    {
        return map[cityBlock.ZeroBasedRow][cityBlock.ZeroBasedColumn];
    }

    public IEnumerable<CityBlock> GetAdjacentCityBlocks(CityBlock cityBlock)
    {
        if (cityBlock.ZeroBasedRow > 0)
            yield return cityBlock.Up();

        if (cityBlock.ZeroBasedColumn > 0)
            yield return cityBlock.Left();

        if (cityBlock.ZeroBasedColumn + 1 < Size)
            yield return cityBlock.Right();

        if (cityBlock.ZeroBasedRow + 1 < Size)
            yield return cityBlock.Down();
    }
}
