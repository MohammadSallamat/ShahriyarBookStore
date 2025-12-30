using Application.CommonApplication;
using Domain.SiteEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SiteEntites.Sliders.Create;

public record CreateSliderCommand(string Title,string Link, IFormFile ImageFile) : IBaseCommand;
