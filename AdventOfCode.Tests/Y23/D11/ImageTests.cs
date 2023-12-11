using AdventOfCode.Y23.D11;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Tests.Y23.D11;

public class ImageTests
{
    private readonly string exampleInput = @"...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....";

    [Test]
    public void Number_image()
    {
        // Arrange
        Image image = new(exampleInput);

        // Act & Assert
        image.Galaxies[0].Should().Be(new Galaxy(0, 3));
        image.Galaxies.Should().HaveCount(9);
    }

    [TestCase(0, 6, 15)]
    [TestCase(2, 5, 17)]
    [TestCase(7, 8, 5)]
    public void Calculate_distances(int galaxy1, int galaxy2, int distance)
    {
        // Arrange
        Image image = new(exampleInput);

        // Act & Assert
        image.CalculateDistance(galaxy1, galaxy2, 2).Should().Be(distance);
    }

    [TestCase(2, 374)]
    [TestCase(10, 1030)]
    [TestCase(100, 8410)]
    public void Calculate_shortest_path_example_input(int expansionRate, int expected)
    {
        // Arrange
        Image image = new(exampleInput);

        // Act & Assert
        image.CalculateShortestPath(expansionRate).Should().Be(expected);
    }
}
