namespace BO;

public class Cart
{
    /// <summary>
    /// name of customer
    /// </summary>
    public string? Name { get; set;}

    /// <summary>
    /// email of customer
    /// </summary>
    public string? Email { get; set;}

    /// <summary>
    /// address of customer
    /// </summary>
    public string? Address { get; set;}

    /// <summary>
    /// items in cart
    /// </summary>
    public List<OrderItem>? Items { get; set;}

    /// <summary>
    /// total price for the customer to pay
    /// </summary>
    public double TotalPrice { get; set;}

    /// <summary>
    /// ToString override method
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
        Name: {Name}
    	Email: {Email}
        Address: {Address}
        Items: {Tools.ToStringProperty(Items)}
        Total Price: {TotalPrice}
";
}
