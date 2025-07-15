using AutoSnake.Enums;
using AutoSnake.Models.Cells;

namespace AutoSnake.Models;

internal class Engine
{
    private readonly Field field;
    private readonly Snake snake;
    private readonly Navigator navigator;
    private Cell food;
    private List<Cell> path;

    internal Engine(bool track)
    {
        Drawer.ConsoleInit();
        field = new(1, 1, Drawer.Width - 1, Drawer.Height - 1);
        snake = new(new(field.Center.X, field.Center.Y, CellView.Head));
        food = FoodGenerator.Generate(field, snake);
        navigator = new(field, snake) { Track = track };
        
        Drawer.DrawCells(field.Border, ConsoleColor.Red);
        Drawer.DrawCell(snake.Body.First(), ConsoleColor.Magenta);
        Drawer.DrawCell(food, ConsoleColor.Magenta);

        path = navigator.FindPath(food);
    }

    internal void Run()
    {
        int next = 0;

        while (snake.IsAlive)
        {
            Thread.Sleep(1);

            Drawer.EraseCell(snake.Body.Last());

            if (path[next].Equals(food))
            {
                snake.Grow(path[next]);

                Drawer.DrawCell(snake.Head, ConsoleColor.Magenta);
                Drawer.DrawCell(snake.Neck, ConsoleColor.Magenta);
                Drawer.DrawCell(snake.Tail, ConsoleColor.Magenta);

                food = FoodGenerator.Generate(field, snake);
                Drawer.DrawCell(food, ConsoleColor.Magenta);

                path = navigator.FindPath(food);

                next = 0;

                continue;
            }
            else
                snake.Move(path[next++]);

            Drawer.DrawCell(snake.Head, ConsoleColor.Magenta);
            Drawer.DrawCell(snake.Neck, ConsoleColor.Magenta);
            Drawer.DrawCell(snake.Tail, ConsoleColor.Magenta);
        }

        Drawer.WriteOnCellPosition(field.Center, "GAME OVER");
        Drawer.AwaitUserInput();
    }
}
