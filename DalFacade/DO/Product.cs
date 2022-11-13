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
    public Product(int iD, string name, double price, Enums.Category category, int inStock)
    {
        ID = iD;
        Name = name;
        Price = price;
        Category = category;
        InStock = inStock;
    }

    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Enums.Category Category { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"
        Product ID = {ID}: {Name}, 
        Category - {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
";
}
