using System;
using DokuWikiTranslator.Application.Generation.Abstractions;
using DokuWikiTranslator.Application.Generation.Features;
using FluentAssertions;
using Xunit;

namespace DokuWikiTranslator.Application.Tests.Generation.HtmlElementTests
{
    public class GivenHtmlElementWithAttributes
    {
        private readonly HtmlAttribute _sampleAttribute = new HtmlAttribute("sample", "123456");
        private readonly HtmlAttribute _anotherAttribute = new HtmlAttribute("another", "xyz");
        private readonly HtmlAttribute _keyOnlyAttribute = new HtmlAttribute("key-only", null);

        [Fact]
        public void WhenGeneratingCode_ThenEachAttributeKeyIsPresent()
        {
            var givenAttributes = new[] { _sampleAttribute, _anotherAttribute, _keyOnlyAttribute };
            var element = new HtmlElement("element", givenAttributes, Array.Empty<IHtmlSyntaxTreeNode>());

            var resultCode = element.Generate();

            var expectedNames = new[] { _sampleAttribute.Key, _anotherAttribute.Key, _keyOnlyAttribute.Value };
            resultCode.Should().ContainAll(expectedNames);
        }

        [Fact]
        public void WhenGeneratingCode_FirstAttributeKeyAppearsBeforeTheSecondKey()
        {
            var givenAttributes = new[] { _sampleAttribute, _anotherAttribute, _keyOnlyAttribute };
            var element = new HtmlElement("element", givenAttributes, Array.Empty<IHtmlSyntaxTreeNode>());

            var resultCode = element.Generate();

            var firstKeyOccurence = resultCode.IndexOf(_sampleAttribute.Key, StringComparison.Ordinal);
            var secondKeyOccurence = resultCode.IndexOf(_anotherAttribute.Key, StringComparison.Ordinal);
            firstKeyOccurence.Should().BeLessThan(secondKeyOccurence);
        }

        [Fact]
        public void WhenGeneratingCode_SecondAttributeKeyAppearsBeforeTheThirdKey()
        {
            var givenAttributes = new[] { _sampleAttribute, _anotherAttribute, _keyOnlyAttribute };
            var element = new HtmlElement("element", givenAttributes, Array.Empty<IHtmlSyntaxTreeNode>());

            var resultCode = element.Generate();

            var secondKeyOccurence = resultCode.IndexOf(_anotherAttribute.Key, StringComparison.Ordinal);
            var thirdKeyOccurence = resultCode.IndexOf(_keyOnlyAttribute.Key, StringComparison.Ordinal);
            secondKeyOccurence.Should().BeLessThan(thirdKeyOccurence);
        }
    }
}
