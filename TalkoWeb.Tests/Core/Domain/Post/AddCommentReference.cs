using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using SQLitePCL;
using TalkoWeb.Core.Domain.Comments.ValueObjects;
using TalkoWeb.Core.Domain.Posts;
using TalkoWeb.Core.Domain.Posts.Handlers;

namespace TalkoWeb.Tests.Core.Domain.Posts
{
    public class AddCommentReferenceTest
    {
        private readonly DatabaseContext _context;
        private readonly AddCommentToPost _handler;
        private readonly Mock<IMediator> _mediatorMock;

        public AddCommentReferenceTest()
        {
            // Set up in-memory database
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase("TestDatabase").Options;
            _mediatorMock = new Mock<IMediator>(); // Mock the Mediator
            _context = new DatabaseContext(options, _mediatorMock.Object);

            // Set up the handler
            _handler = new AddCommentToPost(_context);
        }

        [Fact]
        public async Task Handler_Should_Add_CommentReference_To_Post_When_CommentSaved_Is_Raised()
        {
            // Arrange
            var postId = Guid.NewGuid();
            var postAuthorId = Guid.NewGuid();
            var post = new Post(postId, postAuthorId, "A New Title", "Some Content");
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            // Act

            var commentId = Guid.NewGuid();
            var @event = new CommentSaved(commentId, postId);
            await _handler.Handle(@event, CancellationToken.None);

            // Assert
            var updatedPost = await _context.Posts.FindAsync(postId);
            updatedPost.Should().NotBeNull();
            updatedPost!.CommentsRefernces.Should().Contain(commentId);
        }
    }
}
