using BlApi;
using Dal;
using DalApi;
using System.ComponentModel.DataAnnotations;
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

            }
        }

    }
