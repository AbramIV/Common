using AutoSnake.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models.Cells;

internal static class FoodGenerator
{
    internal static Cell Generate(Field field, Snake snake)
    {
        List<Cell> cells = [];
        int choice = 0;

        for(int i=field.MinX; i < field.Width; i++)
        {
            for(int j=field.MinY; j < field.Height; j++)
            {
                if (snake.body.Where(c => c.X == i && c.Y == j).Any()) continue;
                cells.Add(new(i,j));
            }
        }

        choice = Random.Shared.Next(0, cells.Count);

        cells[choice].ChangeView(CellView.Food);

        return cells[choice];
    }
}
