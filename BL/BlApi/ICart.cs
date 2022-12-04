using BO;
namespace BlApi;

public interface ICart
{
    /// <summary>
    /// add product to the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    public Cart AddPToCart(Cart cart, int productId);

    /// <summary>
    /// update amount in the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <param name="Namount"></param>
    /// <returns></returns>
    public Cart UpdateAmount(Cart cart, int productId, int Namount);

    /// <summary>
    /// confirmation and completing the order process 
    /// </summary>
    /// <param name="cart"></param>
    public void ConfirmationOrder(Cart cart);
}