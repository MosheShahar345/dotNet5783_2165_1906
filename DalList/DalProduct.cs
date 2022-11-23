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
    /// Adding a product to the product array
    /// </summary>
    public void addProduct(Product product)
    {
        for (int i = 0; i < DataSource.Products.Count; i++)//Checks if such a product exists according to ID
        {
            if (product.ID == DataSource.Products[i].ID)
            {
                throw new AlreadyExistException("product already exist");
            }
        }
        DataSource.Products.Add(product);//if it does not exist, it is inserted into the list
    }
    /// <summary>
    /// deletes an existing product
    /// </summary>
    public void deleteProduct(int productID)
    {
        bool flag = false;

        for (int i = 0; i < DataSource.Products.Count; i++)//Checks if such a product exists according to ID
        {
            if (productID == DataSource.Products[i].ID)
            {
                DataSource.Products.RemoveAt(i);
                flag = true;
            } 
        }
        if (!flag) 
            throw new NotExistException("product dose not exist");
    }
    /// <summary>
    /// updateing an existing product
    /// </summary>
    public void update(Product product)
    {
        bool flag = false;
        for (int i = 0; i < DataSource.Products.Count; i++)
        {
            if (DataSource.Products[i].ID == product.ID)//Searching by id which product to update
            {
                DataSource.Products[i] = product;
                flag = true;
            }
        }
        if (!flag)
            throw new NotExistException("product dose not exist");
    }
    /// <summary>
    /// receives a id and returns his product
    /// </summary>
    public Product get(int productID)
    {
        foreach (var item in DataSource.Products)
        {
            if (item.ID == productID)
                return item;
        }
        throw new NotExistException("product dose not exist");//Throws an exception if the product does not exist
    }
    /// <summary>
    /// returns the array of products
    /// </summary>
    public List<Product> get()
    {
        return DataSource.Products;
    }
}
