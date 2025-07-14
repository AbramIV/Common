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

    internal Engine()
    {
        Drawer.ConsoleInit();
        field = new(1, 1, Drawer.Width - 1, Drawer.Height - 1);
        snake = new(new(field.MaxX/2, field.MaxY/2, CellView.Head));
        food = FoodGenerator.Generate(field, snake);
        navigator = new(field, snake);

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
            Thread.Sleep(50);

            Drawer.EraseCell(snake.Body.First());
            snake.Move(path[next++]);
            Drawer.DrawCell(snake.Body.First(), ConsoleColor.Magenta);

            if (snake.Body.First().Equals(food))
            {
                food = FoodGenerator.Generate(field, snake);
                Drawer.DrawCell(food, ConsoleColor.Magenta);
                path = navigator.FindPath(food);
                next = 0;
            }
        }

        Drawer.WriteOnCellPosition(field.Center, "GAME OVER");
        Drawer.AwaitUserInput();
    }
}
