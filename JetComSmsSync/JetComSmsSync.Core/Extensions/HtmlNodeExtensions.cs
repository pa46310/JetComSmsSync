using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace JetComSmsSync.Core.Extensions
{
    public static class HtmlNodeExtensions
    {
        public static string SelectInnerText(this HtmlNode node, string xPath)
        {
            return node.SelectSingleNode(xPath)?.InnerText;
        }

        public static string SelectInnerText(this XmlNode node, string xPath)
        {
            return node.SelectSingleNode(xPath)?.InnerText;
        }
    }
}
