namespace AdventOfCode.Y23.D17;

public readonly record struct UltraCrucible(
    CityBlock LastCityBlock,
    Direction? Direction,
    int StraightForNBlocks)
{
    public UltraCrucible Next(CityBlock nextCityBlock)
    {
        Direction nextDirection = LastCityBlock.DetermineDirectionTo(nextCityBlock);

        return new(
            LastCityBlock: nextCityBlock,
            Direction: nextDirection,
            StraightForNBlocks: Direction == nextDirection
                ? StraightForNBlocks + 1
                : 1);
    }
}
