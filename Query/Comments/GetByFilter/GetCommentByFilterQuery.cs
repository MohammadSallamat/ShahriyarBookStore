using Query.Comments.DTOs;
using Query.Common.Domain;

namespace Query.Comments.GetByFilter;

public record GetCommentByFilterQuery(CommentFilterParams FilterParams) : IBaseQuery<CommentFilterResult>;
