namespace AdventOfCode.Y23.D17;

public class Day17 : IPuzzle
{
    public long CalculatePartOne(string[] lines)
    {
        Map map = new(lines);
        ShortestPath shortestPath = new(map);

        return shortestPath.GetShortestPath();
    }

    public long CalculatePartTwo(string[] lines)
    {
        return -1;
    }
}
