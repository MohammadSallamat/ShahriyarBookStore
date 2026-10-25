using Domain.CategoryAggregate.Services;
using Domain.Common.Domain;
using Domain.Common.Domain.Exception;
using Domain.Common.Domain.Extention;
using Domain.Common.Domain.ValueObjects;
using System.Collections.Generic;

namespace Domain.CategoryAggregate;

public class Category : AggregateRoot
{
    private Category()
    {
        Childs = new List<Category>();
    }

    public Category(string title, string slug, SeoData seoData, ICategoryDomainService service)
    {
        Childs = new List<Category>();
        slug = slug?.ToSlug();
        Guard(title, slug, service);
        Title = title;
        Slug = slug;
        SeoData = seoData;
    }

    public string Title { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    public long? ParentId { get; private set; }
    public List<Category> Childs { get; private set; } = new();

    public void Edit(string title, string slug, SeoData seoData, ICategoryDomainService service)
    {
        slug = slug?.ToSlug();
        Guard(title, slug, service);
        Title = title;
        Slug = slug;
        SeoData = seoData;
    }

    public void AddChild(string title, string slug, SeoData seoData, ICategoryDomainService service)
    {
        Childs ??= new List<Category>();
        Childs.Add(new Category(title, slug, seoData, service)
        {
            ParentId = Id
        });
    }

    public void Guard(string title, string slug, ICategoryDomainService service)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));

        if (slug != Slug)
            if (service.IsSlugExist(slug))
                throw new SlugIsDuplicateException();
    }
}
