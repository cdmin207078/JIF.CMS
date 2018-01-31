using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutofacSample.Winform
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 注册依赖入口
            DependencyRegister.Register();

            Application.Run(new Form1());
        }
    }
}
