//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DO;

namespace Dal;

public class DalOrder
{
    private static void AddOrder(Order order)
    {
        foreach (var item in DataSource.Orders)
        {
            if (item.ID == order.ID)
                throw new ArgumentException("order already exist");
        }
        DataSource.Orders[DataSource.Config.SizeOfOrder++] = order;
    }
    public static void DeleteOrder(int orderID)
    {
        bool flag = false;

        for (int i = 0; i < DataSource.Config.SizeOfOrder; i++)
        {
            if (orderID == DataSource.Orders[i].ID)
            {
                DataSource.Orders[i] = DataSource.Orders[DataSource.Config.SizeOfOrder];
                DataSource.Config.SizeOfOrder--;
                flag = true; // deleting successfully don't throw an exception
            }
        }
        if (!flag)
            throw new ArgumentException("order dose not exist");
    }

    public static void Update(Order order)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.SizeOfOrder; i++)
        {
            if (DataSource.Orders[i].ID == order.ID)
            {
                DataSource.Orders[i] = order;
                flag = true;
            }
        }
        if (!flag)
            throw new ArgumentException("order dose not exist");
    }

    public static Order Get(int orderID)
    {
        foreach (var item in DataSource.Orders)
        {
            if (item.ID == orderID)
                return item;
        }
        throw new ArgumentException("order dose not exist");
    }
    public static Order[] Get()
    {
        return DataSource.Orders;
    }
}
