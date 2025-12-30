using Application.CommonApplication;
using Application.CommonApplication._Utilities;
using Application.CommonApplication.FileUtil.Interfaces;
using Domain.SiteEntities;
using Domain.SiteEntities.Repositories;

namespace Application.SiteEntites.Sliders.Create;

public class CreateSliderCommandHandler : IBaseCommandHandler<CreateSliderCommand>
{
    private readonly ILocalFileService _fileService;
    private readonly ISliderRepository _repository;

    public CreateSliderCommandHandler(ILocalFileService fileService, ISliderRepository repository)
    {
        _fileService = fileService;
        _repository = repository;
    }

    public async Task<OperationResult> Handle(CreateSliderCommand request, CancellationToken cancellationToken)
    {
        var imageName = await _fileService
            .SaveFileAndGenerateName(request.ImageFile, Directories.SliderImages);
        var slider = new Slider(request.Title, request.Link, imageName);

        _repository.Add(slider);
        await _repository.Save();
        return OperationResult.Success();
    }
}
