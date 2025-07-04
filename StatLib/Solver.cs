using Accord.Statistics.Models.Regression.Linear;

namespace Calculator;

public class Solver
{ 
    public Solver()
    {

    }

    public static SimpleLinearRegression GetLinearModel(double[] x, double[] y) => 
        new OrdinaryLeastSquares().Learn(x, y);

    public static MultipleLinearRegression GetMultipleModel(double[][] x, double[] y)
    {
        var ols = new OrdinaryLeastSquares { UseIntercept = true, IsRobust = true };
        return ols.Learn(x, y);
    }

    public static MultivariateLinearRegression GetMultivariateModel(double[][] x, double[][] y) =>
        new OrdinaryLeastSquares().Learn(x, y);

    public static double GetStandardDeviation(IEnumerable<double> values)
    {
        double standardDeviation = 0;

        if (values is not null && values.Any())
        {
            double avg = values.Average();
            double sum = values.Sum(d => Math.Pow(d - avg, 2));
            standardDeviation = Math.Sqrt(sum / (values.Count() - 1));
        }

        return standardDeviation;
    }

    /// <summary>
    /// Root mean square
    /// </summary>
    /// <param name="values"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="middle"></param>
    /// <returns></returns>
    public static double GetRMS(IEnumerable<double> values, long min = 0, long max = 0, double middle = 0)
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

    public static double GetRandomValueOffset(double value, int maxNegativeOffset, int maxPositiveOffset)
    {
        double factor;
        int random = 0;
        double variate = 0;

        while (random == 0)
            random = Random.Shared.Next(-1, 1);

        factor = value.Equals(0) ? random/10 : value;

        while (variate == 0)
            variate = Random.Shared.Next(maxNegativeOffset, maxPositiveOffset) / 100.0;

        return value + factor * variate;
    }

    public static IEnumerable<double> GetLinearExtrapolation(double y1, double y2, double interval, bool offset = false)
    {
        List<double> points = new() { y1 };
        double step = (y2 - y1) / interval;
        double next = y1;

        for (int i=0; i<interval-1; i++)
        {
            next += step;
            if (offset) next = GetRandomValueOffset(next, -10, 10);
            points.Add(next);
        }

        return points;
    }

    public static IEnumerable<double> GetLinearExtrapolation(double x1, double x2, double y1, double y2)
    {
        return new double[1] { 0 };
    }

    public static int GetScatteredInt(int middlePoint, int scatterLimit) =>
        middlePoint + Random.Shared.Next(scatterLimit >= 0 ? scatterLimit * -1 : scatterLimit,
                                         scatterLimit < 0 ? scatterLimit * -1 : scatterLimit);

    /// <summary>
    /// Calculate proportional value according to input value with input and output scales.
    /// </summary>
    /// <param name="input">Input value from sensor.</param>
    /// <param name="inputMin">Minimum of input value.</param>
    /// <param name="inputMax">Maximum of input value.</param>
    /// <param name="outputMin">Minimum of output value.</param>
    /// <param name="outputMax">Maximum of output value.</param>
    /// <returns>Proportional value.</returns> 
    public static double ProportionalValue(double input, double inputMin, double inputMax, double outputMin, double outputMax)
    {
        double ratio;

        if (inputMin >= inputMax) throw new ArgumentException($"Value of \"inputMin\" = {inputMin} must be greater than \"inputMax\" = {inputMax}.");
        if (outputMin >= outputMax) throw new ArgumentException($"Value of \"outputMin\" = {outputMin} must be greater than \"outputMax\" = {outputMax}.");
        if (input < inputMin || input > inputMax) throw new ArgumentOutOfRangeException($"Value of \"input\" = {input} must be greater than \"inputMin\" = {inputMin} and less than \"{inputMax}\".");

        ratio = (outputMax - outputMin) / (inputMax - inputMin);

        return ratio * (input - inputMin) + outputMin;
    }

    /// <summary>
    /// Calculate factorial of number
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static int Factorial(int number) => number < 1 ? 1 : number * Factorial(number - 1);

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
    public static IEnumerable<double> SinWave(int count = 1000, double amplitude = 311, int round = 15)
    {
        return Enumerable.Range(0, count).Select(i => SinPoint(i, amplitude, round));
    }

    /// <summary>
    /// Sin point calculate
    /// </summary>
    /// <param name="value">Input value.</param>
    /// <param name="amplitude">Max amplitude.</param>
    /// <param name="round">Count of numbers after comma.</param>
    /// <returns>One point of sin wav.</returns>
    public static double SinPoint(double value, double amplitude = 311, int round = 15)
    {
        return Math.Round(Math.Sin(6.28 * value) * amplitude, round);
    }
}