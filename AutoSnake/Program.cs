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

Snake snake = new((WindowWidth / 2) - 5, WindowHeight / 2);
FoodSpawner spawner = new(WindowWidth, WindowHeight);
Cell food = spawner.Spawn(snake.Body);

Drawer.DrawField();
Drawer.SetColor(ConsoleColor.Green);
Drawer.Draw(food);
Drawer.Draw(snake.Head);

snake.PositionChanged += Snake_PositionChanged;

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

void Snake_PositionChanged(object? sender, PositionChangedEventArgs e)
{
    if (snake.Head.X == food.X && snake.Head.Y == food.Y)
    {
        snake.Eat();

        food = spawner.Spawn(snake.Body);
        Drawer.Draw(food);
    }

    Drawer.Draw(snake.Head);
    if (snake.Body.Count == 3) Drawer.Draw(snake.Body.ElementAt(1));
    Drawer.Draw(snake.Tail);
    Drawer.Erase(snake.Track);
}