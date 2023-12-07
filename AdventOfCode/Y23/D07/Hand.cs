namespace AdventOfCode.Y23.D07;

public class Hand
{
    public required Card Card1 { get; init; }
    public required Card Card2 { get; init; }
    public required Card Card3 { get; init; }
    public required Card Card4 { get; init; }
    public required Card Card5 { get; init; }

    public IEnumerable<Card> Cards
    {
        get
        {
            yield return Card1;
            yield return Card2;
            yield return Card3;
            yield return Card4;
            yield return Card5;
        }
    }
}

public enum HandType
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind
}

public class HandComparer : IComparer<Hand>
{
    private readonly CardComparer cardComparer = new();

    // Order of strength: weak to strong
    public int Compare(Hand? x, Hand? y)
    {
        if (x is null && y is null)
            return 0;

        if (x is null)
            return -1;

        if (y is null)
            return 1;

        HandType handType1 = GetHandType(x);
        HandType handType2 = GetHandType(y);

        int result = handType1.CompareTo(handType2);

        if (result != 0)
            return result;

        result = cardComparer.Compare(x.Card1, y.Card1);

        if (result != 0)
            return result;

        result = cardComparer.Compare(x.Card2, y.Card2);

        if (result != 0)
            return result;

        result = cardComparer.Compare(x.Card3, y.Card3);

        if (result != 0)
            return result;

        result = cardComparer.Compare(x.Card4, y.Card4);

        if (result != 0)
            return result;

        return cardComparer.Compare(x.Card5, y.Card5);
    }

    private static HandType GetHandType(Hand hand)
    {
        List<IGrouping<Card, Card>> grouping = hand.Cards
            .GroupBy(x => x)
            .OrderByDescending(x => x.Count())
            .ToList();

        if (grouping[0].Count() == 5)
            return HandType.FiveOfAKind;

        if (grouping[0].Count() == 4)
            return HandType.FourOfAKind;

        if (grouping[0].Count() == 3)
            return grouping[1].Count() == 2
                ? HandType.FullHouse
                : HandType.ThreeOfAKind;

        if (grouping[0].Count() == 2)
            return grouping[1].Count() == 2
                ? HandType.TwoPair
                : HandType.OnePair;

        return HandType.HighCard;
    }
}
