using Application.CommonApplication;
using Application.CommonApplication._Utilities;
using Application.CommonApplication.FileUtil.Interfaces;
using Domain.UserAggregate.DomainServices;
using Domain.UserAggregate.Repository;
using Microsoft.AspNetCore.Http;

namespace Application.UsersApp.Edit;

internal partial class EditUserCommandHandler : IBaseCommandHandler<EditUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IDomainUserService _domainService;
    private readonly ILocalFileService _fileService;
    public EditUserCommandHandler(IUserRepository repository, IDomainUserService domainService, ILocalFileService fileService)
    {
        _repository = repository;
        _domainService = domainService;
        _fileService = fileService;
    }

    public async Task<OperationResult> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        var oldAvatar = user.AvatarName;
        user.Edit(request.Name, request.Family, request.PhoneNumber, request.Email,
            request.Gender, _domainService);
        if (request.Avatar != null)
        {
            var imageName = await _fileService
                .SaveFileAndGenerateName(request.Avatar, Directories.UserAvatars);
            user.SetAvatar(imageName);
        }

        DeleteOldAvatar(request.Avatar, oldAvatar);
        await _repository.Save();
        return OperationResult.Success();
    }

    private void DeleteOldAvatar(IFormFile? avatarFile, string oldImage)
    {
        if (avatarFile == null || oldImage == "avatar.png")
            return;

        _fileService.DeleteFile(Directories.UserAvatars, oldImage);
    }
}