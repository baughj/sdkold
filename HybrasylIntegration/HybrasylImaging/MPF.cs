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
    public class MPF
    {
        private int expectedFrames;
        private int width;
        private int height;
        private MPFFrame[] frames;
        private uint expectedDataSize;
        private ushort unknown;
        private uint ffUnknown;
        private bool isNewFormat;
        private bool isFFFormat;
        private byte[] extendedHeader;

        public MPFFrame this[int index]
        {
            get { return frames[index]; }
            set { frames[index] = value; }
        }

        public byte[] ExtendedHeader
        {
            get { return extendedHeader; }
        }

        public bool IsFFFormat
        {
            get { return isFFFormat; }
        }

        public bool IsNewFormat
        {
            get { return isNewFormat; }
        }

        public uint FFUnknown
        {
            get { return ffUnknown; }
        }

        public ushort Unknown
        {
            get { return unknown; }
        }

        public uint ExpectedDataSize
        {
            get { return expectedDataSize; }
        }

        public MPFFrame[] Frames
        {
            get { return frames; }
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

        private static MPF LoadMPF(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            BinaryReader reader = new BinaryReader(stream);
            MPF mpf = new MPF();

            if (reader.ReadUInt32() == 0xFFFFFFFF)
            {
                mpf.isFFFormat = true;
                mpf.ffUnknown = reader.ReadUInt32();
            }
            else
            {
                reader.BaseStream.Seek(-4, SeekOrigin.Current);
            }
            mpf.expectedFrames = reader.ReadByte();
            mpf.frames = new MPFFrame[mpf.expectedFrames];

            mpf.width = reader.ReadUInt16();
            mpf.height = reader.ReadUInt16();
            mpf.expectedDataSize = reader.ReadUInt32();
            mpf.unknown = reader.ReadUInt16();
            mpf.isNewFormat = (reader.ReadUInt16() == 0xFFFF);

            if (mpf.isNewFormat)
                mpf.extendedHeader = reader.ReadBytes(10);
            else
                mpf.extendedHeader = reader.ReadBytes(4);

            long dataStart = reader.BaseStream.Length - mpf.expectedDataSize;

            for (int i = 0; i < mpf.expectedFrames; i++)
            {
                int left = reader.ReadUInt16();
                int top = reader.ReadUInt16();
                int right = reader.ReadUInt16();
                int bottom = reader.ReadUInt16();
                int width = right - left;
                int height = bottom - top;
                int xOffset = reader.ReadUInt16();
                int yOffset = reader.ReadUInt16();

                xOffset = ((xOffset % 256) << 8) + (xOffset / 256);
                yOffset = ((yOffset % 256) << 8) + (yOffset / 256);

                long startAddress = reader.ReadUInt32();

                byte[] frameData = null;

                if (height > 0 && width > 0)
                {
                    long position = reader.BaseStream.Position;
                    reader.BaseStream.Seek(dataStart + startAddress, SeekOrigin.Begin);
                    frameData = reader.ReadBytes(height * width);
                    reader.BaseStream.Seek(position, SeekOrigin.Begin);
                }

                mpf.frames[i] = new MPFFrame(left, top, width, height, xOffset, yOffset, frameData);
            }
            reader.Close();
            return mpf;
        }
    }
}