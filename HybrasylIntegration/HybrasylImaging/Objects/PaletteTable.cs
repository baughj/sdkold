using Hybrasyl.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace Hybrasyl.Imaging.Objects
{
    public class PaletteTable
    {
        private List<Palette> entries = new List<Palette>();
        private Dictionary<int, ColorPalette> palettes = new Dictionary<int, ColorPalette>();
        private List<Palette> overrides = new List<Palette>();

        public ColorPalette this[int index]
        {
            get
            {
                int paletteIndex = 0;

                foreach (Palette o in overrides)
                {
                    if (index >= o.Min && index <= o.Max)
                    {
                        paletteIndex = o.PaletteIndex;
                    }
                }
                foreach (Palette entry in entries)
                {
                    if (index >= entry.Min && index <= entry.Max)
                        paletteIndex = entry.PaletteIndex;
                }

                return palettes[paletteIndex];
            }
        }

        private int LoadTableInternal(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(stream);
            entries.Clear();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] lineSplit = line.Split(' ');

                if (lineSplit.Length == 3)
                {
                    int min = Convert.ToInt32(lineSplit[0]);
                    int max = Convert.ToInt32(lineSplit[1]);
                    int pal = Convert.ToInt32(lineSplit[2]);

                    entries.Add(new Palette(min, max, pal));
                }
                else if (lineSplit.Length == 2)
                {
                    int min = Convert.ToInt32(lineSplit[0]);
                    int max = min;
                    int pal = Convert.ToInt32(lineSplit[1]);

                    entries.Add(new Palette(min, max, pal));
                }
            }
            reader.Close();

            return entries.Count;
        }

        public int LoadPalettes(string pattern, Archive archive)
        {
            palettes.Clear();
            foreach (ArchiveFile file in archive.Files)
            {
                if (file.Name.ToUpper().EndsWith(".PAL") && file.Name.ToUpper().StartsWith(pattern.ToUpper()))
                {
                    int index = Convert.ToInt32(Path.GetFileNameWithoutExtension(file.Name).Remove(0, pattern.Length));
                    ColorPalette palette = ColorPalette.FromArchive(file.Name, archive);

                    palettes.Add(index, palette);
                }
            }
            return palettes.Count;
        }

        public int LoadPalettes(string pattern, string path)
        {
            string[] files = Directory.GetFiles(path, pattern + "*.PAL", SearchOption.TopDirectoryOnly);

            palettes.Clear();
            foreach (string file in files)
            {
                if (Path.GetFileName(file).ToUpper().EndsWith(".PAL") && Path.GetFileName(file).ToUpper().StartsWith(pattern.ToUpper()))
                {
                    int index = Convert.ToInt32(Path.GetFileNameWithoutExtension(file).Remove(0, pattern.Length));
                    ColorPalette palette = ColorPalette.FromFile(file);

                    palettes.Add(index, palette);
                }
            }

            return palettes.Count;
        }

        public int LoadTables(string pattern, Archive archive)
        {
            entries.Clear();
            foreach (ArchiveFile file in archive.Files)
            {
                // Check for Palette
                if (file.Name.ToUpper().EndsWith(".TBL") && file.Name.ToUpper().StartsWith(pattern.ToUpper()))
                {
                    string tableName = Path.GetFileNameWithoutExtension(file.Name).Remove(0, pattern.Length);

                    if (tableName != "ani")
                    {
                        MemoryStream stream = new MemoryStream(archive.ExtractFile(file));
                        StreamReader reader = new StreamReader(stream);

                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            string[] lineSplit = line.Split(' ');

                            if (lineSplit.Length == 3)
                            {
                                int min = Convert.ToInt32(lineSplit[0]);
                                int max = Convert.ToInt32(lineSplit[1]);
                                int pal = Convert.ToInt32(lineSplit[2]);

                                int index = 0;
                                if (int.TryParse(tableName, out index))
                                    overrides.Add(new Palette(min, max, pal));
                                else
                                    entries.Add(new Palette(min, max, pal));
                            }
                            else if (lineSplit.Length == 2)
                            {
                                int min = Convert.ToInt32(lineSplit[0]);
                                int max = min;
                                int pal = Convert.ToInt32(lineSplit[1]);

                                int index = 0;
                                if (int.TryParse(tableName, out index))
                                    overrides.Add(new Palette(min, max, pal));
                                else
                                    entries.Add(new Palette(min, max, pal));
                            }
                        }
                        reader.Close();
                    }
                }
            }
            return entries.Count;
        }

        public int LoadTables(string pattern, string path)
        {
            string[] files = Directory.GetFiles(path, pattern + "*.TBL", SearchOption.TopDirectoryOnly);
            entries.Clear();
            foreach (string file in files)
            {
                if (Path.GetFileName(file).ToUpper().EndsWith(".TBL") && Path.GetFileName(file).ToUpper().StartsWith(pattern.ToUpper()))
                {
                    string tableName = Path.GetFileNameWithoutExtension(file).Remove(0, pattern.Length);

                    if (tableName != "ani")
                    {
                        string[] lines = File.ReadAllLines(file);

                        foreach (string line in lines)
                        {
                            string[] lineSplit = line.Split(' ');

                            if (lineSplit.Length == 3)
                            {
                                int min = Convert.ToInt32(lineSplit[0]);
                                int max = Convert.ToInt32(lineSplit[1]);
                                int pal = Convert.ToInt32(lineSplit[2]);

                                int index = 0;
                                if (int.TryParse(tableName, out index))
                                    overrides.Add(new Palette(min, max, pal));
                                else
                                    entries.Add(new Palette(min, max, pal));
                            }
                            else if (lineSplit.Length == 2)
                            {
                                int min = Convert.ToInt32(lineSplit[0]);
                                int max = min;
                                int pal = Convert.ToInt32(lineSplit[1]);

                                int index = 0;
                                if (int.TryParse(tableName, out index))
                                    overrides.Add(new Palette(min, max, pal));
                                else
                                    entries.Add(new Palette(min, max, pal));
                            }
                        }
                    }
                }
            }
            return entries.Count;
        }
    }
}