namespace AdventOfCode.Y23.D17;

public class CruciblePath
{
    private readonly Crucible crucible;
    private readonly Map map;

    public CruciblePath(Crucible crucible, Map map)
    {
        this.crucible = crucible;
        this.map = map;
    }

    public IEnumerable<CityBlock> GetNextMoves()
    {
        foreach (CityBlock next in map.GetAdjacentCityBlocks(crucible.LastCityBlock))
        {
            if (IsOppositeDirection(crucible.LastCityBlock, next))
                continue;

            if (IsStraightForThreeBlocks(next))
                continue;

            yield return next;
        }
    }

    private bool IsOppositeDirection(CityBlock current, CityBlock next)
    {
        if (crucible.Direction3 is null)
            return false;

        return next.DetermineDirectionTo(current) == crucible.Direction3.Value;
    }

    private bool IsStraightForThreeBlocks(CityBlock next)
    {
        if (crucible.Direction1 is null ||
            crucible.Direction3 is null ||
            crucible.Direction3 is null)
            return false;

        return crucible.Direction1 == crucible.Direction2
            && crucible.Direction2 == crucible.Direction3
            && crucible.LastCityBlock.DetermineDirectionTo(next) == crucible.Direction3;
    }
}
