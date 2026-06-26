using Domain.CategoryAggregate;
using Domain.CategoryAggregate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories0
{
    public class CategoryDomainService : ICategoryDomainService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryDomainService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public bool IsSlugExist(string slug)
        {
            return _categoryRepository.Exists(x=> x.Slug == slug);
        }
    }
}
