namespace AdventOfCode.Y23.D17;

public readonly record struct CityBlock(int ZeroBasedRow, int ZeroBasedColumn)
{
    public CityBlock Up()
    {
        return new(ZeroBasedRow - 1, ZeroBasedColumn);
    }

    public CityBlock Down()
    {
        return new(ZeroBasedRow + 1, ZeroBasedColumn);
    }

    public CityBlock Left()
    {
        return new(ZeroBasedRow, ZeroBasedColumn - 1);
    }

    public CityBlock Right()
    {
        return new(ZeroBasedRow, ZeroBasedColumn + 1);
    }

    public CityBlock Straight(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Up(),
            Direction.Right => Right(),
            Direction.Down => Down(),
            Direction.Left => Left(),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Unknown dirction.")
        };
    }

    public Direction DetermineDirectionTo(CityBlock to)
    {
        if (ZeroBasedRow == to.ZeroBasedRow)
            return ZeroBasedColumn < to.ZeroBasedColumn
                ? Direction.Right
                : Direction.Left;

        return ZeroBasedRow < to.ZeroBasedRow
            ? Direction.Down
            : Direction.Up;
    }
}
