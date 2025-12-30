using Application.CommonApplication;
using Application.CommonApplication._Utilities;
using Application.CommonApplication.FileUtil.Interfaces;
using Domain.SiteEntities.Repositories;

namespace Application.SiteEntites.Banners.Edit;

public class EditBannerCommandHandler : IBaseCommandHandler<EditBannerCommand>
{
    private readonly IBannerRepository _repository;
    private readonly ILocalFileService _localFile;

    public EditBannerCommandHandler(IBannerRepository repository, ILocalFileService localFile)
    {
        _repository = repository;
        _localFile = localFile;
    }
    public async Task<OperationResult> Handle(EditBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _repository.GetTracking(request.BannerId);
        if (banner == null)
            return OperationResult.NotFound();

        if (request.ImageFile != null)
        {
            _localFile.DeleteFile(banner.ImageName);

            var newImageName = await _localFile.SaveFileAndGenerateName(
                request.ImageFile, Directories.BannerImages);

            banner.SetImage(newImageName);
        }

        banner.Edit(request.Link,request.Position);
        await _repository.Save();
        return OperationResult.Success();

    }
}

