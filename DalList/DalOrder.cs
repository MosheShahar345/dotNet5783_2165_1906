//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DO;

namespace Dal;

public class DalOrder
{
    private static void AddOrderItem(Order order)
    {
        foreach (var item in DataSource.Orders)
        {
            if (item.ID == order.ID && item.CustomerName == order.CustomerName && item.OrderDate == order.OrderDate)
                throw new ArgumentException("product already exist");
        }
        DataSource.Orders[DataSource.Config.SizeOfOrder++] = order;
    }
}
