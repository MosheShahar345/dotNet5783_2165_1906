using DalApi;

namespace BlImplementation;

internal class BlOrder : BlApi.IOrder
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    /// <summary>
    /// returns BO list with all the orders from DO 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.NotExistsException"></exception>
    public IEnumerable<BO.OrderForList> GetOrderForList()
    {
        IEnumerable<DO.Order?> orders = dal?.Order.GetAll().ToList()!;
        //List<BO.OrderForList?> ordersForList = new List<BO.OrderForList?>();

        try
        {
            orders = dal?.Order.GetAll().ToList()!;
        }
        catch (DO.NotExistsException e)
        {
            throw new BO.NotExistsException("", e);
        }

        var ordersForList = orders.Select(item =>
        {
            BO.OrderForList tempOrder = new BO.OrderForList()
            {
                ID = (int)(item?.ID)!,
                CustomerName = item?.CustomerName,
                Status = GetStatus(item)
            };

            IEnumerable<DO.OrderItem?> orderItems = dal?.OrderItem.GetAll(it => item?.ID == it?.ID)!;

            tempOrder.AmountOfItems = orderItems.Sum(it => (int)(it?.Amount)!);
            tempOrder.TotalPrice = orderItems.Sum(it => (int)(it?.Price)! * (int)(it?.Amount)!);

            return tempOrder;

        }).ToList();

        return ordersForList;
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
            dOrder = dal?.Order.GetEntity(it => it?.ID == orderId);
        }
        catch (DO.NotExistsException e)
        {
            throw new BO.NotExistsException("", e);
        }

        orderItems = dal?.OrderItem.GetAll().ToList()!;

		BO.Order bOrder = new BO.Order()
		{
			ID = (int)(dOrder?.ID)!,
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
        List<BO.OrderItem> bOrderItem = orderItem
            .Where(item => item?.OrderID == dOrderID)
            .Select(item => new BO.OrderItem
            {
                ID = (int)(item?.ID)!,
                Name = dal?.Product.GetEntity(it => it?.ID == item?.ProductID)?.Name,
                Price = (double)(item?.Price)!,
                ProductID = (int)(item?.ProductID)!,
                Amount = (int)(item?.Amount)!,
                TotalPrice = (int)(item?.Price)! * (int)(item?.Amount)!
            })
            .ToList();

        double s = bOrderItem.Sum(item => item.TotalPrice);

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
            dOrder = (DO.Order)dal?.Order.GetEntity(it => it?.ID == orderId)!;
            bOrder = GetOrder(orderId);
        }
        catch (DO.NotExistsException e) { throw new BO.NotExistsException("", e); }

        if (dOrder.DeliveryDate != null)
            throw new BO.OrderIsAlreadyDeliveredException();


        if (dOrder.ShipDate != null)
            throw new BO.OrderIsAlreadyShippedException();

        if (dOrder.ShipDate == DateTime.MinValue)
		{
			dOrder.ShipDate = DateTime.Now;
            bOrder.ShipDate = DateTime.Now;
        }

        dal?.Order.Update(dOrder);

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
            dOrder = (DO.Order)dal?.Order.GetEntity(it => it?.ID == orderId)!;
			bOrder = GetOrder(orderId);
        }
        catch (DO.NotExistsException e) { throw new BO.NotExistsException("", e); }

        if (dOrder.DeliveryDate != null)
            throw new BO.OrderIsAlreadyDeliveredException();

        if (dOrder.ShipDate == null)
            throw new BO.OrderHasNotShippedException();

        if (dOrder.DeliveryDate == null)
        {
			dOrder.ShipDate = DateTime.Now;
			bOrder.ShipDate = DateTime.Now;
        }

		dal?.Order.Update(dOrder);

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
            dOrder = dal?.Order.GetEntity(it => it?.ID == orderId);
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
