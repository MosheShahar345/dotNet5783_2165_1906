namespace BO;

public class Product
{
    /// <summary>
    /// product id
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// name of product
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// price of product
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// category of product
    /// </summary>
    public Category? Category { get; set; }

    /// <summary>
    /// how many of a product in stock
    /// </summary>
    public int InStock { get; set; }

    /// <summary>
    /// ToString override method
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
        Product ID: {ID}
        Name: {Name} 
        Category: {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
";
}
