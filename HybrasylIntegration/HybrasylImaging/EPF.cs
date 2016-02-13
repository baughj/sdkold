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

using Hybrasyl.Imaging.Objects;
using System.IO;

namespace Hybrasyl.Imaging
{
    public class EPF
    {
        private int expectedFrames;
        private int width;
        private int height;
        private int unknown;
        private long tocAddress;
        private EPFFrame[] frames;

        public EPFFrame this[int index]
        {
            get { return frames[index]; }
            set { frames[index] = value; }
        }

        public EPFFrame[] Frames
        {
            get { return frames; }
        }

        public long TOCAddress
        {
            get { return tocAddress; }
        }

        public int Unknown
        {
            get { return unknown; }
        }

        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width; }
        }

        public int ExpectedFrames
        {
            get { return expectedFrames; }
        }

        private static EPF LoadEPF(Stream stream)
        {
            // Create Binary Reader
            stream.Seek(0, SeekOrigin.Begin);
            BinaryReader reader = new BinaryReader(stream);

            // Create EPF Image
            EPF epf = new EPF();

            epf.expectedFrames = reader.ReadUInt16();
            epf.width = reader.ReadUInt16();
            epf.height = reader.ReadUInt16();
            epf.unknown = reader.ReadUInt16();
            epf.tocAddress = reader.ReadUInt32() + 0x0C;

            if (epf.expectedFrames > 0)
            {
                epf.frames = new EPFFrame[epf.expectedFrames];
            }
            else
            {
                return epf;
            }

            for (int i = 0; i < epf.expectedFrames; i++)
            {
                reader.BaseStream.Seek(epf.tocAddress + (i * 16), SeekOrigin.Begin);

                int left = reader.ReadUInt16();
                int top = reader.ReadUInt16();
                int right = reader.ReadUInt16();
                int bottom = reader.ReadUInt16();

                int width = right - left;
                int height = bottom - top;

                uint startAddress = reader.ReadUInt32() + 0x0C;
                uint endAddress = reader.ReadUInt32() + 0x0C;

                reader.BaseStream.Seek(startAddress, SeekOrigin.Begin);

                byte[] frameBytes = null;
                if ((endAddress - startAddress) != (width * height))
                {
                    frameBytes = reader.ReadBytes((int)(epf.tocAddress - startAddress));
                }
                else
                {
                    frameBytes = reader.ReadBytes((int)(endAddress - startAddress));
                }

                epf.frames[i] = new EPFFrame(left, top, width, height, frameBytes);
            }
            return epf;
        }
    }
}