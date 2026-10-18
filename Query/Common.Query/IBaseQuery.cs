using MediatR;

namespace Query.Common.Domain;

internal interface IBaseQuery<TResponse> : IRequest<TResponse> where TResponse : class?
{

}
