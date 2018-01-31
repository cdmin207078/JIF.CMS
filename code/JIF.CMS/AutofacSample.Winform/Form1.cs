using Common.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutofacSample.Winform
{
    public partial class Form1 : Form
    {
        private ILog _logger;

        public Form1()
        {
            InitializeComponent();

            Init();
        }
        private void Init()
        {
            _logger = DependencyRegister.Resolve<ILog>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _logger.Info("点击按钮");
        }
    }
}
