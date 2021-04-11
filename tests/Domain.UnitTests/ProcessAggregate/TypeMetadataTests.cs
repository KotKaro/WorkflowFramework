using System;
using System.Linq;
using Domain.ProcessAggregate;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.ProcessAggregate
{
    public class TypeMetadataServiceTests
    {
        [Fact]
        public void When_ForResolveMetadataAboutPropertiesClassNotProvided_Expect_ArgumentNullExceptionThrown()
        {
            //Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new TypeMetadata(null);
            });
        }

        [Fact]
        public void When_MetadataResolvedForTestClass_Expect_OnlyPropertiesWithPublicGetReturned()
        {
            //Act
            var sut = new TypeMetadata(typeof(TestClass));
            
            //Assert
            sut.ValueAccessors.Any(x => x.Name == nameof(TestClass.Name)).Should().Be(true);
            sut.ValueAccessors.Any(x => x.Name == nameof(TestClass.Number)).Should().Be(true);
            sut.ValueAccessors.Any(x => x.Name == nameof(TestClass.GetHello)).Should().Be(true);
        }
        
        [Fact]
        public void When_GetHelloValueAccessorFound_Expect_GotCorrectArguments()
        {
            //Act
            var sut = new TypeMetadata(typeof(TestClass));
            
            //Assert
            var getHelloValueAccessor = sut.ValueAccessors
                .First(x => x.Name == nameof(TestClass.GetHello));
            
            getHelloValueAccessor.MethodArguments.Count.Should().Be(1);
            getHelloValueAccessor.MethodArguments.ElementAt(0).Name.Should().Be("name");
        }

        [Fact]
        public void When_TypeMetadataCreatedUsingTypeNameAndAssemblyName_Expect_InstanceToHaveTypeValueAssigned()
        {
            //Arrange
            var typeName = typeof(TestClass).FullName;
            var assemblyName = typeof(TestClass).Assembly.FullName;
            
            //Act
            var sut = new TypeMetadata(typeName, assemblyName);
            
            //Assert
            sut.Type.Should().NotBeNull();
            sut.ValueAccessors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void When_TypeMetadataCreatedUsingTypeNameAndAssemblyName_Expect_ArgumentExceptionWhileCannotFindTypeByProvidedData()
        {
            //Arrange
            var typeName = typeof(TestClass).FullName + "wrong";
            var assemblyName = typeof(TestClass).Assembly.FullName + "wrong";
            
            //Act + Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new TypeMetadata(typeName, assemblyName);
            });
        }
        
        [Theory]
        [InlineData(null, "wrong")]
        [InlineData("", "wrong")]
        [InlineData(" ", "wrong")]
        [InlineData("wrong", null)]
        [InlineData("wrong", "")]
        [InlineData("wrong", " ")]
        public void When_TypeMetadataCreatedUsingTypeNameAndAssemblyName_Expect_ArgumentNullExceptionIfAnyOfArgumentIsNullOrWhitespace(
            string typeName,
            string assemblyName
            )
        {
            //Act + Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new TypeMetadata(typeName, assemblyName);
            });
        }
    }
}