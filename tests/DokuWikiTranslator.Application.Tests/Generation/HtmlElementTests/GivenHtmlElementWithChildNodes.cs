using System;
using System.Collections.Generic;
using DokuWikiTranslator.Application.Generation.Abstractions;
using DokuWikiTranslator.Application.Generation.Features;
using FluentAssertions;
using Xunit;

namespace DokuWikiTranslator.Application.Tests.Generation.HtmlElementTests
{
    public class GivenHtmlElementWithChildNodes
    {
        private readonly HtmlElement _sampleChild = new HtmlElement("sample", new HtmlAttribute[]{}, new ISyntaxTreeNode[]{});
        private readonly HtmlElement _anotherChild = new HtmlElement("another", new HtmlAttribute[]{}, new ISyntaxTreeNode[]{});

        [Fact]
        public void WhenGeneratingCode_ThenEachChildAppearsInCode()
        {
            var givenChildren = new[] {_sampleChild, _anotherChild};
            var element = new HtmlElement("element", new HtmlAttribute[] { }, givenChildren);

            var resultCode = element.Generate();

            var expectedNames = new[] {_sampleChild.Name, _anotherChild.Name};
            resultCode.Should().ContainAll(expectedNames);
        }

        [Fact]
        public void WhenGeneratingCode_ThenFirstChildAppearsBeforeSecondChild()
        {
            var givenChildren = new[] { _sampleChild, _anotherChild };
            var element = new HtmlElement("element", new HtmlAttribute[] { }, givenChildren);

            var resultCode = element.Generate();
            var firstChildOccurence = resultCode.IndexOf(_sampleChild.Name, StringComparison.Ordinal);
            var secondChildOccurence = resultCode.IndexOf(_anotherChild.Name, StringComparison.Ordinal);

            firstChildOccurence.Should().BeLessThan(secondChildOccurence);
        }
    }
}
