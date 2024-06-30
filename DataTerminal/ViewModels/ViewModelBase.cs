using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTerminal.ViewModels;

public class ViewModelBase : INotifyPropertyChanged
{
    private bool isLoaded;
    public bool IsLoaded
    {
        get => isLoaded;
        set
        {
            isLoaded = value;
            OnPropertyChanged(nameof(IsLoaded));
        }
    }

    private ViewModelBase? currentViewModel;
    public ViewModelBase? CurrentViewModel
    {
        get => currentViewModel;
        set
        {
            currentViewModel = value;
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
