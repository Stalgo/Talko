using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using TalkoWeb.Core.Domain.Comments;
using TalkoWeb.Core.Domain.Comments.Handlers;
using TalkoWeb.Core.Domain.Comments.ValueObjects;

namespace TalkoWeb.Tests.Core.Domain.Comments
{
    public class DeleteCommentTests
    {
        private readonly DatabaseContext _context;
        private readonly DeleteComment _handler;
        private readonly Mock<IMediator> _mediatorMock;

        public DeleteCommentTests()
        {
            // Set up in-memory database
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase("TestDatabase").Options;
            _mediatorMock = new Mock<IMediator>(); // Mock the Mediator
            _context = new DatabaseContext(options, _mediatorMock.Object);

            // Set up the handler
            _handler = new DeleteComment(_context);
        }

        [Fact]
        public async Task Handle_Should_Delete_Comment_When_Exists()
        {
            // Arrange: Create and save a new comment
            var comment = new Comment(Guid.NewGuid(), Guid.NewGuid(), "This is a comment");

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            // Assert: Comment should exist before deletion
            var existingComment = await _context.Comments.FindAsync(comment.CommentId);
            existingComment.Should().NotBeNull();

            var request = new DeleteCommentHandler(comment.AuthorId, comment.CommentId);

            // Act: Call the handler to delete the comment
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert: The result should be success
            result.IsSuccess.Should().BeTrue();

            // Assert: The comment should no longer exist in the database
            var deletedComment = await _context.Comments.FindAsync(comment.Id);
            deletedComment.Should().BeNull();
        }

        [Fact]
        public async Task Handle_Should_Return_Error_When_Comment_Not_Found()
        {
            // Arrange: Create and save a new comment
            var comment = new Comment(Guid.NewGuid(), Guid.NewGuid(), "This is a comment");

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            // Assert: Comment should exist before deletion
            var existingComment = await _context.Comments.FindAsync(comment.CommentId);
            existingComment.Should().NotBeNull();
            var nonExistingCommentId = Guid.NewGuid();

            var request = new DeleteCommentHandler(comment.AuthorId, nonExistingCommentId);

            // Act: Call the handler to delete the comment
            var result = await _handler.Handle(request, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().Contain("Could not delete comment");
        }

        [Fact]
        public async Task Handle_Should_Return_Error_When_Invalid_User()
        {
            // Arrange: Create and save a new comment
            var comment = new Comment(Guid.NewGuid(), Guid.NewGuid(), "This is a comment");

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            // Assert: Comment should exist before deletion
            var existingComment = await _context.Comments.FindAsync(comment.CommentId);
            existingComment.Should().NotBeNull();
            var nonExistingUserId = Guid.NewGuid();

            var request = new DeleteCommentHandler(nonExistingUserId, comment.CommentId);

            // Act: Call the handler to delete the comment
            var result = await _handler.Handle(request, CancellationToken.None);

            result.IsSuccess.Should().BeFalse();
            result.Errors.Should().Contain("Could not delete comment");
        }
    }
}
