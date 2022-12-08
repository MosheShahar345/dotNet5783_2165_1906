namespace BO;

public class OrderTracking
{
    /// <summary>
    /// id of order
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// status of order
    /// </summary>
    public OrderStatus? Status { get; set; }

    /// <summary>
    /// order progress
    /// </summary>
    public List<Tuple<DateTime, string>>? Log { get; set; }

    /// <summary>
    /// ToString override method
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        Console.WriteLine();
        Console.WriteLine($"\t\tID: {ID}" +
                          $"\n\t\tStatus: {Status}");

        foreach (var item in Log!)
        {
            Console.WriteLine("\t\t{0} \n \t\t{1}", item.Item1, item.Item2);
        }

        return "";
    }
}
