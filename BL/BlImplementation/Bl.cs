using BlApi;
namespace BlImplementation;

public sealed class Bl : IBl
{
    /// <summary>
    /// property for cart
    /// </summary>
    public ICart Cart => new BlCart();

    /// <summary>
    /// property for order
    /// </summary>
    public IOrder Order => new BlOrder();

    /// <summary>
    /// property for product
    /// </summary>
    public IProduct Product => new BlProduct();
}

