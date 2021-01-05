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
        //public static int[] pic_map(int x, int y, int xoff, int yoff)
        //{
        //    int[] ret = { 0, 0 };
        //    GraphicsPath p = new GraphicsPath();
        //    p.AddLines(SETTINGS.hex_path_tc_pic(0, 0));
        //    Region r = new Region(p);
        //    if (SETTINGS.TRUE_COL)
        //    {
        //        for (int mx = 0; mx < SETTINGS.COL; mx++) // this is to make sure of the accuracy at intersection.
        //        {
        //            for (int my = 0; my < SETTINGS.ROW; my++)
        //            {
        //                if (r.IsVisible(x, y))
        //                {
        //                    ret[0] = mx;
        //                    ret[1] = my;
        //                    return ret;
        //                }
        //                r.Translate(0, SETTINGS.HEIGHT);
        //            }
        //            r.Translate(SETTINGS.WIDTH * 3 / 4, ((mx % 2 == 0) ? 1 : -1) * SETTINGS.HEIGHT / 2 - SETTINGS.HEIGHT * (SETTINGS.COL));
        //        }
        //        ret[0] = -1;
        //        ret[1] = -1;
        //        return ret;
        //    }
        //    else
        //    {
        //        for (int my = 0; my < SETTINGS.ROW; my++) // this is to make sure of the accuracy at intersection.
        //        {
        //            for (int mx = 0; mx < SETTINGS.COL; mx++)
        //            {
        //                if (r.IsVisible(x, y))
        //                {
        //                    ret[0] = mx;
        //                    ret[1] = my;
        //                    return ret;
        //                }
        //                r.Translate(SETTINGS.WIDTH, 0);
        //            }
        //            r.Translate(((my % 2 == 0) ? 1 : -1) * SETTINGS.WIDTH / 2 - SETTINGS.WIDTH * SETTINGS.ROW, SETTINGS.HEIGHT * 3 / 4);
        //        }
        //        ret[0] = -1;
        //        ret[1] = -1;
        //        return ret;
        //    }
        //}
        public static int[] pic_map_fast(int x, int y) // only tests for the rectangular part.
        {
            int mx = x / (SETTINGS.WIDTH / 4 * 3); // flooring.
            int my = (y - (mx % 2) * SETTINGS.HEIGHT / 2) / SETTINGS.HEIGHT;
            //DebugLabel.Text = string.Format("x = {0:d}, y = {1:d}", mx, my);
            return new int[] { mx, my };
        }
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
