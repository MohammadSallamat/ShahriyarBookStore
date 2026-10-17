using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Comments.DTOs;
using Query.Common.Domain;

namespace Query.Comments.GetByFilter;

internal class GetCommentByFilterQueryHandler : IBaseQueryHandler<GetCommentByFilterQuery, CommentFilterResult>
{
    private readonly ShahriarBookStoreContext _context;

    public GetCommentByFilterQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<CommentFilterResult> Handle(GetCommentByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var query = _context.Comments.AsQueryable();

        if (@params.UserId.HasValue)
            query = query.Where(c => c.UserId == @params.UserId.Value);

        if (@params.ProductId.HasValue)
            query = query.Where(c => c.ProductId == @params.ProductId.Value);

        if (@params.CommentStatus.HasValue)
            query = query.Where(c => c.Status == @params.CommentStatus.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var pageId = @params.PageId < 1 ? 1 : @params.PageId;
        var take = @params.Take < 1 ? 10 : @params.Take;
        var skip = (pageId - 1) * take;

        var comments = await query
            .OrderByDescending(c => c.CreationDate)
            .Skip(skip)
            .Take(take)
            .Select(comment => new CommentDto
            {
                Id = comment.Id,
                CreationDate = comment.CreationDate,
                UserId = comment.UserId,
                ProductId = comment.ProductId,
                Text = comment.Text,
                Status = comment.Status,
                UpdateDate = comment.UpdateDate
            })
            .ToListAsync(cancellationToken);

        return new CommentFilterResult
        {
            Comments = comments,
            TotalCount = totalCount,
            PageId = pageId,
            Take = take
        };
    }
}
