
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;



namespace hexMapMaker_CS
{

    static class SETTINGS
    {
        static public int WIDTH = 40;
        static public int HEIGHT = 40;
        static public int ROW = 120;
        static public int COL = 120;
        static public bool TRUE_COL = true;
        static public int UNDERLAY_OFF_X = 0, UNDERLAY_OFF_Y = 0;
        public static int[] XPT_TC = { WIDTH * 3 / 4, WIDTH, WIDTH * 3 / 4, WIDTH / 4, 0, WIDTH / 4 };
        public static int[] YPT_TC = { 0, HEIGHT / 2, HEIGHT, HEIGHT, HEIGHT / 2, 0 };
        public static int[] XPT_TR = { 0, WIDTH / 2, WIDTH, WIDTH, WIDTH / 2, 0 };
        public static int[] YPT_TR = { HEIGHT * 3 / 4, HEIGHT, HEIGHT * 3 / 4, HEIGHT / 4, 0, HEIGHT / 4 };

        public static Point[] hex_tc_pic(int xoff, int yoff)
        {
            Point[] temp = new Point[6];
            for (int i = 0; i< 6; i++)
            {
                temp[i] = new Point(XPT_TC[i] + xoff, YPT_TC[i] + yoff);
            }
            return temp;
        }
        public static Point[] hex_path_tc_pic(int xoff, int yoff)
        {
            Point[] temp = new Point[7];
            Array.Copy(hex_tc_pic(xoff,yoff), temp, 6);
            temp[6] = temp[0];
            return temp;
        }

        public static Point[] hex_tc_grid(int xoff, int yoff) // this may be used anywhere. must get ready raw version and grid coord version.
        {
            //yoff = yoff * HEIGHT + HEIGHT / 2 * (xoff % 2);
            //xoff = (xoff * WIDTH * 3 / 4);
            return hex_tc_pic(xoff * WIDTH * 3 / 4, yoff * HEIGHT + HEIGHT / 2 * (xoff % 2));
        }
        public static Point[] hex_path_tc_grid(int xoff, int yoff)
        {
            return hex_path_tc_pic(xoff * WIDTH * 3 / 4, yoff * HEIGHT + HEIGHT / 2 * (xoff % 2));
            //yoff = (yoff * HEIGHT + HEIGHT / 2 * (xoff % 2));
            //xoff = (xoff * WIDTH * 3 / 4);

            //Point[] temp = new Point[7];
            //for (int i = 0; i < 6; i++)
            //{
            //    temp[i] = new Point(XPT_TC[i] + xoff, YPT_TC[i] + yoff);
            //}
            //temp[6] = new Point(XPT_TC[0] + xoff, YPT_TC[0] + yoff);
            //return temp;
        }
        //public static Point[] hex_tr_pic(int xoff, int yoff)
        //{

        //}
        public static Point[] hex_tr(int xoff, int yoff)
        {
            xoff = (xoff * WIDTH + WIDTH / 2 * (xoff % 2));
            yoff = (yoff * HEIGHT * 3 / 4);
            Point[] temp = new Point[6];
            for (int i = 0; i < 6; i++)
            {
                temp[i] = new Point(XPT_TR[i] + xoff, YPT_TR[i] + yoff);

            }
            return temp;
        }
        public static Point[] hex_path_tr(int xoff, int yoff)
        {
            xoff = (xoff * WIDTH + WIDTH / 2 * (xoff % 2));
            yoff = (yoff * HEIGHT * 3 / 4);
            Point[] temp = new Point[7];
            for (int i = 0; i < 6; i++)
            {
                temp[i] = new Point(XPT_TR[i] + xoff, YPT_TR[i] + yoff);
            }
            temp[6] = new Point(XPT_TR[0] + xoff, YPT_TR[0] + yoff);
            return temp;
        }
    }

    class Stamp
    {
        Color color;
        public string name { get; }
        public string displayname;
        Bitmap stamp_tr, stamp_tc;
        public Stamp(Color color, string name, string displayname)
        {
            this.color = color;
            this.name = name;
            this.displayname = displayname;
            stamp_tr = new Bitmap(SETTINGS.WIDTH,SETTINGS.HEIGHT);
            stamp_tc = new Bitmap(SETTINGS.WIDTH,SETTINGS.HEIGHT);
            using (SolidBrush colorbrush = new SolidBrush(color))
            {
                using (Graphics gs = Graphics.FromImage(stamp_tr))
                {
                    gs.FillPolygon(colorbrush, SETTINGS.hex_tr(0, 0));
                }
                using (Graphics gs = Graphics.FromImage(stamp_tc))
                {
                    //gs.FillPolygon(colorbrush, SETTINGS.hex_tc(0, 0));
                }
            }
        }
        public void ChangeAlpha(int alpha)
        {
            for (int x = 0; x < SETTINGS.WIDTH; x++)
            {
                for (int y = 0; y < SETTINGS.HEIGHT; y++)
                {
                    Color c = stamp_tr.GetPixel(x, y);
                    Color newc = Color.FromArgb(alpha, c.R, c.G, c.B);
                    stamp_tr.SetPixel(x, y, newc);
                    c = stamp_tc.GetPixel(x, y);
                    newc = Color.FromArgb(alpha, c.R, c.G, c.B);
                    stamp_tc.SetPixel(x, y, newc);
                }
            }
        }
    }

    class Mapgen
    {
        List<List<string>> map = new List<List<string>>();
        Dictionary<string, Stamp> stampbase = new Dictionary<string, Stamp>();
        public Mapgen()
        {
        
        }
        public Mapgen(List<List<string>> map)
        {
            this.map = map;
        }
        public void FlushStamp()
        {
            stampbase.Clear();
        }
        public void AddStamp(Stamp s)
        {
            stampbase.Add(s.name, s);
        }
    }
}
