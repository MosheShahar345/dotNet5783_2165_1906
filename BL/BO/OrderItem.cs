using DO;

namespace BO;

public class OrderItem
{
    /// <summary>
    /// order item id
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// name of product
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// product id
    /// </summary>
    public int ProductID { get; set; }

    /// <summary>
    /// price of product
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// amount of items of a product in cart
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// total price for the customer to pay
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// ToString override method
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
        Order item ID: {ID}
        Name: {Name}
        Product ID: {ProductID}
    	Price: {Price}
    	Amount: {Amount}
        Total Price: {TotalPrice}
";
}
