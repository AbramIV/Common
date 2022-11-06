using System;

namespace CommonLib
{
    public static class Randomizer
    {
        private static readonly Random Generator = new((int)DateTime.Now.Ticks);
        public static double GetRoundedDouble(int min, int max, int digits) => Math.Round(GetDouble(min, max), digits);
        public static double GetDouble(int min, int max) => Generator.Next(min, max) + Generator.NextDouble();
        public static int GetInt(int min, int max) => Generator.Next(min, max);
        public static int GetScatteredInt(int middlePoint, int scatterLimit) => middlePoint + GetInt(scatterLimit >= 0 ? scatterLimit * -1 : scatterLimit, scatterLimit < 0 ? scatterLimit * -1 : scatterLimit);
    }
}