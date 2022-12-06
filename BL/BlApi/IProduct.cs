using BO;
namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// gets list of products from DO to BO
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.ProductForList> GetProductForList();

    /// <summary>
    /// for admin gets product by id from DO to BO
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    public Product GetProductForAdmin(int productId);

    /// <summary>
    /// for customer gets product by id and product from the cart from DO to BO
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    public ProductItem GetProductForCustomer(int productId, BO.Cart cart);

    /// <summary>
    /// adds product to DO
    /// </summary>
    /// <param name="product"></param>
    public void AddProductAdmin(BO.Product product);

    /// <summary>
    /// deletes product from DO
    /// </summary>
    /// <param name="productId"></param>
    public void DeleteProductAdmin(int productId);

    /// <summary>
    /// updates product from DO
    /// </summary>
    /// <param name="product"></param>
    public void UpdateProductAdmin(BO.Product product);
}
