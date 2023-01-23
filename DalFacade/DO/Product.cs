//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DO;

/// <summary>
///  geter's and seter's for all variables in  product
/// </summary>
public struct Product
{
    /// <summary>
    /// The unique identifier for the product.
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// The name of the product.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// The price of the product.
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// The category of the product.
    /// </summary>
    public Category? Category { get; set; }
    /// <summary>
    /// The number of items in stock.
    /// </summary>
    public int InStock { get; set; }
    /// <summary>
    /// A string representation of the product.
    /// </summary>
    public override string ToString() => $@"
        Product ID: {ID}
        Name: {Name}
        Category: {Category}
        Price: {Price}
        Amount in stock: {InStock}
";
}
