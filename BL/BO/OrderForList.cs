namespace BO;

public class OrderForList
{
    /// <summary>
    /// id of order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// name of customer
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// order status (CONFIRMED/SENT/DELIVERED)
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// amount of items in order
    /// </summary>
    public int AmountOfItems { get; set; }

    /// <summary>
    /// total price for the customer to pay
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// ToString override method
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
        ID: {ID}
        Customer Name: {CustomerName}
        Order Status: {Status}
    	Amount Of Items: {AmountOfItems}
        Total Price: {TotalPrice}
";
}
