using Application.CommonApplication;
using Application.CommonApplication._Utilities;
using Application.CommonApplication.FileUtil.Interfaces;
using Domain.ProductAggregate;
using Domain.ProductAggregate.Repository;
using Domain.ProductAggregate.Services;

namespace Application.Products.Edit;

public class EditProductcommandHandler : IBaseCommandHandler<EditProductcommand>
{
    private readonly ILocalFileService _fileImage;
    private readonly IProductRepository _repository;
    private readonly IProductDomainService _domainService;

    public EditProductcommandHandler(ILocalFileService fileImage, IProductRepository repository, IProductDomainService domainService)
    {
        _fileImage = fileImage;
        _repository = repository;
        _domainService = domainService;
    }

    public async Task<OperationResult> Handle(EditProductcommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetTracking(request.ProductId);
        if (product == null)
            return OperationResult.NotFound();

        if (request.ImageFile != null)
        {
            _fileImage.DeleteFile(product.ImageName);

            var newImageName = await _fileImage.SaveFileAndGenerateName(
                request.ImageFile, Directories.ProductImages);

            product.SetImage(newImageName);
        }
        if (request.Specifications != null)
        {
            var specifications = request.Specifications
           .Select(s => new ProductSpecification(s.Key, s.Value))
           .ToList();
            product.SetSpecification(specifications);
        }

        product.Edit(request.Title, request.Description, request.CategoryId,
        request.SubCategoryId, request.SecondarySubCategoryId, request.Slug,
        _domainService, request.SeoData);

        await _repository.Save();
        return OperationResult.Success();
    }
}

