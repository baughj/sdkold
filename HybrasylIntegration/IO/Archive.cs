using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hybrasyl.IO
{
    public class Archive
    {
        private int expectedFiles;
        private ArchiveFile[] files;
        private string filename;
        public ArchiveFile this[int index]
        {
            get { return files[index]; }
            set { files[index] = value; }
        }
        public string FileName
        {
            get { return filename; }
            set { filename = value; }
        }
        public ArchiveFile[] Files
        {
            get { return files; }
        }
        public int ExpectedFiles
        {
            get { return expectedFiles; }
        }
        public bool Contains(string name)
        {
            foreach (ArchiveFile file in files)
            {
                if (file.Name == name)
                    return true;

            }
            return false;
        }
        public bool Contains(string name, bool ignoreCase)
        {
            foreach (ArchiveFile file in files)
            {
                if (ignoreCase)
                {
                    if (file.Name.ToUpper() == name.ToUpper())
                        return true;
                }
                else
                {
                    if (file.Name == name)
                        return true;
                }

            }
            return false;
        }
        public int IndexOf(string name)
        {
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Name == name)
                    return i;
            }
            return -1;
        }
        public int IndexOf(string name, bool ignoreCase)
        {
            for (int i = 0; i < files.Length; i++)
            {
                if (ignoreCase)
                {
                    if (files[i].Name.ToUpper() == name.ToUpper())
                        return i;
                }
                else
                {
                    if (files[i].Name == name)
                        return i;
                }
            }
            return -1;
        }
        public byte[] ExtractFile(string name)
        {
            if (!Contains(name)) return null;

            #region Create File Stream and Reader
            FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader reader = new BinaryReader(stream);
            #endregion

            int index = IndexOf(name);
            reader.BaseStream.Seek(files[index].StartAddress, SeekOrigin.Begin);
            byte[] fileData = reader.ReadBytes((int)files[index].FileSize);
            reader.Close();

            return fileData;
        }
        public byte[] ExtractFile(string name, bool ignoreCase)
        {
            if (!Contains(name, ignoreCase)) return null;

            FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);

            BinaryReader reader = new BinaryReader(stream);
            int index = IndexOf(name, ignoreCase);
            reader.BaseStream.Seek(files[index].StartAddress, SeekOrigin.Begin);
            byte[] fileData = reader.ReadBytes((int)files[index].FileSize);
            reader.Close();

            return fileData;
        }
        public byte[] ExtractFile(ArchiveFile entry)
        {
            if (!Contains(entry.Name)) return null;

            FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);

            BinaryReader reader = new BinaryReader(stream);
            reader.BaseStream.Seek(entry.StartAddress, SeekOrigin.Begin);
            byte[] fileData = reader.ReadBytes((int)entry.FileSize);
            reader.Close();

            return fileData;
        }
    }
}
