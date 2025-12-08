using Application.CommonApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Edit;

public record EditCommentCommand(long CommentId, string Text, long UserId) : IBaseCommand;

