using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Domain.Exception;

public class BaseDomainException:System.Exception
{ 
    public BaseDomainException() 
    {

    }

    public BaseDomainException(string message) : base(message) 
    { 
    
    }
}
