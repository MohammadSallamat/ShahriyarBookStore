using Domain.CommentAggregate;
using Query.Common.Domain;
using System;

namespace Query.Comments.DTOs;

public class CommentDto : BaseDto
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public string Text { get; set; } = string.Empty;
    public CommentStatus Status { get; set; }
    public DateTime UpdateDate { get; set; }
}

public class CommentFilterParams
{
    public long? UserId { get; set; }
    public long? ProductId { get; set; }
    public CommentStatus? CommentStatus { get; set; }
    public int PageId { get; set; } = 1;
    public int Take { get; set; } = 10;
}

public class CommentFilterResult
{
    public List<CommentDto> Comments { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageId { get; set; }
    public int Take { get; set; }
    public int PageCount => (int)Math.Ceiling((double)TotalCount / Take);
}
