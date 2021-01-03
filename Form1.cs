using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace hexMapMaker_CS
{
    public partial class MainForm : Form
    {
        private Font fnt = new Font("Arial", 10);
        private int row = 120;
        private int col = 120;
        private int width = 48;
        private int height = 48;
        private byte[] pointType6 = { 0, 1, 1, 1, 1, 128 };
        private Image underlay = Image.FromFile("battlefield_overview.png");
        private byte[] pointType7 = { (byte)PathPointType.Start, 1, 1, 1, 1, 1, 128 };
        private Point[] hex_tc(int xoff, int yoff)
        {
            yoff = yoff * height + height / 2 * (xoff % 2);
            xoff = xoff * width * 3 / 4;

                Point[] temp = new Point[6];
                temp[0] = new Point(width * 3 / 4+xoff, 0+yoff);
                temp[1] = new Point(width+xoff, height / 2+yoff);
                temp[2] = new Point(width * 3 / 4+xoff, height+yoff);
                temp[3] = new Point(width / 4+xoff, height+yoff);
                temp[4] = new Point(0+xoff, height / 2+yoff);
                temp[5] = new Point(width / 4+xoff, 0+yoff);
                return temp;
        }
        private Point[] hex_path_tc(int xoff , int yoff)
        {
            yoff = yoff * height + height / 2 * (xoff % 2);
            xoff = xoff * width * 3 / 4;

            Point[] temp = new Point[7];
            temp[0] = new Point(width * 3 / 4 + xoff, 0 + yoff);
            temp[1] = new Point(width + xoff, height / 2 + yoff);
            temp[2] = new Point(width * 3 / 4 + xoff, height + yoff);
            temp[3] = new Point(width / 4 + xoff, height + yoff);
            temp[4] = new Point(0 + xoff, height / 2 + yoff);
            temp[5] = new Point(width / 4 + xoff, 0 + yoff);
            temp[6] = new Point(width * 3 / 4 + xoff, 0 + yoff);
            return temp;
        }
        public MainForm()
        {
            InitializeComponent();

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
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
            Bitmap redstamp = new Bitmap(width, height);
            SolidBrush redbrush = new SolidBrush(Color.FromArgb(255, 255, 0, 0));
            Pen blackpen = new Pen(Color.FromArgb(255, 255, 255, 255));
            Graphics grs = Graphics.FromImage(redstamp);
            Graphics gcan = pictureBox1.CreateGraphics();
            grs.FillPolygon(redbrush, hex_tc(0, 0));
            GraphicsPath voidstampPath = new GraphicsPath();
            voidstampPath.AddLines(hex_path_tc(0,0));
            Region voidstampReg = new Region(voidstampPath);
            //voidstampPath.CloseFigure();
            gcan.IntersectClip(voidstampReg);
            //gcan.IntersectClip(new RectangleF(0.0f,0.0f,width,height));
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
            Bitmap bmp = new Bitmap(width, height);
            Bitmap bmp2 = new Bitmap(width * col, height * row);
            SolidBrush sb = new SolidBrush(Color.FromArgb(127, 255, 255, 0));
            SolidBrush Transparent_Brush = new SolidBrush(Color.FromArgb(0, 0, 0, 0));

            Graphics gbmp = Graphics.FromImage(bmp);
            Graphics gbmp2 = Graphics.FromImage(bmp2);
            Graphics gcan = pictureBox1.CreateGraphics();
            gbmp.FillPolygon(Transparent_Brush, hex_tc(0, 0));
            //gpcs.DrawLine(blackPen, new Point(0, 0), new Point(100, 100));
            //gbmp.FillPolygon(sb, hex_tc);
            //gcan.DrawImage(underlay,0,0);
            for (int x = 0; x<col; x++)
            {
                for (int y = 0; y<row; y++)
                {
                    //gcan.DrawImage(bmp, x * width*3/4, ((y ) * height+height*(x%2)/2));
                    gbmp2.FillPolygon(sb, hex_tc(x,y));
                    //gbmp2.FillPolygon(sb, hex_tc(0, 0));
                }
            }
            gcan.DrawImage(bmp2, 0, 0);
            //gcan.DrawImage(bmp, 0, 0);
            
            //gpcs.Dispose();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
           // Controls.Add(pictureBox1);
            pictureBox1.BackgroundImage = underlay;

        }
        private void OnPaint(PaintEventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;

            // Draw a string on the PictureBox.
            //g.DrawString("This is a diagonal line drawn on the control",
            //    fnt, System.Drawing.Brushes.Blue, new Point(30, 30));
            // Draw a line in the PictureBox.
            //g.DrawLine(System.Drawing.Pens.Red, pictureBox1.Left, pictureBox1.Top,
            //    pictureBox1.Right, pictureBox1.Bottom);
            Bitmap bmp = new Bitmap(width, height);
            Bitmap bmp2 = new Bitmap(width * col, height * row);
            SolidBrush sb = new SolidBrush(Color.FromArgb(127, 255, 255, 0));
            SolidBrush Transparent_Brush = new SolidBrush(Color.FromArgb(0, 0, 0, 0));

            Graphics gbmp = Graphics.FromImage(bmp);
            Graphics gbmp2 = Graphics.FromImage(bmp2);
            Graphics ge = e.Graphics;
            gbmp.FillPolygon(Transparent_Brush, hex_tc(0, 0));
            //gpcs.DrawLine(blackPen, new Point(0, 0), new Point(100, 100));
            //gbmp.FillPolygon(sb, hex_tc);
            //gcan.DrawImage(underlay,0,0);
            for (int x = 0; x < col; x++)
            {
                for (int y = 0; y < row; y++)
                {
                    //gcan.DrawImage(bmp, x * width*3/4, ((y ) * height+height*(x%2)/2));
                    gbmp2.FillPolygon(sb, hex_tc(x, y));
                    //gbmp2.FillPolygon(sb, hex_tc(0, 0));
                }
            }
            ge.DrawImage(bmp2, 0, 0);
            //gcan.DrawImage(bmp, 0, 0);

        }
    }
}
