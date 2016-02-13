using Hybrasyl.Imaging.Objects;
using System.IO;

namespace Hybrasyl.Imaging
{
    public class Map
    {
        private int _width;
        private int _height;
        private MapTile[] _tiles;
        private int _id;
        private string _name;

        public MapTile this[int x, int y]
        {
            get { return _tiles[y * _width + x]; }
            set { _tiles[y * _width + x] = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public MapTile[] Tiles
        {
            get { return _tiles; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private static Map LoadMap(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            BinaryReader reader = new BinaryReader(stream);

            int tileCount = (int)(reader.BaseStream.Length / 6);

            Map map = new Map();
            map._tiles = new MapTile[tileCount];

            for (int i = 0; i < tileCount; i++)
            {
                ushort floor = reader.ReadUInt16();
                ushort leftWall = reader.ReadUInt16();
                ushort rightWall = reader.ReadUInt16();

                map._tiles[i] = new MapTile(floor, leftWall, rightWall);
            }
            reader.Close();

            return map;
        }
    }
}