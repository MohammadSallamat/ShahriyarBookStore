using Application.CommonApplication;
using Application.Orders.DecreaseItemCount;
using Application.Orders.IncreaseItemCount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.IncreaseItemCount;

public record IncreaseOrderItemCountCommand(long UserId, long ItemId, int Count) : IBaseCommand;

