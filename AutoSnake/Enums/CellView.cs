using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Enums;

internal enum CellView
{
    Horizontal = '-',
    Vertical = '|',
    Head =  '@',
    Body =  '*',
    Tail =  '#',
    Food =  '$',
    Break = 'X',
    Route = '·',
    Empty = ' ',
    None = 'N'
}
