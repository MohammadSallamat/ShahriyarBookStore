using Application.CommonApplication;
using Domain.CommentAggregate;

namespace Application.Comments.Create;

public class CreateCommentCommandHandler : IBaseCommand<CreateCommentCommand>
{
    private readonly ICommentRepository _commentRepository;

    public CreateCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<OperationResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var Comment=new Comment(request.userId,request.productId, request.text);
        await _commentRepository.Add(Comment);
        await _commentRepository.Save();
        return OperationResult.Success();
    }
}
