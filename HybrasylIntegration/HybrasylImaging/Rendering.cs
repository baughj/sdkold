using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
//using System.Drawing.Imaging;
using Hybrasyl.Imaging.Objects;
using System.Runtime.InteropServices;
using Hybrasyl.IO;

namespace Hybrasyl.Imaging
{
    public enum ImageType
    {
        EPF,
        MPF,
        HPF,
        SPF,
        EFA,
        ZP,
        Tile
    }
    public class Rendering
    {
        public static int vscroll = 0;
        public static int hscroll = 0;

        public static Dictionary<int, Bitmap> cachedFloor = new Dictionary<int, Bitmap>();
        public static Dictionary<int, Bitmap> cachedWalls = new Dictionary<int, Bitmap>();
        public static Dictionary<int, Bitmap> cachedFloorLight = new Dictionary<int, Bitmap>();
        public static Dictionary<int, Bitmap> cachedWallsLight = new Dictionary<int, Bitmap>();

        public static int displaywidth = 560;
        public static int displayheight = 480;

        public static bool bgShow = true;
        public static bool fg1Show = true;
        public static bool fg2Show = true;

        public static int selectionXStart = -1;
        public static int selectionYStart = -1;
        public static int selectionXEnd = -1;
        public static int selectionYEnd = -1;

        public static int editMode = 0;

        public static int clickmode = 0;

        public static int MouseHoverX = -1;
        public static int MouseHoverY = -1;

        public static int selectionWidth;
        public static MapTile[] selection;

        public static bool drawEmptyWalls = false;
        public static byte[] sotp;
        public static bool tabMap = false;
        public static bool drawGridLines = false;
        public static bool transparency = false;

        public Bitmap RenderImage(HPF hpf, ColorPalette palette)
        {
            return SimpleRender(hpf.Width, hpf.Height, hpf.RawData, palette, ImageType.HPF);
        }
        public Bitmap RenderImage(EPFFrame epf, ColorPalette palette)
        {
            return SimpleRender(epf.Width, epf.Height, epf.RawData, palette, ImageType.EPF);
        }
        public Bitmap RenderImage(MPFFrame mpf, ColorPalette palette)
        {
            return SimpleRender(mpf.Width, mpf.Height, mpf.RawData, palette, ImageType.MPF);
        }
        public Bitmap RenderTile(byte[] tileData, ColorPalette palette)
        {
            return SimpleRender(Tileset.TileWidth, Tileset.TileHeight, tileData, palette, ImageType.Tile);
        }
        private Bitmap SimpleRender(int width, int height, byte[] data, ColorPalette palette, ImageType type)
        {
            Bitmap image = new Bitmap(width, height);

            System.Drawing.Imaging.BitmapData bmd = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, image.PixelFormat);

