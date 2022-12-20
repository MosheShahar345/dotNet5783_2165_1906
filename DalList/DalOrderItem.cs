using DalApi;
using DO;
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
    public int Add(OrderItem orderItem)
    {
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            if (DataSource.OrderItems[i]?.ID == orderItem.ID) // check if the order item is already exists
                throw new AlreadyExistsException("order item already exist");
        }
        DataSource.OrderItems.Add(orderItem); // if it does not exist add to list
        return orderItem.ID;
    }

    /// <summary>
    /// deletes an existing order item
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="NotExistsException"></exception>
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            if (id == DataSource.OrderItems[i]?.ID) // checks if exists according to ID
            {
                DataSource.OrderItems.RemoveAt(i);
                return; // deleting successfully don't throw an exception
            }
        }
        throw new NotExistsException("order item dose not exist");
    }

    /// <summary>
    /// updating an existing order item
    /// </summary>
    /// <param name="orderItem"></param>
    /// <exception cref="NotExistsException"></exception>
    public void Update(OrderItem orderItem)
    {
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            if (DataSource.OrderItems[i]?.ID == orderItem.ID) // searching by id which order to update
            {
                DataSource.OrderItems[i] = orderItem;
                return;
            }
        }
        throw new NotExistsException("order item dose not exist");
    }

    /// <summary>
    /// receives a filter and returns order-item that matchs the condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="NotExistsException"></exception>
    public OrderItem? GetEntity(Func<OrderItem?, bool>? func)
    {
        foreach (var item in DataSource.OrderItems)
        {
            if (func!(item))
                return item;
        }

        // throws an exception if the order does not exist
        throw new NotExistsException("order item dose not exist");
    }

    /// <summary>
    /// returns list of all order items
    /// </summary>
    /// <returns></returns>
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