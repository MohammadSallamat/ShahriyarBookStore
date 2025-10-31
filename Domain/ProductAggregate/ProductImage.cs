using Domain.Common.Domain;
using Domain.Common.Domain.Extention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ProductAggregate;


public class ProductImage : BaseEntity
{
    public ProductImage(string imageName, int sequence)
    {
        NullOrEmptyDomainDataException.CheckString(imageName, nameof(imageName));

        ImageName = imageName;
        Sequence = sequence;
    }

    public long ProductId { get; internal set; }
    public string ImageName { get; private set; }
    public int Sequence { get; private set; }
}