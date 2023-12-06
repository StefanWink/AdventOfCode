using AdventOfCode.Y23.D06;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Tests.Y23.D06;

public class Day06Tests
{
    [TestCase(0, 7, 0)]
    [TestCase(1, 7, 6)]
    [TestCase(2, 7, 10)]
    [TestCase(3, 7, 12)]
    [TestCase(4, 7, 12)]
    [TestCase(5, 7, 10)]
    [TestCase(6, 7, 6)]
    [TestCase(7, 7, 0)]
    public void Calculate_distance_tests(int timeButtonPressed, int raceDuration, int expected)
    {
        Day06.CalculateDistance(timeButtonPressed, raceDuration).Should().Be(expected);
    }

    [TestCase(7, 9, 4)]
    [TestCase(15, 40, 8)]
    [TestCase(30, 200, 9)]
    public void Ways_to_win(int time, int distance, int expected)
    {
        Day06.WaysToWin(time, distance).Should().Be(expected);
    }
}
