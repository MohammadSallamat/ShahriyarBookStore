using Application.CommonApplication;
using Domain.CategoryAggregate;
using Domain.CategoryAggregate.Services;

namespace Application.Categories0.Create;

public class CreateCategoryCommandHandler : IBaseCommandHandler<CreateCategoryCommand,long>
{
    private readonly ICategoryDomainService _categoryDomainService;
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository,ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }

    public async Task<OperationResult<long>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.title,request.slug,request.seoData, _categoryDomainService);
        _categoryRepository.Add(category);
        await _categoryRepository.Save();
        return OperationResult<long>.Success(category.Id);
    }
}