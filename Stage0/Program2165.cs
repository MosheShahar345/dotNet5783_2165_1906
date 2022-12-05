namespace Stage0;
partial class Program
{
    static void Main(string[] args)
    {
        Welcome2165();
        Welcome1906();
        Console.ReadKey();
    }

    static partial void Welcome1906();
    private static void Welcome2165()
    {
        Console.Write("Enter your name: ");
        string? userName = Console.ReadLine();
        Console.WriteLine("{0}, welcome to my first console application", userName);
    }
}