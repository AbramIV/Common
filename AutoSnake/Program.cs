using AutoSnake.Enums;
using AutoSnake.Models;
using System.Timers;
using static System.Console;
using StepTimer = System.Timers.Timer;

Title = "AutoSnake";
CursorVisible = false;
ForegroundColor = ConsoleColor.Magenta;
WriteLine("Set window size and press any key to start...");
ReadKey();

Border border = new(WindowWidth, WindowHeight);
Snake snake = new((WindowWidth / 2) - 5, WindowHeight / 2); // snake start position
Feeder feeder = new(WindowWidth, WindowHeight); // food generator
Cell food = feeder.Spawn(snake.Body); 
StepTimer timer = new(80); // snake step interval
timer.Elapsed += Timer_Elapsed;

DrawCells(border.Borders, ConsoleColor.Blue);
DrawCell(snake.Head);
DrawCell(food);

snake.PositionChanged += Snake_PositionChanged;
timer.Start();

while (snake.IsAlive)
{
    switch (ReadKey().Key)
    {
        case ConsoleKey.RightArrow:
            if (snake.Direction != Direction.Left)
                snake.SetDirection(Direction.Right);
            break;
        case ConsoleKey.LeftArrow:
            if (snake.Direction != Direction.Right)
                snake.SetDirection(Direction.Left);
            break;
        case ConsoleKey.UpArrow:
            if (snake.Direction != Direction.Down)
                snake.SetDirection(Direction.Up);
            break;
        case ConsoleKey.DownArrow:
            if (snake.Direction != Direction.Up)
                snake.SetDirection(Direction.Down);
            break;
        case ConsoleKey.Escape:
            snake.Suicide();
            break;
        default:
            continue;
    }
}

timer.Stop();
SetCursorPosition(WindowWidth / 2 - 5, WindowHeight / 2);
Write("GAME EXIT");

ReadKey();

void Timer_Elapsed(object? sender, ElapsedEventArgs e)
{
    snake.Step();

    if (!snake.IsAlive) DrawCell(snake.Head, 'X');
}

void Snake_PositionChanged()
{
    if (border.Borders.Where(c => c.X == snake.Head.X && c.Y == snake.Head.Y).Any()) snake.Suicide();

    if (snake.Head.X == food.X && snake.Head.Y == food.Y)
    {
        snake.Eat();

        food = feeder.Spawn(snake.Body);
        DrawCell(food);
    }

    DrawCell(snake.Head);
    DrawCell(snake.Neck);
    DrawCell(snake.Tail);
    EraseCell(snake.Track);
}

static void DrawCells(IEnumerable<Cell> cells, ConsoleColor color)
{
    ConsoleColor last = ForegroundColor;
    ForegroundColor = color;

    if (!cells.Any()) return;

    _ = cells.All(c => { DrawCell(c); return true; });

    ForegroundColor = last;
}

static void DrawCell(Cell cell, char? symbol = null)
{
    if (cell is null) return;

    SetCursorPosition(cell.X, cell.Y);
    Write(symbol ?? (char)cell.View);
}

static void EraseCell(Cell cell) => DrawCell(cell, ' ');
