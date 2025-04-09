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
Cell food = new((WindowWidth / 2), WindowHeight / 2, Views.Food);

Drawer.DrawField();
Drawer.SetColor(ConsoleColor.Green);
Drawer.DrawCell(food);
Drawer.DrawCell(snake.Head);

snake.StepFinished += Snake_StepFinished;

timer.Start();

while (count < 40)
{
    if (step)
    {
        if (count < 10) snake.StepRight();
        if (count > 10 && count < 20) snake.StepUp();
        if (count > 20 && count < 30) snake.StepLeft();
        if (count > 30 && count < 40) snake.StepDown();

        count++;
        step = false;
    }
}

ReadKey();

void Timer_Elapsed(object? sender, ElapsedEventArgs e)
{
    step = true;
}

void Snake_StepFinished(int x, int y)
{
    if (snake.Head.X == food.X && snake.Head.Y == food.Y)
    {
        snake.Eat();
        food = new((WindowWidth / 2) + 5, WindowHeight / 2, Views.Food);
        Drawer.DrawCell(food);
    }

    Drawer.DrawCell(snake.Head);

    if (snake.Neck is not null)
    {
        Drawer.DrawCell(snake.Neck);

        //if (snake.Tail is not null)
        //{
        //    Drawer.DrawCell(snake.Tail);
        //}
    }

    Drawer.Erase(snake.FirstTrack);
}