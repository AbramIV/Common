using AutoSnake.Enums;
using AutoSnake.Models.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Engine
{
    private readonly Field field;
    private Snake snake;
    private Navigator navigator;

    internal Engine()
    {
        field = new(1, 1, Drawer.Width - 1, Drawer.Height - 1);
        snake = new(new(field.MinX, field.MinY, CellView.Head));

        Drawer.DrawCells(field.Border, ConsoleColor.Red);
        Drawer.DrawCell(snake.body.First(), ConsoleColor.Green);
        Drawer.DrawCell(FoodGenerator.Generate(field, snake), ConsoleColor.Magenta);
    }

    internal void Run()
    {
        
    }
}
