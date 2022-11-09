//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DO;

namespace Dal;

public class DalProduct
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

    public static void DeleteProduct(int productID)
    {
        bool flag = false;

        for (int i = 0; i < DataSource.Config.SizeOfProduct; i++)
        {
            if (productID == DataSource.Products[i].ID)
            {
                DataSource.Products[i] = DataSource.Products[DataSource.Config.SizeOfProduct];
                DataSource.Config.SizeOfProduct--;
                flag = true; // deleting successfully don't throw an exception
            } 
        }
        if (!flag) 
            throw new ArgumentException("product dose not exist");
    }

    public static void Update(Product product)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.SizeOfProduct; i++)
        {
            if (DataSource.Products[i].ID == product.ID)
            {
                DataSource.Products[i] = product;
                flag = true;
            }
        }
        if (!flag)
            throw new ArgumentException("product dose not exist");
    }
}
