using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Common.Domain;

internal interface IBaseQuery<TResponse>:IRequest<TResponse>where TResponse : class
{

}

