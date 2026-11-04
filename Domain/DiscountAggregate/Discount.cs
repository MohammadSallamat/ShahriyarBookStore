using Domain.Common.Domain;
using Domain.Common.Domain.Exception;
using Domain.Common.Domain.Extention;
using Domain.DiscountAggregate.Enums;
using System;

namespace Domain.DiscountAggregate;

public class Discount : AggregateRoot
{
    public string Code { get; private set; }
    public DiscountType DiscountType { get; private set; }
    public int DiscountAmount { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public int? Capacity { get; private set; }
    public int UsedCount { get; private set; }
    public int? MinOrderAmount { get; private set; }
    public int? MaxDiscountAmount { get; private set; }
    public bool IsActive { get; private set; }

    private Discount()
    {
    }

    public Discount(string code, DiscountType discountType, int discountAmount,
        DateTime? startDate, DateTime? endDate, int? capacity,
        int? minOrderAmount, int? maxDiscountAmount)
    {
        Guard(code, discountType, discountAmount, startDate, endDate, capacity);

        Code = code.Trim().ToUpper();
        DiscountType = discountType;
        DiscountAmount = discountAmount;
        StartDate = startDate;
        EndDate = endDate;
        Capacity = capacity;
        MinOrderAmount = minOrderAmount;
        MaxDiscountAmount = maxDiscountAmount;
        UsedCount = 0;
        IsActive = true;
    }

    public void Edit(string code, DiscountType discountType, int discountAmount,
        DateTime? startDate, DateTime? endDate, int? capacity,
        int? minOrderAmount, int? maxDiscountAmount)
    {
        Guard(code, discountType, discountAmount, startDate, endDate, capacity);

        Code = code.Trim().ToUpper();
        DiscountType = discountType;
        DiscountAmount = discountAmount;
        StartDate = startDate;
        EndDate = endDate;
        Capacity = capacity;
        MinOrderAmount = minOrderAmount;
        MaxDiscountAmount = maxDiscountAmount;
    }

    public void UseDiscount()
    {
        if (!IsActive)
            throw new InvalidDomainDataException("کد تخفیف غیرفعال است.");

        if (StartDate.HasValue && DateTime.Now < StartDate.Value)
            throw new InvalidDomainDataException("زمان استفاده از کد تخفیف هنوز فرانرسیده است.");

        if (EndDate.HasValue && DateTime.Now > EndDate.Value)
            throw new InvalidDomainDataException("کد تخفیف منقضی شده است.");

        if (Capacity.HasValue && UsedCount >= Capacity.Value)
            throw new InvalidDomainDataException("ظرفیت استفاده از کد تخفیف به پایان رسیده است.");

        UsedCount++;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private void Guard(string code, DiscountType discountType, int discountAmount,
        DateTime? startDate, DateTime? endDate, int? capacity)
    {
        NullOrEmptyDomainDataException.CheckString(code, nameof(code));

        if (discountAmount <= 0)
            throw new InvalidDomainDataException("مقدار تخفیف باید بزرگتر از صفر باشد.");

        if (discountType == DiscountType.Percentage && discountAmount > 100)
            throw new InvalidDomainDataException("درصد تخفیف نمی‌تواند بیشتر از ۱۰۰ باشد.");

        if (startDate.HasValue && endDate.HasValue && startDate > endDate)
            throw new InvalidDomainDataException("تاریخ شروع نمی‌تواند بعد از تاریخ پایان باشد.");

        if (capacity.HasValue && capacity < 0)
            throw new InvalidDomainDataException("ظرفیت تخفیف نمی‌تواند منفی باشد.");
    }
}
