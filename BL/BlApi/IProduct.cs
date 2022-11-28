using BO;
namespace BlApi;

public interface IProduct
{
    public IEnumerable<BO.ProductForList> GetProductForList();
    public BO.ProductItem GetProductUser(int productId, BO.Cart cart);
    public void DeleteProductAdmin(BO.Product product);

}
