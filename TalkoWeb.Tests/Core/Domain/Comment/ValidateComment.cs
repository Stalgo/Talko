using FluentAssertions;
using FluentValidation.TestHelper;
using TalkoWeb.Core.Domain.Comments.Validators;
using TalkoWeb.Core.Domain.Comments.ValueObjects;
using Xunit;

namespace TalkoWeb.Tests.Core.Domain.Comments
{
    public class CommentValidatorTests
    {
        private readonly AddCommentValidator _validator;

        public CommentValidatorTests()
        {
            _validator = new AddCommentValidator();
        }

        [Fact]
        public void Should_Have_No_Validation_Errors_When_Comment_Is_Valid()
        {
            var validComment = new SaveCommentDTO(Guid.NewGuid(), "This is a valid comment.");

            var result = _validator.TestValidate(validComment);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Should_Have_Validation_Error_When_content_Is_Empty()
        {
            var invalidComment = new SaveCommentDTO(Guid.NewGuid(), "");

            var result = _validator.TestValidate(invalidComment);

            result.ShouldHaveValidationErrorFor(x => x.content).WithErrorMessage("Content is required.");
        }

        [Fact]
        public void Should_Have_Validation_Error_When_content_Is_Too_Long()
        {
            var invalidComment = new SaveCommentDTO(Guid.NewGuid(), new string('a', 501));

            var result = _validator.TestValidate(invalidComment);

            result.ShouldHaveValidationErrorFor(x => x.content).WithErrorMessage("Content must be between 1 and 500 characters.");
        }

        [Fact]
        public void Should_Have_Validation_Error_When_AuthorId_Is_Empty_Guid()
        {
            var invalidComment = new SaveCommentDTO(
                Guid.Empty,
                "This is a comment." // Invalid GUID
            );

            var result = _validator.TestValidate(invalidComment);

            result.ShouldHaveValidationErrorFor(x => x.authorId).WithErrorMessage("AuthorId must be a valid GUID.");
        }
    }
}
