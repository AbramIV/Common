using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Cell
{
    internal int X { get; private set; }
    internal int Y { get; private set; }

    internal char View { get; private set; }

    internal Cell(int x, int y, char symbol) 
    {
        X = x;
        Y = y;
        View = symbol;
    }

    internal void SetPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    internal void SetView(char view) => View = view;
}