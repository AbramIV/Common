using SnakeGame.Enums;

namespace SnakeGame.Models;

internal class Snake
{
    public Directions Direction { get; set; }
    public Point Head { get; set; }

    public List<Point> Points { get; set; }
    private readonly ConsoleColor Color;

    private readonly int LeftBorder;
    private readonly int RightBorder;
    private readonly int TopBorder;
    private readonly int BottomBorder;

    public Snake(Tuple<int, int, int, int> borders, int startX = 1, int startY = 1, char symbol = '*', ConsoleColor color = ConsoleColor.Green)
    {
        Color = color;
        Points = new List<Point>();
        ForegroundColor = Color;
        Direction = Directions.None;

        LeftBorder = borders.Item1;
        RightBorder = borders.Item2;
        TopBorder = borders.Item3;
        BottomBorder = borders.Item4;

        if (startX < 1) startX = 1;
        if (startY < 1) startY = 1;

        Points.Add(new(startX, startY, symbol));
        Head = Points[0].Print();
    }

    public bool Step(bool feed)
    {
        if (Direction == Directions.None) return false;

        switch (Direction)
        {
            case Directions.Left:

                if (Points.Last().X - 1 > LeftBorder)
                    Points.Add(new(Points.Last().X - 1, Points.Last().Y));
                else
                {
                    Points.Last().Print('x');
                    return true;
                }

                break;
            case Directions.Right:
                if (Points.Last().X + 1 < RightBorder)
                    Points.Add(new(Points.Last().X + 1, Points.Last().Y));
                else
                {
                    Points.Last().Print('x');
                    return true;
                }
                break;
            case Directions.Up:
                if (Points.Last().Y - 1 > TopBorder)
                    Points.Add(new(Points.Last().X, Points.Last().Y - 1));
                else
                {
                    Points.Last().Print('x');
                    return true;
                }
                break;
            case Directions.Down:
                if (Points.Last().Y + 1 < BottomBorder)
                    Points.Add(new(Points.Last().X, Points.Last().Y + 1));
                else
                {
                    Points.Last().Print('x');
                    return true;
                }
                break;
            default:
                return false;
        }

        if (Points.Count > 1)
        {
            for (int i = 0; i < Points.Count - 1; i++)
            {
                if (Points[i].X.Equals(Points.Last().X) &&
                    Points[i].Y.Equals(Points.Last().Y))
                {
                    Points.Last().Print('x');
                    return true;
                }
            }
        }

        Head = Points.Last();

        if (!feed)
        {
            Points.First().Clear();
            Points.Remove(Points.First());
        }
        else
        {

        }

        Points.Last().Print();

        return false;
    }
}
