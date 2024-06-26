using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TrainingTasksExecutor.Inheritance;

internal class A
{
    internal virtual void Foo()
    {
        Console.WriteLine("Hola A!");
    }
}
