using Application.CommonApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sellers.AddInventory;

public record AddSellerInventoryCommand(long SellerId,long ProductId, int Count,
    int Price, int? DiscountPercentage):IBaseCommand;

