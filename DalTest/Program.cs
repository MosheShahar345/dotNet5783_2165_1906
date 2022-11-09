using System.Threading.Channels;
using DO;

namespace Dal
{
    class Program
    {
        private static DalProduct dal_product = new DalProduct();
        private static DalOrder dal_order = new DalOrder();
        private static DalOrderItem dal_order_item = new DalOrderItem();

        enum ITEMS
        {
            PRODUCT = 1,
            ORDER,
            ORDERITEM
        }

        enum OPTIONS
        {
            ADD = 1,
            DELETE,
            CHECK,
            UPDATE,
            PRINT,
        }
        static int Main(string[] args)
        {
            int choice = int.Parse(Console.ReadLine());
            PrintStartMenu();

            do
            {
                switch (choice)
                {
                    case (int)ITEMS.PRODUCT:
                        MenuOfPruduct();
                        break;

                    case (int)ITEMS.ORDER:
                        MenuOfOrder();
                        break;

                    case (int)ITEMS.ORDERITEM:
                        MenuOfOrderItem();
                        break;

                    default:
                        choice = 0;
                        break;
                }
            } while (choice != 0);

            return 0;
        }

        public static void MenuOfOrderItem()
        {
            
        }

        public static void MenuOfOrder()
        {
            
        }

        public static void MenuOfPruduct()
        {
            Console.WriteLine("add new product - 1 \n" +
                              "delete product - 2 \n" +
                              "check product - 3 \n" +
                              "update product - 4 \n" +
                              "print ptoduct - 5 \n");

            int choice = int.Parse(Console.ReadLine());
            Product product = new Product();
            int ID;
            switch (choice)
            {
                case (int)OPTIONS.ADD:
                    Console.WriteLine("enter: name, category, price, in-stock");
                    product.Name = Console.ReadLine();
                    product.Category = (Enums.Category)Convert.ToInt32(Console.ReadLine());
                    product.Price = double.Parse(Console.ReadLine());
                    product.InStock = int.Parse(Console.ReadLine());
                    try
                    {
                        DalProduct.AddProduct(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.DELETE:
                    Console.WriteLine("enter product ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        DalProduct.DeleteProduct(ID);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.CHECK:
                    Console.WriteLine("enter product ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        product = DalProduct.Get(ID);
                        Console.WriteLine(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.UPDATE:
                    Console.WriteLine("enter product ID");
                    product.ID = int.Parse(Console.ReadLine());
                    try
                    {
                        product = DalProduct.Get(product.ID);
                        Console.WriteLine(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                        break;
                    }
                    Console.WriteLine("enter: name, category, price, in-stock");
                    product.Name = Console.ReadLine();
                    product.Category = (Enums.Category)Convert.ToInt32(Console.ReadLine());
                    product.Price = double.Parse(Console.ReadLine());
                    product.InStock = int.Parse(Console.ReadLine());
                    DalProduct.AddProduct(product);
                    break;

                case (int)OPTIONS.PRINT:
                    Product[] products = DalProduct.Get();
                    foreach (var item in products)
                    {
                        Console.WriteLine(item);
                    }
                    break;

                default:
                    Console.WriteLine("try harder");
                    break;
            }
        }
        public static void PrintStartMenu()
        {
            Console.Write("click 0 to exit \n" +
                          "click 1 to view product \n" +
                          "click 2 to view order \n" +
                          "click 3 to view item \n");
        }
    }
}
