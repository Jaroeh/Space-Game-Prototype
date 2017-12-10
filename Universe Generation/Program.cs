using System;
using System.Windows.Forms;

namespace Space_Explorer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(defaultValue: false);
            Application.Run(mainForm: new MainForm());  //TODO: Remove this comment
        }
    }
}
