using DokuWikiTranslator.Application.Generation.Features;
using FluentAssertions;
using Xunit;

namespace DokuWikiTranslator.Application.Tests.Generation
{
    public class WhenGeneratingHtmlAttribute
    {
        [Theory]
        [InlineData("key", "value", "key='value'")]
        [InlineData("src", "httpz://example.com?q=123", "src='httpz://example.com?q=123'")]
        public void GivenSampleAttribute_ThenExpectedCodeIsGenerated(
            string givenKey, string givenValue, string expectedResult)
        {
            var attribute = new HtmlAttribute(givenKey, givenValue);

            var actualResult = attribute.GenerateCode();

            actualResult.Should().Be(expectedResult);
        }

        [Fact]
        public void GivenNullAttributeValue_ThenOutputContainsOnlyKey()
        {
            var attribute = new HtmlAttribute("key", null);

            var actualResult = attribute.GenerateCode();

            actualResult.Should().Be("key");
        }
    }
}
