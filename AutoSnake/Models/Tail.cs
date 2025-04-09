using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Tail : Body
{
    internal Tail(int x, int y, char symbol, Cell cell) : base(x, y, symbol, cell)
    {
    }
}
