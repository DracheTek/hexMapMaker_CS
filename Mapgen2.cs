using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace hexMapMaker_CS
{
    public static class SETTINGS
    {
        public static int WIDTH = 48;
        public static int HEIGHT = 48;
        public static int ROW = 120;
        public static int COL = 120;
        public static bool TRUE_COL = true;
    }
    public static class OUTLINE // remember to initialize this at Form.
    {
        static Point[] hexPathPointOpen = new Point[6]; // open
        static Point[] hexPathPoint = new Point[7];
        static GraphicsPath hexPath = new GraphicsPath();
        static Region hexRegion = new Region(); // Initialize outside. Remember to make COPY before use.
        static List<Point[]> BrushPathBase_tr = new List<Point[]>();
        static void Move_Right(List<Point> pl)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            x +=SETTINGS.WIDTH / 2;
            pl.Add(new Point(x, y));
        }
        static void Move_Left(List<Point> pl)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            x -= SETTINGS.WIDTH / 2;
            pl.Add(new Point(x, y));
        }
        static void Move_Up(List<Point> pl)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            y += SETTINGS.HEIGHT / 2;
            pl.Add(new Point(x, y));
        }
        static void Move_Down(List<Point> pl)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            y -= SETTINGS.HEIGHT / 2;
            pl.Add(new Point(x, y));
        }
        static void Move_UL(List<Point> pl, bool tc = true)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            if (tc)
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
        static void Move_UR(List<Point> pl, bool tc = true)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            if (tc)
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
        static void Move_LL(List<Point> pl, bool tc = true)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            if (tc)
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
        static void Move_LR(List<Point> pl, bool tc = true)
        {
            int x = pl.Last().X;
            int y = pl.Last().Y;
            if (tc)
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
        static Point[] Brush_Outline(int xoff, int yoff, int r, bool close = false)
            // draw a hexagon with r layers. single hexagon is r = 0.
        {
            List<Point> rtn = new List<Point>();
            if (SETTINGS.TRUE_COL)
            {
                int x = xoff + SETTINGS.WIDTH / 4;
                int y = yoff - SETTINGS.HEIGHT * r;
                rtn.Add(new Point(x, y));
                for (int i = 0; i < r + 1; i++)
                {
                    x += SETTINGS.WIDTH / 2;
                    rtn.Add(new Point(x, y));
                    x += SETTINGS.WIDTH / 4;
                    y += SETTINGS.HEIGHT / 2;
                    rtn.Add(new Point(x, y));
                }
                for (int i = 0; i < r; i++)
                {
                    x -= SETTINGS.WIDTH / 4;
                    y += SETTINGS.HEIGHT / 2;
                    rtn.Add(new Point(x, y));
                    x += SETTINGS.WIDTH / 4;
                    y += SETTINGS.HEIGHT / 2;
                    rtn.Add(new Point(x, y));
                }
                for (int i = 0; i < r + 1; i++)
                {
                    x -= SETTINGS.WIDTH / 4;
                    y += SETTINGS.HEIGHT / 2;
                    rtn.Add(new Point(x, y));
                    x -= SETTINGS.WIDTH / 2;
                    rtn.Add(new Point(x, y));
                }
                x -= SETTINGS.WIDTH / 4;
                y -= SETTINGS.HEIGHT / 2;
                rtn.Add(new Point(x, y));
                for (int i = 0; i < r; i++)
                {
                    x -= SETTINGS.WIDTH / 2;
                    rtn.Add(new Point(x, y));
                    x -= SETTINGS.WIDTH / 4;
                    y -= SETTINGS.HEIGHT / 2;
                    rtn.Add(new Point(x, y));
                }
                x += SETTINGS.WIDTH / 4;
                y -= SETTINGS.HEIGHT / 2;
                rtn.Add(new Point(x, y));

                for (int i = 0; i < r; i++)
                {
                    x -= SETTINGS.WIDTH / 4;
                    y -= SETTINGS.HEIGHT / 2;
                    rtn.Add(new Point(x, y));
                    x += SETTINGS.WIDTH / 4;
                    y -= SETTINGS.HEIGHT / 2;
                    rtn.Add(new Point(x, y));
                }
                for (int i = 0; i < r-1; i++)
                {
                    x += SETTINGS.WIDTH / 2;
                    rtn.Add(new Point(x, y));
                    x += SETTINGS.WIDTH / 4;
                    y -= SETTINGS.HEIGHT / 2;
                    rtn.Add(new Point(x, y));
                }
                x += SETTINGS.WIDTH / 2;
                rtn.Add(new Point(x, y));
                if (close)
                {
                    x += SETTINGS.WIDTH / 4;
                    y -= SETTINGS.HEIGHT / 2;
                    rtn.Add(new Point(x, y));
                }
            }
            else
            {

            }
            return rtn.ToArray();
        }

        static Point[] Map_Outline(bool close = false)
        {
            List<Point> rtn = new List<Point>();
            if (SETTINGS.TRUE_COL)
            {
                int x = SETTINGS.WIDTH / 4;
                int y = 0;
                rtn.Add(new Point(x, y));
                for (int i = 0; i < SETTINGS.COL - 1; i++)
                {
                    x += SETTINGS.WIDTH / 2;
                    rtn.Add(new Point(x, y));
                    if (i % 2 == 0)
                    {
                        x += SETTINGS.WIDTH / 4;
                        y += SETTINGS.HEIGHT / 2;
                        rtn.Add(new Point(x, y));
                    }
                    else
                    {
                        x += SETTINGS.WIDTH / 4;
                        y -= SETTINGS.HEIGHT / 2;
                        rtn.Add(new Point(x, y));
                    }
                }
                x += SETTINGS.WIDTH / 2;
                rtn.Add(new Point(x, y));
                for (int i = 0; i < SETTINGS.ROW; i++)
                {
                    x += SETTINGS.WIDTH / 4;
                    y += SETTINGS.HEIGHT / 2;
                    rtn.Add(new Point(x, y));
                    x -= SETTINGS.WIDTH / 4;
                    y += SETTINGS.HEIGHT / 2;
                    rtn.Add(new Point(x, y));
                }
                for (int i = SETTINGS.COL; i > 1; i--)
                {
                    x -= SETTINGS.WIDTH / 2;
                    rtn.Add(new Point(x, y));
                    if (i % 2 == 0)
                    {
                        x -= SETTINGS.WIDTH / 4;
                        y -= SETTINGS.HEIGHT / 2;
                    }
                    else
                    {
                        x -= SETTINGS.WIDTH / 4;
                        y += SETTINGS.HEIGHT / 2;
                    }
                    rtn.Add(new Point(x, y));
                }
                x -= SETTINGS.WIDTH / 2;
                rtn.Add(new Point(x, y));
                for (int i = 0; i < SETTINGS.ROW; i++)
                {
                    x -= SETTINGS.WIDTH / 4;
                    y -= SETTINGS.HEIGHT / 2;
                    rtn.Add(new Point(x, y));
                    x += SETTINGS.WIDTH / 4;
                    y -= SETTINGS.HEIGHT / 2;
                    rtn.Add(new Point(x, y));
                }
            }
                return rtn.ToArray();
        }
    }
    class Mapgen
    {

    }
}
