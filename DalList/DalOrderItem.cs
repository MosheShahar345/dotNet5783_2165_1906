//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DO;
using static Dal.DataSource;

namespace Dal;

public class DalOrderItem
{
   private static void AddOrderItem(OrderItem orderItem)
    {
        foreach (var item in DataSource.OrderItems)
        {
            if (item.OrderID == orderItem.OrderID && item.ProductID == orderItem.ProductID)
                throw new ArgumentException("product already exist");
        }
        DataSource.OrderItems[Config.SizeOfOrderItem++] = orderItem;
    }
}
