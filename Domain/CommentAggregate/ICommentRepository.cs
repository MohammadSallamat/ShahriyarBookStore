using Domain.Common.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommentAggregate
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
    }
}
