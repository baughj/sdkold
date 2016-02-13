using Hybrasyl.IO;
using System;
using System.IO;

namespace Hybrasyl.Imaging
{
    public class HPF
    {
        private int width;
        private int height;
        private byte[] rawData;
        private byte[] headerData;

        public byte[] HeaderData
        {
            get { return headerData; }
        }

        public byte[] RawData
        {
            get { return rawData; }
        }

        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width; }
        }

        public static HPF FromFile(string file)
        {
            FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);

            return LoadHPF(stream);
        }

        public static HPF FromRawData(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);

            return LoadHPF(stream);
        }

        public static HPF FromArchive(string file, Archive archive)
        {
            if (!archive.Contains(file)) return null;

            return FromRawData(archive.ExtractFile(file));
        }

        public static HPF FromArchive(string file, bool ignoreCase, Archive archive)
        {
            if (!archive.Contains(file, ignoreCase)) return null;

            return FromRawData(archive.ExtractFile(file, ignoreCase));
        }

        private static HPF LoadHPF(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            BinaryReader reader = new BinaryReader(stream);
            uint signature = reader.ReadUInt32();
            reader.BaseStream.Seek(-4, SeekOrigin.Current);
            bool decompress = true;
            if (signature != 0xFF02AA55)
            {
                decompress = false;
            }

            HPF hpf = new HPF();

            if (decompress)
            {
                byte[] hpfData = Decompress(reader.ReadBytes((int)reader.BaseStream.Length));
                reader.Close();

                MemoryStream memStream = new MemoryStream(hpfData);
                reader = new BinaryReader(memStream);

                hpf.headerData = reader.ReadBytes(8);
                hpf.rawData = reader.ReadBytes(hpfData.Length - 8);
                reader.Close();
            }
            else
            {
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                hpf.headerData = reader.ReadBytes(8);
                byte[] hpfData = reader.ReadBytes((int)(reader.BaseStream.Length - 8));
                hpf.rawData = hpfData;
                reader.Close();
            }
            hpf.width = 28;

            if (hpf.rawData.Length % hpf.width != 0) return null;

            hpf.height = hpf.rawData.Length / hpf.width;

            return hpf;
        }

        public static byte[] Decompress(string file)
        {
            byte[] hpfBytes = File.ReadAllBytes(file);
            return Decompress(hpfBytes);
        }

        public static byte[] Decompress(byte[] hpfBytes)
        {
            uint k = 7;
            uint val = 0;
            uint val2 = 0;
            uint val3 = 0;
            uint i = 0;
            uint j = 0;
            uint l = 0;
            uint m = 0;

            int hpfSize = hpfBytes.Length;

            byte[] rawBytes = new byte[hpfSize * 10];

            uint[] int_odd = new uint[256];
            uint[] int_even = new uint[256];
            byte[] byte_pair = new byte[513];

            for (i = 0; i < 256; i++)
            {
                int_odd[i] = (2 * i + 1);
                int_even[i] = (2 * i + 2);

                byte_pair[i * 2 + 1] = (byte)i;
                byte_pair[i * 2 + 2] = (byte)i;
            }

            while (val != 0x100)
            {
                val = 0;
                while (val <= 0xFF)
                {
                    if (k == 7) { l++; k = 0; }
                    else
                        k++;

                    if ((hpfBytes[4 + l - 1] & (1 << (int)k)) != 0)
                        val = int_even[val];
                    else
                        val = int_odd[val];
                }
                val3 = val;
                val2 = byte_pair[val];

                while (val3 != 0 && val2 != 0)
                {
                    i = byte_pair[val2];
                    j = int_odd[i];

                    if (j == val2)
                    {
                        j = int_even[i];
                        int_even[i] = val3;
                    }
                    else
                        int_odd[i] = val3;

                    if (int_odd[val2] == val3)
                        int_odd[val2] = j;
                    else
                        int_even[val2] = j;

                    byte_pair[val3] = (byte)i;
                    byte_pair[j] = (byte)val2;
                    val3 = i;
                    val2 = byte_pair[val3];
                }

                val += 0xFFFFFF00;

                if (val != 0x100)
                {
                    rawBytes[m] = (byte)val;
                    m++;
                }
            }

            byte[] finalData = new byte[m];
            Buffer.BlockCopy(rawBytes, 0, finalData, 0, (int)m);

            return finalData;
        }
    }
}