using Application.Comments.ChangeStatus;
using Application.CommonApplication;
using Domain.CommentAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.ChangeStatus;

public record ChangeCommentStatusCommand(long CommentId,CommentStatus Status):IBaseCommand;
