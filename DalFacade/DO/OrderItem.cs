//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DO;

/// <summary>
///  geter's and seter's for all variables in order item
/// </summary>
public struct OrderItem
{
    public int OrderItemID { get; set; }
    public int ProductID { get; set; }
    public int OrderID { get; set; } 
    public double Price { get; set; }
    public int Amount { get; set; }
    public override string ToString() => $@"
        Product ID = {ProductID}
        Order ID = {OrderID}
    	Price: {Price}
    	Amount: {Amount}
";
}
