using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CategoryAggregate.Services
{
    public interface ICategoryDomainService
    {
        bool IsSlugExist(string slug);
    }
}