            for (int y = 0; y < bmd.Height; y++)
            {

                byte[] row = new byte[Marshal.SizeOf(bmd.Scan0 + (y * bmd.Stride))];
                Marshal.Copy((bmd.Scan0 + (y * bmd.Stride)), row, 0, Marshal.SizeOf(bmd.Scan0 + (y * bmd.Stride)));

                for (int x = 0; x < bmd.Width; x++)
                {
                    int colorIndex = 0;
                    if (type == ImageType.EPF)
                    {
                        colorIndex = data[x * height + y];
                    }
                    else
                    {
                        colorIndex = data[y * width + x];
                    }
                    if (colorIndex > 0)
                    {
                        if (bmd.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb)
                        {
                            row[x * 4] = palette[colorIndex].B;
                            row[x * 4 + 1] = palette[colorIndex].G;
                            row[x * 4 + 2] = palette[colorIndex].R;
                            row[x * 4 + 3] = palette[colorIndex].A;
                        }
                        else if (bmd.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
                        {
                            row[x * 3] = palette[colorIndex].B;
                            row[x * 3 + 1] = palette[colorIndex].G;
                            row[x * 3 + 2] = palette[colorIndex].R;
                        }
                        else if (bmd.PixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppRgb555)
                        {
                            ushort colorWORD = (ushort)(((palette[colorIndex].R & 248) << 7) + ((palette[colorIndex].G & 248) << 2) + (palette[colorIndex].B >> 3));

                            row[x * 2] = (byte)(colorWORD % 256);
                            row[x * 2 + 1] = (byte)(colorWORD / 256);
                        }
                        else if (bmd.PixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppRgb565)
                        {
                            ushort colorWORD = (ushort)(((palette[colorIndex].R & 248) << 8) + ((palette[colorIndex].G & 252) << 3) + (palette[colorIndex].B >> 3));

                            row[x * 2] = (byte)(colorWORD % 256);
                            row[x * 2 + 1] = (byte)(colorWORD / 256);
                        }
                    }
                }
            }
            image.UnlockBits(bmd);

            if (type == ImageType.EPF)
            {
                image.RotateFlip(RotateFlipType.Rotate90FlipX);
            }

            return image;
        }
        public Bitmap RenderMap(Map map, Tileset tiles, PaletteTable tileTable, PaletteTable wallTable, Archive wallSource)
        {
            long debug_initStart = DateTime.Now.Ticks;
            int additionalTop = 0;//256, 
            int additionalBottom = 0;// 96; 


            int sxs = selectionXStart;
            int sys = selectionYStart;
            int sxe = selectionXEnd;
            int sye = selectionYEnd;

            int xMin = sxs < sxe ? sxs : sxe;
            int yMin = sys < sye ? sys : sye;
            int xMax = sxs > sxe ? sxs : sxe;
            int yMax = sys > sye ? sys : sye;

            Bitmap mapImage = new Bitmap(displaywidth, displayheight);
            if (map == null) return mapImage;

            Graphics g = Graphics.FromImage(mapImage);

            int xOrigin = ((mapImage.Width / 2) - 1) - Tileset.TileWidth / 2 + 1 - hscroll;
            int yOrigin = -vscroll;

            bool[,] collision = null;
            if (tabMap)
            {
                collision = new bool[map.Width, map.Height];
            }
            int xMul = Tileset.TileWidth / 2;
            int yxMultiplier = (Tileset.TileHeight + 1) / 2;

            if (bgShow)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    for (int x = 0; x < map.Width; x++)
                    {
                        int xd = xOrigin + x * xMul;
                        int yd = yOrigin + x * yxMultiplier;
                        if (xd >= -Tileset.TileWidth && yd >= -Tileset.TileHeight && xd < displaywidth && yd < displayheight)
                        {
                            int floor = map[x, y].FloorTile;

                            bool showSelection = false;

                            if (editMode == 0 || editMode == 3)
                            {
                                if (clickmode == 0 && selection != null && MouseHoverX != -1)
                                {
                                    int selectionHeight = selection.Length / selectionWidth;
                                    if (x >= MouseHoverX && x < MouseHoverX + selectionWidth && y >= MouseHoverY && y < MouseHoverY + selectionHeight)
                                    {
                                        int _x = x - MouseHoverX;
                                        int _y = y - MouseHoverY;
                                        floor = selection[_x + _y * selectionWidth].FloorTile;
                                        showSelection = true;
                                    }
                                }
                            }
                            if (floor > 0) floor -= 1;
                            if (!cachedFloor.ContainsKey(floor))
                            {
                                if (floor >= 0 && floor < tiles.TileCount)
                                {
                                    Bitmap floorTile = RenderTile(tiles[floor], tileTable[floor + 2]);
                                    cachedFloor.Add(floor, floorTile);
                                }
                            }
                            Bitmap thisTile = cachedFloor[floor];
                            if (editMode == 0 || editMode == 3)
                            {
                                if (showSelection || (selectionXStart != -1 && x >= xMin && x <= xMax && y >= yMin && y <= yMax))
                                {
                                    if (!cachedFloorLight.ContainsKey(floor))
                                    {
                                        if (floor >= 0 && floor < tiles.TileCount)
                                        {
                                            thisTile = RenderTile(tiles[floor], tileTable[floor + 2]);
                                            for (int yy = 0; yy < thisTile.Height; yy++)
                                            {
                                                for (int xx = 0; xx < thisTile.Width; xx++)
                                                {
                                                    Color c = thisTile.GetPixel(xx, yy);
                                                    int colorR = c.R + 25;
                                                    if (colorR > 255)
                                                        colorR = 255;
                                                    int colorG = c.G + 25;
                                                    if (colorG > 255)
                                                        colorG = 255;
                                                    int colorB = c.B + 50;
                                                    if (colorB > 255)
                                                        colorB = 255;
                                                    if (c.R != 0 || c.G != 0 || c.B != 0)
                                                        thisTile.SetPixel(xx, yy, Color.FromArgb(colorR, colorG, colorB));
                                                }

                                            }
                                            cachedFloorLight.Add(floor, thisTile);
                                        }
                                    }
                                    else
                                    {
                                        thisTile = cachedFloorLight[floor];
                                    }
                                }
                            }
                            g.DrawImageUnscaled(thisTile,
                            xd,
                            yd);
                        }
                    }
                    xOrigin -= xMul;
                    yOrigin += yxMultiplier;
                }
            }
            xOrigin = ((mapImage.Width / 2) - 1) - Tileset.TileWidth / 2 + 1 - hscroll;
            yOrigin = -vscroll;

