using System;
using System.Collections.Generic;
using DokuWikiTranslator.Application.Generation.Abstractions;

namespace DokuWikiTranslator.Application.Generation.Features
{
    public class HtmlElement : SyntaxTreeNode
    {
        private readonly string _elementName;
        private readonly IEnumerable<HtmlAttribute> _attributes;

        public HtmlElement(
            string elementName,
            IEnumerable<HtmlAttribute> attributes,
            IReadOnlyCollection<ISyntaxTreeNode> childNodes
            ) : base(childNodes)
        {
            _elementName = elementName;
            _attributes = attributes;
        }

        public override string Generate()
        {
            throw new NotImplementedException();
        }
    }
}
