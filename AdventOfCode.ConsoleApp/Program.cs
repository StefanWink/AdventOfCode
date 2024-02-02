using AdventOfCode.FSharp.Y23.D02;
using System.Diagnostics;

namespace AdventOfCode.ConsoleApp;

internal class Program
{
    static void Main(string[] args)
    {
        string path = "/temp/input.txt";
        string[] lines = File.ReadAllLines(path);

        Stopwatch sw = Stopwatch.StartNew();

        long partOne = Day02.calculatePartOne(lines);
        Console.WriteLine($"[{sw.Elapsed}] Part one: {partOne}");
        sw.Restart();

        //long partTwo = puzzle.CalculatePartTwo(lines);
        //Console.WriteLine($"[{sw.Elapsed}] Part two: {partTwo}");
    }
}
