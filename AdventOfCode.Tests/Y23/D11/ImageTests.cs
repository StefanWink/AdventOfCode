using AdventOfCode.Y23.D11;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Tests.Y23.D11;

public class ImageTests
{
    private readonly string expandedExampleInput = @"....#........
.........#...
#............
.............
.............
........#....
.#...........
............#
.............
.............
.........#...
#....#.......";

    [Test]
    public void Expanded_image()
    {
        // Arrange
        Image image = new(@"...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....");

        // Act & Assert
        image.GetExpandedImage().ToString().Should().Be(expandedExampleInput);
    }

    [Test]
    public void Number_image()
    {
        // Arrange
        Image image = new(expandedExampleInput);

        // Act & Assert
        image.Galaxies[0].Should().Be(new Galaxy(0, 4));
        image.Galaxies.Should().HaveCount(9);
    }

    [TestCase(0, 7, 15)]
    [TestCase(2, 5, 17)]
    [TestCase(7, 8, 5)]
    public void Calculate_distances(int galaxy1, int galaxy2, int distance)
    {
        // Arrange
        Image image = new(expandedExampleInput);

        // Act & Assert
        image.CalculateDistance(galaxy1, galaxy2).Should().Be(distance);
    }

    [Test]
    public void Calculate_shortest_path_example_input()
    {
        // Arrange
        Image image = new(expandedExampleInput);

        // Act & Assert
        image.CalculateShortestPath().Should().Be(374);
    }
}
