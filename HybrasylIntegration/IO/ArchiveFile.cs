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
 
namespace Hybrasyl.IO
{
    public class ArchiveFile
    {
        private string name;
        private long startAddress;
        private long endAddress;

        public long FileSize
        {
            get { return endAddress - startAddress; }
        }

        public long EndAddress
        {
            get { return endAddress; }
        }

        public long StartAddress
        {
            get { return startAddress; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public ArchiveFile(string name, long startAddress, long endAddress)
        {
            this.name = name;
            this.startAddress = startAddress;
            this.endAddress = endAddress;
        }
    }
}