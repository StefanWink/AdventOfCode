using System.Text;

namespace AdventOfCode.Y23.D14;

public class Board : ICloneable, IEquatable<Board>
{
    const char CubeShapedRock = '#';
    const char RoundShapedRock = 'O';
    const char EmptySpace = '.';

    private readonly char[][] board;

    private Board(char[][] board)
    {
        this.board = board;
    }

    public Board(string[] lines)
    {
        board = new char[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
            board[i] = lines[i].ToCharArray();
    }

    public IEnumerable<Rock> GetRocks()
    {
        for (int i = 0; i < board.Length; i++)
            for (int j = 0; j < board[i].Length; j++)
                if (IsRock(i, j))
                    yield return new(
                        ZeroBasedRow: i,
                        ZeroBasedColumn: j);
    }

    public bool IsRock(int zeroBasedRow, int zeroBasedColumn)
    {
        return board[zeroBasedRow][zeroBasedColumn] == RoundShapedRock;
    }

    public int GetLoadOnNorth(Rock rock)
    {
        int rows = board.Length;

        return rows - rock.ZeroBasedRow;
    }

    public void Cycle()
    {
        TiltNorthSouth(Direction.North);
        TiltWestEast(Direction.West);
        TiltNorthSouth(Direction.South);
        TiltWestEast(Direction.East);
    }

    public void TiltNorthSouth(Direction direction)
    {
        int rows = board.Length;
        int cols = board[0].Length;

        for (int col = 0; col < cols; col++)
        {
            char[] input = new char[rows];

            for (int row = 0; row < rows; row++)
                input[row] = board[row][col];

            if (direction == Direction.South)
                input = input.Reverse().ToArray();

            char[] repl = TiltLeft(input);

            if (direction == Direction.South)
                repl = repl.Reverse().ToArray();

            for (int row = 0; row < rows; row++)
                board[row][col] = repl[row];
        }
    }

    public void TiltWestEast(Direction direction)
    {
        int rows = board.Length;
        int cols = board[0].Length;

        for (int row = 0; row < rows; row++)
        {
            char[] input = direction == Direction.West
                ? board[row]
                : board[row].Reverse().ToArray();

            char[] repl = TiltLeft(input);

            if (direction == Direction.East)
                repl = repl.Reverse().ToArray();

            for (int col = 0; col < cols; col++)
                board[row][col] = repl[col];
        }
    }

    public static char[] TiltLeft(char[] chars)
    {
        StringBuilder result = new();
        StringBuilder tmp = new();

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == CubeShapedRock)
            {
                if (tmp.Length > 0)
                {
                    result.Append(TiltLeft(tmp.ToString()));
                    tmp.Clear();
                }

                result.Append(chars[i]);
            }
            else
            {
                tmp.Append(chars[i]);
            }
        }

        if (tmp.Length > 0)
            result.Append(TiltLeft(tmp.ToString()));

        return result.ToString().ToCharArray();
    }

    // Input will not contain any cube shaped rocks.
    private static string TiltLeft(string str)
    {
        int len = str.Length;
        int rocks = str.Count(c => c == RoundShapedRock);
        int spaces = len - rocks;

        StringBuilder sb = new();

        if (rocks > 0)
            sb.Append(RoundShapedRock, rocks);

        if (spaces > 0)
            sb.Append(EmptySpace, spaces);

        return sb.ToString();
    }

    public object Clone()
    {
        int rows = board.Length;
        int cols = board[0].Length;

        char[][] clonedBoard = new char[rows][];

        for (int i = 0; i < rows; i++)
        {
            clonedBoard[i] = new char[cols];

            for (int j = 0; j < cols; j++)
                clonedBoard[i][j] = board[i][j];
        }

        return new Board(clonedBoard);
    }

    public bool Equals(Board? other)
    {
        if (other == null)
            return false;

        for (int i = 0; i < board.Length; i++)
            for (int j = 0; j < board[i].Length; j++)
                if (board[i][j] != other.board[i][j])
                    return false;

        return true;
    }
}
