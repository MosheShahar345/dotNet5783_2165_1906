namespace BO;

public class OrderTracking
{
    /// <summary>
    /// id of order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// status of order
    /// </summary>
    public OrderStatus Status { get; set; }

    /// <summary>
    /// order progress
    /// </summary>
    public List<Tuple<DateTime, string>> Log = new List<Tuple<DateTime, string>>();
}
