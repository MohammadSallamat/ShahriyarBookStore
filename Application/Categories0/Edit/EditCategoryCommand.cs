using Application.CommonApplication;
using Domain.Common.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories0.Edit;
public record EditCategoryCommand(long Id, string Title, string Slug, SeoData SeoData) : IBaseCommand;
