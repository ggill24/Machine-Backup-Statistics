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

        private void btnPropertyWithBackups_Click(object sender, RoutedEventArgs e)
        {
            stats.PropertiesWithBackups.Clear();
            BackUpInfoWindow bkupInfoWindowWithBackups = new BackUpInfoWindow("Properties With Backups (Cached = " + stats.CacheData.ToString() + ")", DataType.PropertiesWithBackups, stats.CacheData, stats);
            bkupInfoWindowWithBackups.Show();
        }

        private void btnPropertyWithoutBackups_Click(object sender, RoutedEventArgs e)
        {
            stats.PropertiesWithoutBackups.Clear();
            BackUpInfoWindow bkupInfoWindowWithoutBackups = new BackUpInfoWindow("Properties Without Backups (Cached = " + stats.CacheData.ToString() + ")", DataType.PropertiesWithNoBackups, stats.CacheData, stats);
            bkupInfoWindowWithoutBackups.Show();
        }
    }
}

       


 
