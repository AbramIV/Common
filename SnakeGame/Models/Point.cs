namespace SnakeGame.Models;

public class Point
{
    public int X = 1;
    public int Y = 1;
    public char Symbol = '*';
    private const char Empty = ' ';

    public Point()
    {

    }

    public Point(int x, int y, char symbol)
    {
        X = x;
        Y = y;
        Symbol = symbol;
    }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point Print()
    {
        SetCursorPosition(X, Y);
        Write(Symbol);

        return this;
    }

    public void Print(char symbol)
    {
        SetCursorPosition(X, Y);
        Write(symbol);
    }

    public void ChangeCoords(int x, int y)
    {
        SetCursorPosition(X, Y);
        Write(Empty);
        X = x;
        Y = y;
        Print();
    }

    public void Clear()
    {
        SetCursorPosition(X, Y);
        Write(Empty);
    }
}
