using System;

namespace CommonLib;

public static class Randomizer
{
    private static readonly Random Generator = new((int)DateTime.Now.Ticks);
    public static double GetRoundedDouble(int min, int max, int digits) => Math.Round(GetDouble(min, max), digits);
    public static double GetDouble(int min, int max) => Generator.Next(min, max) + Generator.NextDouble();
    public static int GetInt(int min, int max) => Generator.Next(min, max);
    public static int GetScatteredInt(int middlePoint, int scatterLimit) => middlePoint + GetInt(scatterLimit >= 0 ? scatterLimit * -1 : scatterLimit, scatterLimit < 0 ? scatterLimit * -1 : scatterLimit);

    /// <summary>
    /// Sin wave collection generator.
    /// </summary>
    /// <param name="count">Count of points.</param>
    /// <param name="amplitude">Max amplitude.</param>
    /// <param name="round">Count of numbers after comma.</param>
    /// <returns>Sequence of numbers to build sin wave.</returns>
    public static IEnumerable<double> GetSinWave(int count = 1000, double amplitude = 311, int round = 15)
    {
        return Enumerable.Range(0, count).Select(i => GetSinPoint(i, amplitude, round));
    }

    /// <summary>
    /// Sin point calculate
    /// </summary>
    /// <param name="value">Input value.</param>
    /// <param name="amplitude">Max amplitude.</param>
    /// <param name="round">Count of numbers after comma.</param>
    /// <returns>One point of sin wav.</returns>
    public static double GetSinPoint(double value, double amplitude = 311, int round = 15)
    {
        return Math.Round(Math.Sin(6.28 * value) * amplitude, round);
    }
}