using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hybrasyl.Imaging.Objects;
using Hybrasyl.IO;

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
