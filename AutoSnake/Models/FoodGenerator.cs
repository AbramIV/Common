using AutoSnake.Enums;
using AutoSnake.Models.Cells;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class FoodGenerator
{
    internal static Cell Generate(Field field, Snake snake)
    {
        List<Cell> freeCells = [];

        for (int i = field.MinX; i < field.MaxX; i++) 
        {
            for (int j = field.MinY; j < field.MaxY; j++)
            {
                if (snake.Body.Where(c => c.X == i && c.Y == j).Any()) continue;

                freeCells.Add(new(i, j));
            }
        }

        return freeCells[Random.Shared.Next(0, freeCells.Count)].ChangeView(CellView.Food);
    }
}
