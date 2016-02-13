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