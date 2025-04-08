using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Body : Head
{
    private readonly Cell lead;

    internal Body(int x, int y, char symbol, Cell cell) : base(x, y, symbol) 
    {
        lead = cell;
        lead.Notify += Lead_Notify;
    }

    private void Lead_Notify(int x, int y)
    {
        SetPosition(x, y);
    }
}
