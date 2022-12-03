namespace BO;

public class OrderTracking
{
    /// <summary>
    /// 
    /// </summary>
    public int ID { get; set; }
    public OrderStatus Status { get; set; }

    public List<Tuple<DateTime, string>> Log = new List<Tuple<DateTime, string>>();
}
