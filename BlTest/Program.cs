using BlApi;
using Dal;
using DalApi;
using BO;
using System.Threading.Channels;

namespace BlTest
{
    internal class Program
    {
        private static IBl test = new BlImplementation.Bl();
        static Cart cart = new Cart();

        static void Main(string[] args)
        {
            cart.Items = new List<OrderItem>();

            int? choice;
            do
            {
                Console.WriteLine("\nEnter your choice:\n 0. Exit. \n 1. Cart. \n 2. Product. \n 3. Order");
                choice = int.Parse(Console.ReadLine());
                StartChoose option = (StartChoose)choice;
                switch (option)
                {
                    case StartChoose.EXIT:
                        Console.WriteLine("Have a good day");
                        break;

                    case StartChoose.CART:
                        choiceCart();
                        break;

                    case StartChoose.PRODUCT:
                        choiceProduct();
                        break;
                    case StartChoose.ORDER:
                        choiceOrder();
                        break;

                    default:
                        Console.WriteLine("Error Typing");
                        break;
                }
            } while (choice != 0);
            Console.WriteLine();
        }
        public static void choiceCart()
        {
            Console.WriteLine("Please enter your choice:\n" +
                              " a. add product to the cart.\n" +
                              " b. Updat the quantity of a product in the shopping cart.\n" +
                              " c. Confrim Order.");


            string? choise = Console.ReadLine();

            switch (choise)
            {
                case "a":

                    Console.Write("Enter the id of product you want to add to the cart: ");
                    int idProduct = int.Parse(Console.ReadLine());
                    try
                    {
                        test.Cart.AddPToCart(cart, idProduct);

                    }
                    catch (NotExistException ex) { Console.WriteLine(ex); }

                    break;

                case "b":

                    int idProduct1, newAmount;
                    Console.Write("ID product: ");
                    int.TryParse(Console.ReadLine(), out idProduct1);
                    OrderItem orderitem = new OrderItem();
                    orderitem = cart.Items.Find(x => x.ProductID == idProduct1);
                    Console.WriteLine(orderitem);
                    Console.WriteLine("For update please enter the following details:");
                    Console.Write("Amount of product to update: ");
                    newAmount = int.Parse(Console.ReadLine());
                    test.Cart.UpdateAmount(cart, idProduct1, newAmount);
                    break;

                case "c":
                    Console.Write("Please enter a your name: ");
                    cart.Name = Console.ReadLine();
                    Console.Write("Please enter tour Email: ");
                    cart.Email = Console.ReadLine();
                    Console.Write("Please enter a your addres home: ");
                    cart.Address = Console.ReadLine();
                    test.Cart.ConfirmationOrder(cart);

                    break;


                default:
                    Console.WriteLine("Error Tayping");
                    break;

            }
        }
        public static void choiceProduct()
        {
            Console.WriteLine("Please enter your choice:\n" +
                              " a. Get product list.\n" +
                              " b. Get product for the admin.\n" +
                              " c. Get product for the claint.\n" +
                              " d. Add product (for the admin).\n" +
                              " e. Delete product.\n" +
                              " f. Update product.");

            string? choise = Console.ReadLine();


            switch (choise)
            {
                case "a":

                    List<BO.ProductForList> products = new List<BO.ProductForList>();
                    products = test.Product.GetProductForList().ToList();
                    products.ForEach(product => Console.WriteLine(product));

                    break;

                case "b":
                    Console.Write("Please enter id :");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(test.Product.GetProduct(id));

                    }
                    catch (DO.DoesNotExistException ex) { Console.WriteLine(ex); }

                    break;

                case "c":
                    Console.Write("Please enter ID :");
                    int idc = int.Parse(Console.ReadLine());
                    Console.WriteLine(test.Product.GetProduct(cart, idc));
                    break;

                case "d":
                    Product product = new Product();

                    Console.Write("Please enter a product ID number: ");
                    product.ID = int.Parse(Console.ReadLine());

                    Console.Write("Please enter Product Name:");
                    product.Name = Console.ReadLine();

                    Console.Write("Please enter a product price: ");
                    product.Price = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please select a product category \n 0. Suits.\n 1. Pants. \n 2. Shirts. \n 3. Ties. \n 4. Cufflinks.");
                    int choise2 = int.Parse(Console.ReadLine());
                    product.Category = (Category)choise2;

                    Console.Write("Please enter the quantity of the product in stock: ");
                    product.InStock = int.Parse(Console.ReadLine());

                    try
                    {
                        test.Product.AddProductAdmin(product);
                    }
                    catch (AlreadyExistsException str) { Console.WriteLine(str); }
                    break;

                case "e":
                    Console.Write("Enter Prodcut ID to delete: ");
                    int IdToDelete = int.Parse(Console.ReadLine());
                    try
                    {
                        test.Product.DeleteProductAdmin(IdToDelete);
                        Console.WriteLine("sucsses");
                    }
                    catch (NotExistException ex) { Console.WriteLine(ex); }
                    break;

                case "f":
                    product = new Product();
                    Console.Write("Enter the ID number of the product you want to update: ");
                    int ID2 = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(test.Product.GetProduct(ID2));
                    }
                    catch (NotExistsException ex) { Console.WriteLine(ex); }


                    Console.WriteLine("Please enter the detials product to update:");

                    Console.Write("Please enter a product ID number: ");
                    product.ID = int.Parse(Console.ReadLine());

                    Console.Write("Please enter Product Name Product ID: ");
                    product.Name = Console.ReadLine();

                    Console.Write("Please enter a product price: ");
                    product.Price = int.Parse(Console.ReadLine());

                    Console.WriteLine("Please select a product category \n 0. Shose.\n 1. Shirts. \n 2. Shorts. \n 3. Hoodies. \n 4. Socks.");
                    int choise3 = int.Parse(Console.ReadLine());
                    product.Category = (Category)choise3;

                    Console.Write("Please enter the quantity of the product in stock: ");
                    product.InStock = int.Parse(Console.ReadLine());
                    try
                    {
                        test.Product.UpdateProduct(product);
                    }
                    catch (NotExistException ex) { Console.WriteLine(ex); }

                    break;

                default:
                    Console.WriteLine("Error Tayping");
                    break;

            }
        }
        public static void choiceOrder()
        {
            Console.WriteLine(" a.Get order list.\n" +
                              " b. Get order.\n" +
                              " c. Shipping update.\n" +
                              " d. Supply Update Order.\n" +
                              " e. TrackingOtder.\n");


            string? choise = Console.ReadLine();

            int orderID;
            switch (choise)
            {
                case "a":
                    List<OrderForList> orders = test.Order.GetOrderLists().ToList();
                    orders.ForEach(order => Console.WriteLine(order));
                    break;

                case "b":
                    Console.Write("Please enter order id: ");
                    orderID = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine(test.Order.GetOrder(orderID));
                    }
                    catch (NotExistException ex) { Console.WriteLine(ex); }

                    break;

                case "c":
                    Console.Write("Please enter order id: ");
                    orderID = int.Parse(Console.ReadLine());
                    Console.WriteLine(test.Order.ShippingUpdate(orderID));

                    break;

                case "d":
                    Console.Write("Please enter order id: ");
                    orderID = int.Parse(Console.ReadLine());
                    Console.WriteLine(test.Order.SupplyUpdateOrder(orderID));
                    break;

                case "e":
                    Console.Write("Please enter order id: ");
                    orderID = int.Parse(Console.ReadLine());
                    Console.WriteLine(test.Order.TrackingOtder(orderID));
                    break;

                default:
                    Console.WriteLine("Error Tayping");
                    break;

            }
        }

    }
}
