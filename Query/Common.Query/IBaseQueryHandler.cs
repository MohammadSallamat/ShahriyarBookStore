using MediatR;

namespace Query.Common.Domain;
internal interface IBaseQueryHandler<TQuery,TResponse>:IRequestHandler<TQuery,TResponse>
    where TQuery : IBaseQuery<TResponse>
    where TResponse : class
{

}

    