            int yAdd = -150 + (Tileset.TileHeight + 1) / 2;
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    int xd = xOrigin + x * xMul;
                    int yd = yOrigin + (x + 1) * yxMultiplier + yAdd;

                    if (xd >= -Tileset.TileWidth && yd >= -200 && xd < displaywidth && yd < displayheight + 200)
                    {
                        int leftWall = map[x, y].LeftWall;
                        if (fg1Show)
                        {
                            bool showSelection = false;

                            if (editMode == 1 || editMode == 3 || editMode == 4)
                            {
                                if (clickmode == 0 && selection != null && MouseHoverX != -1)
                                {
                                    int selectionHeight = selection.Length / selectionWidth;
                                    if (x >= MouseHoverX && x < MouseHoverX + selectionWidth && y >= MouseHoverY && y < MouseHoverY + selectionHeight)
                                    {
                                        int _x = x - MouseHoverX;
                                        int _y = y - MouseHoverY;
                                        int _leftWall = selection[_x + _y * selectionWidth].LeftWall;
                                        if (_leftWall > 0 || drawEmptyWalls)
                                        {
                                            leftWall = _leftWall;
                                            showSelection = true;
                                        }
                                    }
                                }
                            }

                            if (leftWall >= 13)
                            {

                                if (!cachedWalls.ContainsKey(leftWall))
                                {
                                    HPF hpf = HPF.FromArchive("stc" + leftWall.ToString().PadLeft(5, '0') + ".hpf", true, wallSource);
                                    Bitmap wall = RenderImage(hpf, wallTable[leftWall + 1]);
                                    cachedWalls.Add(leftWall, wall);
                                }

                                Bitmap thisTile = cachedWalls[leftWall];
                                if (editMode == 1 || editMode == 3 || editMode == 4)
                                {
                                    if (showSelection || (selectionXStart != -1 && x >= xMin && x <= xMax && y >= yMin && y <= yMax))
                                    {
                                        if (!cachedWallsLight.ContainsKey(leftWall))
                                        {
                                            HPF hpf = HPF.FromArchive("stc" + leftWall.ToString().PadLeft(5, '0') + ".hpf", true, wallSource);
                                            thisTile = RenderImage(hpf, wallTable[leftWall + 1]);
                                            for (int yy = 0; yy < thisTile.Height; yy++)
                                            {
                                                for (int xx = 0; xx < thisTile.Width; xx++)
                                                {
                                                    Color c = thisTile.GetPixel(xx, yy);
                                                    int colorR = c.R + 25;
                                                    if (colorR > 255) colorR = 255;
                                                    int colorG = c.G + 25;
                                                    if (colorG > 255) colorG = 255;
                                                    int colorB = c.B + 50;
                                                    if (colorB > 255) colorB = 255;
                                                    if (c.R != 0 || c.G != 0 || c.B != 0) thisTile.SetPixel(xx, yy, Color.FromArgb(colorR, colorG, colorB));
                                                }

                                            }
                                            cachedWallsLight.Add(leftWall, thisTile);
                                        }
                                        else
                                        {
                                            thisTile = cachedWallsLight[leftWall];
                                        }
                                    }
                                }

                                if ((leftWall % 10000) > 1)
                                {
                                    int xleft = xOrigin + x * xMul;
                                    int yleft = yOrigin + (x + 1) * yxMultiplier -
                                                    thisTile.Height +
                                                    (Tileset.TileHeight + 1) / 2;

                                    if (transparency && leftWall > 0 && leftWall - 1 < sotp.Length && (sotp[leftWall - 1] & 0x80) == 0x80)
                                    {
                                        // gotta be a better way to do this - temporary
                                        for (int yy = 0; yy < thisTile.Height; yy++)
                                        {
                                            for (int xx = 0; xx < thisTile.Width; xx++)
                                            {
                                                int origX = xleft + xx;
                                                int origY = yleft + yy;
                                                if (origX >= 0 && origX < mapImage.Width && origY >= 0 && origY < mapImage.Height)
                                                {
                                                    Color newC = thisTile.GetPixel(xx, yy);
                                                    if (newC.R != 0 || newC.G != 0 || newC.B != 0)
                                                    {
                                                        Color origC = mapImage.GetPixel(origX, origY);
                                                        int _r = origC.R + newC.R;
                                                        if (_r > 255) _r = 255;
                                                        int _g = origC.G + newC.G;
                                                        if (_g > 255) _g = 255;
                                                        int _b = origC.B + newC.B;
                                                        if (_b > 255) _b = 255;
                                                        Color combined = Color.FromArgb(_r, _g, _b);
                                                        mapImage.SetPixel(xleft + xx, yleft + yy, combined);
                                                    }
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        g.DrawImageUnscaled(thisTile, xleft, yleft);
                                    }
                                }
                            }
                        }

                        if (tabMap && leftWall > 0 && leftWall - 1 < sotp.Length && sotp[leftWall - 1] == 0x0F)
                        {
                            collision[x, y] = true;
                        }

                        int rightWall = map[x, y].RightWall;

                        if (fg2Show)
                        {
                            bool showSelection = false;
                            if (editMode == 2 || editMode == 3 || editMode == 4)
                            {
                                if (clickmode == 0 && selection != null && MouseHoverX != -1)
                                {
                                    int selectionHeight = selection.Length / selectionWidth;
                                    if (x >= MouseHoverX && x < MouseHoverX + selectionWidth && y >= MouseHoverY && y < MouseHoverY + selectionHeight)
                                    {
                                        int _x = x - MouseHoverX;
                                        int _y = y - MouseHoverY;
                                        int _rightWall = selection[_x + _y * selectionWidth].RightWall;
                                        if (_rightWall > 0 || drawEmptyWalls)
                                        {
                                            rightWall = _rightWall;
                                            showSelection = true;
                                        }
                                    }
                                }
                            }
                            if (rightWall >= 13)
                            {
                                if (!cachedWalls.ContainsKey(rightWall))
                                {
                                    HPF hpf = HPF.FromArchive("stc" + rightWall.ToString().PadLeft(5, '0') + ".hpf", true, wallSource);
                                    Bitmap wall = RenderImage(hpf, wallTable[rightWall + 1]);
                                    cachedWalls.Add(rightWall, wall);
                                }

                                Bitmap thisTile = cachedWalls[rightWall];
                                if (editMode == 2 || editMode == 3 || editMode == 4)
                                {
                                    if (showSelection || (selectionXStart != -1 && x >= xMin && x <= xMax && y >= yMin && y <= yMax))
                                    {
                                        if (!cachedWallsLight.ContainsKey(rightWall))
                                        {
                                            HPF hpf = HPF.FromArchive("stc" + rightWall.ToString().PadLeft(5, '0') + ".hpf", true, wallSource);
                                            thisTile = RenderImage(hpf, wallTable[rightWall + 1]);
                                            for (int yy = 0; yy < thisTile.Height; yy++)
                                            {
                                                for (int xx = 0; xx < thisTile.Width; xx++)
                                                {
                                                    Color c = thisTile.GetPixel(xx, yy);
                                                    int colorR = c.R + 25;
                                                    if (colorR > 255) colorR = 255;
                                                    int colorG = c.G + 25;
                                                    if (colorG > 255) colorG = 255;
                                                    int colorB = c.B + 50;
                                                    if (colorB > 255) colorB = 255;
                                                    if (c.R != 0 || c.G != 0 || c.B != 0) thisTile.SetPixel(xx, yy, Color.FromArgb(colorR, colorG, colorB));
                                                }
                                            }
                                            cachedWallsLight.Add(rightWall, thisTile);
                                        }
                                        else
                                        {
                                            thisTile = cachedWallsLight[rightWall];
                                        }
                                    }
                                }
                                if ((rightWall % 10000) > 1)
                                {
                                    int xright = xOrigin + (x + 1) * xMul;
                                    int yright = yOrigin + (x + 1) * yxMultiplier - thisTile.Height + (Tileset.TileHeight + 1) / 2;

                                    if (transparency && rightWall > 0 && rightWall - 1 < sotp.Length && (sotp[rightWall - 1] & 0x80) == 0x80)
                                    {
                                        for (int yy = 0; yy < thisTile.Height; yy++)
                                        {
                                            for (int xx = 0; xx < thisTile.Width; xx++)
                                            {
                                                int origX = xright + xx;
                                                int origY = yright + yy;
                                                if (origX >= 0 && origX < mapImage.Width && origY >= 0 && origY < mapImage.Height)
                                                {
                                                    Color origC = mapImage.GetPixel(origX, origY);
                                                    Color newC = thisTile.GetPixel(xx, yy);
                                                    int _r = origC.R + newC.R;
                                                    if (_r > 255) _r = 255;
                                                    int _g = origC.G + newC.G;
                                                    if (_g > 255) _g = 255;
                                                    int _b = origC.B + newC.B;
                                                    if (_b > 255) _b = 255;
                                                    Color combined = Color.FromArgb(_r, _g, _b);
                                                    mapImage.SetPixel(xright + xx, yright + yy, combined);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        g.DrawImageUnscaled(thisTile,xright,yright);
                                    }
                                }
                            }
                        }
                        if (tabMap && rightWall > 0 && rightWall - 1 < sotp.Length && sotp[rightWall - 1] == 0x0F)
                        {
                            collision[x, y] = true;
                        }
                    }
                }
                xOrigin -= xMul;
                yOrigin += yxMultiplier;
            }
            if (tabMap)
            {
                xOrigin = ((mapImage.Width / 2) - 1) - Tileset.TileWidth / 2 + 1 - hscroll;
                yOrigin = -vscroll;

                for (int y = 0; y < map.Height; y++)
                {
                    for (int x = 0; x < map.Width; x++)
                    {
                        if (tabMap && collision[x, y])
                        {
                            int xDraw = xOrigin + x * Tileset.TileWidth / 2 + Tileset.TileWidth / 2;
                            int yDraw = yOrigin + x * (Tileset.TileHeight + 1) / 2;

                            PointF[] ps = new PointF[4];
                            ps[0] = new PointF(xDraw, yDraw);
                            ps[1] = new PointF(xDraw + Tileset.TileWidth / 2, yDraw + Tileset.TileHeight / 2);
                            ps[2] = new PointF(xDraw, yDraw + Tileset.TileHeight);
                            ps[3] = new PointF(xDraw - Tileset.TileWidth / 2, yDraw + Tileset.TileHeight / 2);
                            g.FillPolygon((new SolidBrush(Color.FromArgb(128, 255, 255, 255))), ps);
                        }
                    }

                    xOrigin -= Tileset.TileWidth / 2;
                    yOrigin += (Tileset.TileHeight + 1) / 2;

                }
            }
            if (drawGridLines)
            {
                xOrigin = ((mapImage.Width / 2) - 1) - hscroll;
                yOrigin = -vscroll;
                for (int y = 0; y < map.Height; y++)
                {
                    int xPos = xOrigin - y * (Tileset.TileWidth) / 2;
                    int yPos = (int)(yOrigin + y * ((Tileset.TileHeight + 1) / 2)) - 1;
                    int xEnd = xPos + (map.Width - 1) * (Tileset.TileWidth + 1) / 2;
                    int yEnd = (int)(yPos + (map.Height - 1) * (Tileset.TileHeight + 1.5) / 2);
                    g.DrawLine(new Pen(new SolidBrush(Color.Green)), xPos, yPos, xEnd, yEnd);
                }

                for (int x = 0; x < map.Width; x++)
                {
                    int xPos = xOrigin + x * (Tileset.TileWidth) / 2;
                    int yPos = (int)(yOrigin + x * ((Tileset.TileHeight + 1) / 2)) - 1;
                    int xEnd = xPos - (map.Width - 1) * (Tileset.TileWidth + 1) / 2;
                    int yEnd = (int)(yPos + (map.Height - 1) * (Tileset.TileHeight + 1.5) / 2);
                    g.DrawLine(new Pen(new SolidBrush(Color.Green)), xPos, yPos, xEnd, yEnd);
                }
            }
            g.Dispose();
            System.GC.Collect();

            return mapImage;
        }
    }
}
