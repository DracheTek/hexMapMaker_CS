using System;
using System.Windows.Forms;
using System.Drawing;

namespace hexMapMaker_CS
{
    public partial class MainForm : Form
    {
        private int row = 120;
        private int col = 120;
        private int width = 48;
        private int height = 48;
        private Point[] hex_tc
        {
            get
            {
                Point[] temp = new Point[6];
                temp[0] = new Point(width * 3 / 4, 0);
                temp[1] = new Point(width, height / 2);
                temp[2] = new Point(width * 3 / 4, height);
                temp[3] = new Point(width / 4, height);
                temp[4] = new Point(0, height / 2);
                temp[5] = new Point(width / 4, 0);
                return temp;
            }
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
            DebugLabel.Text = "Zoom - Clicked";
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            //DebugLabel.Text = "Generate Clicked";
            //Pen blackPen = new Pen(Color.Black, 3);
            Bitmap bmp = new Bitmap(width, height);
            SolidBrush sb = new SolidBrush(Color.FromArgb(127, 255, 255, 0));
            Image underlay = Image.FromFile("battlefield_overview.png")
            Graphics gbmp = Graphics.FromImage(bmp);
            Graphics gcan = pictureBox1.CreateGraphics();

            //gpcs.DrawLine(blackPen, new Point(0, 0), new Point(100, 100));
            gbmp.FillPolygon(sb, hex_tc);
            gcan.DrawImage(underlay,0,0);
            gcan.DrawImage(bmp, 0, 0);

            //gpcs.Dispose();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {

        }
    }
}
