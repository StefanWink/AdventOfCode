using AdventOfCode.FSharp.Y23.D03;
using System.Diagnostics;

namespace AdventOfCode.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        string path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "Documents",
            "AdventOfCode",
            "y23d03.txt");

        string[] lines = File.ReadAllLines(path);

        Stopwatch sw = Stopwatch.StartNew();

        long partOne = Day03.calculatePartOne(lines);
        Console.WriteLine($"[{sw.Elapsed}] Part one: {partOne}");
        sw.Restart();

        long partTwo = Day03.calculatePartTwo(lines);
        Console.WriteLine($"[{sw.Elapsed}] Part two: {partTwo}");
    }
}
