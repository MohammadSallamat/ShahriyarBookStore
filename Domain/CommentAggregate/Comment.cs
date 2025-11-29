using Domain.Common.Domain;
using Domain.Common.Domain.Extention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommentAggregate;

public class Comment : AggregateRoot
{
    public long UserId { get; private set; }
    public long ProductId { get; private set; }
    public string Text { get; private set; }
    public CommentStatus Status { get; private set; }
    public DateTime UpdateDate { get; private set; }

    public Comment(long userId, long productId, string text)
    {
        NullOrEmptyDomainDataException.CheckString(text, nameof(text));

        UserId = userId;
        ProductId = productId;
        Text = text;
        Status = CommentStatus.Pennding;
    }

    public void Edit(string text)
    {
        NullOrEmptyDomainDataException.CheckString(text, nameof(text));

        Text = text;
        UpdateDate = DateTime.Now;
    }

    public void ChangeStatus(CommentStatus status)
    {
        Status = status;
        UpdateDate = DateTime.Now;
    }
}

public enum CommentStatus
{
    Pennding,
    Accepted,
    Rejected
}