using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Threading;
using System.Reflection;

using SewingIncentives.Views;
namespace SewingIncentives
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
            bool blCheck;
            Mutex m = new Mutex(true, "SewingIncentives", out blCheck);

            if (blCheck == false)
            {
                MessageBox.Show("Sewing Incentives Is Already Running. Try Again!", "Sewing Incentives", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            App app = new App();
            app.Run(new SelectSectionWindow());
            //app.Run(new SpecialTransferWindow());

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
