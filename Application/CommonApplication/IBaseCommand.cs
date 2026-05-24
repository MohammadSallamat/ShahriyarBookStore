using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommonApplication;

internal interface IBaseCommand : IRequest<OperationResult>
{

}

internal interface IBaseCommand<TData> : IRequest<OperationResult<TData>>
{

}