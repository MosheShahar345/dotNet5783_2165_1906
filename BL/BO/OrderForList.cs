
namespace BO;

public class OrderForList
{
    public int Id { get; set; }
    public string CoustomerName { get; set; }
    public OrderStatus Status { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }
}
