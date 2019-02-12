using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimulatorUI
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            StartUI();
        }
        public static void StartUI()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread t = new Thread(()=> TrafficLights.Program.StartLogic());
            t.Start();
            using (MainForm f = new MainForm())
            {
                Application.Run(f);
            }
            t.Join();
        }
    }
}
