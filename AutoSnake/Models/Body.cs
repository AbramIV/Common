using AutoSnake.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Body : Head
{
    private readonly Cell lead;

    internal Body(int x, int y, Views view, Cell cell) : base(x, y, view) 
    {
        lead = cell;
        lead.PositionChanged += SetPosition;
    }
}
