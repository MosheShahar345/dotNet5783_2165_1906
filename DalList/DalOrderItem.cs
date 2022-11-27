using DalApi;
using DO;
using System.Security.Principal;

namespace Dal;

/// <summary>
/// A class with basic order items functions
/// </summary>
internal class DalOrderItem : IOrderItem
{
   /// <summary>
   /// Adding an order item to the array
   /// </summary>
    public int Add(OrderItem orderItem)
    {
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            if (DataSource.OrderItems[i].ID == orderItem.ID)// A check uf the order item is already exists
                throw new AlreadyExistsException("order item already exist");
        }
        DataSource.OrderItems.Add(orderItem);//if it does not exist, it is inserted into the list
        return orderItem.ID;
    }
    /// <summary>
    /// deletes an existing order item
    /// </summary>
    public void Delete(int id)
    {
        bool flag = false;

        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            if (id == DataSource.OrderItems[i].ID)//Checks if such a order exists according to ID
            {
                DataSource.OrderItems.RemoveAt(i);
                flag = true; // deleting successfully don't throw an exception
                if (flag)
                    break;
            }
        }
        if (!flag)
            throw new DoesNotExistException("order item dose not exist");
    }
    /// <summary>
    /// updateing an existing order item
    /// </summary>
    public void Update(OrderItem orderItem)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            if (DataSource.OrderItems[i].ID == orderItem.ID)//Searching by id which order to update
            {
                DataSource.OrderItems[i] = orderItem;
                flag = true;
            }
        }
        if (!flag)
            throw new DoesNotExistException("order item dose not exist");
    }
    /// <summary>
    /// receives a id and returns his order item
    /// </summary>
    public OrderItem GetById(int orderItemID)
    {
        OrderItem orderItem;
        orderItem = DataSource.OrderItems.FirstOrDefault(item => item.ID == orderItemID);
        if(orderItem.ID != orderItemID)
        {
            throw new DoesNotExistException("order item dose not exist");//Throws an exception if the order does not exist
        }
        return orderItem;
    }
    /// <summary>
    /// returns the array of order items
    /// </summary>
    public IEnumerable<OrderItem> GetAll()
    {
        List<OrderItem> orderitems = new List<OrderItem>();
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            OrderItem orderitem = new OrderItem();
            orderitem = DataSource.OrderItems[i];
            orderitems.Add(orderitem);
        }
        return orderitems;
    }
    /// <summary>
    /// returns the array of order items if the product id and the order id is equal 
    /// </summary>
    public OrderItem GetByIds(int orderID ,int productID )
    {
        foreach(var item in DataSource.OrderItems)
        {
            if(item.ProductID == productID && item.OrderID == orderID)
                return item;
        }
        throw new DoesNotExistException("order item dose not exist");
    }
    public List<OrderItem> GetOrderItem(int orderId)
    {
        List<OrderItem> orderitems = new List<OrderItem>();
        for (int i = 0; i < DataSource.OrderItems.Count; i++)
        {
            if (DataSource.OrderItems[i].OrderID == orderId)
            {
                orderitems[i] = DataSource.OrderItems[i];
            }
        }
        return orderitems;
    }
 }