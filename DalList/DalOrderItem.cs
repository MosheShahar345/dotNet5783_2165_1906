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
    /// receives an id and returns its order item
    /// </summary>
    /// <param name="orderItemID"></param>
    /// <returns></returns>
    /// <exception cref="NotExistsException"></exception>
    public OrderItem? GetById(int orderItemID)
    {
        OrderItem? orderItem;
        orderItem = DataSource.OrderItems.FirstOrDefault(item => item?.ID == orderItemID);
        if(orderItem?.ID != orderItemID)
        {
            // throws an exception if the order does not exist
            throw new NotExistsException("order item dose not exist");
        }
        return orderItem;
    }

    /// <summary>
    /// returns list of all order items
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? func)
    {
        if (func == null)
        {
            List<OrderItem?> orderItem = new List<OrderItem?>();
            foreach (var item in DataSource.OrderItems)
            {
                orderItem.Add(item);
            }
            return orderItem;
        }
        List<OrderItem?> orderItem1 = new List<OrderItem?>();
        foreach (var item in DataSource.OrderItems)
        {
            if (func(item))
                orderItem1.Add(item);
        }
        return orderItem1;
    }

    /// <summary>
    /// returns list of order items if the product id and the order id is equal
    /// </summary>
    /// <param name="orderID"></param>
    /// <param name="productID"></param>
    /// <returns></returns>
    /// <exception cref="NotExistsException"></exception>
    public OrderItem? GetByIds(int orderID ,int productID )
    {
        foreach(var item in DataSource.OrderItems)
        {
            if(item?.ProductID == productID && item?.OrderID == orderID)
                return item;
        }
        throw new NotExistsException("order item dose not exist");
    }

    /// <summary>
    /// returns list of all order items if order id is match
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public List<OrderItem?> GetOrderItem(int orderId)
    {
        List<OrderItem?> orderitems = new List<OrderItem?>();
        foreach (var item in DataSource.OrderItems)
        {
            if (item?.OrderID == orderId)
                orderitems.Add(item ?? throw new Exception());
        }
        return orderitems;
    }
}