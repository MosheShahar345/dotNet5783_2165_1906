using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal sealed class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    public IOrder Order { get; } 
    public IOrderItem OrderItem { get; } 
    public IProduct Product { get; } 
    private DalXml()
    {
        Order = new DalOrder();
        OrderItem = new DalOrderItem();
        Product = new DalProduct();
    }
}
