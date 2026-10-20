using Application.CommonApplication;
using Domain.SellerAggregate.Enums;

namespace Application.Sellers.ChangeStatus;

public record ChangeSellerStatusCommand(long SellerId, SellerStatus Status) : IBaseCommand;
