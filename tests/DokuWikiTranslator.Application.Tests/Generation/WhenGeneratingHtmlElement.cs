using DokuWikiTranslator.Application.Generation.Abstractions;
using DokuWikiTranslator.Application.Generation.Features;
using FluentAssertions;
using Xunit;

namespace DokuWikiTranslator.Application.Tests.Generation
{
    public class WhenGeneratingHtmlElement
    {
        [Fact]
        public void GivenEmptyElement_ThenExpectedStartingTagIsGenerated()
        {
            var element = new HtmlElement("element", new HtmlAttribute[] { }, new ISyntaxTreeNode[] { });
            var actualResult = element.Generate();
            actualResult.Should().StartWith("<element");
        }

        [Fact]
        public void GivenEmptyElement_ThenClosingTagIsNotGenerated()
        {
            var element = new HtmlElement("element", new HtmlAttribute[] { }, new ISyntaxTreeNode[] { });
            var actualResult = element.Generate();
            actualResult.Should().NotEndWith("</element>");
        }

        [Fact]
        public void GivenEmptyElement_ThenShorthandClosingIsUsed()
        {
            var element = new HtmlElement("element", new HtmlAttribute[] { }, new ISyntaxTreeNode[] { });
            var actualResult = element.Generate();
            actualResult.Should().EndWith("/>");
        }
    }
}
