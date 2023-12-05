namespace AdventOfCode.Y23.D05;

public class Day05 : IPuzzle
{
    public long CalculatePartOne(string[] lines)
    {
        List<long> seeds = ParseSeeds(lines[0]);
        List<List<string>> maps = ParseMaps(lines.Skip(2));

        List<long> locations = seeds
            .Select(seed => GetLocationNumber(seed, maps))
            .ToList();

        return locations.Min();
    }

    public long CalculatePartTwo(string[] lines)
    {
        IEnumerable<long> seeds = ParseSeedRanges(lines[0]);
        List<List<string>> maps = ParseMaps(lines.Skip(2));

        return seeds
            .Select(seed => GetLocationNumber(seed, maps))
            .Min();
    }

    private static List<long> ParseSeeds(string line)
    {
        return line.Substring(7)
            .Split(' ')
            .Select(long.Parse)
            .ToList();
    }

    public static IEnumerable<long> ParseSeedRanges(string line)
    {
        List<long> parts = ParseSeeds(line);

        for (int i = 0; i < parts.Count; i += 2)
        {
            long rangeStart = parts[i];
            long length = parts[i + 1];

            for (int j = 0; j < length; j++)
                yield return rangeStart + j;
        }
    }

    private static List<List<string>> ParseMaps(IEnumerable<string> lines)
    {
        List<List<string>> maps = new();
        List<string> map = new();

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                // next part
                maps.Add(map);
                map = new List<string>();
            }
            else
            {
                map.Add(line);
            }
        }

        maps.Add(map);

        return maps;
    }

    private static long GetLocationNumber(long seed, List<List<string>> maps)
    {
        long number = seed;

        foreach (List<string> map in maps)
            number = Map(number, map);

        return number;
    }

    public static long Map(long number, List<string> map)
    {
        foreach (string line in map.Skip(1))
        {
            string[] parts = line.Split(' ');

            long destinationRangeStart = long.Parse(parts[0]);
            long sourceRangeStart = long.Parse(parts[1]);
            long range = long.Parse(parts[2]);

            long index = number - sourceRangeStart;

            if (index >= 0 && index < range)
                return destinationRangeStart + index;
        }

        return number;
    }
}
