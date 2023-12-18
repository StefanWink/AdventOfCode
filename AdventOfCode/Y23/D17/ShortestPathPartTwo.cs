namespace AdventOfCode.Y23.D17;

public class ShortestPathPartTwo
{
    private readonly Map map;
    private readonly PriorityQueue<UltraCrucible, int> paths = new();
    private readonly HashSet<UltraCrucible> visited = new();

    public ShortestPathPartTwo(Map map)
    {
        this.map = map;
    }

    public int GetShortestPath()
    {
        UltraCrucible initial = new(
            LastCityBlock: new(0, 0),
            Direction: null,
            StraightForNBlocks: 0);

        paths.Enqueue(initial, 0);

        while (paths.TryDequeue(out UltraCrucible crucible, out int incurredHeatLoss))
        {
            if (crucible.LastCityBlock == map.Destination)
                return incurredHeatLoss;

            if (!visited.Add(crucible))
                continue; // Already processed - skip.

            UltraCruciblePath path = new(crucible, map);

            foreach (CityBlock nextCityBlock in path.GetNextMoves())
            {
                UltraCrucible nextCrucible = crucible.Next(nextCityBlock);
                int nextIncurredHeatLoss = incurredHeatLoss + map.GetHeatLoss(nextCityBlock);

                paths.Enqueue(nextCrucible, nextIncurredHeatLoss);
            }
        }

        throw new InvalidOperationException("No solution found.");
    }
}
