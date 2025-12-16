using Application.CommonApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.DecreaseItemCount;

public record DecreaseOrderItemCountCommand(long UserId, long ItemId, int Count) : IBaseCommand;