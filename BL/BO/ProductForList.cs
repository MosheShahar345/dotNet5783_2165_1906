namespace BO;

public class ProductForList
{
    /// <summary>
    /// id of order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// name of product
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// category of product
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// price of product
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// ToString override method
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $@"
        Product ID: {ID}
        Name: {Name}
        Category: {Category}
    	Price: {Price}
";
}
