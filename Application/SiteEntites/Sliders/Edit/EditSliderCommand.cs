using Application.CommonApplication;
using Application.SiteEntites.Banners.Edit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SiteEntites.Sliders.Edit;

public record EditSliderCommand(long SliderId,string Title,string Link, IFormFile? ImageFile):IBaseCommand;
