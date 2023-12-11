using AdventOfCode.Y23.D11;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Tests.Y23.D11;

public class Day11Tests
{
    [Test]
    public void Test2Pairs()
    {
        Day11.GetPairs(2).Should().SatisfyRespectively(first =>
        {
            first.Item1.Should().Be(0);
            first.Item2.Should().Be(1);
        });
    }

    [Test]
    public void Test3Pairs()
    {
        Day11.GetPairs(3).Should().SatisfyRespectively(
            first =>
            {
                first.Item1.Should().Be(0);
                first.Item2.Should().Be(1);
            },
            second =>
            {
                second.Item1.Should().Be(0);
                second.Item2.Should().Be(2);
            },
            third =>
            {
                third.Item1.Should().Be(1);
                third.Item2.Should().Be(2);
            });
    }

    [Test]
    public void Test9Pairs()
    {
        Day11.GetPairs(9).Should().HaveCount(36);
    }
}
