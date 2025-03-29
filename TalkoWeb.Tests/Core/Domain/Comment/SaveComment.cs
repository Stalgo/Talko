using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TalkoWeb.Core.Domain.Comments.Handlers;
using TalkoWeb.Core.Domain.Comments.Validators;
using TalkoWeb.Core.Domain.Comments.ValueObjects;
using Xunit;

namespace TalkoWeb.Tests.Core.Domain.Comments
{
    public class SaveCommentTests : IDisposable
    {
        private readonly DatabaseContext _context;
        private readonly SaveComment _handler;
        private readonly IServiceProvider _serviceProvider;
        private readonly Mock<IMediator> _mediatorMock;

        public SaveCommentTests()
        {
            // Setup in-memory database
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            _mediatorMock = new Mock<IMediator>(); // Mock the Mediator
            _context = new DatabaseContext(options, _mediatorMock.Object);

            // Setup DI container and register services for MediatR and DatabaseContext
            var services = new ServiceCollection();

            // Register MediatR services and mock the mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SaveComment).Assembly));
            services.AddSingleton<IMediator>(_mediatorMock.Object);

            // Register DatabaseContext and AddCommentValidator
            services.AddSingleton(_context); // Register the database context
            services.AddTransient<IValidator<SaveCommentDTO>, AddCommentValidator>(); // Register the validator
            services.AddTransient<SaveComment>();

            _serviceProvider = services.BuildServiceProvider();

            // Retrieve AddCommentHandler from the DI container
            _handler = _serviceProvider.GetRequiredService<SaveComment>();
        }

        [Fact]
        public async Task Handle_Should_Save_Comment_To_Database()
        {
            // Arrange
            var request = new SaveCommentDTO(Guid.NewGuid(), Guid.NewGuid(), "This is a test comment.");

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            var savedComment = await _context.Comments.FirstOrDefaultAsync(c => c.CommentContent == request.Content);
            savedComment.Should().NotBeNull();
            savedComment!.AuthorId.Should().Be(request.AuthorId);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
