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
    public static int AddOrderItem(OrderItem orderItem)
    {
        foreach (var item in DataSource.OrderItems)
        {
            if (item.ProductID == orderItem.ProductID && item.OrderID == orderItem.OrderID)// A check uf the order item is already exists
                throw new ArgumentException("order item already exist");
        }
        DataSource.OrderItems[DataSource.Config.SizeOfOrderItem++] = orderItem;//if it does not exist, it is inserted into the array
        return orderItem.OrderID;
    }
    /// <summary>
    /// deletes an existing order item
    /// </summary>
    public static void DeleteOrderItem(int orderItemID)
    {
        bool flag = false;

        for (int i = 0; i < DataSource.Config.SizeOfOrderItem; i++)
        {
            if (orderItemID == DataSource.OrderItems[i].OrderID)//Checks if such a order exists according to ID
            {
                DataSource.OrderItems[i] = DataSource.OrderItems[DataSource.Config.SizeOfOrderItem];//replaces the last one with the one that is deleted
                DataSource.Config.SizeOfOrderItem--;//decreases the array by one
                flag = true; // deleting successfully don't throw an exception
            }
        }
        if (!flag)
            throw new ArgumentException("order item dose not exist");
    }
    /// <summary>
    /// updateing an existing order item
    /// </summary>
    public static void Update(OrderItem orderItem)
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
    public static OrderItem Get(int orderItemID)
    {
        foreach (var item in DataSource.OrderItems)
        {
            if (item.OrderID == orderItemID)
                return item;
        }
        throw new ArgumentException("order item dose not exist");//Throws an exception if the order does not exist
    }
    /// <summary>
    /// returns the array of order items
    /// </summary>
    public static OrderItem[] Get()
    {
        return DataSource.OrderItems;
    }
    /// <summary>
    /// returns the array of order items if the product id and the order id is equal 
    /// </summary>
    public static OrderItem Get(Product product, Order order)
    {
        foreach(var item in DataSource.OrderItems)
        {
            if(item.ProductID == product.ID && item.OrderID == order.ID)
                return item;
        }
        throw new ArgumentException("order item dose not exist");
    }
}