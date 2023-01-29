using DalApi;
using DO;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    /// <summary>
    /// adding an order item to the list
    /// </summary>
    /// <param name="orderItem"></param>
    /// <returns></returns>
    /// <exception cref="AlreadyExistsException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(OrderItem orderItem)
    {
        orderItem.ID = DataSource.Config.GetOrderItemId(); 
        DataSource.OrderItems.Add(orderItem);
        return orderItem.ID;
    }

    /// <summary>
    /// deletes an existing order item
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotExistsException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        OrderItem? orderItem = DataSource.OrderItems.FirstOrDefault(x => x?.ID == id)
            ?? throw new NotExistsException("order item does not exist");
        
        DataSource.OrderItems.Remove(orderItem);
    }


    /// <summary>
    /// updating an existing order item
    /// </summary>
    /// <param name="orderItem"></param>
    /// <exception cref="NotExistsException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(OrderItem orderItem)
    {
        int index = DataSource.OrderItems.IndexOf(DataSource.OrderItems.FirstOrDefault(o => o?.ID == orderItem.ID));
        if (index != -1) // if was found update the order item
        {
            DataSource.OrderItems[index] = orderItem;
        }
        else
        {
            throw new NotExistsException("order item does not exist");
        }
    }

    /// <summary>
    /// receives a filter and returns order-item that matches the condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="NotExistsException"></exception>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderItem? GetEntity(Func<OrderItem?, bool>? func)
    {
        OrderItem? result = DataSource.OrderItems.FirstOrDefault(func!) ?? 
            throw new NotExistsException("order dose not exist");

        return result;
    }

    /// <summary>
    /// returns list of all order items
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func)
    {
        if (func == null)
        {
            var list = from item in DataSource.OrderItems
                       select item;
            return list;
        }
        else
        {
            var list = from item in DataSource.OrderItems
                       where (func(item))
                       select item;
            return list;
        }
    }
}