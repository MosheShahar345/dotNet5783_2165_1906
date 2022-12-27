using BlApi;
namespace BlImplementation;

internal sealed class Bl : IBl
{
    /// <summary>
    /// property for cart
    /// </summary>
    public ICart Cart { get; } = new BlCart();

    /// <summary>
    /// property for order
    /// </summary>
    public IOrder Order { get; } = new BlOrder();

    /// <summary>
    /// property for product
    /// </summary>
    public IProduct Product { get; } = new BlProduct();
}

