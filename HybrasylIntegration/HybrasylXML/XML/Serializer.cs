using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Hybrasyl.XSD;
using System.IO;
using System.Diagnostics;

namespace Hybrasyl.XML
{
    public class Serializer : XMLBase
    {
        public static void Serialize(XmlWriter xWrite, Castable contents)
        {
            XmlSerializer Writer = new XmlSerializer(contents.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            Writer.Serialize(xWrite, contents, ns);
        }
        public static void Serialize(XmlWriter xWrite, Mob contents)
        {
            XmlSerializer Writer = new XmlSerializer(contents.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            Writer.Serialize(xWrite, contents, ns);
        }
        public static void Serialize(XmlWriter xWrite, Dropset contents)
        {
            XmlSerializer Writer = new XmlSerializer(contents.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            Writer.Serialize(xWrite, contents, ns);
        }
        public static void Serialize(XmlWriter xWrite, ItemType contents)
        {
            XmlSerializer Writer = new XmlSerializer(contents.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            Writer.Serialize(xWrite, contents, ns);
        }
        public static void Serialize(XmlWriter xWrite, VariantGroupType contents)
        {
            XmlSerializer Writer = new XmlSerializer(contents.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            Writer.Serialize(xWrite, contents, ns);
        }
        public static void Serialize(XmlWriter xWrite, Map contents)
        {
            XmlSerializer Writer = new XmlSerializer(contents.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            Writer.Serialize(xWrite, contents, ns);
        }
        public static void Serialize(XmlWriter xWrite, WorldMap contents)
        {
            XmlSerializer Writer = new XmlSerializer(contents.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            Writer.Serialize(xWrite, contents, ns);
        }
        public static void Serialize(XmlWriter xWrite, Nation contents)
        {
            XmlSerializer Writer = new XmlSerializer(contents.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            Writer.Serialize(xWrite, contents, ns);
        }
        public static string SerializeToString(object contents)
        {
            XmlSerializer Writer = new XmlSerializer(contents.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            Utf8StringWriter stringWriter = new Utf8StringWriter();
            Writer.Serialize(stringWriter, contents, ns);
            return stringWriter.ToString();
        }
        public static Castable Deserialize(XmlReader reader, Castable contents = null)
        {
            if (contents == null) contents = new Castable();
            //reader.Settings.IgnoreWhitespace = false;
            XmlSerializer XmlSerial = new XmlSerializer(contents.GetType());
            if (XmlSerial.CanDeserialize(reader))
            {
                var xContents = XmlSerial.Deserialize(reader);
                contents = (Castable)xContents;
            }
            return contents;
        }
        public static Mob Deserialize(XmlReader reader, Mob contents = null)
        {
            //reader.Settings.IgnoreWhitespace = false;
            if (contents == null) contents = new Mob();
            XmlSerializer XmlSerial = new XmlSerializer(contents.GetType());
            if (XmlSerial.CanDeserialize(reader))
            {
                var xContents = XmlSerial.Deserialize(reader);
                contents = (Mob)xContents;
            }
            return contents;
        }
        public static Dropset Deserialize(XmlReader reader, Dropset contents = null)
        {
            //reader.Settings.IgnoreWhitespace = false;
            if (contents == null) contents = new Dropset();
            XmlSerializer XmlSerial = new XmlSerializer(contents.GetType());
            if (XmlSerial.CanDeserialize(reader))
            {
                var xContents = XmlSerial.Deserialize(reader);
                contents = (Dropset)xContents;
            }
            return contents;
        }
        public static ItemType Deserialize(XmlReader reader, ItemType contents = null)
        {
            //reader.Settings.IgnoreWhitespace = false;
            if (contents == null) contents = new ItemType();
            XmlSerializer XmlSerial = new XmlSerializer(contents.GetType());
            if (XmlSerial.CanDeserialize(reader))
            {
                var xContents = XmlSerial.Deserialize(reader);
                contents = (ItemType)xContents;
            }
            return contents;
        }
        public static VariantGroupType Deserialize(XmlReader reader, VariantGroupType contents = null)
        {
            //reader.Settings.IgnoreWhitespace = false;
            if (contents == null) contents = new VariantGroupType();
            XmlSerializer XmlSerial = new XmlSerializer(contents.GetType());
            if (XmlSerial.CanDeserialize(reader))
            {
                var xContents = XmlSerial.Deserialize(reader);
                contents = (VariantGroupType)xContents;
            }
            return contents;
        }
        public static Map Deserialize(XmlReader reader, Map contents = null)
        {
            //reader.Settings.IgnoreWhitespace = false;
            if (contents == null) contents = new Map();
            XmlSerializer XmlSerial = new XmlSerializer(contents.GetType());
            if (XmlSerial.CanDeserialize(reader))
            {
                var xContents = XmlSerial.Deserialize(reader);
                contents = (Map)xContents;
            }
            return contents;
        }
        public static WorldMap Deserialize(XmlReader reader, WorldMap contents = null)
        {
            //reader.Settings.IgnoreWhitespace = false;
            if (contents == null) contents = new WorldMap();
            XmlSerializer XmlSerial = new XmlSerializer(contents.GetType());
            if (XmlSerial.CanDeserialize(reader))
            {
                var xContents = XmlSerial.Deserialize(reader);
                contents = (WorldMap)xContents;
            }
            return contents;
        }
        public static Nation Deserialize(XmlReader reader, Nation contents = null)
        {
            //reader.Settings.IgnoreWhitespace = false;
            if (contents == null) contents = new Nation();
            XmlSerializer XmlSerial = new XmlSerializer(contents.GetType());
            if (XmlSerial.CanDeserialize(reader))
            {
                var xContents = XmlSerial.Deserialize(reader);
                contents = (Nation)xContents;
            }
            return contents;
        }
    }
}
