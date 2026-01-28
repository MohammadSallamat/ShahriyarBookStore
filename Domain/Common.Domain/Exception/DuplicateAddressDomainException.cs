using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Domain.Exception;

public class DuplicateAddressDomainException : BaseDomainException
{
    public DuplicateAddressDomainException() : base("Address تکراری است")
    {
    }

    public DuplicateAddressDomainException(string message) : base(message)
    {
    }
}