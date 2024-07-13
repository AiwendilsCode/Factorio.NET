using Factorio.Modding.Api.Mod;
using FluentAssertions;

namespace Factorio.Modding.Api.Tests.ParsingTests
{
    public class ModDependencyParsingTests
    {
        [Fact]
        public void ShouldParse_hardDependency()
        {
            // Arrange
            var str = "mod-a";

            // Act
            ModDependency.TryParse(str, null, out var parsed).Should().BeTrue();

            // Assert
            parsed!.Name.Should().Be("mod-a");
            parsed!.Kind.Should().Be(DependencyKind.Hard);
            Assert.Null(parsed!.EqualityOperator);
            Assert.Null(parsed!.Version);
        }

        [Fact]
        public void ShouldParse_optionalDependency()
        {
            // Arrange
            var str = "? mod-c > 0.4.3";

            // Act
            ModDependency.TryParse(str, null, out var parsed).Should().BeTrue();

            // Assert
            parsed!.Name.Should().Be("mod-c");
            parsed!.Kind.Should().Be(DependencyKind.Optional);
            parsed!.Version!.Major.Should().Be(0);
            parsed!.Version!.Minor.Should().Be(4);
            parsed!.Version!.Build.Should().Be(3);
            parsed!.EqualityOperator.Should().Be(EqualityOperator.Higher);
        }

        [Fact]
        public void ShouldParse_incompatibleDependency()
        {
            // Arrange
            var str = "! mod-g";

            // Act
            ModDependency.TryParse(str, null, out var parsed).Should().BeTrue();

            // Assert
            parsed!.Name.Should().Be("mod-g");
            parsed!.Kind.Should().Be(DependencyKind.Incompatible);
            Assert.Null(parsed!.EqualityOperator);
            Assert.Null(parsed!.Version);
        }

        [Fact]
        public void ShouldParse_hardDependencyWithVersion()
        {
            // Arrange
            var str = "mod-c > 0.4.3";

            // Act
            ModDependency.TryParse(str, null, out var parsed).Should().BeTrue();

            // Assert
            parsed!.Name.Should().Be("mod-c");
            parsed!.Kind.Should().Be(DependencyKind.Hard);
            parsed!.Version!.Major.Should().Be(0);
            parsed!.Version!.Minor.Should().Be(4);
            parsed!.Version!.Build.Should().Be(3);
            parsed!.EqualityOperator.Should().Be(EqualityOperator.Higher);
        }

        [Fact]
        public void ShouldNotParse_missingEqualityOperator()
        {
            // Arrange
            var str = "mod-c 0.4.3";

            // Act
            ModDependency.TryParse(str, null, out var parsed).Should().BeFalse();

            // Assert
            Assert.Null(parsed);
        }

        [Fact]
        public void ShouldNotParse_missingVersion()
        {
            // Arrange
            var str = "mod-c >";

            // Act
            ModDependency.TryParse(str, null, out var parsed).Should().BeFalse();

            // Assert
            Assert.Null(parsed);
        }
    }
}
