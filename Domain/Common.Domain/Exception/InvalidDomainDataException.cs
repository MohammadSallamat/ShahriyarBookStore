using Domain.Common.Domain.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Domain.Extention;

public class InvalidDomainDataException : BaseDomainException
{
    public InvalidDomainDataException()
    {

    }
    public InvalidDomainDataException(string message) : base(message)
    {

    }
}