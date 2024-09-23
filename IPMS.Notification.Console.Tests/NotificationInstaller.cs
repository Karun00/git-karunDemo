using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace IPMS.Notifications.Engine
{
    [RunInstaller(true)]
    public partial class NotificationInstaller : System.Configuration.Install.Installer
    {
        public NotificationInstaller()
        {
            InitializeComponent();
        }

        private void NotificationProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        private void NotificationInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
