using Application.CommonApplication;
using Domain.ProductAggregate;
using Domain.SiteEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SiteEntites.Banners.Edit;

public record EditBannerCommand(long BannerId, string Link, IFormFile ImageFile, BannerPosition Position) : IBaseCommand;

