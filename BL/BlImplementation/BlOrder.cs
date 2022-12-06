﻿using BO;
using Dal;
using DalApi;

namespace BlImplementation;

internal class BlOrder : BlApi.IOrder
{
    private IDal Dal = new DalList();
    public IEnumerable<BO.OrderForList> GetOrderForList()
    {
        List<DO.Order> orders = new List<DO.Order>();
        List<BO.OrderForList> ordersForList = new List<BO.OrderForList>();

        try
        {
            orders = (List<DO.Order>)Dal.Order.GetAll();
        }
        catch (DO.NotExistsException e)
        {
            throw new BO.NotExistsException("", e);
        }

		foreach (var item in orders)
		{
			BO.OrderForList temporder = new BO.OrderForList()
			{
				ID = item.ID,
				CustomerName = item.CustomerName,
				Status = GetStatus(item)
            };

			List<DO.OrderItem> orderitems = Dal.OrderItem.GetOrderItem(item.ID);

			foreach (var it in orderitems)
			{
				temporder.AmountOfItems += it.Amount;
				temporder.TotalPrice += it.Price * it.Amount;
			}

			ordersForList.Add(temporder);
		}
		return ordersForList;
    }
	public BO.Order GetOrder(int orderId)
	{
        if (orderId <= 0)
            throw new BO.IdIsLessThanZeroException();

		List<DO.OrderItem> orderItems = new List<DO.OrderItem>();
		DO.Order dOrder = new DO.Order();

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
			ID = dOrder.ID,
			CustomerName = dOrder.CustomerName,
			CustomerEmail = dOrder.CustomerEmail,
			CustomerAddress = dOrder.CustomerAddress,
			Status = GetStatus(dOrder),
			OrderDate = dOrder.OrderDate,
			ShipDate = dOrder.ShipDate,
			DeliveryDate = dOrder.DeliveryDate,
			Items = DoBoConvert(orderItems, dOrder.ID).Item1,
		    TotalPrice = DoBoConvert(orderItems, dOrder.ID).Item2
        };
        return bOrder;
    }
	private BO.OrderStatus GetStatus(DO.Order o)
	{
		if (o.DeliveryDate > DateTime.MinValue)
			return BO.OrderStatus.Delivered;
		if (o.ShipDate > DateTime.MinValue)
			return BO.OrderStatus.Sent;
		return BO.OrderStatus.Confirmed;
	}
	private(List<BO.OrderItem>, double) DoBoConvert(List<DO.OrderItem> orderItem, int dOrderID)
	{
		List<BO.OrderItem> bOrderItem = new List<BO.OrderItem>();
		double s = 0;

		foreach (var item in orderItem)
		{
			if (item.OrderID == dOrderID)
			{
				BO.OrderItem orderitem = new BO.OrderItem()
				{
					ID = item.ID,
					Name = Dal.Product.GetById(item.ProductID).Name,
					Price = item.Price,
					ProductID = item.ProductID,
					Amount = item.Amount,
				};
				bOrderItem.Add(orderitem);
			}
		}
		return (bOrderItem, s);
	}
	public BO.Order UpdateOrderShipping(int orderId)
	{
		if (orderId < 0)
			throw new BO.IdIsLessThanZeroException();

		DO.Order dOrder = new DO.Order();
		BO.Order bOrder = new BO.Order();

        try
        {
            dOrder = Dal.Order.GetById(orderId);
            bOrder = GetOrder(orderId);
        }
        catch (DO.NotExistsException e) { throw new BO.NotExistsException("", e); }

		if (dOrder.ShipDate == DateTime.MinValue)
		{
			dOrder.ShipDate = DateTime.Now;
            bOrder.ShipDate = DateTime.Now;
        }
		Dal.Order.Update(dOrder);
		return bOrder;
	}
	public BO.Order UpdateOrderDelivery(int orderId)
	{
        if (orderId < 0)
            throw new BO.IdIsLessThanZeroException();

        DO.Order dOrder = new DO.Order();
        BO.Order bOrder = new BO.Order();

        try
        {
            dOrder = Dal.Order.GetById(orderId);
            bOrder = GetOrder(orderId);
        }
        catch (DO.NotExistsException e) { throw new BO.NotExistsException("", e); }

        if (dOrder.DeliveryDate == DateTime.MinValue)
        {
            dOrder.DeliveryDate = DateTime.Now;
            bOrder.DeliveryDate = DateTime.Now;
        }
        Dal.Order.Update(dOrder);
        return bOrder;
    }
	public BO.OrderTracking TrackOrder(int orderId)
	{
        if (orderId <= 0)
            throw new BO.InvalidInputException();

        DO.Order dOrder = new DO.Order();

        try
        {
            dOrder = Dal.Order.GetById(orderId);
        }
        catch (DO.NotExistsException e) { throw new BO.NotExistsException("", e); }
		
		BO.OrderTracking orderTracking = new BO.OrderTracking();
        orderTracking.ID = orderId;
		orderTracking.Status = GetStatus(dOrder);

        Tuple<DateTime, string>? tuple = new Tuple<DateTime, string>(
            (DateTime)dOrder.OrderDate, orderTracking.Status.ToString());

		orderTracking.Log.Add(tuple);

		return orderTracking;
    }
}
