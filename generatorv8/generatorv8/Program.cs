using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace generatorv8
{
    public partial class Program
    {
        [STAThread]
        static void Main()
        {
            Directory.SetCurrentDirectory("../../../..");
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());



        }

    }
}

