using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace hexMapMaker_CS
{
    enum tools
    {
        pan = 0,
        draw = 1
    }

    static class Coord
    {
        //public static int[] pic_canvas(int x, int y)
        //{

        //}
        //public static int[] canvas_pic(int x, int y)
        //{

        //}
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
                        p.AddLines(SETTINGS.hex_path_tc(mx, my));
                        if (p.IsVisible(x - xoff, y - yoff))
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
                        p.AddLines(SETTINGS.hex_path_tr(mx, my));
                        if (p.IsVisible(x - xoff, y - yoff))
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
        //public static int[] map_pic(int x, int y)
        //{

        //}
    }
    public partial class MainForm : Form
    {
        private bool initFlag = false;
        private tools toolInUse = tools.pan;
        private int x, y, x0,y0;
        private int x_zm, y_zm;
        private bool isDragging = false;
        private Font fnt = new Font("Arial", 10);
        private byte[] pointType6 = { 0, 1, 1, 1, 1, 128 };
        private Image underlay = Image.FromFile("battlefield_overview.png");
        private Bitmap map = new Bitmap(SETTINGS.WIDTH * SETTINGS.COL, SETTINGS.HEIGHT * SETTINGS.ROW);
        private Bitmap grid = new Bitmap(SETTINGS.WIDTH * SETTINGS.COL, SETTINGS.HEIGHT * SETTINGS.ROW);
        private byte[] pointType7 = { (byte)PathPointType.Start, 1, 1, 1, 1, 1, 128 };
        private List<Control> toolButtons = new List<Control>();

        public MainForm()
        {
            x = 0;
            y = 0;

            InitializeComponent();

        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (!initFlag)
            {
                toolButtons.Add(panview);
                toolButtons.Add(drawTile);
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
            Bitmap redstamp = new Bitmap(SETTINGS.WIDTH, SETTINGS.HEIGHT);
            SolidBrush redbrush = new SolidBrush(Color.FromArgb(255, 255, 0, 0));
            Pen blackpen = new Pen(Color.FromArgb(255, 255, 255, 255));
            Graphics grs = Graphics.FromImage(redstamp);
            Graphics gcan = pictureBox1.CreateGraphics();
            grs.FillPolygon(redbrush, SETTINGS.hex_tc(0, 0));
            GraphicsPath voidstampPath = new GraphicsPath();
            voidstampPath.AddLines(SETTINGS.hex_path_tc(0,0));
            Region voidstampReg = new Region(voidstampPath);
            //voidstampPath.CloseFigure();
            gcan.IntersectClip(voidstampReg);
            //gcan.IntersectClip(new RectangleF(0.0f,0.0f,SETTINGS.WIDTH,SETTINGS.HEIGHT));
            //gcan.FillRectangle(redbrush,gcan.VisibleClipBounds);
            //gcan.DrawPath(blackpen, voidstampPath);
            //gcan.FillPath(redbrush, voidstampPath);
            //gcan.FillRegion(redbrush, voidstampReg);
            gcan.Clear(Color.FromArgb(0, 0, 0, 0));
            gcan.DrawImage(underlay, 0,0);
            gcan.DrawImage(redstamp, 0, 0);

            DebugLabel.Text = "Zoom - Clicked";
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            //DebugLabel.Text = "Generate Clicked";
            //Pen blackPen = new Pen(Color.Black, 3);
            Bitmap bmp = new Bitmap(SETTINGS.WIDTH, SETTINGS.HEIGHT);
            Bitmap bmp2 = new Bitmap(SETTINGS.WIDTH * SETTINGS.COL, SETTINGS.HEIGHT * SETTINGS.ROW);
            SolidBrush sb = new SolidBrush(Color.FromArgb(127, 255, 255, 0));
            SolidBrush Transparent_Brush = new SolidBrush(Color.FromArgb(0, 0, 0, 0));

            Graphics gbmp = Graphics.FromImage(bmp);
            Graphics gbmp2 = Graphics.FromImage(bmp2);
            Graphics gcan = pictureBox1.CreateGraphics();
            gbmp.FillPolygon(Transparent_Brush, SETTINGS.hex_tc(0, 0));
            //gpcs.DrawLine(blackPen, new Point(0, 0), new Point(100, 100));
            //gbmp.FillPolygon(sb, hex_tc);
            //gcan.DrawImage(underlay,0,0);
            for (int x = 0; x<SETTINGS.COL; x++)
            {
                for (int y = 0; y<SETTINGS.ROW; y++)
                {
                    //gcan.DrawImage(bmp, x * SETTINGS.WIDTH*3/4, ((y ) * SETTINGS.HEIGHT+SETTINGS.HEIGHT*(x%2)/2));
                    gbmp2.FillPolygon(sb, SETTINGS.hex_tc(x,y));
                    //gbmp2.FillPolygon(sb, hex_tc(0, 0));
                }
            }
            gcan.DrawImage(bmp2, 0, 0);
            //gcan.DrawImage(bmp, 0, 0);
            
            //gpcs.Dispose();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics ge = e.Graphics;

            ge.DrawImage(underlay, x, y);
            ge.DrawImage(map, x, y);
            ge.DrawImage(grid, x, y);

            //Graphics g = e.Graphics;

            //// Draw a string on the PictureBox.
            ////g.DrawString("This is a diagonal line drawn on the control",
            ////    fnt, System.Drawing.Brushes.Blue, new Point(30, 30));
            //// Draw a line in the PictureBox.
            ////g.DrawLine(System.Drawing.Pens.Red, pictureBox1.Left, pictureBox1.Top,
            ////    pictureBox1.Right, pictureBox1.Bottom);
            //Bitmap bmp = new Bitmap(SETTINGS.WIDTH, SETTINGS.HEIGHT);
            //Bitmap bmp2 = new Bitmap(SETTINGS.WIDTH * SETTINGS.COL, SETTINGS.HEIGHT * SETTINGS.ROW);
            //SolidBrush sb = new SolidBrush(Color.FromArgb(127, 255, 255, 0));
            //SolidBrush Transparent_Brush = new SolidBrush(Color.FromArgb(0, 0, 0, 0));

            //Graphics gbmp = Graphics.FromImage(bmp);
            //Graphics gbmp2 = Graphics.FromImage(bmp2);
            //Graphics ge = e.Graphics;
            //gbmp.FillPolygon(Transparent_Brush, hex_tc(0, 0));
            ////gpcs.DrawLine(blackPen, new Point(0, 0), new Point(100, 100));
            ////gbmp.FillPolygon(sb, hex_tc);
            ////gcan.DrawImage(underlay,0,0);
            //for (int x = 0; x < SETTINGS.COL; x++)
            //{
            //    for (int y = 0; y < SETTINGS.ROW; y++)
            //    {
            //        //gcan.DrawImage(bmp, x * SETTINGS.WIDTH*3/4, ((y ) * SETTINGS.HEIGHT+SETTINGS.HEIGHT*(x%2)/2));
            //        gbmp2.FillPolygon(sb, hex_tc(x, y));
            //        //gbmp2.FillPolygon(sb, hex_tc(0, 0));
            //    }
            //}
            //ge.DrawImage(bmp2, 0, 0);
            ////gcan.DrawImage(bmp, 0, 0);

        }

        private void panview_Click(object sender, EventArgs e)
        {
            toolInUse = tools.pan;
            foreach (Button b in toolButtons)
            {
                //DebugLabel.Text = b.Text;
                b.Enabled = true;
            }
            panview.Enabled = false;
            //drawTile.Enabled = true;
        }

        private void drawTile_Click(object sender, EventArgs e)
        {
            toolInUse = tools.draw;
            foreach (Button b in toolButtons)
            {
                //DebugLabel.Text = b.Text;
                b.Enabled = true;
            }
            //panview.Enabled = true;
            drawTile.Enabled = false;
        }

        // reserved for bucket(flood fill)
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            x0 = -x+ e.X;
            y0 = -y+ e.Y;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int dx = e.X - x0;
                int dy = e.Y - y0;
                x = dx;
                y = dy;
                pictureBox1.Invalidate();
            }

        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
