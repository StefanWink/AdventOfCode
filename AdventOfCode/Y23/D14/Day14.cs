namespace AdventOfCode.Y23.D14;

public class Day14 : IPuzzle
{
    public long CalculatePartOne(string[] lines)
    {
        Board board = new(lines);
        board.TiltNorth();

        return board.GetRocks()
            .Select(board.GetLoadOnNorth)
            .Sum();
    }

    public long CalculatePartTwo(string[] lines)
    {
        return -1;
    }
}
