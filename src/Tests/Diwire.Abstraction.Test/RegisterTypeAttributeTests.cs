using Diwire.Abstraction.Attributes;
using Diwire.Container.Test.TestTypes;
using FluentAssertions;
using System;
using Xunit;

namespace Diwire.Abstraction.Test
{
    public class RegisterTypeAttributeTests
    {
        [Fact]
        public void RegisterTypeAttribute_WithFromToTypeMapping_SetsPropertiesCorrect()
        {
            // Arrange
            var expectedFromType = typeof(IDependency1);
            var expectedToType = typeof(Dependency1);
            var expectedLifetime = Lifetime.Transient;
            
            // Act
            var attribute = new RegisterTypeAttribute(expectedFromType, expectedToType, expectedLifetime);

            // Assert
            attribute.FromType.Should().Be(expectedFromType);
            attribute.ToType.Should().Be(expectedToType);
            attribute.Lifetime.Should().Be(expectedLifetime);
        }

        [Fact]
        public void RegisterTypeAttribute_WithSingleTypeMapping_SetsPropertiesCorrect()
        {
            // Arrange
            var expectedType = typeof(Dependency1);
            var expectedLifetime = Lifetime.Singleton;

            // Act
            var attribute = new RegisterTypeAttribute(expectedType,  expectedLifetime);

            // Assert
            attribute.FromType.Should().Be(expectedType);
            attribute.ToType.Should().Be(expectedType);
            attribute.Lifetime.Should().Be(expectedLifetime);
        }

        [Fact]
        public void RegisterTypeAttribute_WithSingleNullTypeMapping_ShouldThrowException()
        {
            // Assert
            Action ctr = () => new RegisterTypeAttribute(null);

            // Act
            ctr.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RegisterTypeAttribute_WithNullFromTypeMapping_ShouldThrowException()
        {
            // Assert
            Action ctr = () => new RegisterTypeAttribute(null, typeof(Dependency1));

            // Act
            ctr.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RegisterTypeAttribute_WithNullToTypeMapping_ShouldThrowException()
        {
            // Assert
            Action ctr = () => new RegisterTypeAttribute(typeof(Dependency1), null);

            // Act
            ctr.Should().Throw<ArgumentNullException>();
        }

    }
}
