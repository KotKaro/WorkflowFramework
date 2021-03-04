using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WorkflowFramework.Domain.ProcessAggregate;

namespace WorkflowFramework.Domain.UnitTests
{
    [TestFixture]
    public class TypeMetadataServiceTests
    {
        [Test]
        public void When_ForResolveMetadataAboutPropertiesClassNotProvided_Expect_ArgumentNullExceptionThrown()
        {
            //Act + Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // ReSharper disable once ObjectCreationAsStatement
                new TypeMetadata(null);
            });
        }

        [Test]
        public void When_MetadataResolvedForTestClass_Expect_OnlyPropertiesWithPublicGetReturned()
        {
            //Act
            var sut = new TypeMetadata(typeof(TestClass));
            
            //Assert
            Assert.That(sut.ValueAccessors.Length, Is.EqualTo(7));
            Assert.That(sut.ValueAccessors.First().Name, Is.EqualTo("Name"));
            Assert.That(sut.ValueAccessors.ElementAt(1).Name, Is.EqualTo("GetHello"));
        }
        
        [Test]
        public void When_GetHelloValueAccessorFound_Expect_GotCorrectArguments()
        {
            //Act
            var sut = new TypeMetadata(typeof(TestClass));
            
            //Assert
            sut.ValueAccessors[1].MethodArguments.Length.Should().Be(1);
            sut.ValueAccessors[1].MethodArguments[0].Name.Should().Be("name");
        }
    }
}