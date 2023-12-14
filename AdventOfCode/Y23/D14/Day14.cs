namespace AdventOfCode.Y23.D14;

public class Day14 : IPuzzle
{
    public long CalculatePartOne(string[] lines)
    {
        Board board = new(lines);
        board.TiltNorthSouth(Direction.North);

        return board.GetRocks()
            .Select(board.GetLoadOnNorth)
            .Sum();
    }

    public long CalculatePartTwo(string[] lines)
    {
        Board board = new(lines);

        List<Board> boards = new();

        int targetCycles = 1_000_000_000;

        for (int i = 0; i < targetCycles; i++)
        {
            board.Cycle();

            Board clone = (Board)board.Clone();

            for (int j = 0; j < boards.Count; j++)
            {
                if (boards[j].Equals(clone))
                {
                    int firstOccurrence = j;
                    int lastOccurrence = i;

                    // From firstOccurrence, the following states are repeated every cadence steps.
                    int cadence = lastOccurrence - firstOccurrence;

                    int cyclesRemaining = targetCycles - lastOccurrence - 1;
                    int relativeIndex = cyclesRemaining % cadence;
                    int index = firstOccurrence + relativeIndex;

                    Board finalBoard = boards[index];

                    return finalBoard.GetRocks()
                        .Select(finalBoard.GetLoadOnNorth)
                        .Sum();
                }
            }

            boards.Add(clone);
        }

        return boards
            .Last()
            .GetRocks()
            .Select(board.GetLoadOnNorth)
            .Sum();
    }
}
