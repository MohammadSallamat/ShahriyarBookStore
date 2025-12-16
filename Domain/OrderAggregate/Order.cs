using Domain.Common.Domain;
using Domain.Common.Domain.Extention;
using Domain.OrderAggregate.Enums;
using Domain.OrderAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OrderAggregate;

public class Order : AggregateRoot
{
    private Order()
    {
    }
    public Order(long userId)
    {
        UserId = userId;
        Status = OrderStatus.Pennding;
        Items = new List<OrderItem>();
    }

    public long UserId { get; private set; }
    public OrderStatus Status { get; private set; }
    public OrderDiscount? Discount { get; private set; }
    public OrderAddress? Address { get; private set; }
    public ShippingMethod? ShippingMethod { get; private set; }
    public List<OrderItem> Items { get; private set; }
    public DateTime? LastUpdate { get; set; }

    public int TotalPrice
    {
        get
        {
            var totalPrice = Items.Sum(x => x.TotalPrice);

            if (ShippingMethod != null)
                totalPrice += ShippingMethod.ShippingCost;
            if (Discount != null)
                totalPrice -= Discount.Amount;

            return totalPrice;
        }
    }
    public int ItemCount=>Items.Count;

    public void AddItem(OrderItem item, int currentInventoryCount )
    {
        ChangeOrderGuard();

        if (currentInventoryCount < item.Count)
            throw new InvalidDomainDataException("تعداد درخواستی از موجودی محصول بیشتر است.");

        var oldItem=Items.FirstOrDefault(f=>f.InventoryId == item.InventoryId);
        if (oldItem != null)
        {
            oldItem.ChangeCount(oldItem.Count+item.Count);
            if (currentInventoryCount < oldItem.Count)
                throw new InvalidDomainDataException("تعداد درخواستی از موجودی محصول بیشتر است.");
            return;
        }

        Items.Add(item);
    }

    public void RemoveItem(long itemId)
    {
        ChangeOrderGuard();
 
        var currentItem = Items.FirstOrDefault(f => f.Id == itemId);
        if (currentItem != null)
            Items.Remove(currentItem);
    }

    public void IncreaseOrderItemCount(long itemId, int count)
    {
        ChangeOrderGuard();

        var currentItem = Items.FirstOrDefault(x => x.Id == itemId);
        if (currentItem == null)
            throw new NullOrEmptyDomainDataException();

        currentItem.IncreaseCount(count);
    }

    public void DecreaseOrderItemCount(long itemId, int count)
    {
        ChangeOrderGuard();

        var currentItem = Items.FirstOrDefault(x => x.Id == itemId);
        if (currentItem == null)
            throw new NullOrEmptyDomainDataException();

        currentItem.DecreaseCount(count);
    }
    public void ChangeCountItem(long itemId, int newCount)
    {
        ChangeOrderGuard();

        var currentItem = Items.FirstOrDefault(f => f.Id == itemId);
        if(currentItem == null)
            throw new NullOrEmptyDomainDataException();
        currentItem.ChangeCount(newCount);
    }

    public void ChangeStatus(OrderStatus status)
    {
        Status= status;
        LastUpdate= DateTime.Now;
    }

    public void CheckOut(OrderAddress orderAddress)
    {
        ChangeOrderGuard();

       Address= orderAddress;
    }
    public void ChangeOrderGuard()
    {
        if (Status != OrderStatus.Pennding)
        
            throw new InvalidDomainDataException("امکان ویرایش محصول وجود ندارد.");
        
    }

}
