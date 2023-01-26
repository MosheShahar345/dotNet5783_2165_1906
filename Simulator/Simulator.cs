using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

//namespace Simulator;
public static class Simulator
{
    private static BlApi.IBl? bl = BlApi.Factory.Get();
    private static Thread? thread;
    private static volatile bool beContinue = true;
    private static event EventHandler<Tuple<BO.Order, int>>? updateSim;
    private static event EventHandler? stopSim;

    public static void RegisterToStopEvent(EventHandler handler)
    {
        stopSim += handler;
    }

    public static void UnRegisterToStopEvent(EventHandler handler)
    {
        if (stopSim!.GetInvocationList().Contains(handler))
            stopSim -= handler;
    }

    public static void RegisterToUpdateEvent(EventHandler<Tuple<BO.Order, int>> handler)
    {
        updateSim += handler;
    }

    public static void UnRegisterToUpdateEvent(EventHandler<Tuple<BO.Order, int>> handler)
    {
        if (updateSim!.GetInvocationList().Contains(handler))
            updateSim -= handler;
    }

    public static void StopSim()
    {
        beContinue = false;
        thread?.Interrupt();
    }

    public static void StartSim()
    {
        thread = new Thread(() =>
        {
            while (beContinue && bl?.Order.OldestOrder() != null)
            {
                try
                {
                    var order = bl!.Order.GetOrder(bl?.Order.OldestOrder() ?? throw new NullReferenceException());
                    var processTime = new Random().Next(3, 11);
                    var estimatedTime = new Random().Next(processTime - 2, processTime + 2);

                    updateSim?.Invoke(null, new Tuple<BO.Order, int>(order, estimatedTime));

                    try
                    {
                        Thread.Sleep(processTime * 1000);
                    }
                    catch (ThreadInterruptedException) { }

                    if (!beContinue)
                        break;

                    switch (order.Status)
                    {
                        case BO.OrderStatus.Confirmed:
                            bl?.Order.UpdateOrderShipping(order.ID);
                            break;
                        case BO.OrderStatus.Sent:
                            bl?.Order.UpdateOrderDelivery(order.ID);
                            break;
                        default:
                            stopSim?.Invoke(null, EventArgs.Empty);
                            break;
                    }
                }
                catch (BO.NotExistsException e)
                {
                    Console.WriteLine(e);
                    StopSim();
                }   
            }
            stopSim?.Invoke(null, EventArgs.Empty);
        });

        thread.Start();
    }
}