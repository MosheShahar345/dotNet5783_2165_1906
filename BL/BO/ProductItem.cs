namespace BO;

public class ProductItem
{
    /// <summary>
    /// product id
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// ma,e of product
    /// </summary>
    public string? Name{ get; set; }

    /// <summary>
    /// category of product
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// price of product
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// amount in cart of customer
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// does exist in stock
    /// </summary>
    public bool InStock { get; set; }

    /// <summary>
    /// ToString override method
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
        Product ID: {ID}
        Name: {Name} 
        Category: {Category}
    	Price: {Price}
        Amount: {Amount}
    	Amount in stock: {InStock}
";
}
