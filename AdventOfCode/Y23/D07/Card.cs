namespace AdventOfCode.Y23.D07;

public record Card(char Rank);

public class CardComparer : IComparer<Card>
{
    private readonly char[] ranks = new[] { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };

    // Order of strength: weak to strong
    public int Compare(Card? x, Card? y)
    {
        if (x is null && y is null)
            return 0;

        if (x is null)
            return -1;

        if (y is null)
            return 1;

        int index1 = Array.IndexOf(ranks, x.Rank);
        int index2 = Array.IndexOf(ranks, y.Rank);

        return index1.CompareTo(index2);
    }
}
