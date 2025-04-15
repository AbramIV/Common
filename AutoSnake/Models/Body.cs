using AutoSnake.Enums;
using AutoSnake.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Body : Cell
{
    public Body(int x, int y, Views view) : base(x, y, view) { }

    internal Body(int x, int y, Views view, Cell cell) : base(x, y, view)
    {
        cell.PositionChanged += SetPosition;
    }
}