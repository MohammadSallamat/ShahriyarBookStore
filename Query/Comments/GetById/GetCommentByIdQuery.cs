using Query.Comments.DTOs;
using Query.Common.Domain;

namespace Query.Comments.GetById;

public record GetCommentByIdQuery(long CommentId) : IBaseQuery<CommentDto>;
