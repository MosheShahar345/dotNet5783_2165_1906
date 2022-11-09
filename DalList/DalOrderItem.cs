//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DO;

namespace Dal;

public class DalOrderItem
{
    public static int AddOrderItem(OrderItem orderItem)
    {
        foreach (var item in DataSource.OrderItems)
        {
            if (item.ProductID == orderItem.ProductID && item.OrderID == orderItem.OrderID)
                throw new ArgumentException("order item already exist");
        }
        DataSource.OrderItems[DataSource.Config.SizeOfOrderItem++] = orderItem;
        return orderItem.OrderID;
    }
    public static void DeleteOrderItem(int orderItemID)
    {
        bool flag = false;

        for (int i = 0; i < DataSource.Config.SizeOfOrderItem; i++)
        {
            if (orderItemID == DataSource.OrderItems[i].OrderID)
            {
                DataSource.OrderItems[i] = DataSource.OrderItems[DataSource.Config.SizeOfOrderItem];
                DataSource.Config.SizeOfOrderItem--;
                flag = true; // deleting successfully don't throw an exception
            }
        }
        if (!flag)
            throw new ArgumentException("order item dose not exist");
    }

    public static void Update(OrderItem orderItem)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.SizeOfOrderItem; i++)
        {
            if (DataSource.OrderItems[i].OrderID == orderItem.OrderID)
            {
                DataSource.OrderItems[i] = orderItem;
                flag = true;
            }
        }
        if (!flag)
            throw new ArgumentException("order item dose not exist");
    }

    public static OrderItem Get(int orderItemID)
    {
        foreach (var item in DataSource.OrderItems)
        {
            if (item.OrderID == orderItemID)
                return item;
        }
        throw new ArgumentException("order item dose not exist");
    }
    public static OrderItem[] Get()
    {
        return DataSource.OrderItems;
    }
}
