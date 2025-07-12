using AutoSnake.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models.Cells;

internal class BodyCell : Cell
{
    public event Action<int, int> PositionChanged;

    public BodyCell(int x, int y,CellView view) : base(x, y, view) { }
    internal BodyCell(int x, int y, CellView view, BodyCell parent) : base(x, y, view) 
    {
        parent.PositionChanged += SetPosition;
    }

    internal new void SetPosition(int x, int y)
    {
        int lastX = X, lastY = Y;

        X = x;
        Y = y;

        PositionChanged?.Invoke(lastX, lastY);
    }
}
