using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

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
        private Font fnt = new Font("Arial", 10);
        private byte[] pointType6 = { 0, 1, 1, 1, 1, 128 };
        private Image underlay = Image.FromFile("battlefield_overview.png");
        private Bitmap map = new Bitmap(SETTINGS.WIDTH * SETTINGS.COL+10, SETTINGS.HEIGHT * SETTINGS.ROW+10);
        private Bitmap grid = new Bitmap(SETTINGS.WIDTH * SETTINGS.COL, SETTINGS.HEIGHT * SETTINGS.ROW);
        private Bitmap coordNum = new Bitmap(SETTINGS.WIDTH * SETTINGS.COL, SETTINGS.HEIGHT * SETTINGS.ROW);
        private byte[] pointType7 = { (byte)PathPointType.Start, 1, 1, 1, 1, 1, 128 };
        private List<Control> toolButtons = new List<Control>();
        //private int[] brush3 = {
        //    0,1,
        //    1,0,
        //    0,-1,
        //    -1,-1,
        //    -1,1,
        //    0,1
        //}

        private List<Point> getSizedBrush(int r)
        {
            List<Point> ret = new List<Point>();
            if (SETTINGS.TRUE_COL)
            {
                int ub = 2;
                int lb = -2;
                for (int i =0; i<= (r-1)/2; i++)
                {
                    for (int j = ub; j >= lb; j--)
                    {
                        ret.Add(new Point(i, j));
                        if (i>0)
                        {
                            ret.Add(new Point(-i, j));
                        }
                    }
                    if (i % 2 == 0)
                    {
                        lb += 1;
                    }
                    else ub -= 1;
                }
            }
            return ret;
        }

        public MainForm()
        {
            pan_right_value = 0;
            pan_down_value = 0;

            InitializeComponent();

        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (!initFlag)
            {
                int w = 0, h =0;
                if (SETTINGS.TRUE_COL)
                {
                    w = SETTINGS.WIDTH * 3 / 4 * SETTINGS.COL + SETTINGS.WIDTH / 4+10;
                    h = SETTINGS.HEIGHT* SETTINGS.ROW+SETTINGS.HEIGHT/2+10;
                }else
                {
                    w = SETTINGS.WIDTH * SETTINGS.COL + SETTINGS.WIDTH / 2+10;
                    h = SETTINGS.HEIGHT * 3 / 4 * SETTINGS.ROW + SETTINGS.HEIGHT / 4+10;
                }
                map = new Bitmap(w, h);
                grid = new Bitmap(w, h);
                coordNum = new Bitmap(w, h);

                toolButtons.Add(panview);
                toolButtons.Add(drawTile);
                toolInUse = Tools.pan;
                panview.Enabled = false;
                initFlag = true;
            }
            // Controls.Add(pictureBox1);
            //pictureBox1.BackgroundImage = underlay;

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void zup_Click(object sender, EventArgs e)
        {
            DebugLabel.Text = "Zoom + Clicked";
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

        private void zdn_Click(object sender, EventArgs e)
        {

        }

        private void Generate_Click(object sender, EventArgs e)
        {
            Pen blackpen = new Pen(Color.Black);
            SolidBrush yellowbrush = new SolidBrush(Color.FromArgb(127, 255, 255, 0));
            // Graphics gUnderlay = Graphics.FromImage(underlay);
            Graphics gMap = Graphics.FromImage(map);
            Graphics gText = Graphics.FromImage(coordNum);
            Graphics gGrid = Graphics.FromImage(grid);
            for (int x = 0; x< SETTINGS.COL; x++)
            {
                for (int y = 0; y< SETTINGS.ROW; y++)
                {
                    gMap.FillPolygon(yellowbrush, SETTINGS.hex_tc_grid(x, y));
                    int[] txtpos = Coord.map_pic(x, y);
                    gText.DrawString(string.Format("{0:D}:{1:D}", x, y), fnt, new SolidBrush(Color.Black), new Point(txtpos[0],txtpos[1]));
                    //gGrid.DrawPolygon(blackpen, SETTINGS.hex_tc_grid(x, y));
                    gGrid.DrawLines(blackpen, SETTINGS.outer_border_tc());
                }
            }
            gMap.Dispose();
            gText.Dispose();
            gGrid.Dispose();
            blackpen.Dispose();
            yellowbrush.Dispose();
            pictureBox1.Invalidate();
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics ge = e.Graphics;

            ge.DrawImage(underlay, pan_right_value, pan_down_value); //TODO: change x, y to a more representative name.
            ge.DrawImage(map, pan_right_value, pan_down_value);
            ge.DrawImage(grid, pan_right_value, pan_down_value);
            ge.DrawImage(coordNum, pan_right_value, pan_down_value);

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
        private void DrawMapTile(int x, int y)
        {
            /*
            之后要考虑和地图数据联动。考虑以下顺序：
            从事件收到的位置数据是画布坐标。先把画布坐标转换成图面坐标。只要转换
             
             */



            Graphics gm = Graphics.FromImage(map);
            GraphicsPath gp = new GraphicsPath();
            GraphicsPath ol = new GraphicsPath();
            int[] piccoord = Coord.canvas_pic(x, y, this.pan_right_value, this.pan_down_value);
            int[] gridcoord = Coord.pic_map(piccoord[0], piccoord[1], 0, 0);
            gp.AddLines(SETTINGS.hex_path_brush_tc_grid(gridcoord[0], gridcoord[1], ((int)numericUpDown1.Value - 1) / 2));
            ol.AddLines(SETTINGS.outer_border_tc());
            Region rp = new Region(gp);
            Region o = new Region(ol);
            gm.IntersectClip(rp);
            gm.IntersectClip(o);
            //gm.DrawImage()
            //gm.FillPath(redbrush,gp);
            gm.Clear(Color.FromArgb(30, 255, 0, 0));
            pictureBox1.Invalidate();
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
            int[] gridcoord = Coord.pic_map(piccoord[0], piccoord[1], 0, 0);
            switch (toolInUse)
            {
                case Tools.draw:
                    DrawMapTile(e.X, e.Y);
                    //PictureBox1.Invalidate();
                    //Graphics gm = Graphics.FromImage(map);
                    //GraphicsPath gp = new GraphicsPath();
                    ////gp.AddLines(SETTINGS.hex_path_tc_grid(gridcoord[0], gridcoord[1]));
                    //gp.AddLines(SETTINGS.hex_path_brush_tc_grid(gridcoord[0], gridcoord[1], ((int)numericUpDown1.Value-1)/2));
                    //Region rp = new Region(gp);
                    //SolidBrush redbrush = new SolidBrush(Color.Red);
                    //gm.IntersectClip(rp);
                    ////gm.FillPath(redbrush,gp);
                    //gm.Clear(Color.FromArgb(127, 255, 0, 0));
                    //pictureBox1.Invalidate();
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
            int mx = x / (SETTINGS.WIDTH / 4 * 3); // flooring.
            int my = (y - (mx % 2) * SETTINGS.HEIGHT / 2) / SETTINGS.HEIGHT;
            DebugLabel.Text = string.Format("x = {0:d}, y = {1:d}", mx, my);
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
                        DrawMapTile(e.X, e.Y);
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
