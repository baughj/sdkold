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
 
using System.IO;

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

            FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader reader = new BinaryReader(stream);

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