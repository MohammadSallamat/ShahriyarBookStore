using Domain.CommentAggregate;
using Infrastructure._Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.EF.CommentAgg;
internal class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(ShahriarBookStoreContext context) : base(context)
    {
    }
}
