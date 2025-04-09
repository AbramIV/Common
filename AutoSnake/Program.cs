using AutoSnake.Enums;
using AutoSnake.Helpers;
using AutoSnake.Models;
using System.Timers;
using static System.Console;
using StepTimer = System.Timers.Timer;

bool step = false;
int count = 0;

StepTimer timer = new(100);
timer.Elapsed += Timer_Elapsed;

Controller controller = new();
Snake snake = new((WindowWidth / 2) - 5, WindowHeight / 2);
Cell food = new((WindowWidth / 2) + 10, WindowHeight / 2, (char)Symbols.Food);

Drawer.SetColor(ConsoleColor.Green);
Drawer.DrawCell(snake.Head);
Drawer.DrawCell(food);
DrawField();

snake.StepFinished += Snake_StepFinished;

timer.Start();

while (count < 30)
{
    if (step)
    {
        snake.StepRight();

        count++;
        step = false;
    }
}

ReadKey();

static void DrawField()
{
    ForegroundColor = ConsoleColor.Red;

    CursorVisible = false;

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

void Snake_StepFinished(int x, int y)
{
    Drawer.DrawCell(snake.Head);
    Drawer.Erase(snake.Track);

    if (snake.Head.X == food.X && snake.Head.Y == food.Y)
        Drawer.DrawCell(snake.Eat());
}