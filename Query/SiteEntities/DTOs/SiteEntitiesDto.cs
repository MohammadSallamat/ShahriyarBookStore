using Domain.SiteEntities;
using Query.Common.Domain;

namespace Query.SiteEntities.DTOs;

public class SliderDto : BaseDto
{
    public string Title { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
}

public class BannerDto : BaseDto
{
    public string Link { get; set; } = string.Empty;
    public string ImageName { get; set; } = string.Empty;
    public BannerPosition Position { get; set; }
}
