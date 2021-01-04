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
        private int x, y, x0,y0;
        private int x_zm, y_zm;
        private bool isDragging = false;
        private Font fnt = new Font("Arial", 10);
        private byte[] pointType6 = { 0, 1, 1, 1, 1, 128 };
        private Image underlay = Image.FromFile("battlefield_overview.png");
        private Bitmap map = new Bitmap(SETTINGS.WIDTH * SETTINGS.COL, SETTINGS.HEIGHT * SETTINGS.ROW);
        private Bitmap grid = new Bitmap(SETTINGS.WIDTH * SETTINGS.COL, SETTINGS.HEIGHT * SETTINGS.ROW);
        private Bitmap coordNum = new Bitmap(SETTINGS.WIDTH * SETTINGS.COL, SETTINGS.HEIGHT * SETTINGS.ROW);
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
                    gGrid.DrawPolygon(blackpen, SETTINGS.hex_tc_grid(x, y));
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

            ge.DrawImage(underlay, x, y); //TODO: change x, y to a more representative name.
            ge.DrawImage(map, x, y);
            ge.DrawImage(grid, x, y);
            ge.DrawImage(coordNum, x, y);

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
            toolInUse = Tools.pan;
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
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int posx = e.X;
            int posy = e.Y;
            //int[] 
            int[] piccoord = Coord.canvas_pic(posx, posy, this.x, this.y);
            int[] gridcoord = Coord.pic_map(piccoord[0], piccoord[1], 0,0);

            string[] linesToPush = new string[3];
            debugText.Lines = new string[10];
            debugText.ResetText();
            //linesToPush[0] = 
            debugText.AppendText(string.Format("movement x = {0:D}, y = {1:D}\n", this.x, this.y));

            debugText.AppendText(string.Format("mouse x = {0:D}, y = {1:D}\n", e.X, e.Y));
            //linesToPush[1] = 
            debugText.AppendText(string.Format("pic x = {0:D}, y = {1:D}\n", piccoord[0], piccoord[1]));
            //linesToPush[2] = 
            debugText.AppendText(string.Format("grid x = {0:D}, y = {1:D}\n", gridcoord[0], gridcoord[1]));
            //string stringToPush = linesToPush[0]+"\n"+linesToPush[1]+"\n"+linesToPush[2];
               
            //debugText.Text = stringToPush;
            //foreach (string l in linesToPush)
            //{
            //    debugText.Text += (l + "\n\r");
            //}
            //Array.Copy(linesToPush.ToArray(), debugText.Lines, 3);
            GraphicsPath p = new GraphicsPath();
            p.AddLines(SETTINGS.hex_path_tc_pic(-SETTINGS.WIDTH/2+this.x,-SETTINGS.HEIGHT/2+this.y));
            Pen blackpen = new Pen(Color.Black);
            //SolidBrush alphabrush = new SolidBrush(Color.Transparent);
            Graphics g = pictureBox1.CreateGraphics();
            g.DrawPath(blackpen, p);
            //g.FillPath(alphabrush, p);
            if (p.IsVisible(posx, posy)) //这个方法就算不画出来也有效。
            {
                DebugLabel.Text = "visible";
            }
            else DebugLabel.Text = "not visible";
            isDragging = true;
            x0 = -x + e.X;
            y0 = -y + e.Y;
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
