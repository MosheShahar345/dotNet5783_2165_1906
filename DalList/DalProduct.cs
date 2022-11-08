//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DO;
using static Dal.DataSource;

namespace Dal;

public class DalProduct
{
    private static void AddProduct(Product product)
    {
        foreach (var item in DataSource.Products)
        {
            if (item.Name == product.Name && item.Category == product.Category)
                throw new ArgumentException("product already exist");
        }
        DataSource.Products[Config.SizeOfProduct++] = product;
    }

    private static void DeleteProduct(int productID)
    {
        bool flag = false;
        for(int i =0; i< DataSource.Config.SizeOfProduct; i++)
        {
            
            if (productID == DataSource.Products[i].ID)
            {
                DataSource.Products[i] = DataSource.Products[DataSource.Config.SizeOfProduct];
                flag = true; // deleting successfully don't throw an exception
            } 
        }
        if (!flag) 
            throw new ArgumentException("product dose not exist");
    }
}
