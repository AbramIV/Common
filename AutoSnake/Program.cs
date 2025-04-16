using AutoSnake.Enums;
using AutoSnake.Helpers;
using AutoSnake.Models;
using System.Timers;
using static System.Console;
using StepTimer = System.Timers.Timer;

bool step = false;
bool game = true;

StepTimer timer = new(100);
timer.Elapsed += Timer_Elapsed;

Snake snake = new((WindowWidth / 2) - 5, WindowHeight / 2);
FoodSpawner spawner = new(WindowWidth, WindowHeight);
Cell food = spawner.Spawn(snake.Body);

Drawer.DrawField();
Drawer.SetColor(ConsoleColor.Green);
Drawer.Draw(food);
Drawer.Draw(snake.Head);

snake.PositionChanged += Snake_PositionChanged;
timer.Start();

new Thread(() => 
{
    while (game)
    {
        if (step)
        {
            snake.Step();
            step = false;
        }
    }
}).Start();

while (game)
{
    switch (ReadKey().Key)
    {
        case ConsoleKey.RightArrow:
            if (snake.CurrentDirection != Direction.Left)
                snake.SetDirection(Direction.Right);
            break;
        case ConsoleKey.LeftArrow:
            if (snake.CurrentDirection != Direction.Right)
                snake.SetDirection(Direction.Left);
            break;
        case ConsoleKey.UpArrow:
            if (snake.CurrentDirection != Direction.Down)
                snake.SetDirection(Direction.Up);
            break;
        case ConsoleKey.DownArrow:
            if (snake.CurrentDirection != Direction.Up)
                snake.SetDirection(Direction.Down);
            break;
        default:
            game = false;
            break;
    }
}

timer.Stop();
SetCursorPosition(WindowWidth / 2 - 5, WindowHeight / 2);
Write("GAME EXIT");

ReadKey();

void Timer_Elapsed(object? sender, ElapsedEventArgs e)
{
    step = true;
}

void Snake_PositionChanged(object? sender, PositionChangedEventArgs e)
{
    if (snake.Head.X == food.X && snake.Head.Y == food.Y)
    {
        snake.Eat();

        food = spawner.Spawn(snake.Body);
        Drawer.Draw(food);
    }

    if (snake.Body.Count == 3) Drawer.DrawAll(snake.Body);
    else
    {
        Drawer.Draw(snake.Head);
        Drawer.Draw(snake.Tail);
    }

    Drawer.Erase(snake.Track);
}