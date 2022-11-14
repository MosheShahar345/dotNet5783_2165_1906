//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DO;

namespace Dal;

/// <summary>
/// A class with basic order items functions
/// </summary>
public class DalOrderItem
{
   /// <summary>
   /// Adding an order item to the array
   /// </summary>
    public int addOrderItem(OrderItem orderItem)
    {
        for (int i = 0; i < DataSource.Config.SizeOfOrderItem; i++)
        {
            if (DataSource.OrderItems[i].OrderItemID == orderItem.OrderItemID)// A check uf the order item is already exists
                throw new ArgumentException("order item already exist");
        }
        DataSource.OrderItems[DataSource.Config.SizeOfOrderItem++] = orderItem;//if it does not exist, it is inserted into the array
        return orderItem.OrderItemID;
    }
    /// <summary>
    /// deletes an existing order item
    /// </summary>
    public void deleteOrderItem(int orderItemID)
    {
        bool flag = false;

        for (int i = 0; i < DataSource.Config.SizeOfOrderItem; i++)
        {
            if (orderItemID == DataSource.OrderItems[i].OrderID)//Checks if such a order exists according to ID
            {
                DataSource.OrderItems[i] = DataSource.OrderItems[DataSource.Config.SizeOfOrderItem];//replaces the last one with the one that is deleted
                DataSource.Config.SizeOfOrderItem--;//decreases the array by one
                flag = true; // deleting successfully don't throw an exception
                if (flag)
                    break;
            }
        }
        if (!flag)
            throw new ArgumentException("order item dose not exist");
    }
    /// <summary>
    /// updateing an existing order item
    /// </summary>
    public void update(OrderItem orderItem)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.SizeOfOrderItem; i++)
        {
            if (DataSource.OrderItems[i].OrderID == orderItem.OrderID)//Searching by id which order to update
            {
                DataSource.OrderItems[i] = orderItem;
                flag = true;
            }
        }
        if (!flag)
            throw new ArgumentException("order item dose not exist");
    }
    /// <summary>
    /// receives a id and returns his order item
    /// </summary>
    public OrderItem get(int orderItemID)
    {
        OrderItem orderItem;
        orderItem = DataSource.OrderItems.FirstOrDefault(item => item.OrderItemID == orderItemID);
        if(orderItem.OrderItemID != orderItemID)
        {
            throw new ArgumentException("order item dose not exist");//Throws an exception if the order does not exist
        }
        return orderItem;
    }
    /// <summary>
    /// returns the array of order items
    /// </summary>
    public OrderItem[] get()
    {
        return DataSource.OrderItems;
    }
    /// <summary>
    /// returns the array of order items if the product id and the order id is equal 
    /// </summary>
    public OrderItem get(Product product, Order order)
    {
        foreach(var item in DataSource.OrderItems)
        {
            if(item.ProductID == product.ProductID && item.OrderID == order.OrderID)
                return item;
        }
        throw new ArgumentException("order item dose not exist");
    }
}