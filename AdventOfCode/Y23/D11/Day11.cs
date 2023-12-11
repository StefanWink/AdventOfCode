namespace AdventOfCode.Y23.D11;

public class Day11 : IPuzzle
{
    public long CalculatePartOne(string[] lines)
    {
        Image image = new(lines);
        Image expandedImage = image.GetExpandedImage();
        return expandedImage.CalculateShortestPath();
    }

    public long CalculatePartTwo(string[] lines)
    {
        return -1;
    }

    public static IEnumerable<Pair> GetPairs(int number)
    {
        if (number > 1)
            for (int i = 0; i < number; i++)
                for (int j = i + 1; j < number; j++)
                    yield return new Pair(i, j);
    }
}
