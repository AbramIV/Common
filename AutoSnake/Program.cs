using AutoSnake.Enums;
using AutoSnake.Helpers;
using AutoSnake.Models;
using System.Timers;
using static System.Console;

bool step = false;
int count = 0;
Drawer.SetColor(ConsoleColor.Green);
System.Timers.Timer timer = new(100);
Snake snake = new((WindowWidth / 2) - 5, WindowHeight / 2);
Cell food = new((WindowWidth / 2) - 10, WindowHeight / 2, (char)Symbols.Food);
Drawer.Draw(food);

DrawField();
CursorVisible = false;

timer.Elapsed += Timer_Elapsed;
timer.Start();

while(count < 30)
{
    if (step)
    {
        snake.StepLeft();
        if (snake.Head.X.Equals(food.X) && snake.Head.Y.Equals(food.Y)) snake.Eat();
        count++;
        step = false;
    }
}

ReadKey();

static void DrawField()
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
}

void Timer_Elapsed(object? sender, ElapsedEventArgs e)
{
    step = true;
}