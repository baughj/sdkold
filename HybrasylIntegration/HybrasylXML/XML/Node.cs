using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace Hybrasyl.XML
{
    public static class Node
    {
        public static string Replace(string nodeName, string source, string contents)
        {
            try
            {
                var xContents = XElement.Parse(contents);

                var xTraverse = XElement.Parse(source);
                XElement node = xTraverse.Descendants(nodeName).First();
                node.ReplaceWith(xContents);
                return xTraverse.ToString();
            }
            catch (Exception e)
            {
                //need exception handling
            }
            return source;
        }
    }
}