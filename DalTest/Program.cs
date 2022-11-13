using System;
using System.Diagnostics;
using Dal;
using DO;


namespace Dal
{
   public class Program
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
            PRINT
        }
        static int Main(string[] args)
        {
            PrintStartMenu();
            int choice;
            bool status = int.TryParse(Console.ReadLine(), out choice);

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

                int.TryParse(Console.ReadLine(), out choice);

            } while (choice != 0);

            return 0;
        }

        public static void MenuOfOrderItem()
        {
            Console.WriteLine("add new order item - 1 \n" +
                             "delete order item - 2 \n" +
                             "check order item - 3 \n" +
                             "update order item - 4 \n" +
                             "print order item - 5 \n");
            int choice;
            int orderItemID;
            int productID;
            int orderID;
            double price;
            int amount;

            bool status = int.TryParse(Console.ReadLine(), out choice);
            OrderItem orderitem; 
            int ID;
            switch (choice)
            {
                case (int)OPTIONS.ADD:
                    Console.WriteLine("enter: product ID, order ID, price, amount ");

                    orderItemID = int.Parse(Console.ReadLine());
                    productID = int.Parse(Console.ReadLine());
                    orderID = int.Parse(Console.ReadLine());
                    price = double.Parse(Console.ReadLine());
                    amount = int.Parse(Console.ReadLine());

                    try
                    {
                        orderitem = new OrderItem
                        {
                            OrderItemID = orderItemID,
                            ProductID = productID,
                            OrderID = orderID,
                            Price = price,
                            Amount = amount
                        };
                        dal_order_item.addOrderItem(orderitem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.DELETE:
                    Console.WriteLine("enter order item ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        dal_order_item.deleteOrderItem(ID);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.CHECK:
                    Console.WriteLine("enter order item ID:");
                    ID = int.Parse(Console.ReadLine());
                    try
                    {
                        orderitem = dal_order_item.get(ID);
                        Console.WriteLine(orderitem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.UPDATE:
                    Console.WriteLine("enter: product ID, order ID, price, amount ");

                    orderItemID = int.Parse(Console.ReadLine());
                    productID = int.Parse(Console.ReadLine());
                    orderID = int.Parse(Console.ReadLine());
                    price = double.Parse(Console.ReadLine());
                    amount = int.Parse(Console.ReadLine());

                    try
                    {
                        orderitem = new OrderItem
                        {
                            OrderItemID = orderItemID,
                            ProductID = productID,
                            OrderID = orderID,
                            Price = price,
                            Amount = amount
                        };
                        dal_order_item.update(orderitem);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.PRINT:
                    Product[] products = dal_product.get();
                    foreach (var item in products)
                    {
                        Console.WriteLine(item);
                    }
                    break;

                default:
                    Console.WriteLine("try harder, i did");
                    break;
            }
        }

        public static void MenuOfOrder()
        {
            Console.WriteLine("add new order - 1 \n" +
                              "delete order - 2 \n" +
                              "check order - 3 \n" +
                              "update order - 4 \n" +
                              "print order - 5 \n");
            int orderID;
            string customerName;
            string customerEmail;
            string customerAdress;

            int choice;
            bool status = int.TryParse(Console.ReadLine(), out choice);

            Order order;
            switch (choice)
            {
                case (int)OPTIONS.ADD:
                    Console.WriteLine("enter: customerName, customerEmail, customerAdress");

                    orderID = int.Parse(Console.ReadLine());
                    customerName = Console.ReadLine();
                    customerEmail = Console.ReadLine();
                    customerAdress = Console.ReadLine();

                    try
                    {
                        order = new Order()
                        {
                            OrderID = orderID,
                            CustomerName = customerName,
                            CustomerEmail = customerEmail,
                            CustomerAdress = customerAdress
                        };
                        dal_order.addOrder(order);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.DELETE:
                    Console.WriteLine("enter order ID:");
                    orderID = int.Parse(Console.ReadLine());
                    try
                    {
                        dal_order.deleteOrder(orderID);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.CHECK:
                    Console.WriteLine("enter order ID:");
                    orderID = int.Parse(Console.ReadLine());
                    try
                    {
                        order = dal_order.get(orderID);
                        Console.WriteLine(order);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.UPDATE:
                    Console.WriteLine("enter order ID");
                    orderID = int.Parse(Console.ReadLine());
                    try
                    {
                        order = dal_order.get(orderID);
                        dal_order.update(order);
                        Console.WriteLine(order);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                        break;
                    }
                    Console.WriteLine("enter:  name, email, adress");
                    order = new Order()
                    {
                        CustomerName = Console.ReadLine(),
                        CustomerEmail = Console.ReadLine(),
                        CustomerAdress = Console.ReadLine()
                    };
                    dal_order.addOrder(order);
                    break;

                case (int)OPTIONS.PRINT:
                    Order[] orders = dal_order.get();
                    foreach (var item in orders)
                    {
                        Console.WriteLine(item);
                    }
                    break;

                default:
                    Console.WriteLine("try harder");
                    break;
            }

        }

        public static void MenuOfPruduct()
        {
            Console.WriteLine("add new product - 1 \n" +
                              "delete product - 2 \n" +
                              "check product - 3 \n" +
                              "update product - 4 \n" +
                              "print ptoduct - 5 \n");

            int productID;
            string name;
            double price;
            Enums.Category category;
            int inStock;

            int choice;
            bool status = int.TryParse(Console.ReadLine(), out choice);
            Product product;
            switch (choice)
            {
                case (int)OPTIONS.ADD:
                    Console.WriteLine("enter: name, email, adress");

                    productID = int.Parse(Console.ReadLine());
                    name = Console.ReadLine();
                    price = double.Parse(Console.ReadLine());
                    category = (Enums.Category)Convert.ToInt32(Console.ReadLine());
                    inStock = int.Parse(Console.ReadLine());

                    try
                    {
                        product = new Product()
                        {
                            ID = productID,
                            Name = name,
                            Category = category,
                            Price = price,
                            InStock = inStock
                        };
                        dal_product.addProduct(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.DELETE:
                    Console.WriteLine("enter product ID:");
                    productID = int.Parse(Console.ReadLine());
                    try
                    {
                        dal_product.deleteProduct(productID);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.CHECK:
                    Console.WriteLine("enter product ID:");
                    productID = int.Parse(Console.ReadLine());
                    try
                    {
                        product = dal_product.get(productID);
                        Console.WriteLine(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                    }
                    break;

                case (int)OPTIONS.UPDATE:
                    Console.WriteLine("enter product ID");
                    productID = int.Parse(Console.ReadLine());
                    try
                    {
                        product = dal_product.get(productID);
                        Console.WriteLine(product);
                    }
                    catch (Exception str)
                    {
                        Console.WriteLine(str);
                        break;
                    }
                    Console.WriteLine("enter: name, category, price, in-stock");
                    product = new Product()
                    {
                        Name = Console.ReadLine(),
                        Category = (Enums.Category)Convert.ToInt32(Console.ReadLine()),
                        Price = double.Parse(Console.ReadLine()),
                        InStock = int.Parse(Console.ReadLine())

                    };
                    dal_product.addProduct(product);
                    break;

                case (int)OPTIONS.PRINT:
                    Product[] products = dal_product.get();
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
            Console.WriteLine("click 0 to exit \n" +
                          "click 1 to view product \n" +
                          "click 2 to view order \n" +
                          "click 3 to view order item \n");
        }
    }
}
