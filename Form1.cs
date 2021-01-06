using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace hexMapMaker_CS
{
    enum Tools
    {
        pan = 0,
        draw = 1
    }


    public partial class MainForm : Form
    {
        private bool initFlag = false;
        private Tools toolInUse = Tools.pan;
        private int pan_right_value, pan_down_value, x0, y0;
        private int x_zm, y_zm;
        private bool isDragging = false;
        private Font fnt = new Font("Bahnschrift", 48);
        private Image underlay = Image.FromFile("super_big_picture.png");
        //private Image mapImg, gridImg, coordImg;
        //private Bitmap underlay_bmp;
        private Bitmap map;// = new Bitmap(SETTINGS.WIDTH * 3 / 4 * SETTINGS.COL + SETTINGS.WIDTH / 4 + 10, SETTINGS.HEIGHT * SETTINGS.ROW + SETTINGS.HEIGHT / 2 + 10);
        private Bitmap grid;// = new Bitmap(SETTINGS.WIDTH * 3 / 4 * SETTINGS.COL + SETTINGS.WIDTH / 4 + 10, SETTINGS.HEIGHT * SETTINGS.ROW + SETTINGS.HEIGHT / 2 + 10, PixelFormat.Format1bppIndexed);
        private Bitmap coordNum;// = new Bitmap(SETTINGS.WIDTH * 3 / 4 * SETTINGS.COL + SETTINGS.WIDTH / 4 + 10, SETTINGS.HEIGHT * SETTINGS.ROW + SETTINGS.HEIGHT / 2 + 10);
        //private Bitmap[] maptiles;
        private List<Control> toolButtons = new List<Control>();
        private ColorMap COLOR_MAP_ENTRY;
        //private ColorPalette blackwhite;

        private float[] colormatrix_r = { 1.0f, 0, 0, 0, 0 };
        private float[] colormatrix_g = { 0, 1.0f, 0, 0, 0 };
        private float[] colormatrix_b = { 0, 0, 1.0f, 0, 0 };
        private float[] colormatrix_a = { 0, 0, 0, 1.0f, 0 };
        private float[] colormatrix_t = { 0f, 0, 0, 0, 1.0f };
        private float[][] cme;
        ColorMatrix cm = new ColorMatrix();
        private Outline outline = new Outline();
        
        //private int[] brush3 = {
        //    0,1,
        //    1,0,
        //    0,-1,
        //    -1,-1,
        //    -1,1,
        //    0,1
        //}

        // Initialize

        public MainForm()
        {
            int w = 0, h = 0;
            if (SETTINGS.TRUE_COL)
            {
                w = SETTINGS.WIDTH * 3 / 4 * SETTINGS.COL + SETTINGS.WIDTH / 4 + 10;
                h = SETTINGS.HEIGHT * SETTINGS.ROW + SETTINGS.HEIGHT / 2 + 10;
            }
            else
            {
                w = SETTINGS.WIDTH * SETTINGS.COL + SETTINGS.WIDTH / 2 + 10;
                h = SETTINGS.HEIGHT * 3 / 4 * SETTINGS.ROW + SETTINGS.HEIGHT / 4 + 10;
            }
            pan_right_value = 0;
            pan_down_value = 0;
            cme = new float[][] { colormatrix_r, colormatrix_g, colormatrix_b, colormatrix_a, colormatrix_t };
            cm = new ColorMatrix(cme);
            Bitmap maptemplate = new Bitmap(w, h,PixelFormat.Format24bppRgb);
            Graphics gt = Graphics.FromImage(maptemplate);
            //gt.FillRectangle(new SolidBrush(Color.Magenta), new Rectangle(0, 0, w, h));
            //maptemplate.MakeTransparent(Color.Magenta);
            map = (Bitmap)maptemplate.Clone();
            grid = (Bitmap)maptemplate.Clone();
            coordNum = (Bitmap)maptemplate.Clone();
            maptemplate.Dispose();
            COLOR_MAP_ENTRY = new ColorMap();
            COLOR_MAP_ENTRY.OldColor = Color.Magenta;
            COLOR_MAP_ENTRY.NewColor = Color.Transparent;
            //underlay_bmp = new Bitmap(underlay);
            //blackwhite = grid.Palette;  //这段操作必须是：先创建新的颜色表，再填写颜色表，最后把颜色表放回位图。
            //blackwhite.Entries[0] = Color.Transparent;
            //blackwhite.Entries[1] = Color.Black;
            //grid.Palette = blackwhite;
            InitializeComponent();

        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (!initFlag)
            {
                toolButtons.Add(panview);
                toolButtons.Add(drawTile);
                toolInUse = Tools.pan;
                panview.Enabled = false;
                initFlag = true;
                Mapgen.ReadMap("1.txt");
            }
            // Controls.Add(pictureBox1);
            //pictureBox1.BackgroundImage = underlay;

        }

        // Events

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Generate_Click(object sender, EventArgs e)
        {
            int w = 0, h = 0;

            Pen blackpen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(Color.Black);

            if (SETTINGS.TRUE_COL)
            {
                w = SETTINGS.WIDTH * 3 / 4 * SETTINGS.COL + SETTINGS.WIDTH + 10;
                h = SETTINGS.HEIGHT * SETTINGS.ROW + SETTINGS.HEIGHT + 10;
            }
            else
            {
                w = SETTINGS.WIDTH * SETTINGS.COL + SETTINGS.WIDTH / 2 + 10;
                h = SETTINGS.HEIGHT * 3 / 4 * SETTINGS.ROW + SETTINGS.HEIGHT / 4 + 10;
            }
            Bitmap maptemplate = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            map = (Bitmap)maptemplate.Clone();
            grid = (Bitmap)maptemplate.Clone();
            coordNum = (Bitmap)maptemplate.Clone();
            maptemplate.Dispose();

            Graphics gMap = Graphics.FromImage(map);
            Graphics gText = Graphics.FromImage(coordNum);
            Graphics gGrid = Graphics.FromImage(grid);
            brush.Color = Color.Magenta;
            gMap.FillRectangle(brush, new Rectangle(0, 0, w, h));
            gText.FillRectangle(brush, new Rectangle(0, 0, w, h));
            gGrid.FillRectangle(brush, new Rectangle(0, 0, w, h));


            GraphicsPath stamppath = new GraphicsPath();
            stamppath.AddLines(outline.hexPathPointOpen_tc);
            Region stampregion = new Region(stamppath);
            foreach (Point p in Mapgen.map.Keys)
            {
                int[] pxy = Coord.map_pic(p.X, p.Y);
                brush.Color = Mapgen.stampBase[Mapgen.map[p]].color;
                gMap.FillPolygon(brush, outline.Brush_Outline(pxy[0], pxy[1], 0, false));
                gGrid.DrawPolygon(blackpen, outline.Brush_Outline(pxy[0], pxy[1], 0, false));
                gText.DrawString(string.Format("{0:D},{1:D}", p.X, p.Y), fnt, new SolidBrush(Color.Black), pxy[0], pxy[1]);
            }
            //map.MakeTransparent(Color.Magenta);
            //grid.MakeTransparent(Color.Magenta);
            //coordNum.MakeTransparent(Color.Magenta); // 一旦用了MakeTransparent，图像属性会变回32ARGB
            //DebugLabel.Text = map.PixelFormat.ToString();
            //gMap.DrawPolygon(blackpen,outline.mapOutlineOpen_tc);
            //List<Point> ptsToDraw = new List<Point>();


            brush.Dispose();
            gMap.Dispose();
            gText.Dispose();
            gGrid.Dispose();
            blackpen.Dispose();
            //yellowbrush.Dispose();
            pictureBox1.Invalidate();

        }

        private void zup_Click(object sender, EventArgs e)
        {
            DebugLabel.Text = "Zoom + Clicked";
        }
        private void zdn_Click(object sender, EventArgs e)
        {

        }

        private void aup_Click(object sender, EventArgs e)
        {
            DebugLabel.Text = "Alpha + Clicked";
        }

        private void adn_Click(object sender, EventArgs e)
        {
            DebugLabel.Text = "Alpha - Clicked";
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void othersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }





        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics ge = e.Graphics;

            ImageAttributes attr = new ImageAttributes();
            //Rectangle sourcerect = new Rectangle(0, 0, underlay.Width, underlay.Height);
            Rectangle destrect = new Rectangle(pan_right_value, pan_down_value, underlay.Width, underlay.Height);
            Rectangle maprect = new Rectangle(pan_right_value, pan_down_value, map.Width, map.Height);
            attr.SetColorMatrix(cm);
            attr.SetRemapTable(new ColorMap[] { COLOR_MAP_ENTRY }, ColorAdjustType.Bitmap);
            //attr.SetColorKey(Color.Magenta, Color.Black);
            ge.DrawImage(underlay, destrect, 0,0,underlay.Width,underlay.Height, GraphicsUnit.Pixel, attr);
            //ge.DrawImage(underlay, pan_right_value, pan_down_value,attr);
            ge.DrawImage(map, maprect, 0,0,map.Width,map.Height,GraphicsUnit.Pixel,attr);
            ge.DrawImage(grid, maprect, 0,0, grid.Width, grid.Height,GraphicsUnit.Pixel,attr);
            ge.DrawImage(coordNum, maprect, 0,0, coordNum.Width, coordNum.Height,GraphicsUnit.Pixel,attr);
            //ge.Dispose();
        }

        private void panview_Click(object sender, EventArgs e)
        {
            toolInUse = Tools.pan;
            foreach (Button b in toolButtons)
            {
                //DebugLabel.Text = b.Text;
                b.Enabled = true;
            }
            panview.Enabled = false;
            //drawTile.Enabled = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void drawTile_Click(object sender, EventArgs e)
        {
            toolInUse = Tools.draw;
            foreach (Button b in toolButtons)
            {
                //DebugLabel.Text = b.Text;
                b.Enabled = true;
            }
            //panview.Enabled = true;
            drawTile.Enabled = false;
        }

        // reserved for bucket(flood fill)
        private void DrawMapTile(int x, int y, string newname)
            /*
             * 输入值是画布坐标。顺序如下：
             * 将画布坐标转换成图面坐标和网格坐标。
             * 按照网格坐标生成要更换名字的图面坐标和网格坐标。
             * 更换图面坐标和网格坐标对应的颜色名字
             * 在图面坐标所示点绘制多边形
             */
        {
            int[] pxy = Coord.canvas_pic(x, y, pan_right_value, pan_down_value);
            int[] mxy = Coord.pic_map_fast(pxy);
            int[] mpxy = Coord.map_pic(mxy[0], mxy[1]);
            int mx = mxy[0], my = mxy[1];
            int mxm = 0, mym = 0, pxm = 0, pym = 0;//short hand for map_x/y_modified
            int r = (int)numericUpDown1.Value;
            int ub = r;
            int lb = -r;
            Point mmp = new Point(0, 0); //short hand for map/pic_modified_point
            Point mpp = new Point(0, 0);


            Color newcolor = Mapgen.stampBase[newname].color;
            Graphics gm = Graphics.FromImage(map);
            GraphicsPath gp = new GraphicsPath();
            GraphicsPath ol = new GraphicsPath();
            gp.AddLines(outline.Brush_Outline(mpxy[0], mpxy[1], r, true));
            ol.AddLines(outline.Map_Outline(true));
            Region rp = new Region(gp);
            Region o = new Region(ol);
            gm.IntersectClip(rp);
            gm.IntersectClip(o);
            gm.Clear(newcolor);
            pictureBox1.Invalidate();
            gm.Dispose();
            gp.Dispose();
            ol.Dispose();
            rp.Dispose();
            o.Dispose();


            for (int i = 0; i <= r; i++)
            {
                for (int j = ub; j >= lb; j--)
                {
                    //ret.Add(new Point(i, j));
                    if (my + j < SETTINGS.ROW && my + j > 0)
                    {
                        mym = my + j;
                        if (mx + i < SETTINGS.COL)// 0 col and right col
                        {
                            mxm = mx + i;
                            int[] pxym = Coord.map_pic(mxm, mym);
                            mmp.X = mxm;
                            mmp.Y = mym;
                            mpp.X = pxym[0];
                            mpp.Y = pxym[1];
                            Mapgen.map[mmp] = newname;
                            Mapgen.map_p[mpp] = newname;
                        }
                        if (i > 0) // left col
                        {
                            if (mx - i > 0)
                            {
                                mxm = mx - i;

                                int[] pxym = Coord.map_pic(mxm, mym);
                                mmp.X = mxm;
                                mmp.Y = mym;
                                mpp.X = pxym[0];
                                mpp.Y = pxym[1];
                                Mapgen.map[mmp] = newname;
                                Mapgen.map_p[mpp] = newname;
                            }
                        }
                    }
                }
                if (i % 2 == 0)
                {
                    lb += 1;
                }
                else ub -= 1;
            }


        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //int x = e.X;
            //int y = e.Y;
            //int mx = x/(SETTINGS.WIDTH/4*3); // flooring.
            //int my = (y-(mx%2)*SETTINGS.HEIGHT/2) / SETTINGS.HEIGHT;
            //DebugLabel.Text = string.Format("x = {0:d}, y = {1:d}", mx, my);

            int posx = e.X;
            int posy = e.Y;
            isDragging = true;
            int[] piccoord = Coord.canvas_pic(posx, posy, this.pan_right_value, this.pan_down_value);
            //int[] gridcoord = Coord.pic_map(piccoord[0], piccoord[1], 0, 0);
            switch (toolInUse)
            {
                case Tools.draw:
                    DrawMapTile(e.X, e.Y, "grassland");

                    break;
                case Tools.pan:

                    x0 = -pan_right_value + e.X;
                    y0 = -pan_down_value + e.Y;
                    break;
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            int[] pxy = Coord.canvas_pic(new int[] { x, y },pan_right_value, pan_down_value);
            int[] mxy = Coord.pic_map_fast(pxy[0], pxy[1]);
            //int mx = x / (SETTINGS.WIDTH / 4 * 3); // flooring.
            //int my = (y - (mx % 2) * SETTINGS.HEIGHT / 2) / SETTINGS.HEIGHT;
            DebugLabel.Text = string.Format("x = {0:d}, y = {1:d}", mxy[0], mxy[1]);
            if (isDragging)
            {
            switch (toolInUse)
                {
                    case Tools.pan:
                        {
                            int dx = e.X - x0;
                            int dy = e.Y - y0;
                            int xm = max(underlay.Width, map.Width);
                            int ym = max(underlay.Height, map.Height);
                            debugText.Text = underlay.Width.ToString();
                            pan_right_value = max(min(dx, SETTINGS.WIDTH), (int)(pictureBox1.Width / 2) - xm);
                            pan_down_value = max(min(dy, SETTINGS.HEIGHT), -(ym - (int)(pictureBox1.Height / 2)));
                            pictureBox1.Invalidate();
                        }
                        break;
                    case Tools.draw:
                        DrawMapTile(e.X, e.Y, "grassland");
                        //int posx = e.X;
                        //int posy = e.Y;
                        //int[] piccoord = Coord.canvas_pic(posx, posy, this.pan_right_value, this.pan_down_value);
                        //int[] gridcoord = Coord.pic_map(piccoord[0], piccoord[1], 0, 0);
                        //Graphics gm = Graphics.FromImage(map);
                        //GraphicsPath gp = new GraphicsPath();
                        //gp.AddLines(SETTINGS.hex_path_tc_grid(gridcoord[0], gridcoord[1]));
                        //Region rp = new Region(gp);
                        //SolidBrush redbrush = new SolidBrush(Color.Red);
                        //gm.IntersectClip(rp);
                        ////gm.FillPath(redbrush,gp);
                        //gm.Clear(Color.FromArgb(127, 255, 0, 0));
                        //pictureBox1.Invalidate();
                        break;
                }

            }


        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
        private int max(int a, int b)
        {
            return a > b ? a : b;
        }
        private int min(int a, int b)
        {
            return a > b ? b : a;
        }
    }

}
