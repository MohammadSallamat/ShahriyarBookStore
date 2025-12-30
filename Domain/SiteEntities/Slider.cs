using Domain.Common.Domain;
using Domain.Common.Domain.Extention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SiteEntities;

public class Slider : BaseEntity
{
    public string Title { get; private set; }
    public string Link { get; private set; }
    public string ImageName { get; private set; }

    public Slider(string title, string link, string imageName)
    {
        Guard(title, link);

        Title = title;
        Link = link;
        ImageName = imageName;
    }

    public void Edit(string title, string link)
    {
        Guard(title, link);
        Title = title;
        Link = link;
    }

    public void SetImage(string imageName)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        ImageName = imageName;
    }
    public void Guard(string title, string link)
    {
        NullOrEmptyDomainDataException.CheckString(link, nameof(link));
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
    }
}