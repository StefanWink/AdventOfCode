using AdventOfCode.Y23.D07;
using FluentAssertions;
using NUnit.Framework;

namespace AdventOfCode.Tests.Y23.D07;

public class Day07Tests
{
    private readonly CardComparer cardComparer = new();
    private readonly HandComparer handComparer = new();

    [TestCase('A', 'K')]
    [TestCase('K', 'Q')]
    [TestCase('Q', 'J')]
    [TestCase('J', 'T')]
    [TestCase('T', '9')]
    [TestCase('9', '8')]
    [TestCase('8', '7')]
    [TestCase('7', '6')]
    [TestCase('6', '5')]
    [TestCase('5', '4')]
    [TestCase('3', '2')]
    public void Card_rank_ordering(char rank1, char rank2)
    {
        // Arrange
        Card strongCard = Day07.ParseCard(rank1);
        Card weakCard = Day07.ParseCard(rank2);

        List<Card> cards = new() { strongCard, weakCard };

        // Act & Assert
        cardComparer.Compare(weakCard, strongCard).Should().BeNegative();
        cardComparer.Compare(strongCard, weakCard).Should().BePositive();

        cards.OrderBy(x => x, cardComparer).Should().SatisfyRespectively(
            first => first.Should().Be(weakCard),
            second => second.Should().Be(strongCard));
    }

    [TestCase("AAAAA", "AA8AA")]
    [TestCase("AA8AA", "23332")]
    [TestCase("23332", "TTT98")]
    [TestCase("TTT98", "23432")]
    [TestCase("23432", "A23A4")]
    [TestCase("A23A4", "23456")]
    public void Hands_are_primarily_ordered_on_type(string hand1, string hand2)
    {
        // Arrange
        Hand strongHand = Day07.ParseHand(hand1);
        Hand weakHand = Day07.ParseHand(hand2);

        List<Hand> hands = new() { strongHand, weakHand };

        // Act & Assert
        handComparer.Compare(weakHand, strongHand).Should().BeNegative();
        handComparer.Compare(strongHand, weakHand).Should().BePositive();

        hands.OrderBy(x => x, handComparer).Should().SatisfyRespectively(
            first => first.Should().Be(weakHand),
            second => second.Should().Be(strongHand));
    }

    [TestCase("33332", "2AAAA")]
    [TestCase("77888", "77788")]
    public void Hands_second_ordering_rule(string hand1, string hand2)
    {
        // Arrange
        Hand strongHand = Day07.ParseHand(hand1);
        Hand weakHand = Day07.ParseHand(hand2);

        List<Hand> hands = new() { strongHand, weakHand };

        // Act & Assert
        handComparer.Compare(weakHand, strongHand).Should().BeNegative();
        handComparer.Compare(strongHand, weakHand).Should().BePositive();

        hands.OrderBy(x => x, handComparer).Should().SatisfyRespectively(
            first => first.Should().Be(weakHand),
            second => second.Should().Be(strongHand));
    }

    [Test]
    public void Calculate_total_winnings_example()
    {
        // Arrange
        HandBid[] handBids = new[]
        {
            new HandBid(Day07.ParseHand("32T3K"), 765),
            new HandBid(Day07.ParseHand("T55J5"), 684),
            new HandBid(Day07.ParseHand("KK677"), 28),
            new HandBid(Day07.ParseHand("KTJJT"), 220),
            new HandBid(Day07.ParseHand("QQQJA"), 483),
        };

        // Act
        long result = Day07.CalculateTotalWinnings(handBids);

        // Assert
        result.Should().Be(6440);
    }
}
