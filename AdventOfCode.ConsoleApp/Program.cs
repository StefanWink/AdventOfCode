using AdventOfCode.Y23.D08;
using System.Diagnostics;

namespace AdventOfCode.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        string path = "/temp/input.txt";
        string[] lines = File.ReadAllLines(path);

        IPuzzle puzzle = new Day08();

        Stopwatch sw = Stopwatch.StartNew();

        long partOne = puzzle.CalculatePartOne(lines);
        Console.WriteLine($"[{sw.Elapsed}] Part one: {partOne}");
        sw.Restart();

        long partTwo = puzzle.CalculatePartTwo(lines);
        Console.WriteLine($"[{sw.Elapsed}] Part two: {partTwo}");
    }
}
