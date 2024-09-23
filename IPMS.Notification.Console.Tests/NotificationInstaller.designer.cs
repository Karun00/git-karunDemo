namespace IPMS.Notifications.Engine
{
    partial class NotificationInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NotificationProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.NotificationserviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            // 
            // NotificationProcessInstaller1
            // 
            this.NotificationProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.NotificationProcessInstaller1.Password = null;
            this.NotificationProcessInstaller1.Username = null;
            this.NotificationProcessInstaller1.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.NotificationProcessInstaller1_AfterInstall);
            // 
            // NotificationserviceInstaller1
            // 
            this.NotificationserviceInstaller1.ServiceName = "IPMS Notifications";
            this.NotificationserviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.NotificationserviceInstaller1.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.NotificationInstaller1_AfterInstall);
            // 
            // NotificationInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.NotificationProcessInstaller1,
            this.NotificationserviceInstaller1});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller NotificationProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller NotificationserviceInstaller1;
    }
}