using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DokuWikiTranslator.Application.DokuWiki.Abstractions;
using DokuWikiTranslator.Application.DokuWiki.Markers;
using DokuWikiTranslator.Application.Generation.Abstractions;
using DokuWikiTranslator.Application.Generation.Features;

namespace DokuWikiTranslator.Application.DokuWiki.Features
{
    public class BasicMarkerNode : DokuWikiTreeNode
    {
        public BasicMarkerNode(string sourceCode, ReadOnlyCollection<IDokuWikiTreeNode> childNodes, IHtmlMarker marker)
            : base(sourceCode, childNodes)
        {
            Marker = marker;
        }

        public IHtmlMarker Marker { get; }

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
