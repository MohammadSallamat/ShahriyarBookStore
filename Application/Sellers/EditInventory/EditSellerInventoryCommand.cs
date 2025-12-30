using Application.CommonApplication;
using Application.Sellers.AddInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sellers.EditInventory;

public record EditSellerInventoryCommand(long SellerId, long inventoryId, int Count,
    int Price, int? DiscountPercentage) : IBaseCommand;
