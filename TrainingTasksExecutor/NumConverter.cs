using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTasksExecutor;

internal static class NumConverter
{ 
    internal static string DecimalToHex(int number)
    {
        if (number < 16) return HexComparer(number);

        int value = number / 16;
        int result = number - value * 16;

        if (value < 16) return HexComparer(value) + HexComparer(result);

        return DecimalToHex(value) + HexComparer(result);
    }

    private static string HexComparer(int number)
    {
        return number switch
        {
            10 => "a",
            11 => "b",
            12 => "c",
            13 => "d",
            14 => "e",
            15 => "f",
            _ => number.ToString(),
        };
    }
}