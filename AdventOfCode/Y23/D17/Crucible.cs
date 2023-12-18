namespace AdventOfCode.Y23.D17;

public readonly record struct Crucible(
    CityBlock LastCityBlock,
    Direction? Direction1,
    Direction? Direction2,
    Direction? Direction3)
{
    public Crucible Next(CityBlock nextCityBlock)
    {
        return new(
            LastCityBlock: nextCityBlock,
            Direction1: Direction2,
            Direction2: Direction3,
            Direction3: LastCityBlock.DetermineDirectionTo(nextCityBlock));
    }
}
