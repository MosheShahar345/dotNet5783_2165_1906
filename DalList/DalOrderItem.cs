//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DO;

namespace Dal;

public class DalOrderItem
{
    public static int AddProduct(Product product)
    {
        foreach (var item in DataSource.Products)
        {
            if (item.Name == product.Name && item.Category == product.Category)
                throw new ArgumentException("product already exist");
        }
        DataSource.Products[DataSource.Config.SizeOfProduct++] = product;
        return product.ID;
    }
}
