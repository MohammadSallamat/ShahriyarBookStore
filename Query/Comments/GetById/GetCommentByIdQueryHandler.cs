using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Comments.DTOs;
using Query.Common.Domain;

namespace Query.Comments.GetById;

internal class GetCommentByIdQueryHandler : IBaseQueryHandler<GetCommentByIdQuery, CommentDto>
{
    private readonly ShahriarBookStoreContext _context;

    public GetCommentByIdQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<CommentDto> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(c => c.Id == request.CommentId, cancellationToken);

        if (comment == null)
            return null;

        return new CommentDto
        {
            Id = comment.Id,
            CreationDate = comment.CreationDate,
            UserId = comment.UserId,
            ProductId = comment.ProductId,
            Text = comment.Text,
            Status = comment.Status,
            UpdateDate = comment.UpdateDate
        };
    }
}
