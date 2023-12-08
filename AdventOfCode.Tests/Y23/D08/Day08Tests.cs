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
}
