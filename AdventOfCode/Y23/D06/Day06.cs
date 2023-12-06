namespace AdventOfCode.Y23.D06;

public class Day06 : IPuzzle
{
    public long CalculatePartOne(string[] lines)
    {
        return TotalWaysToWin(lines);
    }

    public long CalculatePartTwo(string[] lines)
    {
        return -1;
    }

    public int TotalWaysToWin(string[] lines)
    {
        List<Race> races = ParseRaces(lines).ToList();

        return races.Aggregate(1, Accumulator);
    }

    private int Accumulator(int totalWaysToWin, Race race)
    {
        return totalWaysToWin * WaysToWin(race.Time, race.RecordDistance);
    }

    private static IEnumerable<Race> ParseRaces(string[] lines)
    {
        int[] times = ParseLine(lines[0]).ToArray();
        int[] distances = ParseLine(lines[1]).ToArray();

        for (int i = 0; i < times.Length; i++)
            yield return new(times[i], distances[i]);
    }

    private static IEnumerable<int> ParseLine(string line)
    {
        int index = line.IndexOf(':') + 1;

        return line
            .Substring(index)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse);
    }

    public static int WaysToWin(int time, int distance)
    {
        int waysToWin = 0;

        for (int i = 0; i <= time; i++)
            if (CalculateDistance(i, time) > distance)
                waysToWin++;

        return waysToWin;
    }

    public static int CalculateDistance(int timeButtonPressed, int raceDuration)
    {
        int speed = timeButtonPressed;
        int duration = raceDuration - timeButtonPressed;

        return speed * duration;
    }
}
