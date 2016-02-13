/*
 * This file is part of Project Hybrasyl.
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the Affero General Public License as published by
 * the Free Software Foundation, version 3.
 *
 * This program is distributed in the hope that it will be useful, but
 * without ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE. See the Affero General Public License
 * for more details.
 *
 * You should have received a copy of the Affero General Public License along
 * with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 * (C) 2016 Project Hybrasyl (info@hybrasyl.com)
 *
 */
 
using Hybrasyl.IO;
using System.Drawing;
using System.IO;

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