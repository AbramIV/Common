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
    internal event EventHandler<PositionChangedEventArgs>? PositionChanged;

    internal int X { get; private set; }
    internal int Y { get; private set; }
    internal char View { get; set; }

    internal Cell(int x, int y, Views view) 
    {
        X = x;
        Y = y;
        View = (char)view;
    }

    internal void SetPosition(object? sender, PositionChangedEventArgs e)
    {
        int lastX = X;
        int lastY = Y;

        X = e.X;
        Y = e.Y;

        e.X = lastX;
        e.Y = lastY;

        OnPositionChanged(e);
    }

    internal void SetPosition(int x,  int y)
    {
        X = x;
        Y = y;
    }

    protected virtual void OnPositionChanged(PositionChangedEventArgs e) => PositionChanged?.Invoke(this, e);
}