using Application.CommonApplication;
using Domain.CategoryAggregate;
using Domain.CategoryAggregate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories0.Edit;

public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
{
    private readonly ICategoryRepository _repository;
    private readonly ICategoryDomainService _domainServicer;

    public EditCategoryCommandHandler(ICategoryRepository repository, ICategoryDomainService domainServicer)
    {
        _repository = repository;
        _domainServicer = domainServicer;
    }

    public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetTracking(request.Id);
        if (category == null)
            return OperationResult.NotFound();

        category.Edit(request.Title, request.Slug, request.SeoData, _domainServicer);
        await _repository.Save();
        return OperationResult.Success();
    }
}