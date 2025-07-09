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
    internal List<Cell> BorderCells = [];

    internal readonly int Width;
    internal readonly int Height;

    internal Border(int width, int height)
    {
        Width = width; 
        Height = height;

        for (int i = 0; i < width - 1; i++)
        {
            BorderCells.Add(new(i, 0, CellView.Horizontal));
            BorderCells.Add(new(i, height - 1, CellView.Horizontal));
        }

        for (int i = 1; i < height - 1; i++)
        {
            BorderCells.Add(new(0, i, CellView.Vertical));
            BorderCells.Add(new(width - 1, i, CellView.Vertical));
        }
    }
}
