using Application.CommonApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.AddItem;

public record AddOrderItemCommand(long InventoryId,long UserId,int Count):IBaseCommand;
