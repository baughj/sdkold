namespace Hybrasyl.Imaging.Objects
{
    public class EPFFrame
    {
        private int left;
        private int top;
        private int width;
        private int height;
        private byte[] rawData;

        public bool IsValid
        {
            get
            {
                if (rawData == null || rawData.Length < 1)
                    return false;
                else if (width < 1 || height < 1)
                    return false;
                else if (width * height != rawData.Length)
                    return false;
                else
                    return true;
            }
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

        public int Top
        {
            get { return top; }
            set { top = value; }
        }

        public int Left
        {
            get { return left; }
            set { left = value; }
        }

        public EPFFrame(int left, int top, int width, int height, byte[] rawData)
        {
            this.left = left;
            this.top = top;
            this.width = width;
            this.height = height;
            this.rawData = rawData;
        }
    }
}