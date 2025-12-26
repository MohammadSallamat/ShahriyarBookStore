using Application.CommonApplication;
using Application.CommonApplication._Utilities;
using Application.CommonApplication.FileUtil.Interfaces;
using Domain.ProductAggregate.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.RemoveImage;

public record RemoveImageProductCommand(long ProductId,long ImageId) : IBaseCommand;

public class RemoveImageProductCommandHandle : IBaseCommandHandler<RemoveImageProductCommand>
{
    private readonly ILocalFileService _localFileService;
    private readonly IProductRepository _repository;

    public RemoveImageProductCommandHandle(ILocalFileService localFileService, IProductRepository repository)
    {
        _localFileService = localFileService;
        _repository = repository;
    }

    public async Task<OperationResult> Handle(RemoveImageProductCommand request, CancellationToken cancellationToken)
    {
        var product=await _repository.GetTracking(request.ProductId);
        if (product == null)
            return OperationResult.NotFound();

       var imageName = product.RemoveImage(request.ImageId);
        _localFileService.DeleteFile(Directories.ProductGalleryImage,imageName);
        await _repository.Save();
        return OperationResult.Success();
    }
}
