using Application.CommonApplication;
using Application.CommonApplication._Utilities;
using Application.CommonApplication.FileUtil.Interfaces;
using Domain.ProductAggregate;
using Domain.ProductAggregate.Repository;

namespace Application.Products.AddImage;

public class AddImageProductCommandHandler : IBaseCommandHandler<AddImageProductCommand>
{
    private readonly IProductRepository _repository;
    private readonly ILocalFileService _fileImage;

    public AddImageProductCommandHandler(IProductRepository repository, ILocalFileService fileImage)
    {
        _repository = repository;
        _fileImage = fileImage;
    }

    public async Task<OperationResult> Handle(AddImageProductCommand request, CancellationToken cancellationToken)
    {
        var product=await _repository.GetTracking(request.ProductId);
        if (product == null)
            return OperationResult.NotFound();

        var imageName = await _fileImage.SaveFileAndGenerateName(request.ImageName, Directories.ProductImages);
        product.AddImage(new ProductImage(imageName,request.Sequence));
        await _repository.Save();
        return OperationResult.Success();
    }
}
