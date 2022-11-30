using BlApi;
using BO;
using Dal;
using DalApi;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using static BO.NotEnoughRoomInStockException;

namespace BlImplementation;

internal class BlCart : ICart
{
    private IDal Dal = new DalList();

    public BO.Cart AddToCart(BO.Cart cart, int productId)
    {
        DO.Product product1 = new DO.Product();
        DO.OrderItem orderitem1 = new DO.OrderItem();

        try
        {
            product1 = Dal.Product.GetById(productId);
            orderitem1 = Dal.OrderItem.GetById(productId);
        }
        catch (Exception) { }
        foreach (var item in cart.Items)
        {
            if (productId == item.ProductID)
            {
                if (product1.InStock > item.Amount)
                {
                    item.Amount++;
                    item.TotalPrice += item.Price;
                    cart.TotalPrice += item.Price;
                    return cart;
                }
            }
        }
        if (product1.InStock > 0)
        {
            BO.OrderItem orderitemBO = new BO.OrderItem
            {
                ID = orderitem1.OrderID,
                Name = product1.Name,
                ProductID = orderitem1.ProductID,
                Price = orderitem1.Price,
                Amount = 1,
                TotalPrice = orderitem1.Price
            };
            cart.Items.Add(orderitemBO);
        }
        return cart;
    }
    public BO.Cart UpdateAmount(BO.Cart cart, int productId, int Namount)
    {
        DO.Product product1 = new DO.Product();
        try
        {
            product1 = Dal.Product.GetById(productId);
        }
        catch (Exception) { }

        foreach (var item in cart.Items)
        {
            if (productId == item.ProductID)
            {
                if (Namount > item.Amount)
                {
                    if (product1.InStock >= item.Amount + Namount)
                    {
                        item.Amount += Namount;
                        item.TotalPrice += item.Price;
                        cart.TotalPrice += item.Price;
                    }
                }
                else if (Namount != 0 && Namount < item.Amount)
                {
                    item.Amount -= Namount;
                    item.TotalPrice -= item.Price;
                    cart.TotalPrice -= item.Price;
                }
                else
                {
                    cart.Items.Remove(item);
                    cart.TotalPrice-= item.TotalPrice; 
                }
            }
        }
        return cart;
    }
    public BO.Cart ConfirmationOrder(BO.Cart cart)
    {
        foreach (var item in cart.Items)
        {
          
        }

    }
    
}
