using BO;
namespace BlApi;

public interface ICart
{
    public Cart AddPToCart(Cart cart, int productId);
    public Cart UpdateAmount(Cart cart, int productId, int Namount);
    public void ConfirmationOrder(Cart cart);
}