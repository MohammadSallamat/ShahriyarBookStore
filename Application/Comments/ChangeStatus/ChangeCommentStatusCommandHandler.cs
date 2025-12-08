using Application.CommonApplication;
using Domain.CommentAggregate;

namespace Application.Comments.ChangeStatus;

public class ChangeCommentStatusCommandHandler : IBaseCommand<ChangeCommentStatusCommand>
{
    private readonly ICommentRepository _commentRepository;

    public ChangeCommentStatusCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<OperationResult> Handle(ChangeCommentStatusCommand request, CancellationToken cancellationToken)
    {
        var status =await _commentRepository.GetTracking(request.CommentId);
        if (status == null)
            return OperationResult.NotFound();
        status.ChangeStatus(request.Status);
        await _commentRepository.Save();
        return OperationResult.Success();
    }
}