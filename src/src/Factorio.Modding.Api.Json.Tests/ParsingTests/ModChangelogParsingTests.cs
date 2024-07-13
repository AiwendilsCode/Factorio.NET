using Factorio.Modding.Api.Mod;
using FluentAssertions;

namespace Factorio.Modding.Api.Tests.ParsingTests
{
    public class ModChangelogParsingTests
    {
        [Fact]
        public void ShouldParse_ComplexChangelog()
        {
            // Arrange
            var changelogStr = @"Version: 1.1.60
Date: 06. 06. 2022
  Features:
    - This is an entry in the ""Features"" category.
    - This is another entry in the ""Features"" category.
    - This general section is the 1.1.60 version section.
  Balancing:
    - This is a multiline entry in the ""Balancing"" category.
      There is some extra text here because it is needed for the example.
      Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
  Bugfixes:
    - Fixed that canceling syncing mods with a save would exit the GUI.
    - Fixed a desync when fast-replacing burner generators.";

            // Act
            ModChangelog.TryParse(changelogStr, null, out var changelog).Should().BeTrue();

            // Assert
            changelog!.Version.Major.Should().Be(1);
            changelog!.Version.Minor.Should().Be(1);
            changelog!.Version.Build.Should().Be(60);
            changelog!.Date.Should().Be(new DateOnly(2022, 6, 6));

            changelog!.Sections.Should().HaveCount(3);

            changelog!.Sections![0].Category.Should().Be("Features");
            changelog!.Sections![0].Entries.Should().HaveCount(3);
            changelog!.Sections![0].Entries[0].Should().Be("This is an entry in the \"Features\" category.");
            changelog!.Sections![0].Entries[1].Should().Be("This is another entry in the \"Features\" category.");
            changelog!.Sections![0].Entries[2].Should().Be("This general section is the 1.1.60 version section.");

            changelog!.Sections![1].Category.Should().Be("Balancing");
            changelog!.Sections![1].Entries.Should().HaveCount(1);
            changelog!.Sections![1].Entries[0].Should().Be(@"This is a multiline entry in the ""Balancing"" category.
There is some extra text here because it is needed for the example.
Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");

            changelog!.Sections![2].Category.Should().Be("Bugfixes");
            changelog!.Sections![2].Entries.Should().HaveCount(2);
            changelog!.Sections![2].Entries[0].Should().Be("Fixed that canceling syncing mods with a save would exit the GUI.");
            changelog!.Sections![2].Entries[1].Should().Be("Fixed a desync when fast-replacing burner generators.");
        }

        [Fact]
        public void ShouldParse_onlyVersion()
        {
            // Arrange
            var changelogStr = "Version: 1.1.60";

            // Act
            ModChangelog.TryParse(changelogStr, null, out var changelog).Should().BeTrue();

            // Assert
            changelog!.Version.Major.Should().Be(1);
            changelog!.Version.Minor.Should().Be(1);
            changelog!.Version.Build.Should().Be(60);
            Assert.Null(changelog!.Date);

            changelog!.Sections.Should().BeEmpty();
        }

        [Fact]
        public void ShouldParse_onlyVersionAndDate()
        {
            // Arrange
            var changelogStr = @"Version: 1.1.60
Date: 06. 06. 2022";

            // Act
            ModChangelog.TryParse(changelogStr, null, out var changelog).Should().BeTrue();

            // Assert
            changelog!.Version.Major.Should().Be(1);
            changelog!.Version.Minor.Should().Be(1);
            changelog!.Version.Build.Should().Be(60);
            changelog!.Date.Should().Be(new DateOnly(2022, 6, 6));

            changelog!.Sections.Should().BeEmpty();
        }

        [Fact]
        public void ShouldNotParse_missingVersion()
        {
            // Arrange
            var changelogStr = "Date: 06. 06. 2022";

            // Act
            ModChangelog.TryParse(changelogStr, null, out var changelog).Should().BeFalse();

            // Assert
            Assert.Null(changelog);
        }

        [Fact]
        public void ShouldNotParse_wrongIndentationInCategory()
        {
            // Arrange
            var changelogStr = @"Version: 1.1.60
Date: 06. 06. 2022
 Features:
    - This is an entry in the ""Features"" category.";

            // Act
            ModChangelog.TryParse(changelogStr, null, out var changelog).Should().BeFalse();

            // Assert
            Assert.Null(changelog);
        }

        [Fact]
        public void ShouldNotParse_wrongIndentationInEntry()
        {
            // Arrange
            var changelogStr = @"Version: 1.1.60
Date: 06. 06. 2022
  Features:
   - This is an entry in the ""Features"" category.";

            // Act
            ModChangelog.TryParse(changelogStr, null, out var changelog).Should().BeFalse();

            // Assert
            Assert.Null(changelog);
        }
    }
}
