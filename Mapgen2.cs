using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace hexMapMaker_CS
{
    public static class SETTINGS
    {
        public static int WIDTH = 48;
        public static int HEIGHT = 48;
        public static int ROW = 12;
        public static int COL = 12;
        public static bool TRUE_COL = true;
    }
    public class Outline // remember to initialize this at Form.
    {
        public Point[] hexPathPointOpen_tc; // open
        public Point[] hexPathPoint_tc;
        public Point[] mapOutlineOpen_tc;
        public GraphicsPath hexPath_tc = new GraphicsPath();
        public Region hexRegion_tc = new Region(); // Initialize outside. Remember to make COPY before use.
        public List<Point[]> BrushPathBase_tc = new List<Point[]>();

        public Point[] hexPathPointOpen_tr; // open
        public Point[] hexPathPoint_tr;
        public Point[] mapOutlineOpen_tr;
        public GraphicsPath hexPath_tr = new GraphicsPath();
        public Region hexRegion_tr = new Region();
        public List<Point[]> BrushPathBase_tr = new List<Point[]>();
        
        public Outline()
        {
            SETTINGS.TRUE_COL = false;
            hexPathPointOpen_tr = (Brush_Outline(0, 0, 0, false)); // open
            hexPathPoint_tr = (Brush_Outline(0, 0, 0, true));
            mapOutlineOpen_tr = (Map_Outline(true));
            hexPath_tr.AddLines(hexPathPoint_tr.ToArray());
            hexRegion_tr = new Region(hexPath_tr);


            SETTINGS.TRUE_COL = true;
            hexPathPointOpen_tc = Brush_Outline(0, 0, 0, false); // open
            hexPathPoint_tc = Brush_Outline(0, 0, 0, true);
            mapOutlineOpen_tc = (Map_Outline(true));
            hexPath_tc.AddLines(hexPathPoint_tc.ToArray());
            hexRegion_tc = new Region(hexPath_tc); // Initialize outside. Remember to make COPY before use.

            for (int i = 0; i < 10; i++)
            {
                SETTINGS.TRUE_COL = false;
                BrushPathBase_tr.Add(Brush_Outline(0, 0, i, false));
                SETTINGS.TRUE_COL = true;
                BrushPathBase_tc.Add(Brush_Outline(0, 0, i, false));
            }
        }

        public Point[] getOffsetPointArr(Point[] brush, Point p)
        {
            List<Point> rtn = new List<Point>();
            foreach (Point pp in brush)
            {
                pp.Offset(p);
                rtn.Add(pp);
            }
            return rtn.ToArray();
        }
        public Point[] getOffsetPointArr(Point[] brush, int x, int y)
        {
            Point p = new Point(x, y);
            return getOffsetPointArr(brush, p);
        }

         void Move_Right(List<Point> pl)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            x +=SETTINGS.WIDTH / 2;
            pl.Add(new Point(x, y));
        }
         void Move_Left(List<Point> pl)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            x -= SETTINGS.WIDTH / 2;
            pl.Add(new Point(x, y));
        }
         void Move_Up(List<Point> pl)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            y -= SETTINGS.HEIGHT / 2;
            pl.Add(new Point(x, y));
        }
         void Move_Down(List<Point> pl)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            y += SETTINGS.HEIGHT / 2;
            pl.Add(new Point(x, y));
        }
         void Move_UL(List<Point> pl)//(List<Point> pl, bool tc = true)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            if (SETTINGS.TRUE_COL)
            {
                x -= SETTINGS.WIDTH / 4;
                y -= SETTINGS.HEIGHT / 2;
            }else
            {
                x -= SETTINGS.WIDTH / 2;
                y -= SETTINGS.HEIGHT / 4;
            }

            pl.Add(new Point(x, y));
        }
         void Move_UR(List<Point> pl)//(List<Point> pl, bool tc = true)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            if (SETTINGS.TRUE_COL)
            {
                x += SETTINGS.WIDTH / 4;
                y -= SETTINGS.HEIGHT / 2;
            }
            else
            {
                x += SETTINGS.WIDTH / 2;
                y -= SETTINGS.HEIGHT / 4;
            }
            pl.Add(new Point(x, y));
        }
         void Move_LL(List<Point> pl)//(List<Point> pl, bool tc = true)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            if (SETTINGS.TRUE_COL)
            {
                x -= SETTINGS.WIDTH / 4;
                y += SETTINGS.HEIGHT / 2;
            }
            else
            {
                x -= SETTINGS.WIDTH / 2;
                y += SETTINGS.HEIGHT / 4;
            }
            pl.Add(new Point(x, y));
        }
         void Move_LR(List<Point> pl)//(List<Point> pl, bool tc = true)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            if (SETTINGS.TRUE_COL)
            {
                x += SETTINGS.WIDTH / 4;
                y += SETTINGS.HEIGHT / 2;
            }
            else
            {
                x += SETTINGS.WIDTH / 2;
                y += SETTINGS.HEIGHT / 4;
            }
            pl.Add(new Point(x, y));
        }
        public  Point[] Brush_Outline(int x_pic, int y_pic, int r, bool close = false)
            // draw a hexagon with r layers. single hexagon is r = 0.
            //
        {
            List<Point> rtn = new List<Point>();
            int x = 0, y = 0;
            if (SETTINGS.TRUE_COL)
            {
                x = x_pic + SETTINGS.WIDTH / 4;
                y = y_pic - SETTINGS.HEIGHT * r;
                rtn.Add(new Point(x, y));
                Move_Right(rtn);
                for (int i = 0; i< r; i++)
                {
                    Move_LR(rtn);
                    Move_Right(rtn);
                }
                Move_LR(rtn);
                for (int i = 0; i < r; i++)
                {
                    Move_LL(rtn);
                    Move_LR(rtn);
                }
                Move_LL(rtn);
                for (int i = 0; i < r; i++)
                {
                    Move_Left(rtn);
                    Move_LL(rtn);
                }
                Move_Left(rtn);
                for (int i = 0; i < r; i++)
                {
                    Move_UL(rtn);
                    Move_Left(rtn);
                }
                Move_UL(rtn);
                for (int i = 0; i < r; i++)
                {
                    Move_UR(rtn);
                    Move_UL(rtn);
                }
                Move_UR(rtn);
                for (int i = 0; i < r; i++)
                {
                    Move_Right(rtn);
                    Move_UR(rtn);
                }

            }
            else
            {
                x -= SETTINGS.WIDTH * r;
                y += SETTINGS.HEIGHT*3 / 4;
                rtn.Add(new Point(x, y));
                Move_Up(rtn);
                for (int i = 0; i < r; i++)
                {
                    Move_UR(rtn);
                    Move_Up(rtn);
                }
                Move_UR(rtn);
                for (int i = 0; i < r; i++)
                {
                    Move_LR(rtn);
                    Move_UR(rtn);
                }
                Move_LR(rtn);
                for (int i = 0; i < r; i++)
                {
                    Move_Down(rtn);
                    Move_LR(rtn);
                }
                Move_Down(rtn);
                for (int i = 0; i < r; i++)
                {
                    Move_LL(rtn);
                    Move_Down(rtn);
                }
                    Move_LL(rtn);
                for (int i = 0; i < r; i++)
                {
                    Move_UL(rtn);
                    Move_LL(rtn);
                }
                    Move_UL(rtn);
                for (int i = 0; i < r; i++)
                {
                    Move_Up(rtn);
                    Move_UL(rtn);
                }
            }
            if (!close)
            {
                rtn.RemoveAt(rtn.Count - 1);
            }
            return rtn.ToArray();
        }

        public  Point[] Map_Outline(bool close = false) // in some cases we may need a square region of any width and height
        {
            List<Point> rtn = new List<Point>();
            int x = 0; int y = 0;
            if (SETTINGS.TRUE_COL)
            {
                x = SETTINGS.WIDTH / 4;
                rtn.Add(new Point(x, y));
                for (int c = 0; c < SETTINGS.COL - 1; c++)
                {
                    Move_Right(rtn);
                    if (c % 2 == 0)
                    {
                        Move_LR(rtn);
                    }
                    else Move_UR(rtn);

                }
                Move_Right(rtn);
                for (int r = 0; r < SETTINGS.ROW; r++)
                {
                    Move_LR(rtn);
                    Move_LL(rtn);
                }
                for (int c = SETTINGS.COL; c > 1; c--)
                {
                    Move_Left(rtn);
                    if (c % 2 == 1)
                    {
                        Move_LL(rtn);
                    }
                    else Move_UL(rtn);

                }
                Move_Left(rtn);
                for (int r = 0; r<SETTINGS.ROW; r++)
                {
                    Move_UL(rtn);
                    Move_UR(rtn);
                }

            }
            else
            {
                y = SETTINGS.HEIGHT / 4;
                rtn.Add(new Point(x, y));
                for (int c  = 0; c<SETTINGS.COL; c++)
                {
                    Move_UR(rtn);
                    Move_LR(rtn);
                }
                for (int r = 0; r < SETTINGS.ROW - 1; r++)
                {
                    Move_Down(rtn);
                    if (r % 2 == 0)
                    {
                        Move_LR(rtn);
                    }
                    else Move_LL(rtn);
                }Move_Down(rtn);
                for (int c = 0;c<SETTINGS.COL;c++)
                {
                    Move_LL(rtn);
                    Move_UL(rtn);
                }
                for (int r = SETTINGS.ROW; r > 1; r--)
                {
                    Move_Up(rtn);
                    if (r % 2 == 0)
                        Move_UR(rtn);
                    else Move_UL(rtn);
                }
            }
            if (!close)
                rtn.RemoveAt(rtn.Count - 1);

            return rtn.ToArray();
        }
    }
    public class Stamp
    {
        public string name;
        public string displayname;
        public Color color;
        public Stamp(string name, string displayname, Color color)
        {
            this.name = name;
            this.displayname = displayname;
            this.color = color;
        }
        public Stamp(string name, string displayname, int a, int r, int g, int b)
        {
            this.name = name;
            this.displayname = displayname;
            this.color = Color.FromArgb(a,r,g,b);
        }
    }
    static class Mapgen
    {
        //public static List<List<string>> map = new List<List<string>>();
        public static Dictionary<Point, string> map = new Dictionary<Point, string>(); // 网格坐标和点的对应
        public static Dictionary<Point, string> map_p = new Dictionary<Point, string>(); // 图面坐标和点的对应
        public static Dictionary<string, Stamp> stampBase = new Dictionary<string, Stamp>();
        public static void ReadMap(string path)
        {
            string readstate = "";
            int wmax = 0, hmax = 0;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while((line = sr.ReadLine())!= null)
                    {
                        switch(line)
                        {
                            default:

                                switch (readstate)
                                {
                                    case "hexunitdef":
                                        string[] node = line.Split(',');
                                        int r = 0, g = 0, b = 0;
                                        int.TryParse(node[1], out r);
                                        int.TryParse(node[2], out g);
                                        int.TryParse(node[3], out b);
                                        stampBase.Add(node[0], new Stamp(node[0], node[0], 255, r, g, b));
                                        break;
                                    case "coorddef":
                                        string[] node2 = line.Split(',');
                                        int x = 0, y = 0;
                                        int.TryParse(node2[0], out x);
                                        wmax = wmax > x ? wmax : x;
                                        int.TryParse(node2[1], out y);
                                        hmax = hmax > y ? hmax : y;
                                        map.Add(new Point(x, y), node2[2]);
                                        int[] mp = Coord.map_pic(x,y);
                                        map_p.Add(new Point(mp[0], mp[1]), node2[2]);
                                        break;
                                }

                                break;
                            case "hexunitdef":
                                readstate = line;
                                break;
                            case "coorddef":
                                readstate = line;
                                break;
                        }
                    }
                }
            }catch (FileNotFoundException e)
            {
                System.Windows.Forms.MessageBox.Show("File not found");
            }
            SETTINGS.COL = wmax;
            SETTINGS.ROW = hmax;
        }
    }
}
