using System.Threading.Channels;

namespace BO;

public class Order
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
    /// email of customer
    /// </summary>
    public string? CustomerEmail { get; set; }

    /// <summary>
    /// address of customer
    /// </summary>
    public string? CustomerAddress { get; set; }

    /// <summary>
    /// order status (CONFIRMED/SENT/DELIVERED)
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// order date
    /// </summary>
    public DateTime? OrderDate { get; set; }

    /// <summary>
    /// shipping date
    /// </summary>
    public DateTime? ShipDate { get; set; }

    /// <summary>
    /// delivery date
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

    /// <summary>
    /// list of order items
    /// </summary>
    public List<OrderItem?>? Items { get; set; }

    /// <summary>
    /// total price for the customer to pay
    /// </summary>
    public double TotalPrice { get; set; }

    /// <summary>
    /// ToString override method
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        Console.WriteLine($"        ID: {ID}\n" +
                          $"        Customer Name: {CustomerName}\n" +
                          $"        Customer Email: {CustomerEmail}\n" +
                          $"        Customer Address: {CustomerAddress}\n" +
                          $"        Order Status: {Status}\n" +
                          $"        Order Date: {OrderDate}\n" +
                          $"        Ship Date: {ShipDate}\n" +
                          $"        Delivery Date: {DeliveryDate}\n" +
                          $"        Total Price: {TotalPrice}\n " );
        foreach (var item in Items)
        {
            Console.WriteLine(item);
        }
        return "";
    }
}
