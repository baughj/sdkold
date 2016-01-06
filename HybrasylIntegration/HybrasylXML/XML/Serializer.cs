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
        public static void Serialize(XmlWriter xWrite, Item contents)
        {
            XmlSerializer Writer = new XmlSerializer(contents.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            Writer.Serialize(xWrite, contents, ns);
        }
        public static void Serialize(XmlWriter xWrite, VariantGroup contents)
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
        public static Mob Deserialize(XmlReader reader, Mob contents)
        {
            reader.Settings.IgnoreWhitespace = false;
            XmlSerializer XmlSerial = new XmlSerializer(contents.GetType());
            if (XmlSerial.CanDeserialize(reader))
            {
                var xContents = XmlSerial.Deserialize(reader);
                contents = (Mob)xContents;
            }
            return contents;
        }
        public static Dropset Deserialize(XmlReader reader, Dropset contents)
        {
            reader.Settings.IgnoreWhitespace = false;
            XmlSerializer XmlSerial = new XmlSerializer(contents.GetType());
            if (XmlSerial.CanDeserialize(reader))
            {
                var xContents = XmlSerial.Deserialize(reader);
                contents = (Dropset)xContents;
            }
            return contents;
        }
        public static Item Deserialize(XmlReader reader, Item contents)
        {
            reader.Settings.IgnoreWhitespace = false;
            XmlSerializer XmlSerial = new XmlSerializer(contents.GetType());
            if (XmlSerial.CanDeserialize(reader))
            {
                var xContents = XmlSerial.Deserialize(reader);
                contents = (Item)xContents;
            }
            return contents;
        }
        public static VariantGroup Deserialize(XmlReader reader, VariantGroup contents)
        {
            reader.Settings.IgnoreWhitespace = false;
            XmlSerializer XmlSerial = new XmlSerializer(contents.GetType());
            if (XmlSerial.CanDeserialize(reader))
            {
                var xContents = XmlSerial.Deserialize(reader);
                contents = (VariantGroup)xContents;
            }
            return contents;
        }
        public static Map Deserialize(XmlReader reader, Map contents)
        {
            reader.Settings.IgnoreWhitespace = false;
            XmlSerializer XmlSerial = new XmlSerializer(contents.GetType());
            if (XmlSerial.CanDeserialize(reader))
            {
                var xContents = XmlSerial.Deserialize(reader);
                contents = (Map)xContents;
            }
            return contents;
        }
        public static WorldMap Deserialize(XmlReader reader, WorldMap contents)
        {
            reader.Settings.IgnoreWhitespace = false;
            XmlSerializer XmlSerial = new XmlSerializer(contents.GetType());
            if (XmlSerial.CanDeserialize(reader))
            {
                var xContents = XmlSerial.Deserialize(reader);
                contents = (WorldMap)xContents;
            }
            return contents;
        }
        public static Nation Deserialize(XmlReader reader, Nation contents)
        {
            reader.Settings.IgnoreWhitespace = false;
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
