namespace AdventOfCode.Y23.D06;

public class Day06 : IPuzzle
{
    public long CalculatePartOne(string[] lines)
    {
        return TotalWaysToWin(lines);
    }

    public long CalculatePartTwo(string[] lines)
    {
        long time = ParseAsOneNumber(lines[0]);
        long distance = ParseAsOneNumber(lines[1]);

        return WaysToWin(time, distance);
    }

    private static long ParseAsOneNumber(string line)
    {
        char[] digits = line
            .Where(char.IsDigit)
            .ToArray();

        return long.Parse(digits);
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
        long[] times = ParseLine(lines[0]).ToArray();
        long[] distances = ParseLine(lines[1]).ToArray();

        for (int i = 0; i < times.Length; i++)
            yield return new(times[i], distances[i]);
    }

    private static IEnumerable<long> ParseLine(string line)
    {
        int index = line.IndexOf(':') + 1;

        return line
            .Substring(index)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(long.Parse);
    }

    public static int WaysToWin(long time, long distance)
    {
        int waysToWin = 0;

        for (long i = 0; i <= time; i++)
            if (CalculateDistance(i, time) > distance)
                waysToWin++;

        return waysToWin;
    }

    public static long CalculateDistance(long timeButtonPressed, long raceDuration)
    {
        long speed = timeButtonPressed;
        long duration = raceDuration - timeButtonPressed;

        return speed * duration;
    }
}
