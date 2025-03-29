using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using TalkoWeb.Core.Domain.Comments.ValueObjects;
using TalkoWeb.Core.Domain.Posts;
using TalkoWeb.Core.Domain.Posts.Handlers;

namespace TalkoWeb.Tests.Core.Domain.Posts
{
    public class SavePostTest
    {
        private readonly DatabaseContext _context;
        private readonly Mock<IMediator> _mediatorMock;

        public SavePostTest()
        {
            // Set up in-memory database
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase("TestDatabase").Options;
            _mediatorMock = new Mock<IMediator>(); // Mock the Mediator
            _context = new DatabaseContext(options, _mediatorMock.Object);

            // Set up the handler
        }

        [Fact]
        public async Task Handle_Should_Save_Comment_To_Database()
        {
            //Given

            //When

            //Then
        }
    }
}
