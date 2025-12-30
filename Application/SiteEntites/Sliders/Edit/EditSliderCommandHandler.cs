using Application.CommonApplication;
using Application.CommonApplication._Utilities;
using Application.CommonApplication.FileUtil.Interfaces;
using Domain.SiteEntities.Repositories;

namespace Application.SiteEntites.Sliders.Edit;

internal class EditSliderCommandHandler :IBaseCommandHandler<EditSliderCommand>
{
    private readonly ISliderRepository _repository;
    private readonly ILocalFileService _localFile;

    public EditSliderCommandHandler(ISliderRepository repository, ILocalFileService localFile)
    {
        _repository = repository;
        _localFile = localFile;
    }
    public async Task<OperationResult> Handle(EditSliderCommand request, CancellationToken cancellationToken)
    {
        var slider = await _repository.GetTracking(request.SliderId);
        if (slider == null)
            return OperationResult.NotFound();

        if (request.ImageFile != null)
        {
            _localFile.DeleteFile(slider.ImageName);

            var newImageName = await _localFile.SaveFileAndGenerateName(
                request.ImageFile, Directories.BannerImages);

            slider.SetImage(newImageName);
        }

        slider.Edit(request.Title,request.Link);
        await _repository.Save();
        return OperationResult.Success();

    }
}
