using AutoSnake.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

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

    internal void ChangeView(CellView view) => View = view;

    internal void SetPosition(int x, int y)
    {
        X = x;
        Y = y;
    }
}