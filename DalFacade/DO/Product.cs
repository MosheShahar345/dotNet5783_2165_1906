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
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"
        Product ID = {ID}
        Name: {Name}, 
        Category - {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
";
}
