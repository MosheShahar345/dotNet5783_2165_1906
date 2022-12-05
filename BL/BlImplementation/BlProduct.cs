using BO;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlProduct : BlApi.IProduct
{
    private IDal Dal = new DalList();

    public IEnumerable<BO.ProductForList> GetProductForList()
    {
        List<DO.Product> products = new List<DO.Product>();
        List<BO.ProductForList> productForList = new List<BO.ProductForList>();

        try
        {
            products = Dal.Product.GetAll().ToList();
        }
        catch (Exception)
        {
            throw new NotExistsException();
        }
        foreach (var item in products)
        {
            BO.ProductForList tempproduct = new BO.ProductForList()
            {
                ID = item.ID,
                Name= item.Name,
                Price= item.Price,
                Category = (Category)item.Category
            };
            productForList.Add(tempproduct);
        }
        return productForList;
    }

    public Product GetProduct(int productId)
    {
        if (productId <= 0)
            throw new BO.IdIsLessThanZeroException();

        DO.Product dProduct = new DO.Product();

        try
        {
            dProduct = Dal.Product.GetById(productId);
        }
        catch (Exception e)
        {
            throw new BO.NotExistsException("", e);
        }

        BO.Product bProduct = new BO.Product()
        {
            ID = dProduct.ID,
            Name = dProduct.Name,
            Price = dProduct.Price,
            Category = (Category)dProduct.Category,
            InStock = dProduct.InStock,
        };
        return bProduct;
    }
    public BO.ProductItem GetProductCustomer(int productId, BO.Cart cart)
    {
        if (productId <= 0)
            throw new BO.IdIsLessThanZeroException();

        DO.Product dProduct = new DO.Product();
        BO.ProductItem bProductItem;
        
        try
        {
            dProduct = Dal.Product.GetById(productId);
            bProductItem = new BO.ProductItem()
            {
                ID = dProduct.ID,
                Name = dProduct.Name,
                Price = dProduct.Price,
                Category = (Category)dProduct.Category,
                InStock = dProduct.InStock > 0 ? true : false,
                Amount = cart.Items.Find(it => it.ProductID == productId).Amount
            };
        }
        catch (Exception e)
        {
            throw new BO.NotExistsException("",e);
        }
        
        return bProductItem;
    }

    public void AddProductAdmin(BO.Product product)
    {
        if(product.ID < 100000 || product.ID > 999999 
            || product.Name == null || product.Price <= 0 || product.InStock < 0)
            throw new BO.InvalidInputException();

        DO.Product dProduct = new DO.Product()
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            InStock = product.InStock,
            Category = (DO.Category)product.Category
        };
     
        try
        {
            Dal.Product.Add(dProduct);
        }
        catch (Exception)
        {
            throw new BO.AlreadyExistsException();
        }
    }

    public void DeleteProductAdmin(int productId)
    {
        List<DO.OrderItem> orderItems= new List<DO.OrderItem>();
        orderItems = (List<DO.OrderItem>)Dal.OrderItem.GetAll();

        foreach (var item in orderItems)
        {
            if(item.ProductID == productId)
                throw new AlreadyExistsException();
        }

        try
        {
            Dal.Product.Delete(productId);
        }
        catch (Exception)
        {
            throw new BO.NotExistsException();
        }
    }

    public void UpdateProductAdmin(BO.Product product)
    {
        if (product.ID < 100000 || product.ID > 999999 ||
            product.Name == null || product.Price <= 0 ||
            product.InStock < 0)
            throw new BO.NotExistsException();

        DO.Product dProduct = new DO.Product()
        {
            ID = product.ID,
            Name = product.Name,
            Price = product.Price,
            InStock = product.InStock,
            Category = (DO.Category)product.Category
        };

        try
        {
            Dal.Product.Update(dProduct);
        }
        catch (Exception)
        {
            throw new BO.NotExistsException();
        }
    }
}
