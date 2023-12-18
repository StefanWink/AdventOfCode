namespace AdventOfCode.Y23.D17;

public class ShortestPath
{
    private readonly Map map;
    private readonly PriorityQueue<Crucible, int> paths = new();
    private readonly HashSet<Crucible> visited = new();

    public ShortestPath(Map map)
    {
        this.map = map;
    }

    public int GetShortestPath()
    {
        Crucible initial = new(
            LastCityBlock: new(0, 0),
            Direction1: null,
            Direction2: null,
            Direction3: null);

        paths.Enqueue(initial, 0);

        while (paths.TryDequeue(out Crucible crucible, out int incurredHeatLoss))
        {
            if (crucible.LastCityBlock == map.Destination)
                return incurredHeatLoss;

            if (!visited.Add(crucible))
                continue; // Already processed - skip.

            CruciblePath path = new(crucible, map);

            foreach (CityBlock nextCityBlock in path.GetNextMoves())
            {
                Crucible nextCrucible = crucible.Next(nextCityBlock);
                int nextIncurredHeatLoss = incurredHeatLoss + map.GetHeatLoss(nextCityBlock);

                paths.Enqueue(nextCrucible, nextIncurredHeatLoss);
            }
        }

        throw new InvalidOperationException("No solution found.");
    }
}
