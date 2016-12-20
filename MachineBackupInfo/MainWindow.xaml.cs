using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MachineBackupInfo.Classes;
using System.ComponentModel;
using System.IO;
using MachineBackupInfo.Windows;

namespace MachineBackupInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Statistics stats;

        public MainWindow()
        {
            InitializeComponent();
            IOHandler.CreateRootPath();
            IOHandler.CreateChildDirectories();
            stats = new Statistics();
            DataContext = stats;

        }

        private void btnPropertyData_Click(object sender, RoutedEventArgs e)
        {
            stats.PropertiesWithBackups.Clear();
            BackUpInfoWindow bkupInfoWindowWithBackups = new BackUpInfoWindow("Properties With Backups (Cached = " + stats.CacheData.ToString() + ")", stats.CacheData, stats);
            bkupInfoWindowWithBackups.Show();
        }

       
    }
}

       


 
