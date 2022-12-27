using DalApi;
namespace BlImplementation;

internal class BlProduct : BlApi.IProduct
{
    DalApi.IDal? dal = DalApi.Factory.Get(); // to access DO entities

    /// <summary>
    /// returns BO list with all the products from DO 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="BO.NotExistsException"></exception>
    public IEnumerable<BO.ProductForList?> GetProductForList(Func<BO.ProductForList?, bool>? func)
    {
        List<DO.Product?>? products = new List<DO.Product?>();
        List<BO.ProductForList?> productForList = new List<BO.ProductForList?>();

        try
        {
            products = dal?.Product.GetAll().ToList();
        }
        catch (DO.NotExistsException e) { throw new BO.NotExistsException("", e); }

        foreach (var item in products)
        {
            BO.ProductForList? tempproduct = new BO.ProductForList()
            {
                ID = (int)(item?.ID)!,
                Name = item?.Name,
                Price = (double)(item?.Price)!,
                Category = (BO.Category)item?.Category!
            };
            productForList.Add(tempproduct);
        }

        if (func != null)
        {
            var list = from item in productForList where func(item) select item;
            return (IEnumerable<BO.ProductForList>)list;
        }

        return productForList!;
    }

    /// <summary>
    /// gets id to find a product in the store and returns it
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="BO.IdIsLessThanZeroException"></exception>
    /// <exception cref="BO.NotExistsException"></exception>
    public BO.Product GetProductForAdmin(int productId)
    {
        if (productId < 0)
            throw new BO.IdIsLessThanZeroException();

        DO.Product? dProduct = new DO.Product();

        try
        {
            dProduct = dal?.Product.GetEntity(it => it?.ID == productId);
        }
        catch (DO.NotExistsException e)
        {
            throw new BO.NotExistsException("", e);
        }

        BO.Product bProduct = new BO.Product()
        {
            ID = (int)(dProduct?.ID)!,
            Name = dProduct?.Name,
            Price = (double)(dProduct?.Price)!,
            Category = (BO.Category)dProduct?.Category!,
            InStock = dProduct?.InStock ?? 0,
        };
        return bProduct;
    }

    /// <summary>
    /// gets cart and id to find a product in the cart and returns it
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    /// <exception cref="BO.IdIsLessThanZeroException"></exception>
    /// <exception cref="BO.NotExistsException"></exception>
    public BO.ProductItem GetProductForCustomer(int productId, BO.Cart cart)
    {
        if (productId < 0)
            throw new BO.IdIsLessThanZeroException();

        DO.Product? dProduct = new DO.Product();
        BO.ProductItem bProductItem;
        BO.OrderItem bOrderItem;

        bOrderItem = cart.Items?.Find(it => it!.ProductID == productId)??
                     throw new BO.NotExistsException();

        try
        {
            dProduct = dal?.Product.GetEntity(it => it?.ID == productId);
            bProductItem = new BO.ProductItem()
            {
                ID = (int)dProduct?.ID!,
                Name = dProduct?.Name,
                Price = (int)dProduct?.Price!,
                Category = (BO.Category)dProduct?.Category!,
                InStock = dProduct?.InStock > 0 ? true : false,
                Amount = bOrderItem.Amount
            };
        }
        catch (DO.NotExistsException e)
        {
            throw new BO.NotExistsException("",e);
        }
        
        return bProductItem;
    }

    /// <summary>
    /// gets product and add it to Dal
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="BO.IdIsLessThanZeroException"></exception>
    /// <exception cref="BO.InvalidNameException"></exception>
    /// <exception cref="BO.InvalidPriceException"></exception>
    /// <exception cref="BO.InvalidInStockException"></exception>
    /// <exception cref="BO.AlreadyExistsException"></exception>
    public void AddProductAdmin(BO.Product product)
    {
        if(product.ID < 0)
            throw new BO.IdIsLessThanZeroException();

        if (product.Name == null) 
            throw new BO.InvalidNameException();

        if (product.Price <= 0)
            throw new BO.InvalidPriceException();

        if (product.InStock < 0)
            throw new BO.InvalidInStockException();

        DO.Product dProduct = new DO.Product()
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            InStock = product.InStock,
            Category = (DO.Category)product.Category!
        };
     
        try
        {
            dal?.Product.Add(dProduct);
        }
        catch (DO.AlreadyExistsException e)
        {
            throw new BO.AlreadyExistsException("", e);
        }
    }

    /// <summary>
    /// gets product and delete it from Dal
    /// </summary>
    /// <param name="productId"></param>
    /// <exception cref="BO.IdIsLessThanZeroException"></exception>
    /// <exception cref="BO.NotExistsException"></exception>
    /// <exception cref="BO.CanNotDeleteProductException"></exception>
    public void DeleteProductAdmin(int productId)
    {
        if (productId < 0)
            throw new BO.IdIsLessThanZeroException();

        List<DO.OrderItem> orderItems= new List<DO.OrderItem>();
        orderItems = (List<DO.OrderItem>)dal?.OrderItem.GetAll()!; 

        try
        {
            dal?.Product.GetEntity(it => it?.ID == productId);
        }
        catch (DO.NotExistsException e)
        {
            throw new BO.NotExistsException("", e);
        }

        foreach (var item in orderItems)
        {
            if (item.ProductID == productId)
                throw new BO.CanNotDeleteProductException();
        }

        dal?.Product.Delete(productId);
    }

    /// <summary>
    /// gets a new product and updates the old product in Dal
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="BO.IdIsLessThanZeroException"></exception>
    /// <exception cref="BO.InvalidNameException"></exception>
    /// <exception cref="BO.InvalidPriceException"></exception>
    /// <exception cref="BO.InvalidInStockException"></exception>
    /// <exception cref="BO.NotExistsException"></exception>
    public void UpdateProductAdmin(BO.Product product)
    {
        if (product.ID < 0)
            throw new BO.IdIsLessThanZeroException();

        if (product.Name == null)
            throw new BO.InvalidNameException();

        if (product.Price <= 0)
            throw new BO.InvalidPriceException();

        if (product.InStock < 0)
            throw new BO.InvalidInStockException();

        DO.Product dProduct = new DO.Product()
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            InStock = product.InStock,
            Category = (DO.Category)product.Category!
        };

        try
        {
            dal?.Product.Update(dProduct);
        }
        catch (DO.NotExistsException e)
        {
            throw new BO.NotExistsException("", e);
        }
    }
}
