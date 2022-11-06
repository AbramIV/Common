namespace SnakeGame.Models;

public class Food : Point
{
    private readonly int LeftBorder;
    private readonly int RightBorder;
    private readonly int TopBorder;
    private readonly int BottomBorder;
    private readonly Random Randomizer;

    public Food(Tuple<int, int, int, int> borders)
    {
        LeftBorder = borders.Item1;
        RightBorder = borders.Item2;
        TopBorder = borders.Item3;
        BottomBorder = borders.Item4;
        Randomizer = new Random((int)DateTime.Now.Ticks);
    }

    public void Generate(List<Point> snakePoints)
    {
        bool calculation;

        do
        {
            calculation = false;

            X = Randomizer.Next(LeftBorder + 1, RightBorder - 1);
            Y = Randomizer.Next(TopBorder + 1, BottomBorder - 1);

            foreach (Point p in snakePoints)
            {
                if (p.X.Equals(X) && p.Y.Equals(Y))
                {
                    calculation = true;
                    break;
                }
            }
        }
        while (calculation);

        Print();
    }
}
