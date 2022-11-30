using BO;
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlProduct : IProduct
{
    private IDal Dal = new DalList();

    public IEnumerable<BO.ProductForList> GetProductForList(int productId)
    {
        if (productId <= 0)
            throw new BO.InvalidInputException();   
       
        List<DO.Product> products = new List<DO.Product>();
        List<BO.ProductForList> productForList = new List<BO.ProductForList>();

        try
        {
            products = (List<DO.Product>)Dal.Product.GetAll();
        }
        catch (Exception) { }

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
    public BO.ProductItem GetProductCustomer(int productId, BO.Cart cart)
    {
        if (productId <= 0)
            throw new BO.InvalidInputException();

        DO.Product dProduct = new DO.Product();
        DO.OrderItem dOrderItem = new DO.OrderItem();

        dProduct = Dal.Product.GetById(productId);
        dOrderItem = Dal.OrderItem.GetById(productId);

        if (dProduct.Name == null)
            throw new BO.NotExsitsException();

        BO.ProductItem productItem = new BO.ProductItem()
        {
            ID = productId,
            Name = dProduct.Name,
            Price = dProduct.Price,
            Category = (Category)dProduct.Category,
            InStock = dProduct.InStock > 0 ? true : false
        };

        return productItem;
    }

    public void AddProductAdmin(BO.Product product)
    {
        if(product.ID <= 0 || product.Name == null || product.Price <= 0 || product.InStock < 0)
            throw new BO.NotExsitsException();
        DO.Product dProduct = new DO.Product();
    }
}
