using System;
using System.Collections.Generic;
using DokuWikiTranslator.Application.DokuWiki.Abstractions;
using DokuWikiTranslator.Application.Generation;
using DokuWikiTranslator.Application.Generation.Abstractions;
using DokuWikiTranslator.Application.Generation.Features;

namespace DokuWikiTranslator.Application.DokuWiki.Features
{
    public class MediaNode : IDokuWikiTreeNode
    {
        public MediaNode(string mediaPath, string? description, string sourceCode, MediaAlignment alignment)
        {
            MediaPath = mediaPath;
            Description = description;
            SourceCode = sourceCode;
            Alignment = alignment;
        }

        public string MediaPath { get; }
        public string? Description { get; }
        public string SourceCode { get; }
        public MediaAlignment Alignment { get; }

        public IHtmlSyntaxTreeNode Generate()
        {
            var attributes = new List<HtmlAttribute> { new HtmlAttribute("src", MediaPath) };
            if (Description != null)
            {
                attributes.Add(new HtmlAttribute("title", Description));
                attributes.Add(new HtmlAttribute("alt", Description));
            }

            var style = GetMediaStyle();
            if (style != null)
                attributes.Add((HtmlAttribute) style);

            return new HtmlElement("img", attributes, Array.Empty<IHtmlSyntaxTreeNode>());
        }

        private HtmlAttribute? GetMediaStyle()
        {
            var value = Alignment switch
            {
                MediaAlignment.Left => "float: left",
                MediaAlignment.Center => "display: block; margin: 0 auto",
                MediaAlignment.Right => "float: right",
                _ => null
            };
            return value == null ? (HtmlAttribute?) null : new HtmlAttribute("style", value);
        }

        public IEnumerable<ISyntaxTreeNode<IHtmlSyntaxTreeNode>> Children
            => Array.Empty<ISyntaxTreeNode<IHtmlSyntaxTreeNode>>();
    }
}
