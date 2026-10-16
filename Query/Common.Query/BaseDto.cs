using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Common.Domain;

public class BaseDto
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; }
}
