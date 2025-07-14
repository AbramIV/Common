using AutoSnake.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models.Cells;

internal class Cell
{
    internal int X { get; private protected set; }
    internal int Y { get; private protected set; }
    internal CellView View { get; private set; }

    internal Cell(int x, int y, CellView view = CellView.None) 
    {
        X = x;
        Y = y;
        View = view;
    }

    internal Cell ChangeView(CellView view)
    {
        View = view;
        return this;
    }

    internal void SetPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Cell? cell)
    {
        return cell is not null && (cell.X == X && cell.Y == Y);
    }
}