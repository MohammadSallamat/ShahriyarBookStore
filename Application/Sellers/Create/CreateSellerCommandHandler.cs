using Application.CommonApplication;
using Domain.SellerAggregate;
using Domain.SellerAggregate.Repository;
using Domain.SellerAggregate.Services;

namespace Application.Sellers.Create;

public class CreateSellerCommandHandler : IBaseCommandHandler<CreateSellerCommand>
{
    private readonly ISellerDomainService _domainService;
    private readonly ISellerRepository _repository;

    public CreateSellerCommandHandler(ISellerDomainService domainService, ISellerRepository repository)
    {
        _domainService = domainService;
        _repository = repository;
    }

    public async Task<OperationResult> Handle(CreateSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = new Seller(request.UserId, request.ShopName, request.NationalCode, _domainService);

        _repository.Add(seller);
        await _repository.Save();
        return OperationResult.Success();
    }
}
