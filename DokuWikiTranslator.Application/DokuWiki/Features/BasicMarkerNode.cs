using System;
using System.Collections.Generic;
using System.Linq;
using DokuWikiTranslator.Application.DokuWiki.Abstractions;
using DokuWikiTranslator.Application.Generation.Abstractions;
using DokuWikiTranslator.Application.Generation.Features;

namespace DokuWikiTranslator.Application.DokuWiki.Features
{
    public class BasicMarkerNode : DokuWikiTreeNode
    {
        public BasicMarkerNode(string sourceCode, IReadOnlyCollection<DokuWikiTreeNode> childNodes, IDokuWikiMarker marker)
            : base(sourceCode, childNodes)
        {
            Marker = marker;
        }

        public IDokuWikiMarker Marker { get; }

        public override IHtmlSyntaxTreeNode Generate()
        {
            var correspondingNode = new HtmlElement(
                Marker.HtmlTag,
                Array.Empty<HtmlAttribute>(),
                ProcessChildrenRecursively().ToList().AsReadOnly());
            return correspondingNode;
        }
    }
}
