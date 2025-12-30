using Application.CommonApplication;
using Application.CommonApplication._Utilities;
using Application.CommonApplication.FileUtil.Interfaces;
using Domain.SiteEntities;
using Domain.SiteEntities.Repositories;

namespace Application.SiteEntites.Banners.Create;

public class CreateBannerCommandHandler : IBaseCommandHandler<CreateBannerCommand>
{
    private readonly IBannerRepository _repository;
    private readonly ILocalFileService _localFile;

    public CreateBannerCommandHandler(IBannerRepository repository, ILocalFileService localFile)
    {
        _repository = repository;
        _localFile = localFile;
    }

    public async Task<OperationResult> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        var imageName =await _localFile.SaveFileAndGenerateName(request.ImageFile, Directories.BannerImages);
        var banner=new Banner(request.Link,imageName,request.position);

        _repository.Add(banner);
        await _repository.Save();
        return OperationResult.Success();
    }
}
