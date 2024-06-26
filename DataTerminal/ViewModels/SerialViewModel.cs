﻿using System.Collections.ObjectModel;
using System.Data;
using System.IO.Ports;
using System.Timers;
using System.Windows.Input;

namespace DataTerminal.ViewModels;

public class SerialViewModel : ViewModelBase
{
    private bool isConnected;
    public bool IsConnected
    {
        get => isConnected;
        set
        {
            isConnected = value;
            OnPropertyChanged(nameof(IsConnected));
        }
    }

    private string? information;
    public string? Information
    {
        get => information;
        set
        {
            information = value;
            OnPropertyChanged(nameof(Information));
        }
    }

    private ObservableCollection<string> ports;
    public ObservableCollection<string> Ports
    {
        get => ports;
        set
        {
            ports = value;
            OnPropertyChanged(nameof(Ports));
        }
    }

    private DataTable data;
    public DataTable Data
    {
        get => data;
        set
        {
            data = value;
            OnPropertyChanged(nameof(Data));
        }
    }

    private SerialPort port;
    public SerialPort Port
    {
        get => port;
        set
        {
            port = value;
            OnPropertyChanged(nameof(Port));
        }
    }

    private ICommand? refreshPorts;
    public ICommand? RefreshPorts
    {
        get
        {
            refreshPorts ??= new RelayCommand(param =>
            {
                Ports = new(SerialPort.GetPortNames());
            }, null);

            return refreshPorts;
        }
    }

    private ICommand? connection;
    public ICommand? Connection
    {
        get
        {
            connection ??= new RelayCommand(param =>
            {
                if (IsConnected)
                {
                    Port?.Close();
                    Port?.Dispose();
                    IsConnected = !IsConnected;
                }
                else
                {
                    try
                    {
                        Port.DataReceived += Port_DataReceived;
                        Port.DiscardNull = true;
                        Port.Open();
                        IsConnected = !IsConnected;
                    }
                    catch (Exception ex)
                    {
                        //AddLog(ex.Message, Level.Error);
                        Information = ex.Message;
                    }
                }
            }, null);

            return connection;
        }
    }

    public SerialViewModel()
    {
        Port = new SerialPort() { DataBits = 8, Parity = Parity.None, BaudRate = 250000 };
        IsConnected = false;
    }

    private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        try
        {
            Task.Run(() =>
            {
                Information += ((SerialPort)sender).ReadExisting() + "\r\n";
            });
        }
        catch (Exception ex)
        {
            //AddLog(ex.Message, Level.Error);
            Information += ex.Message;
        }
    }
}
