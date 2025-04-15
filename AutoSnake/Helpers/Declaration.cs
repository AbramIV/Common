using AutoSnake.Models;
using System.Collections;

namespace AutoSnake.Helpers;

internal class PositionChangedEventArgs : EventArgs
{
    internal int X { get; set; }
    internal int Y { get; set; }
}