using System.ComponentModel.DataAnnotations;
using BO;
using Dal;
using DalApi;
using DO;

namespace BlImplementation;

internal class BlCart : BlApi.ICart
{
    private IDal Dal = new DalList(); // to access DO entities

    public BO.Cart AddPToCart(BO.Cart cart, int productId)
    {
        if (productId < 0)
            throw new IdIsLessThanZeroException("ID is less than zero");

        DO.Product product;
        BO.OrderItem orderItem;
        //List<DO.OrderItem> orderItems = new();

        if (cart.Items == null)
        {
            cart.Items = new();
        }

        orderItem = cart.Items.Find(it => it.ProductID == productId);

        try
        {
            product = Dal.Product.GetById(productId);
        }
        catch (BO.NotExistsException e)
        {
            throw new BO.NotExistsException("", e);
        }

        if (orderItem != null)
        {
            if (product.InStock > orderItem.Amount + 1)
            {
                orderItem.Amount++;
                orderItem.TotalPrice += orderItem.Price;
                cart.TotalPrice += orderItem.Price;
            }
        }
        else if (product.InStock > 0)
        {
            cart.Items.Add(new BO.OrderItem
            {
                ID = 0,
                Name = product.Name,
                ProductID = product.ID,
                Price = product.Price,
                Amount = 1,
                TotalPrice = product.Price
            });
        }
        else
        {
            throw new BO.NotEnoughInStockException(
                $"product with name: {product.Name} not enough in stock");
        }
        return cart;

        //foreach (var item in cart.Items)
        //{
        //    if (productId == item.ProductID)
        //    {
        //        if (product.InStock > item.Amount + 1)
        //        {
        //            item.Amount++;
        //            item.TotalPrice += item.Price;
        //            cart.TotalPrice += item.Price;

        //            return cart;
        //        }
        //        else
        //        {
        //            throw new BO.NotEnoughInStockException(product.Name);
        //        }
        //    }
        //}

        //if (product.InStock > 0)
        //{
        //    cart.Items.Add(new BO.OrderItem
        //    {   
        //        ID = 0,
        //        Name = product.Name,
        //        ProductID = product.ID,
        //        Price = product.Price,
        //        Amount = 1,
        //        TotalPrice = product.Price
        //    });

        //    return cart;
        //}

        //throw new BO.NotEnoughInStockException(
        //    $"product with name: {product.Name} not enough in stock");
    }

    public BO.Cart UpdateAmount(BO.Cart cart, int productId, int nAmount)
    {
        if (productId < 0)
            throw new IdIsLessThanZeroException("ID is less than zero");

        if (nAmount < 0)
            throw new InvalidAmountException("amount is less than zero");

        DO.Product product = new DO.Product();
        BO.OrderItem orderItem;

        try
        {
            product = Dal.Product.GetById(productId);
        }
        catch (BO.NotExistsException e)
        {
            throw new BO.NotExistsException("", e);
        }

        orderItem = cart.Items?.Find(it => productId == it.ProductID) ??
                    throw new BO.NotExistsException("not exists");

        if (nAmount == orderItem.Amount) { return cart; }

        if (nAmount > orderItem.Amount)
        {
            if (product.InStock >= orderItem.Amount + nAmount)
            {
                orderItem.Amount += nAmount;
                orderItem.TotalPrice += orderItem.Price * nAmount;
                cart.TotalPrice += orderItem.TotalPrice;
            }
            else
            {
                throw new NotEnoughInStockException("not enough in stock");
            }
        }

        else if (nAmount != 0 && nAmount < orderItem.Amount)
        {
            orderItem.Amount -= nAmount;
            orderItem.TotalPrice -= orderItem.Price * nAmount;
            cart.TotalPrice -= orderItem.TotalPrice;
        }

        else
        {
            cart.TotalPrice -= orderItem.TotalPrice;
            cart.Items.Remove(orderItem);
        }

        //foreach (var item in cart.Items)
        //{
        //    if (item.ProductID == productId)
        //    {
        //        if (nAmount == item.Amount) { return cart; }

        //        if (nAmount > item.Amount)
        //        {
        //            if (product.InStock >= item.Amount + nAmount)
        //            {
        //                item.Amount += nAmount;
        //                item.TotalPrice += item.Price * nAmount;
        //                cart.TotalPrice += item.TotalPrice;
        //            }
        //            else
        //            {
        //                throw new NotEnoughInStockException("not enough in stock");
        //            }
        //        }
        //        else if (nAmount != 0 && nAmount < item.Amount)
        //        {
        //            item.Amount -= nAmount;
        //            item.TotalPrice -= item.Price * nAmount;
        //            cart.TotalPrice -= item.TotalPrice;
        //        }
        //        else
        //        {
        //            cart.TotalPrice -= item.TotalPrice;
        //            cart.Items.Remove(item);
        //            return cart;
        //        }
        //    }
        //}

        return cart;
    }
    public void ConfirmationOrder(BO.Cart cart)
    {
        if (cart.Items == null || cart.Name == null || cart.Address == null)
            throw new BO.NotExistsException();

        if (!new EmailAddressAttribute().IsValid(cart.Email))
            throw new BO.InvalidEmailException();

        DO.Order dOrder = new DO.Order()
        {
            CustomerName = cart.Name,
            CustomerAddress = cart.Address,
            CustomerEmail = cart.Email,
            OrderDate = DateTime.Now,
            ShipDate = DateTime.MinValue,
            DeliveryDate = DateTime.MinValue
        };

        int OrderId;

        try
        {
            OrderId = Dal.Order.Add(dOrder);
        }
        catch (Exception)
        {
            throw new BO.AlreadyExistsException();
        }

        foreach (var item in cart.Items)
        {
            DO.OrderItem dOrderItem = new DO.OrderItem()
            {
                ID = item.ID,
                ProductID = item.ProductID,
                OrderID = OrderId,
                Price = item.Price,
                Amount = item.Amount
            };

            Dal.OrderItem.Add(dOrderItem);
        }

        DO.Product product = new DO.Product();

        foreach (var item in cart.Items)
        {
            product = Dal.Product.GetById(item.ProductID);
            product.InStock -= item.Amount;
            Dal.Product.Add(product);
        }
    }
}