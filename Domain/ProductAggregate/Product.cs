using Domain.Common.Domain;
using Domain.Common.Domain.Exception;
using Domain.Common.Domain.Extention;
using Domain.Common.Domain.ValueObjects;
using Domain.ProductAggregate.Services;
namespace Domain.ProductAggregate;

public class Product : AggregateRoot
{

    public string Title { get; private set; }
    public string? ImageName { get; private set; }
    public string Description { get; private set; }
    public long CategoryId { get; private set; }
    public long SubCategoryId { get; private set; }
    public long SecondarySubCategoryId { get; private set; }
    public string Slug { get; private set; }
    public SeoData SeoData { get; private set; }
    public List<ProductImage>? Images { get; private set; }
    public List<ProductSpecification> Specifications { get; private set; }

    public Product(string title, string imageName, string description, long categoryId,
       long subCategoryId, long secondarySubCategoryId, IProductDomainService domainService,
       string slug, SeoData seoData)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        Guard(title, slug, description, domainService);

        Title = title;
        ImageName = imageName;
        Description = description;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Slug = slug.ToSlug();
        SeoData = seoData;
    }

    public void Edit(string title, string description, long categoryId,
        long subCategoryId, long secondarySubCategoryId, string slug, IProductDomainService domainService
        , SeoData seoData)
    {
        Guard(title, slug, description, domainService);
        Title = title;
        Description = description;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        SecondarySubCategoryId = secondarySubCategoryId;
        Slug = slug.ToSlug();
        SeoData = seoData;
    }

    public void AddImage(ProductImage image)
    {
        image.ProductId = Id;
        Images.Add(image);
    }

    public string RemoveImage(long id)
    {
        var image = Images.FirstOrDefault(f => f.Id == id);
        if (image == null)
            throw new NullOrEmptyDomainDataException("image not found.");
        Images.Remove(image);
        return image.ImageName;
    }

    public void SetImage(string imageName)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));
        ImageName = imageName;
    }

    public void SetSpecification(List<ProductSpecification> specifications)
    {
        specifications.ForEach(s => s.ProductId = Id);
        Specifications = specifications;
    }

    public void Guard(string title, string slug,string description,
        IProductDomainService domainService)
    {
        NullOrEmptyDomainDataException.CheckString(title, nameof(title));
        NullOrEmptyDomainDataException.CheckString(description, nameof(description));
        NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));

        if (slug != Slug)
            if (domainService.SlugIsExist(slug.ToSlug()))
                throw new SlugIsDuplicateException();
    }
}