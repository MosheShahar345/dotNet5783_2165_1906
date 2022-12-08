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
        for (int i = 0;i < DataSource.Orders.Count; i++)
        {
            if (DataSource.Orders[i]?.ID == order.ID) // check if the order is already exists
                throw new AlreadyExistsException("order already exist");
        }
        DataSource.Orders.Add(order); // if it does not exist, add to list
        return order.ID;
    }

    /// <summary>
    /// deletes an existing order
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotExistsException"></exception>
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.Orders.Count; i++)
        {
            if (id == DataSource.Orders[i]?.ID) // checks if exists according to ID
            {
                DataSource.Orders.RemoveAt(i);
                return; // deleting successfully don't throw an exception
            }
        }
        throw new NotExistsException("order dose not exist");
    }

    /// <summary>
    /// updating an existing order
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="NotExistsException"></exception>
    public void Update(Order order)
    {
        for (int i = 0; i < DataSource.Orders.Count; i++)
        {
            if (DataSource.Orders[i]?.ID == order.ID) // searching by id which order to update
            {
                DataSource.Orders[i] = order;
                return;
            }
        }
        throw new NotExistsException("order dose not exist");
    }

    /// <summary>
    /// receives an id and returns its order
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    /// <exception cref="NotExistsException"></exception>
    public Order? GetById(int orderID)
    {
        foreach (var item in DataSource.Orders)
        {
            if (item?.ID == orderID)
                return item;
        }

        // throws an exception if the order does not exist
        throw new NotExistsException("order dose not exist");
    }

    /// <summary>
    /// returns list of all orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? func)
    {
        if (func == null)
        {
            List<Order?> order = new List<Order?>();
            foreach (var item in DataSource.Orders)
            {
                order.Add(item);
            }
            return order;
        }
        List<Order?> order1 = new List<Order?>();
        foreach (var item in DataSource.Orders)
        {
            if (func(item))
                order1.Add(item);
        }
        return order1;
    }
}
