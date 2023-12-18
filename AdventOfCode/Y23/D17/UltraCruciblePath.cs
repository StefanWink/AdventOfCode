using System.ComponentModel;
using System.Runtime.ConstrainedExecution;

namespace AdventOfCode.Y23.D17;

public class UltraCruciblePath
{
    private readonly UltraCrucible crucible;
    private readonly Map map;

    public UltraCruciblePath(UltraCrucible crucible, Map map)
    {
        this.crucible = crucible;
        this.map = map;
    }

    public IEnumerable<CityBlock> GetNextMoves()
    {
        if (crucible.StraightForNBlocks > 0 && crucible.StraightForNBlocks < 4)
        {
            CityBlock straight = crucible.LastCityBlock.Straight(crucible.Direction!.Value);

            if (map.IsInBounds(straight))
                yield return straight;
            
            yield break;
        }

        foreach (CityBlock next in map.GetAdjacentCityBlocks(crucible.LastCityBlock))
        {
            if (IsOppositeDirection(crucible.LastCityBlock, next))
                continue;

            if (IsStraightForTenBlocks(next))
                continue;

            yield return next;
        }
    }

    private bool IsOppositeDirection(CityBlock current, CityBlock next)
    {
        if (crucible.Direction is null)
            return false;

        return next.DetermineDirectionTo(current) == crucible.Direction.Value;
    }

    private bool IsStraightForTenBlocks(CityBlock next)
    {
        return crucible.StraightForNBlocks == 10
            && crucible.Direction == crucible.LastCityBlock.DetermineDirectionTo(next);
    }
}
