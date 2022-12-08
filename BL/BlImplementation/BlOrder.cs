using BO;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlOrder : BlApi.IOrder
{
    private IDal Dal = new DalList();

    /// <summary>
    /// returns BO list with all the orders from DO 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.NotExistsException"></exception>
    public IEnumerable<BO.OrderForList> GetOrderForList()
    {
        IEnumerable<DO.Order?> orders = Dal.Order.GetAll();
        List<BO.OrderForList?> ordersForList = new List<BO.OrderForList?>();

        try
        {
            orders = (List<DO.Order?>)Dal.Order.GetAll();
        }
        catch (DO.NotExistsException e)
        {
            throw new BO.NotExistsException("", e);
        }

		foreach (var item in orders)
		{
			BO.OrderForList temporder = new BO.OrderForList()
			{
				ID = (int)(item?.ID)!,
				CustomerName = item?.CustomerName,
				Status = GetStatus(item)
            };

			IEnumerable<DO.OrderItem?> orderitems = Dal?.OrderItem.GetAll(it => item?.ID == it?.ID)!;

			foreach (var it in orderitems)
			{
				temporder.AmountOfItems += (int)(it?.Amount)!;
				temporder.TotalPrice += (int)(it?.Price)! * (int)(it?.Amount)!;
			}

			ordersForList.Add(temporder);
		}
		return ordersForList!;
    }

    /// <summary>
    /// gets id to find a order in the DO and returns a BO order
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    /// <exception cref="BO.IdIsLessThanZeroException"></exception>
    /// <exception cref="BO.NotExistsException"></exception>
    public BO.Order GetOrder(int orderId)
	{
        if (orderId < 0)
            throw new BO.IdIsLessThanZeroException();

		List<DO.OrderItem?> orderItems = new List<DO.OrderItem?>();
		DO.Order? dOrder = new DO.Order();

        try
        {
            dOrder = Dal.Order.GetById(orderId);
        }
        catch (DO.NotExistsException e)
        {
            throw new BO.NotExistsException("", e);
        }

        orderItems = Dal.OrderItem.GetAll().ToList();

		BO.Order bOrder = new BO.Order()
		{
			ID = dOrder?.ID ?? 0,
			CustomerName = dOrder?.CustomerName,
			CustomerEmail = dOrder?.CustomerEmail,
			CustomerAddress = dOrder?.CustomerAddress,
			Status = GetStatus(dOrder),
			OrderDate = dOrder?.OrderDate,
			ShipDate = dOrder?.ShipDate,
			DeliveryDate = dOrder?.DeliveryDate,
			Items = DoBoConvert(orderItems, dOrder?.ID).Item1!,
		    TotalPrice = DoBoConvert(orderItems, dOrder?.ID).Item2
        };
        return bOrder;
    }

    /// <summary>
    /// gets a DO order to find what is status
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private BO.OrderStatus GetStatus(DO.Order? o)
	{
		if (o?.DeliveryDate > DateTime.MinValue)
			return BO.OrderStatus.Delivered;
		if (o?.ShipDate > DateTime.MinValue)
			return BO.OrderStatus.Sent;
		return BO.OrderStatus.Confirmed;
	}

    ///// <summary>
    ///// gets id to find an order in Dal and returns a BO order
    ///// </summary>
    ///// <param name="orderId"></param>
    ///// <returns></returns>
    ///// <exception cref="BO.IdIsLessThanZeroException"></exception>
    ///// <exception cref="BO.NotExistsException"></exception> 
	private (List<BO.OrderItem>, double) DoBoConvert(List<DO.OrderItem?> orderItem, int? dOrderID)
	{
		List<BO.OrderItem> bOrderItem = new List<BO.OrderItem>();
		double s = 0;

		foreach (var item in orderItem)
		{
			if (item?.OrderID == dOrderID)
			{
				BO.OrderItem orderitem = new BO.OrderItem()
				{
					ID = item?.ID ?? 0,
					Name = Dal?.Product.GetById((int)(item?.ProductID)!)?.Name,
					Price = item?.Price ?? 0,
					ProductID = item?.ProductID ?? 0,
					Amount = item?.Amount ?? 0,
					TotalPrice = (int)(item?.Price)! * (int)(item?.Amount)!
				};
				s += orderitem.TotalPrice;
				bOrderItem.Add(orderitem);
			}
		}
		return (bOrderItem, s);
	}

    /// <summary>
    /// gets an order Updates the ship time and returns updated BO order
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="BO.IdIsLessThanZeroException"></exception>
    /// <exception cref="BO.OrderIsAlreadyDeliveredException"></exception>
    /// <exception cref="BO.OrderIsAlreadyShippedException"></exception>
    /// <exception cref="BO.OrderHasNotShippedException"></exception>
    /// <exception cref="BO.NotExistsException"></exception>
    public BO.Order UpdateOrderShipping(int orderId)
	{
		if (orderId < 0)
			throw new BO.IdIsLessThanZeroException();

		DO.Order dOrder = new DO.Order();
		BO.Order bOrder = new BO.Order();

        try
        {
            dOrder = (DO.Order)Dal.Order.GetById(orderId)!;
            bOrder = GetOrder(orderId);
        }
        catch (DO.NotExistsException e) { throw new BO.NotExistsException("", e); }

        if (dOrder.DeliveryDate != null)
            throw new OrderIsAlreadyDeliveredException();


        if (dOrder.ShipDate != null)
            throw new OrderIsAlreadyShippedException();

        if (dOrder.ShipDate == DateTime.MinValue)
		{
			dOrder.ShipDate = DateTime.Now;
            bOrder.ShipDate = DateTime.Now;
        }

        Dal.Order.Update(dOrder);

		return bOrder;
	}

    /// <summary>
    /// gets a order and Updates the delivery time
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="BO.IdIsLessThanZeroException"></exception>
    /// <exception cref="BO.OrderIsAlreadyDeliveredException"></exception>
    /// <exception cref="BO.OrderHasNotShippedException"></exception>
    /// <exception cref="BO.NotExistsException"></exception>
    public BO.Order UpdateOrderDelivery(int orderId)
	{
        if (orderId < 0)
            throw new BO.IdIsLessThanZeroException();

        DO.Order dOrder = new DO.Order();
        BO.Order bOrder = new BO.Order();

        try
        {
            dOrder = (DO.Order)Dal.Order.GetById(orderId)!;
			bOrder = GetOrder(orderId);
        }
        catch (DO.NotExistsException e) { throw new BO.NotExistsException("", e); }

        if (dOrder.DeliveryDate != null)
            throw new OrderIsAlreadyDeliveredException();

        if (dOrder.ShipDate == null)
            throw new OrderHasNotShippedException();

        if (dOrder.DeliveryDate == DateTime.MinValue)
        {
			dOrder.ShipDate = DateTime.Now;
			bOrder.ShipDate = DateTime.Now;
        }

		Dal.Order.Update(dOrder);

        return bOrder;
    }

    /// <summary>
    /// gets a order id and returns an order with its status
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="BO.InvalidInputException"></exception>
    public BO.OrderTracking TrackOrder(int orderId)
	{
        if (orderId < 0)
            throw new BO.InvalidInputException();

        DO.Order? dOrder = new DO.Order();

        try
        {
            dOrder = Dal.Order.GetById(orderId);
        }
        catch (DO.NotExistsException e) { throw new BO.NotExistsException("", e); }
      
        BO.OrderTracking orderTracking = new BO.OrderTracking();
        orderTracking.ID = orderId;
		orderTracking.Status = GetStatus(dOrder);

        orderTracking.Log = new List<Tuple<DateTime, string>>();

        orderTracking.Log.Add(new Tuple<DateTime, string>(
            (DateTime)dOrder?.OrderDate!, orderTracking?.Status.ToString()!));

		return orderTracking!;
    }
}
