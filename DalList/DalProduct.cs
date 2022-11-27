using DalApi;
using DO;
using System.Security.Principal;

namespace Dal;

/// <summary>
/// A class with basic product functions
/// </summary>
internal class DalProduct : IProduct
{
    /// <summary>
    /// Adding a product to the product array
    /// </summary>
    public int Add(Product product)
    {
        for (int i = 0; i < DataSource.Products.Count; i++)//Checks if such a product exists according to ID
        {
            if (product.ID == DataSource.Products[i].ID)
            {
                throw new AlreadyExistsException($"product with ID={product.ID} already exists");
            }
        }
        DataSource.Products.Add(product);//if it does not exist, it is inserted into the list
        return product.ID;  
    }
    /// <summary>
    /// deletes an existing product
    /// </summary>
    public void Delete(Product product)
    {
        bool flag = false;

        for (int i = 0; i < DataSource.Products.Count; i++)//Checks if such a product exists according to ID
        {
            if (product.ID == DataSource.Products[i].ID)
            {
                DataSource.Products.RemoveAt(i);
                flag = true;
            } 
        }
        if (!flag) 
            throw new DoesNotExistException("product dose not exist");
    }
    /// <summary>
    /// updateing an existing product
    /// </summary>
    public void Update(Product product)
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
            throw new DoesNotExistException("product dose not exist");
    }
    /// <summary>
    /// receives a id and returns his product
    /// </summary>
    public Product GetById(int productID)
    {
        foreach (var item in DataSource.Products)
        {
            if (item.ID == productID)
                return item;
        }
        throw new DoesNotExistException("product dose not exist");//Throws an exception if the product does not exist
    }
    /// <summary>
    /// returns the array of products
    /// </summary>
    public IEnumerable<Product> GetAll()
    {
        List<Product> products = new List<Product>();
        for (int i = 0; i < DataSource.Products.Count; i++)
        {
            Product product = new Product();
            product = DataSource.Products[i];
            products.Add(product);
        }
        return products;
    }
}
