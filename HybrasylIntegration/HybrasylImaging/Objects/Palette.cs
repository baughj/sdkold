using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hybrasyl.Imaging.Objects
{
    public class Palette
    {
        private int min;
        private int max;
        private int palette;

        public int PaletteIndex
        {
            get { return palette; }
            set { palette = value; }
        }
        public int Max
        {
            get { return max; }
            set { max = value; }
        }
        public int Min
        {
            get { return min; }
            set { min = value; }
        }
        public Palette(int min, int max, int palette)
        {
            this.min = min;
            this.max = max;
            this.palette = palette;
        }
    }
}
