//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DO;

namespace Dal;

/// <summary>
/// A class with basic product functions
/// </summary>
public class DalProduct
{
    /// <summary>
    /// Adding an product to the product array
    /// </summary>
    public static void AddProduct(Product product)
    {
        foreach (var item in DataSource.Products)
        {
            if (item.Name == product.Name && item.Category == product.Category)// A check uf the product is already exists
                throw new ArgumentException("product already exist");
        }
        DataSource.Products[DataSource.Config.SizeOfProduct++] = product;//if it does not exist, it is inserted into the array
    }
    /// <summary>
    /// deletes an existing product
    /// </summary>
    public static void DeleteProduct(int productID)
    {
        bool flag = false;

        for (int i = 0; i < DataSource.Config.SizeOfProduct; i++)//Checks if such a product exists according to ID
        {
            if (productID == DataSource.Products[i].ID)
            {
                DataSource.Products[i] = DataSource.Products[DataSource.Config.SizeOfProduct];//replaces the last one with the one that is deleted
                DataSource.Config.SizeOfProduct--;//decreases the array by one
                flag = true; // deleting successfully don't throw an exception
            } 
        }
        if (!flag) 
            throw new ArgumentException("product dose not exist");
    }
    /// <summary>
    /// updateing an existing product
    /// </summary>
    public static void Update(Product product)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Config.SizeOfProduct; i++)
        {
            if (DataSource.Products[i].ID == product.ID)//Searching by id which product to update
            {
                DataSource.Products[i] = product;
                flag = true;
            }
        }
        if (!flag)
            throw new ArgumentException("product dose not exist");
    }
    /// <summary>
    /// receives a id and returns his product
    /// </summary>
    public static Product Get(int productID)
    {
        foreach (var item in DataSource.Products)
        {
            if (item.ID == productID)
                return item;
        }
        throw new ArgumentException("product dose not exist");//Throws an exception if the product does not exist
    }
    /// <summary>
    /// returns the array of products
    /// </summary>
    public static Product[] Get()
    {
        return DataSource.Products;
    }
}
