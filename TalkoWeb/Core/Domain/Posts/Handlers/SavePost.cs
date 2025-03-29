using MediatR;
using TalkoWeb.Application;

namespace TalkoWeb.Core.Domain.Posts.Handlers
{
    public class SavePost : IRequestHandler<SavePostDTO, Result>
    {
        private readonly DatabaseContext _context;

        public SavePost(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(SavePostDTO savePostDTO, CancellationToken cancellationToken)
        {
            Post post = new(savePostDTO.AutorId, savePostDTO.Title, savePostDTO.Content);

            await _context.Posts.AddAsync(post, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
