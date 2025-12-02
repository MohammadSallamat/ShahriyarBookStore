using Application.CommonApplication;
using Domain.Common.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories0.Create;

public record CreateCategoryCommand(string title, string slug, SeoData seoData):IBaseCommand<long>;
