using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace hexMapMaker_CS
{
    static class Coord
    {
        //public static int[] pic_canvas(int x, int y)
        //{

        //}
        public static int[] canvas_pic(int x, int y, int xoff = 0, int yoff = 0)
        {
            int[] ret = { x - xoff, y - yoff };
            return ret;
        }
        public static int[] canvas_pic(int[] xy, int xoff = 0, int yoff = 0)
        {
            int[] ret = { xy[0] - xoff, xy[1] - yoff };
            return ret;
        }
        public static int[] pic_map(int x, int y, int xoff, int yoff)
        {
            int[] ret = { 0, 0 };
            if (SETTINGS.TRUE_COL)
            {
                for (int mx = 0; mx < SETTINGS.COL; mx++) // this is to make sure of the accuracy at intersection.
                {
                    for (int my = 0; my < SETTINGS.ROW; my++)
                    {
                        GraphicsPath p = new GraphicsPath();
                        int[] picCoordToCheck = map_pic(mx, my, xoff, yoff);
                        p.AddLines(SETTINGS.hex_path_tc_pic(picCoordToCheck[0], picCoordToCheck[1]));
                        if (p.IsVisible(x, y))
                        {
                            ret[0] = mx;
                            ret[1] = my;
                            return ret;
                        }
                    }
                }
                ret[0] = -1;
                ret[1] = -1;
                return ret;
            }
            else
            {
                for (int mx = 0; mx < SETTINGS.COL; mx++) // this is to make sure of the accuracy at intersection.
                {
                    for (int my = 0; my < SETTINGS.ROW; my++)
                    {
                        GraphicsPath p = new GraphicsPath();
                        int[] picCoordToCheck = map_pic(mx, my, xoff, yoff);
                        p.AddLines(SETTINGS.hex_path_tr(picCoordToCheck[0], picCoordToCheck[1]));
                        if (p.IsVisible(x, y))
                        {
                            ret[0] = mx;
                            ret[1] = my;
                            return ret;
                        }
                    }
                }
                ret[0] = -1;
                ret[1] = -1;
                return ret;
            }
        }
        //public static int[] pic_map_fast(int x, int y) // only tests for the rectangular part.
        //{

        //}
        public static int[] map_pic(int x, int y, int anchor = 0)// 0: UL 1:L 2:DL 3:D 4:DR 5:R 6:UR 7:U 8:C 
        {
            int offx = 0, offy = 0;
            switch (anchor)
            {
                case 0:
                    offx = 0;
                    offy = 0;
                    break;
                case 1:
                    offx = 0;
                    offy = SETTINGS.HEIGHT / 2;
                    break;
                case 2:
                    offx = 0;
                    offy = SETTINGS.HEIGHT;
                    break;
                case 3:
                    offx = SETTINGS.WIDTH / 2;
                    offy = SETTINGS.HEIGHT;
                    break;
                case 4:
                    offx = SETTINGS.WIDTH;
                    offy = SETTINGS.HEIGHT;
                    break;
                case 5:
                    offx = SETTINGS.WIDTH;
                    offy = SETTINGS.HEIGHT / 2;
                    break;
                case 6:
                    offx = SETTINGS.WIDTH;
                    offy = 0;
                    break;
                case 7:
                    offx = SETTINGS.WIDTH / 2;
                    offy = 0;
                    break;
                case 8:
                    offx = SETTINGS.WIDTH / 2;
                    offy = SETTINGS.HEIGHT / 2;
                    break;
            }


            if (SETTINGS.TRUE_COL)
            {
                int[] ret = { x * SETTINGS.WIDTH * 3 / 4 + offx, y * SETTINGS.HEIGHT + SETTINGS.HEIGHT / 2 * (x % 2) + offy };
                return ret;
            }
            else
            {
                int[] ret = { x * SETTINGS.WIDTH + SETTINGS.WIDTH / 2 * (y % 2) + offx, y * SETTINGS.HEIGHT * 3 / 4 + offy };
                return ret;

            }
        }
        public static int[] map_pic(int x, int y, int offx, int offy)
        {
            if (SETTINGS.TRUE_COL)
            {
                int[] ret = { x * SETTINGS.WIDTH * 3 / 4 + offx, y * SETTINGS.HEIGHT + SETTINGS.HEIGHT / 2 * (x % 2) + offy };
                return ret;
            }
            else
            {
                int[] ret = { x * SETTINGS.WIDTH + SETTINGS.WIDTH / 2 * (y % 2) + offx, y * SETTINGS.HEIGHT * 3 / 4 + offy };
                return ret;

            }
        }
    }
}
