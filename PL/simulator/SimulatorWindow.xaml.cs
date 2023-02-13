using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO.Packaging;
using MaterialDesignThemes.Wpf;
using PL.simulator;

namespace PL.simulator;

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window , INotifyPropertyChanged
{
    BackgroundWorker worker;
    private Stopwatch stopWatch;

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChange(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private int orderId;

    public int OrderId
    {
        get => orderId;
        set
        {
            orderId = value;
            OnPropertyChange(nameof(OrderId));
        }
    }

    private BO.OrderStatus currentStatus;
    public BO.OrderStatus CurrentStatus
    {
        get => currentStatus;
        set
        {
            currentStatus = value;
            OnPropertyChange(nameof(CurrentStatus));
        }
    }

    private BO.OrderStatus newStatus;

    public BO.OrderStatus NewStatus
    {
        get => newStatus;
        set
        {
            newStatus = value;
            OnPropertyChange(nameof(NewStatus));
        }
    }

    private DateTime? startTime;
    public string? StartTime => startTime?.ToString("HH:mm:ss");

    private DateTime? estimatedTime;
    public string? EstimatedTime => estimatedTime?.ToString("HH:mm:ss");

    private string? resultLabelMsg;
    public string? ResultLabelMsg
    {
        get => resultLabelMsg;
        set
        {
            resultLabelMsg = value;
            OnPropertyChange(nameof(ResultLabelMsg));
        }
    }

    private string? timerText;
    public string? TimerText
    {
        get => timerText;
        set
        {
            timerText = value;
            OnPropertyChange(nameof(TimerText));
        }
    }

    private int? estimatedTimeInSec;
    public int? EstimatedTimeInSec
    {
        get => estimatedTimeInSec;
        set
        {
            estimatedTimeInSec = value;
            OnPropertyChange(nameof(EstimatedTimeInSec));
        }
    }

    public SimulatorWindow()
    {
        TimerText = "00:00:00";
        InitializeComponent();
        stopWatch = new Stopwatch();
        worker = new BackgroundWorker();
        worker.DoWork += Worker_DoWork;
        worker.ProgressChanged += Worker_ProgressChanged;
        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

        if (!worker.IsBusy)
            worker.RunWorkerAsync();

        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;

        this.Closing += new CancelEventHandler(MyWindowClosing!);
    }

    private void MyWindowClosing(object sender, CancelEventArgs e)
    {
        MessageBoxResult end = MessageBox.Show("Are you sure?", "ERROR", MessageBoxButton.YesNo, MessageBoxImage.Stop);
        switch (end)
        {
            case MessageBoxResult.Yes:
                worker.CancelAsync();
                break;
            case MessageBoxResult.No:
                e.Cancel = true;
                break;
        }
    }

    private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        Simulator.UnRegisterToStopEvent(stopSim);
        Simulator.UnRegisterToUpdateEvent(updateOrder);
    }

    public int progress { get; set; }
    private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        TimerText = stopWatch.Elapsed.ToString();
        TimerText = TimerText.Substring(0, 8);
        progress = e.ProgressPercentage;
        OnPropertyChange(nameof(progress));

        if (e.UserState != null)
        {
            var a = e.UserState as Tuple<BO.Order, int>;
            var now = DateTime.Now;
            startTime = now;
            EstimatedTimeInSec = a?.Item2;
            estimatedTime = now + new TimeSpan(0, 0, EstimatedTimeInSec ?? 0);
            OnPropertyChange(nameof(EstimatedTime));
            OnPropertyChange(nameof(StartTime));
            OrderId = a!.Item1.ID;
            CurrentStatus = (BO.OrderStatus)a!.Item1.Status!;
            NewStatus = a!.Item1.Status == BO.OrderStatus.Confirmed ? BO.OrderStatus.Sent : BO.OrderStatus.Delivered;
        }
    }

    private void Worker_DoWork(object? sender, DoWorkEventArgs e)
    {
        Simulator.RegisterToStopEvent(stopSim);
        Simulator.RegisterToUpdateEvent(updateOrder);
        Simulator.StartSim();
        stopWatch.Start();
        
        while (!worker.CancellationPending)
        {
            for (int i = 1; i <= estimatedTimeInSec; i++)
            {
                Thread.Sleep(1000);
                worker.ReportProgress((int)(i * 100 / estimatedTimeInSec)!);
            }
        }
    }

    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        if (worker.WorkerSupportsCancellation)
        {
            stopWatch.Stop();
            worker.CancelAsync();
            Simulator.StopSim();
            
            ResultLabelMsg = "Stopped!";
        }
    }

    private void stopSim(object? sender, EventArgs e)
    {
        worker.CancelAsync();
    }

    private void updateOrder(object? sender, Tuple<BO.Order, int> t)
    {
        worker.ReportProgress(0, t);
    }
}