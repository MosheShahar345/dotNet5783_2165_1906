

namespace BO;

public class OrderTracking
{
    public int Id { get; set; }
    public OrderStatus Status { get; set; }

    public List<Tuple<DateTime, string>> var = new List<Tuple<DateTime, string>>();
}
