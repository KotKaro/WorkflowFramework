using Application.Commands.CreateTypeMetadata;
using Application.Commands.RemoveTypeMetadata;
using FluentAssertions;
using Xunit;

namespace Application.UnitTests.Commands.AddTypeMetadata
{
    public class AddTypeMetadataCommandValidatorTests
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
            var sut = new CreateTypeMetadataCommandValidator();

            //Act
            var result = sut.Validate(new CreateTypeMetadataCommand
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
            var sut = new CreateTypeMetadataCommandValidator();

            //Act
            var result = sut.Validate(new CreateTypeMetadataCommand
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
            var sut = new CreateTypeMetadataCommandValidator();

            //Act
            var result = sut.Validate(new CreateTypeMetadataCommand
            {
                TypeFullName = "test",
                AssemblyFullName = "test"
            });
            
            //Assert
            result.IsValid.Should().BeFalse();
        }
    }
}