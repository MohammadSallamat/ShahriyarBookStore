using Application.CommonApplication;
using Domain.SiteEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SiteEntites.Banners.Create;

public record CreateBannerCommand(string Link, IFormFile ImageFile, BannerPosition position) : IBaseCommand;
