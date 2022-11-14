//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DO;

namespace Dal;
/// <summary>
/// A class with basic orders functions
/// </summary>
public class DalOrder
{
    /// <summary>
    /// Adding an order to the order array
    /// </summary>
    public void addOrder(Order order)
    {
        for (int i = 0;i < DataSource.Config.SizeOfOrder; i++)
        {
            if (DataSource.Orders[i].OrderID == order.OrderID)// A check uf the order is already exists
                throw new ArgumentException("order already exist");
        }
        DataSource.Orders[DataSource.Config.SizeOfOrder++] = order;//if it does not exist, it is inserted into the array
    }
    /// <summary>
    /// deletes an existing order
    /// </summary>
    public void deleteOrder(int orderID)
    {
        bool flag = false;

        for (int i = 0; i < DataSource.Config.SizeOfOrder; i++)
        {
            if (orderID == DataSource.Orders[i].OrderID)//Checks if such a order exists according to ID
            {
                DataSource.Orders[i] = DataSource.Orders[DataSource.Config.SizeOfOrder];//replaces the last one with the one that is deleted
                DataSource.Config.SizeOfOrder--;//decreases the array by one
                flag = true; // deleting successfully don't throw an exception
            }
        }
        if (!flag)
            throw new ArgumentException("order dose not exist");
    }
    /// <summary>
    /// updateing an existing order
    /// </summary>
    public void update(Order order)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.SizeOfOrder; i++)
        {
            if (DataSource.Orders[i].OrderID == order.OrderID)//Searching by id which order to update
            {
                DataSource.Orders[i] = order;
                flag = true;
            }
        }
        if (!flag)
            throw new ArgumentException("order dose not exist");
    }
    /// <summary>
    /// receives a id and returns his order
    /// </summary>
    public Order get(int orderID)
    {
        foreach (var item in DataSource.Orders)
        {
            if (item.OrderID == orderID)
                return item;
        }
        throw new ArgumentException("order dose not exist");//Throws an exception if the order does not exist
    }
    /// <summary>
    /// returns the array of orders
    /// </summary>
    public Order[] get()
    {
        return DataSource.Orders;
    }
}
