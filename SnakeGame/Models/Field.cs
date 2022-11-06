namespace SnakeGame.Models;

internal class Field
{
    private ConsoleColor OriginalColor;

    public Field()
    {
        OriginalColor = ForegroundColor;
    }

    public Tuple<int, int, int, int> Init(ConsoleColor borderColor)
    {
        ForegroundColor = borderColor;

        for (int i = 0; i < WindowWidth - 1; i++)
        {
            SetCursorPosition(i, 0);
            Write('-');
            SetCursorPosition(i, WindowHeight - 1);
            Write('-');
        }

        for (int i = 1; i < WindowHeight - 1; i++)
        {
            SetCursorPosition(0, i);
            Write('|');
            SetCursorPosition(WindowWidth - 1, i);
            Write('|');
        }

        ForegroundColor = OriginalColor;
        SetCursorPosition(1, 1);

        return new Tuple<int, int, int, int>(0, WindowWidth - 1, 0, WindowHeight - 1);
    }
}
