using AdventOfCode.Y23.D05;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Tests.Y23.D05;

public class Day05Tests
{
    [Test]
    public void Map_test()
    {
        // Arrange
        List<string> map = new()
        {
            "map:",
            "50 98 2",
            "52 50 48"
        };

        // Act & Assert
        Day05.Map(98, map).Should().Be(50);
        Day05.Map(99, map).Should().Be(51);
        Day05.Map(100, map).Should().Be(100);
        Day05.Map(53, map).Should().Be(55);
    }

    [Test]
    public void ParseSeedRanges_Tests()
    {
        // Arrange
        string line = "seeds: 79 14 55 13";

        // Act
        List<long> seeds = Day05.ParseSeedRanges(line).ToList();

        // Assert
        seeds.Should().HaveCount(27);
    }
}
