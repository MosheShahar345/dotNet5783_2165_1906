//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DO;

/// <summary>
/// Represents an order placed by a customer
/// </summary>
public struct Order
{
    /// <summary>
    /// ID of the order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Name of the customer
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// Email of the customer
    /// </summary>
    public string? CustomerEmail { get; set; }

    /// <summary>
    /// Address of the customer
    /// </summary>
    public string? CustomerAddress { get; set; }

    /// <summary>
    /// Date the order was placed
    /// </summary>
    public DateTime? OrderDate { get; set; }

    /// <summary>
    /// Date the order was shipped
    /// </summary>
    public DateTime? ShipDate { get; set; }

    /// <summary>
    /// Date the order was delivered
    /// </summary>
    public DateTime? DeliveryDate { get; set; }

    /// <summary>
    /// Returns a string representation of the order
    /// </summary>
    public override string ToString() => $@"
        Order ID: {ID}
        Customer Name: {CustomerName}
        Customer Email: {CustomerEmail}
        Customer Address: {CustomerAddress}
        Order Date: {OrderDate}
        Ship Date: {ShipDate}
        Delivery Date: {DeliveryDate}
";
}
