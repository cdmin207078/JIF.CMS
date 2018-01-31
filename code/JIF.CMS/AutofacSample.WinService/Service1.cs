using Common.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AutofacSample.WinService
{
    public partial class Service1 : ServiceBase
    {
        private ILog _logger;

        public Service1()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            _logger = DependencyRegister.Resolve<ILog>();
        }

        protected override void OnStart(string[] args)
        {
            _logger.Info("OnStart");
        }

        protected override void OnPause()
        {
            _logger.Info("OnPause");

            base.OnPause();
        }

        protected override void OnContinue()
        {
            _logger.Info("OnContinue");

            base.OnContinue();
        }

        protected override void OnStop()
        {
            _logger.Info("OnStop");

            base.OnStop();
        }

        protected override void OnShutdown()
        {
            _logger.Info("OnShutdown");

            base.OnShutdown();
        }

    }
}
