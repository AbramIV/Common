using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTasksExecutor.Disposing;

public class DisposableClass : IDisposable
{
    private bool disposed;

    public void Dispose()
    {
        disposed = true;
        GC.SuppressFinalize(this);
    }

    public bool IsDisposed { get { return disposed; } }
}
