using AutoSnake.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Border
{
    internal List<Cell> Borders = [];

    internal Border(int width, int height)
    {
        for (int i = 0; i < width - 1; i++)
        {
            Borders.Add(new(i, 0, CellView.Horizontal));
            Borders.Add(new(i, height - 1, CellView.Horizontal));
        }

        for (int i = 1; i < height - 1; i++)
        {
            Borders.Add(new(0, i, CellView.Vertical));
            Borders.Add(new(width - 1, i, CellView.Vertical));
        }
    }
}
