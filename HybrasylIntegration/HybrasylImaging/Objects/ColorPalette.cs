using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Hybrasyl.IO;

namespace Hybrasyl.Imaging.Objects
{
    public class ColorPalette
    {
        private Color[] colors = new Color[256];

        public Color this[int index]
        {
            get { return colors[index]; }
            set { colors[index] = value; }
        }
        public Color[] Colors
        {
            get { return colors; }
        }
        private static ColorPalette LoadPalette(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            BinaryReader reader = new BinaryReader(stream);
            ColorPalette pal = new ColorPalette();

            for (int i = 0; i < 256; i++)
            {
                pal.colors[i] = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
            }

            return pal;
        }
        public static ColorPalette FromArchive(string file, Archive archive)
        {
            if (!archive.Contains(file)) return null;

            return FromRawData(archive.ExtractFile(file));
        }
        public static ColorPalette FromArchive(string file, bool ignoreCase, Archive archive)
        {
            if (!archive.Contains(file, ignoreCase)) return null;

            return FromRawData(archive.ExtractFile(file, ignoreCase));
        }
        public static ColorPalette FromFile(string file)
        {
            FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);

            return LoadPalette(stream);
        }
        public static ColorPalette FromRawData(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);

            return LoadPalette(stream);
        }
    }
}
