using System;
using System.Collections.Generic;
using System.Linq;
using DokuWikiTranslator.Application.Generation.Abstractions;

namespace DokuWikiTranslator.Application.Generation.Features
{
    public class HtmlElement : HtmlSyntaxTreeNode
    {
        public string Name { get; }
        
        private readonly IEnumerable<HtmlAttribute> _attributes;

        public HtmlElement(
            string elementName,
            IEnumerable<HtmlAttribute> attributes,
            IReadOnlyCollection<IHtmlSyntaxTreeNode> childNodes
            ) : base(childNodes)
        {
            Name = elementName;
            _attributes = attributes;
        }

        public override string Generate()
        {
            // Two possible scenarios:
            // 1. <abc attributes>body</abc>
            // 2. <abc attributes />
            var attributes = GetAttributesCode();
            var body = GetBody();

            return string.IsNullOrEmpty(body)
                ? $"<{Name} {attributes} />"
                : $"<{Name} {attributes}>{body}</{Name}>";
        }

        private string GetAttributesCode()
            => string.Join(' ', _attributes.Select(attr => attr.GenerateCode()));

        private string GetBody()
            => Children.Any()
                ? string.Join("\n", Children.Select(node => node.Generate()))
                : "";
    }
}
