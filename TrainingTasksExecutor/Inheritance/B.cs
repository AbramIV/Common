using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTasksExecutor.Inheritance;

internal class B : A
{
    internal override void Foo()
    {
        Console.WriteLine("Hola B!");
    }
}
