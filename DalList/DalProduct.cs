using DalApi;
using DO;

namespace Dal;

internal class DalProduct : IProduct
{
    /// <summary>
    /// adding a product to the product list and making sure id is uniq
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    /// <exception cref="AlreadyExistsException"></exception>
    public int Add(Product product)
    {
        if (product.ID != 0)
        {
            try
            {
                GetEntity(it => it?.ID == product.ID);
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
                GetEntity(it => it?.ID == id);
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
    /// <param name="id"></param>
    /// <exception cref="NotExistsException"></exception>
    public void Delete(int id)
    {
        Product? productToDelete = DataSource.Products.FirstOrDefault(product => id == product?.ID) ??
                                   throw new NotExistsException("product does not exist");

        DataSource.Products.Remove(productToDelete);
    }

    /// <summary>
    /// updating an existing product
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="NotExistsException"></exception>
    public void Update(Product product)
    {
        int index = DataSource.Products.IndexOf(DataSource.Products.FirstOrDefault(o => o?.ID == product.ID));
        if (index != -1) // if was found update the product
        {
            DataSource.Products[index] = product;
        }
        else
        {
            throw new NotExistsException("product does not exist");
        }
    }

    /// <summary>
    /// receives a filter and returns product that matches the condition
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="NotExistsException"></exception>
    public Product? GetEntity(Func<Product?, bool>? func)
    {
        Product? result = DataSource.Products.FirstOrDefault(func!) ??
                          throw new NotExistsException("product dose not exist");

        return result;
    }

    /// <summary>
    /// returns list of all products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? func)
    {
        if (func == null)
        {
            var list = from item in DataSource.Products
                       select item;
            return list;
        }
        else
        {
            var list = from item in DataSource.Products
                       where(func(item))
                       select item;
            return list;
        }
    }   
}
