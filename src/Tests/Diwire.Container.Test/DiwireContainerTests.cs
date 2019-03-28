using Diwire.Container.Test.TestTypes;
using FluentAssertions;
using System;
using Xunit;

namespace Diwire.Container.Test
{
    public class DiwireContainerTests
    {
        private readonly DiwireContainer _container;

        public DiwireContainerTests()
        {
            _container = new DiwireContainer();
        }

        [Fact]
        public void RegisterSingelton_WhenTypeAlreadyRegisteredAsSingelton_ShouldThrowException()
        {
            // Arrange
            _container.RegisterSingelton<IDependency1>(_ => new Dependency1());

            // Act & Assert
            _container.Invoking(x => x.RegisterSingelton<IDependency1>(_ => new Dependency1()))
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void RegisterSingelton_WhenTypeAlreadyRegisteredAsTransient_ShouldThrowException()
        {
            // Arrange
            _container.RegisterTransient<IDependency1>(_ => new Dependency1());

            // Act & Assert
            _container.Invoking(x => x.RegisterSingelton<IDependency1>(_ => new Dependency1()))
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void RegisterTransient_WhenTypeAlreadyRegisteredAsTransient_ShouldThrowException()
        {
            // Arrange
            _container.RegisterTransient<IDependency1>(_ => new Dependency1());

            // Act & Assert
            _container.Invoking(x => x.RegisterTransient<IDependency1>(_ => new Dependency1()))
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void RegisterTransient_WhenTypeAlreadyRegisteredAsSingelton_ShouldThrowException()
        {
            // Arrange
            _container.RegisterSingelton<IDependency1>(_ => new Dependency1());

            // Act & Assert
            _container.Invoking(x => x.RegisterTransient<IDependency1>(_ => new Dependency1()))
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Resolve_WhenTypeNotRegistered_ShouldThrowException()
        {
            // Act & Assert
            _container.Invoking(x => x.Resolve<IDependency1>())
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Resolve_WhenRegisterSingelton_ShouldReturnSingelton()
        {
            // Arrange
            _container.RegisterSingelton<IDependency1>(_ => new Dependency1());

            // Act & Assert
            var first = _container.Resolve<IDependency1>();
            var second = _container.Resolve<IDependency1>();

            // Assert
            first.Should().BeSameAs(second);
        }

        [Fact]
        public void Resolve_WhenRegisterTransient_ShouldReturnNewInstance()
        {
            // Arrange
            _container.RegisterTransient<IDependency1>(_ => new Dependency1());

            // Act & Assert
            var first = _container.Resolve<IDependency1>();
            var second = _container.Resolve<IDependency1>();

            // Assert
            first.Should().NotBeSameAs(second);
        }

        [Fact]
        public void Contains_WhenRegisterSingelton_ShouldReturnTrue()
        {
            // Arrange
            _container.RegisterSingelton<IDependency1>(_ => new Dependency1());

            // Assert
            var containsType = _container.Contains<IDependency1>();

            // Assert
            containsType.Should().BeTrue();
        }

        [Fact]
        public void Contains_WhenRegisterTransient_ShouldReturnTrue()
        {
            // Arrange
            _container.RegisterTransient<IDependency1>(_ => new Dependency1());

            // Assert
            var containsType = _container.Contains<IDependency1>();

            // Assert
            containsType.Should().BeTrue();
        }

        [Fact]
        public void Contains_WhenTypeIsNotRegistered_ShouldReturnFalse()
        {
            // Arrange
            _container.RegisterTransient<IDependency1>(_ => new Dependency1());

            // Assert
            var containsType = _container.Contains<IDependency2>();

            // Assert
            containsType.Should().BeFalse();
        }

        [Fact]
        public void Remove_WhenTypeNotRegistered_ShouldReturnFalse()
        {
            // Act
            var wasRemoved = _container.Remove<IDependency1>();

            // Assert
            wasRemoved.Should().BeFalse();
        }

        [Fact]
        public void Remove_WhenTypeRegistered_ShouldRemoveTypeAndReturnTrue()
        {
            // Arrange
            _container.RegisterSingelton<IDependency1>(_ => new Dependency1());

            // Act
            var wasRemoved = _container.Remove<IDependency1>();
            var containsType = _container.Contains<IDependency1>();

            // Assert
            wasRemoved.Should().BeTrue();
            containsType.Should().BeFalse();
        }
    }
}
