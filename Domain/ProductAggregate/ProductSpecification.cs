using Domain.Common.Domain;
using Domain.Common.Domain.Extention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductAggregate;


public class ProductSpecification : BaseEntity
{
    public ProductSpecification(string key, string value)
    {
        NullOrEmptyDomainDataException.CheckString(key, nameof(key));
        NullOrEmptyDomainDataException.CheckString(value, nameof(value));

        Key = key;
        Value = value;
    }

    public long ProductId { get; internal set; }
    public string Key { get; private set; }
    public string Value { get; private set; }
}