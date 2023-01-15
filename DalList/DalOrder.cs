using DalApi;
using DO;
using System.Linq;

namespace Dal;

internal class DalOrder : IOrder
{
    /// <summary>
    /// adding an order to the order list
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    /// <exception cref="AlreadyExistsException"></exception>
    public int Add(Order order)
    {
        order.ID = DataSource.Config.GetOrderId();
        DataSource.Orders.Add(order);
        return order.ID;
    }

    /// <summary>
    /// deletes an existing order
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotExistsException"></exception>
    public void Delete(int id)
    {
        Order? orderToDelete = DataSource.Orders.FirstOrDefault(order => id == order?.ID) 
            ?? throw new NotExistsException("order does not exist");
      
        DataSource.Orders.Remove(orderToDelete);
    }

    // searching by id which order to update
    /// <summary>
    /// updating an existing order
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="NotExistsException"></exception>
    public void Update(Order order)
    {
        int index = DataSource.Orders.IndexOf(DataSource.Orders.FirstOrDefault(o => o?.ID == order.ID));
        if (index != -1) // if was found update the order
        {
            DataSource.Orders[index] = order;
        }
        else
        {
            throw new NotExistsException("order does not exist");
        }
    }

    /// <summary>
    /// receives a filter and returns order that matches the condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="NotExistsException"></exception>
    public Order? GetEntity(Func<Order?, bool>? func)
    {
        Order? result = DataSource.Orders.FirstOrDefault(func!)
            ?? throw new NotExistsException("order dose not exist");

        return result;
    }


    /// <summary>
    /// returns list of all orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func)
    {
        if (func == null)
        {
            var list = from item in DataSource.Orders
                       select item;
            return list;
        }
        else
        {
            var list = from item in DataSource.Orders
                       where (func(item))
                       select item;
            return list;
        }
    }
}
