namespace AdventOfCode.Y23.D07;

public class Day07 : IPuzzle
{
    public long CalculatePartOne(string[] lines)
    {
        return CalculateTotalWinnings(ParseHandBids(lines));
    }

    public long CalculatePartTwo(string[] lines)
    {
        return -1;
    }

    public static long CalculateTotalWinnings(IEnumerable<HandBid> handBids)
    {
        return handBids
            .OrderBy(x => x.Hand, new HandComparer())
            .Select((handBid, index) => handBid.Bid * (index + 1))
            .Sum();
    }

    private static IEnumerable<HandBid> ParseHandBids(string[] lines)
    {
        foreach (string line in lines)
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            yield return new HandBid(
                ParseHand(parts[0]),
                int.Parse(parts[1]));
        }
    }

    public static Card ParseCard(char c)
    {
        return new(c);
    }

    public static Hand ParseHand(string hand)
    {
        return new()
        {
            Card1 = ParseCard(hand[0]),
            Card2 = ParseCard(hand[1]),
            Card3 = ParseCard(hand[2]),
            Card4 = ParseCard(hand[3]),
            Card5 = ParseCard(hand[4])
        };
    }
}
