﻿//using System;
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
}
