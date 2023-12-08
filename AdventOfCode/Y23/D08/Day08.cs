namespace AdventOfCode.Y23.D08;

public class Day08 : IPuzzle
{
    public long CalculatePartOne(string[] lines)
    {
        Map map = MapParser.Parse(lines);

        return map.GetStepCount();
    }

    public long CalculatePartTwo(string[] lines)
    {
        Map map = MapParser.Parse(lines);

        return map.GetGhostStepCount();
    }
}
