﻿

namespace BO;

public class OrderItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductID { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public double TotalPrice { get; set; }
}
