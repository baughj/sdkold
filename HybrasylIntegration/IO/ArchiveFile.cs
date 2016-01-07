using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
