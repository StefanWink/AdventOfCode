using AdventOfCode.Y23.D08;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Tests.Y23.D08;

public class Day08Tests
{
    [Test]
    public void Part_one_example()
    {
        // Arrange
        Map map = new()
        {
            Instructions = "LLR",
            Nodes = new[]
            {
                new Node() { Name = "AAA", Left = "BBB", Right = "BBB" },
                new Node() { Name = "BBB", Left = "AAA", Right = "ZZZ" },
                new Node() { Name = "ZZZ", Left = "ZZZ", Right = "ZZZ" },
            }
        };

        // Act & Assert
        map.GetStepCount().Should().Be(6);
    }

    [Test]
    public void Part_two_example()
    {
        // Arrange
        Map map = new()
        {
            Instructions = "LR",
            Nodes = new[]
            {
                new Node() { Name = "11A", Left = "11B", Right = "XXX" },
                new Node() { Name = "11B", Left = "XXX", Right = "11Z" },
                new Node() { Name = "11Z", Left = "11B", Right = "XXX" },
                new Node() { Name = "22A", Left = "22B", Right = "XXX" },
                new Node() { Name = "22B", Left = "22C", Right = "22C" },
                new Node() { Name = "22C", Left = "22Z", Right = "22Z" },
                new Node() { Name = "22Z", Left = "22B", Right = "22B" },
                new Node() { Name = "XXX", Left = "XXX", Right = "XXX" },
            }
        };

        // Act & Assert
        map.GetGhostStepCount().Should().Be(6);
    }
}
