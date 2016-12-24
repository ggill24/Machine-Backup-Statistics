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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MachineBackupInfo.Classes;
using System.ComponentModel;
namespace MachineBackupInfo.Windows
{
    /// <summary>
    /// Interaction logic for BackUpInfoWindow.xaml
    /// </summary>
    public partial class BackUpInfoWindow : MetroWindow
    {


        Statistics stats;

        string title { get; set; }

        List<Property> data = new List<Property>();

        public BackUpInfoWindow(string title, Statistics statisticsClass)
        {
            InitializeComponent();
            DataContext = this;
            lblTitle.Content = title;
            stats = statisticsClass;
            this.title = title;
            GetAndDisplayData();
        }
        void GetAndDisplayData()
        {
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork += new DoWorkEventHandler(BackGroundDoWork);
            bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackGroundWorkCompleted);
            if (!bg.IsBusy) { bg.RunWorkerAsync(); }
        }



        private void BackGroundDoWork(object sender, DoWorkEventArgs e)
        {
                data = stats.PropertyData(stats.BackupDirectory);
        }

        private void DisplayData()
        {
            dataGridtest.ItemsSource = data;
           
        }
        private void BackGroundWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DisplayData();
        }
       
    }
}


