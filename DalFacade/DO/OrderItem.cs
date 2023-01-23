//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DO;

/// <summary>
/// This struct represents an OrderItem
/// </summary>
public struct OrderItem
{
    /// <summary>
    /// ID of the OrderItem
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// ID of the Product
    /// </summary>
    public int ProductID { get; set; }

    /// <summary>
    /// ID of the Order
    /// </summary>
    public int OrderID { get; set; }

    /// <summary>
    /// Price of the OrderItem
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Amount of the product
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Overridden method that returns a string representation of the OrderItem
    /// </summary>
    public override string ToString() => $@"
        Order item ID: {ID}
        Product ID: {ProductID}
        Order ID: {OrderID}
    	Price: {Price}
    	Amount: {Amount}
";
}
