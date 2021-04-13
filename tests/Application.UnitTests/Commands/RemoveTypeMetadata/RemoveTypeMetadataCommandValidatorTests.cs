using Application.Commands.RemoveTypeMetadata;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Commands.RemoveTypeMetadata
{
    public class RemoveTypeMetadataCommandValidatorTests
    {
        [Theory]
        [InlineData("test", "")]
        [InlineData("test", " ")]
        [InlineData("test", null)]
        [InlineData("", "test")]
        [InlineData(" ", "test")]
        [InlineData(null, "test")]
        public void When_CommandIsInvalid_Expect_ValidationFail(string typeFullName, string assemblyFullName)
        {
            //Arrange
            var sut = new RemoveTypeMetadataCommandValidator();

            //Act
            var result = sut.Validate(new Application.Commands.RemoveTypeMetadata.RemoveTypeMetadataCommand
            {
                TypeFullName = typeFullName,
                AssemblyFullName = assemblyFullName
            });
            
            //Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void When_CommandIsValid_Expect_ValidationNotFail()
        {
            //Arrange
            var sut = new RemoveTypeMetadataCommandValidator();

            //Act
            var result = sut.Validate(new Application.Commands.RemoveTypeMetadata.RemoveTypeMetadataCommand
            {
                TypeFullName = typeof(RemoveTypeMetadataCommandValidator).FullName,
                AssemblyFullName = typeof(RemoveTypeMetadataCommandValidator).Assembly.FullName
            });
            
            //Assert
            result.IsValid.Should().BeTrue();
        }
        
        [Fact]
        public void When_TypeDoesNotExists_Expect_ValidationFail()
        {
            //Arrange
            var sut = new RemoveTypeMetadataCommandValidator();

            //Act
            var result = sut.Validate(new Application.Commands.RemoveTypeMetadata.RemoveTypeMetadataCommand
            {
                TypeFullName = "test",
                AssemblyFullName = "test"
            });
            
            //Assert
            result.IsValid.Should().BeFalse();
        }
    }
}