using static System.Console;
using System.Linq;
using System.Text;
using TrainingTasksExecutor.Disposing;
using TrainingTasksExecutor.Inheritance;

namespace TrainingTasksExecutor;

internal class Exercises
{
    private static readonly Random Randomizer;

    static Exercises() => Randomizer = new Random((int)DateTime.Now.Ticks);

    /// <summary>
    /// The method is reverse words letters in array of char. Order of words will the same.
    /// </summary>
    internal static void ReverseWords()
    {
        char[] array = new char[] { 's', 'i', 'h', 'T', ' ', 's', 'i', ' ', 'o', 'l', 'l', 'e', 'H', ' ', 'd', 'l', 'r', 'o', 'W' };
        char[] newArray = new char[array.Length];
        char[] buffer = new char[array.Length];
        int pos = 0;

        for (var i = 0; i < array.Length; i++)
        {
            if (i + 1 == array.Length)
            {
                newArray[i] = array[i];
                break;
            }

            if (array[i] == ' ')
            {
                newArray[i] = ' ';
                continue;
            }

            if (array[i + 1] == ' ')
            {
                newArray[i] = array[i];
                newArray[i + 1] = ' ';
                i++;
                continue;
            }

            for (var j = i; j < array.Length; j++)
            {
                buffer[pos] = array[j];
                if (j + 1 == array.Length || array[j + 1] == ' ')
                {
                    while (pos >= 0)
                    {
                        newArray[i] = buffer[pos];
                        pos--;
                        i++;
                    }

                    buffer = new char[array.Length];
                    pos = 0;
                    break;
                }
                pos++;
                if (j + 1 == array.Length) break;
            }
        }

        foreach (var chr in newArray)
            Write(chr);
    }

    /// <summary>
    /// Output is sum of even numbers in array.
    /// </summary>
    internal static void EvenNumbersSum(int MaxValue)
    {
        int result = 0;
        int[] array = new int[Randomizer.Next(MaxValue)];

        for (var i = 0; i < array.Length; i++) array[i] = Randomizer.Next(MaxValue);

        foreach (var number in array) if (number % 2 == 0) result += number;

        WriteLine(result);
    }

    /// <summary>
    /// Http request example
    /// </summary>
    internal static void WebRequestContentExample()
    {
        string data = ""; 
        string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");


        Stream stream = new MemoryStream();
        string formData = "application/json"; 
        byte[] byte1 = Encoding.UTF8.GetBytes("" + Environment.NewLine +
            "Content-Disposition: form-data; name=\"json\"; "
            + Environment.NewLine + "Content-Type: "
            + formData + Environment.NewLine + Environment.NewLine);
        stream.Write(byte1, 0, byte1.Length);
        byte[] byte2 = Encoding.UTF8.GetBytes(data + Environment.NewLine
        + Environment.NewLine + "--" + boundary + "--" + Environment.NewLine
        + Environment.NewLine);
        stream.Write(byte2, 0, byte2.Length);
    }

    /// <summary>
    /// Reverse number
    /// </summary>
    /// <param name="Number">Initial number</param>
    /// <param name="Limitation">Only 10,100,1000.....</param>
    /// <returns>Reversed initial number</returns>
    internal static void ReverseNumber(int number)
    {
        string result = string.Empty;

        while (number > 0)
        {
            result += number % 10;
            number /= 10;
        }

        WriteLine(int.Parse(result));
    }

    internal static double? SmallestFullRoot(int[] unsortedArray)
    {
        int[] sortedArray = MergeSort(unsortedArray);

        foreach (int num in sortedArray)
        {
            var sqrt = Math.Sqrt(num);

            if ((sqrt - (int)sqrt) == 0) return sqrt;
        }

        return null;
    }

    internal static int[] MergeSort(int[] array)
    {
        if (array.Length == 1) return array;

        int middleIndex = array.Length / 2;
        int[] a = new int[array.Length - middleIndex];
        int[] b = new int[middleIndex];

        for (int i = 0; i < a.Length; i++)
        {
            a[i] = array[i];
            if (i < b.Length) b[i] = array[i + a.Length];
        }

        return MergeArrays(MergeSort(a), MergeSort(b));
    }

    internal static int[] MergeArrays(int[] a, int[] b)
    {
        int[] mergedArray = new int[a.Length + b.Length];

        int indexA = 0, indexB = 0;

        for (int i = 0; i < mergedArray.Length; i++)
        {
            if (indexA < a.Length && indexB < b.Length)
            {
                mergedArray[i] = a[indexA] > b[indexB] ? a[indexA++] : b[indexB++];
            }
            else
            {
                mergedArray[i] = indexB < b.Length ? b[indexB++] : a[indexA++];
            }
        }

        return mergedArray;
    }

    internal static bool IsSimpleNumber(int number)
    {
        for (int j = 2; j <= number / 2; j++)
            if (number % j == 0) return false;
        return true;
    }

    internal static void ShuffleArray(int[] array)
    {
        var position = array.Length;

        foreach (var item in array)
            Write(item + " ");

        while (position > 0)
        {
            var randomIndex = Random.Shared.Next(array.Length);

            (array[randomIndex], array[--position]) = (array[position], array[randomIndex]);
        }

        WriteLine();

        foreach (var item in array)
            Write(item + " ");
    }

    internal static IEnumerable<int> FindDuplicatesInArrays(int[] a, int[] b) // result is [2, 6, 10, 15]
    {
        List<int> result = new();

        foreach (int number in a)
        {
            if (b.Contains(number) && !result.Contains(number)) 
                result.Add(number);
        }

        foreach (int number in b)
        {
            if (a.Contains(number) && !result.Contains(number))
                result.Add(number);
        }

        return a.Intersect(b);

        return result;
    }

    #region principals
    internal static void InheritanceCalls()
    {
        //B obj1 = new A(); // impossible to cast child to parent
        //obj1.Foo();

        B obj2 = new B(); // will be called an overrided method of a child call B
        obj2.Foo();

        A obj3 = new B(); // will be called overrided method of a child class B
        obj3.Foo();
    }

    internal static void DisposingCall()
    {
        var disposableClass = new DisposableClass();

        using (disposableClass)
            WriteLine(disposableClass.IsDisposed);

        WriteLine(disposableClass.IsDisposed);
    }

    internal static void ActionCallig()
    {
        List<Action> actions = new();

        for (var count = 0; count < 10; count++)
            actions.Add(() => WriteLine(count));

        foreach (var action in actions) // within call method gets copy of local variable stored in stack, so result is 10, 10, 10,...
            action();
    }

    internal static void CastCalling()
    {
        int i = 1;
        object obj = i;
        ++i;
        WriteLine(i);
        WriteLine(obj);
        WriteLine((short)obj); // impossible to downcast int to short since data loosing occurs
    }
    #endregion
}