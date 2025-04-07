using static System.Console;

BuildField();   

ReadKey();

void BuildField()
{
    ForegroundColor = ConsoleColor.Red;

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

    ForegroundColor = ConsoleColor.Green;

    for (int i = 1; i < WindowWidth - 1; i++)
        for (int j = 1; j < WindowHeight - 1; j++)
        {
            SetCursorPosition(i, j);
            Write("#");
        }
}