using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLib
{
    public static class Maths
    {
        /// <summary>
        /// Square of crocked figure
        /// </summary>
        /// <param name="values"></param>
        /// <param name="middle"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static double Integral(IEnumerable<double> values, double middle = 0, long begin = 0, long end = 0)
        {
            if (end < 1) end = values.Count();
            if (begin > values.Count() || begin > end) throw new ArgumentException("Incorrect start/end points");

            double result = 0, previous = 0, average, natural;
            long index = 0;

            foreach (double value in values)
            {
                if (index < begin) { index++; continue; }

                natural = Math.Abs(value - middle);
                average = (previous + natural) / 2;
                result += average;
                previous = natural;
            }

            return result / (end - begin);
        }

        /// <summary>
        /// Root mean square
        /// </summary>
        /// <param name="values"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="middle"></param>
        /// <returns></returns>
        public static double RMS(IEnumerable<double> values, long min = 0, long max = 0, double middle = 0)
        {
            List<double> pValues = new();
            List<double> nValues = new();
            double previous = 0;
            double pResult = 0;
            double nResult = 0;
            long pCount = 0;
            long nCount = 0;
            long zCount = 0;
            double average;
            double temp;

            if (max < 1) max = values.Count();

            for (long i = min; i < max; i++)
            {
                temp = values.ElementAt((Index)i) - middle;

                if (temp > 0)
                {
                    pValues.Add(temp);
                    pCount++;
                    continue;
                }

                if (temp < 0)
                {
                    nValues.Add(Math.Abs(temp));
                    nCount++;
                    continue;
                }

                zCount++;
            }

            foreach (double pValue in pValues)
            {
                average = (previous + pValue) / 2;
                pResult += Math.Pow(average, 2);
                previous = pValue;
            }

            previous = 0;

            foreach (double nValue in nValues)
            {
                average = (previous + nValue) / 2;
                nResult += Math.Pow(average, 2);
                previous = nValue;
            }

            //foreach (double pValue in pValues) pResult += Math.Pow(pValue, 2);
            //foreach (double nValue in nValues) nResult += Math.Pow(nValue, 2);

            return Math.Sqrt((pResult + nResult) / (pCount + nCount));
        }

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
}
