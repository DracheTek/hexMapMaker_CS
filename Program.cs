using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace hexMapMaker_CS
{
    public static class Program
    {
        public static string nl = System.Environment.NewLine;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mf = new MainForm();
            Application.Run(mf);
        }
    }
}
