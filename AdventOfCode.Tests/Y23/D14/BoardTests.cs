﻿using AdventOfCode.Y23.D14;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Tests.Y23.D14;

public class BoardTests
{
    private readonly string[] input = new[]
    {
        "O....#....",
        "O.OO#....#",
        ".....##...",
        "OO.#O....O",
        ".O.....O#.",
        "O.#..O.#.#",
        "..O..#O..O",
        ".......O..",
        "#....###..",
        "#OO..#...."
    };

    [Test]
    public void GetRocksTests()
    {
        // Arrange
        Board board = new(input);

        // Act
        List<Rock> rocks = board.GetRocks().ToList();

        // Assert
        rocks.Count.Should().Be(18);
        rocks.First().Should().Be(new Rock(0, 0));
        rocks.Last().Should().Be(new Rock(9, 2));
    }

    [TestCase('O', '.', '.')]
    [TestCase('.', 'O', '.')]
    [TestCase('.', '.', 'O')]
    public void Tilt_left_one_rock_in_empty_space_slides_left(char c1, char c2, char c3)
    {
        // Arrange
        char[] input = new[] { c1, c2, c3 };

        // Act
        char[] result = Board.TiltLeft(input);

        // Assert
        result[0].Should().Be('O');
        result[1].Should().Be('.');
        result[2].Should().Be('.');
    }

    [TestCase('#', 'O', '.')]
    [TestCase('#', '.', 'O')]
    public void Tilt_left_one_rock_with_cube_shaped_rock_slides_left(char c1, char c2, char c3)
    {
        // Arrange
        char[] input = new[] { c1, c2, c3 };

        // Act
        char[] result = Board.TiltLeft(input);

        // Assert
        result[0].Should().Be('#');
        result[1].Should().Be('O');
        result[2].Should().Be('.');
    }

    [Test]
    public void Tilt_left_with_two_cube_shaped_rocks()
    {
        // Arrange
        char[] input = new[] { '.', 'O', '.', '#', '.', 'O', '.', 'O' };

        // Act
        char[] result = Board.TiltLeft(input);

        // Assert
        result[0].Should().Be('O');
        result[1].Should().Be('.');
        result[2].Should().Be('.');
        result[3].Should().Be('#');
        result[4].Should().Be('O');
        result[5].Should().Be('O');
        result[6].Should().Be('.');
        result[7].Should().Be('.');
    }

    [Test]
    public void Tilt_north_tests()
    {
        // Arrange
        Board board = new(input);

        // Act
        board.TiltNorthSouth(Direction.North);

        // Assert
        board.IsRock(0, 0).Should().BeTrue();
        board.IsRock(1, 0).Should().BeTrue();
        board.IsRock(2, 0).Should().BeTrue();
        board.IsRock(3, 0).Should().BeTrue();
        board.IsRock(4, 0).Should().BeFalse();
    }

    [Test]
    public void Tilt_south_tests()
    {
        // Arrange
        Board board = new(input);

        // Act
        board.TiltNorthSouth(Direction.South);

        // Assert
        board.IsRock(7, 0).Should().BeTrue();
        board.IsRock(7, 1).Should().BeTrue();
        board.IsRock(8, 1).Should().BeTrue();
        board.IsRock(9, 1).Should().BeTrue();
    }

    [Test]
    public void Tilt_west_tests()
    {
        // Arrange
        Board board = new(input);

        // Act
        board.TiltWestEast(Direction.West);

        // Assert
        board.IsRock(0, 0).Should().BeTrue();
        board.IsRock(0, 1).Should().BeFalse();
        
        board.IsRock(1, 0).Should().BeTrue();
        board.IsRock(1, 1).Should().BeTrue();
        board.IsRock(1, 2).Should().BeTrue();
        board.IsRock(1, 3).Should().BeFalse();

        board.IsRock(2, 0).Should().BeFalse();
    }

    [Test]
    public void Tilt_east_tests()
    {
        // Arrange
        Board board = new(input);

        // Act
        board.TiltWestEast(Direction.East);

        // Assert
        board.IsRock(0, 0).Should().BeFalse();
        board.IsRock(0, 4).Should().BeTrue();

        board.IsRock(1, 0).Should().BeFalse();
        board.IsRock(1, 1).Should().BeTrue();
        board.IsRock(1, 2).Should().BeTrue();
        board.IsRock(1, 3).Should().BeTrue();
    }

    [Test]
    public void Get_load_on_north()
    {
        // Arrange
        Board board = new(input);

        // Act & Assert
        board.GetLoadOnNorth(new Rock(0, 0)).Should().Be(10);
    }

    [Test]
    public void Cycle()
    {
        // Arrange
        Board board = new(input);

        // Act
        board.Cycle();

        // Assert
        board.IsRock(1, 8).Should().BeTrue();
        board.IsRock(2, 3).Should().BeTrue();
        board.IsRock(2, 4).Should().BeTrue();
        board.IsRock(3, 1).Should().BeTrue();
        board.IsRock(3, 2).Should().BeTrue();
    }
}
