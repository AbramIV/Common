﻿using AutoSnake.Enums;
using AutoSnake.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSnake.Models;

internal class Head : Cell
{
    internal Head(int x, int y, Views view) : base(x, y, view) { }
}
