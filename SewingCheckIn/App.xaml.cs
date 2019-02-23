using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Reflection;

namespace SewingCheckIn
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private App()
        {
            InitializeComponent();
        }
        [STAThread]
        private static void Main()
        {
            App app = new App();
            app.Run(new MainWindow());

            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
            // Get the connectionStrings section. 
            ConfigurationSection configSection = config.GetSection("connectionStrings");
            //Ensures that the section is not already protected.
            if (!configSection.SectionInformation.IsProtected)
            {
                //Uses the Windows Data Protection API (DPAPI) to encrypt the configuration section using a machine-specific secret key.
                configSection.SectionInformation.ProtectSection(
                    "DataProtectionConfigurationProvider");
                config.Save();
            }
        }
    }
}
