using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommonApplication;


internal interface IBaseCommandHandler<TCommand> : IRequestHandler<TCommand, OperationResult> where TCommand : IBaseCommand
{
}
internal interface IBaseCommandHandler<TCommand, TResponseData> : IRequestHandler<TCommand, OperationResult<TResponseData>> where TCommand : IBaseCommand<TResponseData>
{
}