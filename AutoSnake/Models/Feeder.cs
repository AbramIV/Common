using AutoSnake.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Feeder
{
    private readonly int leftBorder;
    private readonly int rightBorder;
    private readonly int topBorder;
    private readonly int bottomBorder;
    private readonly Random randomizer;

    internal Feeder(int fieldWidth, int fieldHeight) 
    {
        leftBorder = 0;
        rightBorder = fieldWidth - 1;
        topBorder = 0;
        bottomBorder = fieldHeight - 1;
        randomizer = new Random((int)DateTime.Now.Ticks);
    }

    public Cell Spawn(IEnumerable<Cell> snake)
    {
        Cell food;

        do
        {
            food = new(randomizer.Next(leftBorder + 1, rightBorder - 1),
                       randomizer.Next(topBorder + 1, bottomBorder - 1), CellView.Food);
        }
        while (snake.Where(b => b.X.Equals(food.X) && b.Y.Equals(food.Y)).Any());

        return food;
    }
}
