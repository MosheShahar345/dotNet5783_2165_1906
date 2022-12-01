using BO;
namespace BlApi;

public interface IProduct
{
    public IEnumerable<BO.ProductForList> GetProductForList(int productId);
    public ProductItem GetProductCustomer(int productId, BO.Cart cart);
    public void AddProductAdmin(BO.Product product);
    public void DeleteProductAdmin(int productId);
    public void UpdateProductAdmin(BO.Product product);

}
