namespace BlApi;

public interface IBl   
{
    /// <summary>
    /// main Product
    /// </summary>
    public IProduct Product { get; }

    /// <summary>
    /// main Cart
    /// </summary>
    public ICart Cart { get; }

    /// <summary>
    /// main Order
    /// </summary>
    public IOrder Order { get; }
}
