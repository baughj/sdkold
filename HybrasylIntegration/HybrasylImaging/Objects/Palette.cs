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