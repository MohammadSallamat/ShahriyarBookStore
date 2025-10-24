using Domain.Common.Domain;
using Domain.UserAggregate.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate;

public class Wallet:BaseEntity
{
    public Wallet(decimal price, string description, bool isConfirmed, DateTime? finallyDate, WalletType walletType)
    {
        Price = price;
        Description = description;
        IsConfirmed = isConfirmed;
        FinallyDate = finallyDate;
        WalletType = walletType;
    }

    public long UserId {  get; internal set; }
    public decimal Price {  get; private set; }
    public string Description {  get; private set; }
    public bool IsConfirmed { get; private set; }
    public DateTime? FinallyDate { get; private set; }
    public WalletType WalletType { get; private set; }


    public void Finally(string refCode)
    {
        IsConfirmed = true;
        FinallyDate = DateTime.Now;
        Description += $" کد پیگیری : {refCode}";
    }

    public void Finally()
    {
        IsConfirmed = true;
        FinallyDate = DateTime.Now;
    }
}
