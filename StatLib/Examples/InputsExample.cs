using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatLib
{
    public static class InputsExample
    {
        public readonly static double[] LinearY = new double[] { 6, 7, 8, 6, 9 };
        public readonly static double[] LinearX = new double[] { 1, 2, 3, 4, 5 };

        // it is column in table
        public static readonly double[] MultipleY = new double[] { 6, 7, 8, 9, 9, 6 };
        // every array it is row 
        public static readonly double[][] MultipleX = new double[][]
        {
            new double[] { 1, 2, 3, 4, 5 },
            new double[] { 2, 5, 4, 3, 1 },
            new double[] { 3, 4, 1, 5, 2 },
            new double[] { 5, 3, 4, 2, 1 }, 
            new double[] { 5, 4, 3, 2, 1 },
            new double[] { 1, 2, 6, 4, 3 },
        };
    }
}
