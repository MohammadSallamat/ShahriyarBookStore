using Application.CommonApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Remove;

public record RemoveOrderItemCommand(long UserId, long ItemId) : IBaseCommand;

