//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DO;

namespace Dal;

internal struct DataSource
{
    public static readonly int random = 0;
    internal static Product[] product = new Product[50];
    internal static Order[] order = new Order[100];
    internal static OrderItem[] orderItem = new OrderItem[200];

    static readonly int sizeOfProduct = 0;
    static readonly int sizeOfOrder = 0;
    static readonly int sizeOfOrderItem = 0;

    private static void AddProduct(Product[] arr, int size);
    private static void AddOrder(Order[] arr, int sizr);
    private static void AddOrderItem(OrderItem[] arr, int size);
}