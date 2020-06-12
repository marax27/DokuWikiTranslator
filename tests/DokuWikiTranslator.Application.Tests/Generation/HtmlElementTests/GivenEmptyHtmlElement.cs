using System;
using DokuWikiTranslator.Application.Generation.Abstractions;
using DokuWikiTranslator.Application.Generation.Features;
using FluentAssertions;
using Xunit;

namespace DokuWikiTranslator.Application.Tests.Generation.HtmlElementTests
{
    public class GivenEmptyHtmlElement
    {
        [Fact]
        public void WhenGeneratingCode_ThenExpectedStartingTagIsGenerated()
        {
            var element = new HtmlElement("element", new HtmlAttribute[] { }, Array.Empty<IHtmlSyntaxTreeNode>());
            var actualResult = element.Generate();
            actualResult.Should().StartWith("<element");
        }

        [Fact]
        public void WhenGeneratingCode_ThenClosingTagIsNotGenerated()
        {
            var element = new HtmlElement("element", new HtmlAttribute[] { }, Array.Empty<IHtmlSyntaxTreeNode>());
            var actualResult = element.Generate();
            actualResult.Should().NotEndWith("</element>");
        }

        [Fact]
        public void WhenGeneratingCode_ThenShorthandClosingIsUsed()
        {
            var element = new HtmlElement("element", new HtmlAttribute[] { }, Array.Empty<IHtmlSyntaxTreeNode>());
            var actualResult = element.Generate();
            actualResult.Should().EndWith("/>");
        }
    }
}
