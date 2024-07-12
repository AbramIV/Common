using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTasksExecutor.OOP;

internal abstract class Encapsulation : IDisposable
{
    internal Encapsulation() { }

    internal Encapsulation(string name, string description) 
    {
        Name = name;
        Description = description;
    }

    private string _name;

    private string _description;

    private bool IsDisposed;

    internal string Name
    { 
        get { return _name; }
        set { _name = value ?? "No name"; }
    }

    internal string Description 
    { 
        get { return _description; } 
        set { _description = value.Trim(); } 
    }

    public void Nothing()
    {
        
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!IsDisposed)
        {
            if (disposing)
            {
                
            }

            IsDisposed = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~Encapsulation()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public string ToString()
    {
        return GetType().Name;
    }
}

internal class Abstraction(string nwe, string description) : Encapsulation(nwe, description)
{

}

class Polymorph(string nwe, string description) : Abstraction(nwe, description)
{
    internal int Age { get; set; } = 0;
}

internal class Test
{
    internal Test() { }

    private Abstraction abstr;

    internal Abstraction Abstr
    { 
        get { return abstr; }
        set { abstr = value; } 
    }

    internal Polymorph Poly { get; set; }

    internal List<Encapsulation> LocalList { get; set; }

    internal void Anything() 
    {
        //Encapsulation e = new(); // impossible to create abstract object

        Abstr = new("1", "2");
        Poly = new("2", "2");
        LocalList = [ Abstr, Poly ];
    }
}
