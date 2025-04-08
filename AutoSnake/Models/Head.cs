using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Head : Cell
{
    internal Head(int x, int y, char symbol) : base(x, y, symbol) { }

    internal void SetHeadPosition(int x, int y)
    {
        SetPosition(x, y);
        Notify?.Invoke(X, Y);
    }

    internal delegate void PositionChanged(int x, int y);
    internal event PositionChanged Notify;
}
