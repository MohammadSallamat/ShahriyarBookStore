using Application.CommonApplication;
using Application.CommonApplication._Utilities;
using Application.CommonApplication.FileUtil.Interfaces;
using Domain.ProductAggregate;
using Domain.ProductAggregate.Repository;
using Domain.ProductAggregate.Services;
namespace Application.Products.Create;

internal class CreateProducteCommandHandler : IBaseCommandHandler<CreateProducteCommand>
{
    private readonly IProductDomainService _domainService;
    private readonly IProductRepository _repository;
    private readonly ILocalFileService _fileImage;

    public CreateProducteCommandHandler(IProductDomainService domainService, IProductRepository repository, ILocalFileService fileImage)
    {
        _domainService = domainService;
        _repository = repository;
        _fileImage = fileImage;
    }

    public async Task<OperationResult> Handle(CreateProducteCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileImage.SaveFileAndGenerateName(request.ImageFile, Directories.ProductImages);
        var product = new Product(request.Title, imageName, request.Description, request.CategoryId, request.SubCategoryId,
            request.SecondarySubCategoryId, _domainService, request.Slug, request.SeoData);
        await _repository.Add(product);

        var specifications = request.Specifications
       .Select(s => new ProductSpecification(s.Key, s.Value))
       .ToList();
        product.SetSpecification(specifications);

        await _repository.Add(product);
        await _repository.Save();
        return OperationResult.Success();
    }
}

