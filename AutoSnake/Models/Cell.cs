using AutoSnake.Enums;
using AutoSnake.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Cell
{
    internal event PositionChanged_EventHandler PositionChanged;

    internal int X { get; private set; }
    internal int Y { get; private set; }
    internal char View { get; private set; }

    internal Cell(int x, int y, Views view) 
    {
        X = x;
        Y = y;
        View = (char)view;
    }

    internal void SetPosition(int x, int y)
    {
        int x_old = X, y_old = Y;

        X = x;
        Y = y;
        
        PositionChanged?.Invoke(x_old, y_old);
    }

    internal void SetView(char view) => View = view;
}