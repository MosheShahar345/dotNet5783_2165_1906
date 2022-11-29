namespace BO;

public class OrderForList
{
    public int ID { get; set; }
    public string? CoustomerName { get; set; }
    public OrderStatus Status { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }
}
