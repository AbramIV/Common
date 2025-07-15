using AutoSnake.Enums;
using AutoSnake.Models.Cells;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AutoSnake.Models;

internal class Navigator
{
    private readonly Snake snake;
    private readonly Field field;
    private readonly Cell[] directions;
    internal bool Track { get; set; }

    internal Navigator(Field field, Snake snake)
    {
        this.snake = snake;
        this.field = field;
        //directions = [new(0, 1), new(0, -1), new(1, 0), new(-1, 0), new(1, 1), new(-1, -1), new(-1, 1), new(1, -1)];
        directions = [new(1, 0), new(-1, 0), new(0, 1), new(0, -1)];
    }

    /// <summary>
    /// Shortest way by BSF algorithm.
    /// </summary>
    /// <param name="end">Target point.</param>
    /// <returns>Cells from start point to target point.</returns>
    internal List<Cell> FindPath(Cell end)
    {
        List<Cell> path = [];
        List<Cell> visited = [];
        Queue<Cell> queue = new([snake.Head]);
        Dictionary<Cell, Cell?> map = new() { { snake.Head, null } };

        while (queue.Count > 0)
        {
            Cell vertex = queue.Dequeue();

            if (visited.Where(v => v.Equals(vertex)).Any()) continue;

            visited.Add(vertex);

            if (vertex.Equals(end)) break;

            if (!vertex.Equals(snake.Head) && Track)
                Drawer.DrawCell(vertex, ConsoleColor.Blue);

            foreach (var direction in directions)
            {
                Cell neighbor = new(vertex.X + direction.X, vertex.Y + direction.Y, CellView.Path);

                if (field.BorderContains(neighbor) || snake.Contains(neighbor)) continue;
                if (queue.Where(c => c.Equals(neighbor)).Any()) continue;

                queue.Enqueue(neighbor);
                map.Add(neighbor, vertex);
            }
        }

        if (!map.Where(kvp => kvp.Key.Equals(end)).Any()) return path;
        var key = map.Where(kvp => kvp.Key.Equals(end)).First().Key;

        while (!key.Equals(snake.Head))
        {
            if (Track && !key.Equals(end))
                Drawer.DrawCell(key, ConsoleColor.White);

            path.Add(key);
            key = map[key];
        }

        path.Reverse();

        if (Track)
        { 
            foreach (var v in visited)
            {
                if (v.Equals(snake.Head) || v.Equals(end) || path.Where(c => c.Equals(v)).Any())
                    continue;

                Drawer.EraseCell(v);
            }
        }

        return path;
    }

    internal Cell FindFreeCell()
    {
        return snake.Head;
    }
}