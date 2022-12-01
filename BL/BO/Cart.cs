namespace BO;

public class Cart
{
    public string? Name { get; set;}
    public string? Email { get; set;}
    public string? Address { get; set;}
    public List<OrderItem>? Items { get; set;}
    public double TotalPrice { get; set;}
}
