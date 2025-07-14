using AutoSnake.Enums;
using AutoSnake.Models.Cells;
using System.ComponentModel;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace AutoSnake.Models;

internal class Navigator
{
    private readonly Snake snake;
    private readonly Field field;
    private readonly Cell[] directions;

    internal Navigator(Field field, Snake snake)
    {
        this.snake = snake;
        this.field = field;
        directions = [new(0, 1), new(0, -1), new(1, 0), new(-1, 0), new(1, 1), new(-1, -1), new(-1, 1), new(1, -1)];
        //directions = [new(1, 0), new(-1, 0), new(0, 1), new(0, -1)];
    }

    internal List<Cell> FindPath(Cell end)
    {
        List<Cell> path = [];
        List<Cell> visited = [];
        Queue<Cell> queue = new([snake.Body.First()]);
        Dictionary<Cell, Cell?> map = new() { { snake.Body.First(), null } };

        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();

            if (visited.Where(c => c.X == vertex.X && c.Y == vertex.Y).Any()) continue;

            if (!vertex.Equals(snake.Body.First()) && !vertex.Equals(end))
                Drawer.DrawCell(vertex, ConsoleColor.Blue);

            visited.Add(vertex);

            if (vertex.Equals(end)) break;

            foreach (var direction in directions)
            {
                Cell neighbor = new(vertex.X + direction.X, vertex.Y + direction.Y, CellView.Path);

                if (field.BorderContains(neighbor) || snake.Contains(neighbor)) continue;

                queue.Enqueue(neighbor);
                map.Add(neighbor, vertex);
            }
        }

        var key = map.Where(kvp => kvp.Key.X == end.X && kvp.Key.Y == end.Y).FirstOrDefault().Key.ChangeView(CellView.None);

        while (true)
        {
            Thread.Sleep(20);

            if (!key.Equals(snake.Body.First())) Drawer.DrawCell(key, ConsoleColor.White);

            if (key.X == snake.Body.First().X && key.Y == snake.Body.First().Y) break;

            path.Add(key);

            key = map[key];
        }

        path.Reverse();
        Drawer.DrawCells(path, ConsoleColor.Green);

        foreach (var v in visited)
        {
            if (v.Equals(snake.Body.First()) || v.Equals(end) || path.Where(c => c.Equals(v)).Any())
                continue;

            Drawer.EraseCell(v);
        }

        return path;
    }
}