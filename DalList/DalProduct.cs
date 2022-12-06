using DalApi;
using DO;

namespace Dal;

/// <summary>
/// A class with basic product functions
/// </summary>
internal class DalProduct : IProduct
{
    /// <summary>
    /// Adding a product to the product list
    /// </summary>
    public int Add(Product product)
    {
        if (product.ID != 0)
        {
            try
            {
                GetById(product.ID);
            }
            catch (NotExistsException)
            {
                DataSource.Products.Add(product);
                return product.ID;
            }
            
            throw new AlreadyExistsException($"product with ID: {product.ID} already exists");
        }

        bool inStock = false;
        int id = 0;

        while (!inStock)
        {
            id = DataSource.Config.Num.Next(100000, 999999);

            try
            {
                GetById(id);
            }
            catch (NotExistsException)
            {
                inStock = true;
            }
        }

        product.ID = id;
        DataSource.Products.Add(product);
        return product.ID;  
    }

    /// <summary>
    /// deletes an existing product
    /// </summary>
    public void Delete(int id)
    {
        bool flag = false;

        for (int i = 0; i < DataSource.Products.Count; i++)//Checks if such a product exists according to ID
        {
            if (id == DataSource.Products[i].ID)
            {
                DataSource.Products.RemoveAt(i);
                flag = true;
            } 
        }
        if (!flag) 
            throw new NotExistsException("product dose not exist");
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
            throw new NotExistsException("product dose not exist");
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
        throw new NotExistsException("product dose not exist");//Throws an exception if the product does not exist
    }
    /// <summary>
    /// returns the array of products
    /// </summary>
    public IEnumerable<Product> GetAll()
    {
        List<Product> products = new List<Product>();
        foreach (var item in DataSource.Products)
        {
            products.Add(item);
        }
        return products;
    }
}
