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
    public class MapTile
    {
        private ushort floor;
        private ushort leftWall;
        private ushort rightWall;

        public ushort RightWall
        {
            get { return rightWall; }
            set { rightWall = value; }
        }

        public ushort LeftWall
        {
            get { return leftWall; }
            set { leftWall = value; }
        }

        public ushort FloorTile
        {
            get { return floor; }
            set { floor = value; }
        }

        public MapTile(ushort floor, ushort leftWall, ushort rightWall)
        {
            this.floor = floor;
            this.leftWall = leftWall;
            this.rightWall = rightWall;
        }
    }
}