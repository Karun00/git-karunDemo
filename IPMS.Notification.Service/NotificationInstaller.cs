using System.ComponentModel;
using System.Configuration.Install;

namespace IPMSNotificationService
{
    [RunInstaller(true)]
    public partial class NotificationInstaller : System.Configuration.Install.Installer
    {
        public NotificationInstaller()
        {
            InitializeComponent();
        }
        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }

    }
}
