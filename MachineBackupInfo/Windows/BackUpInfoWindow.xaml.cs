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
        DataType dataType;

        Statistics stats;

        bool cacheData;

        string title { get; set; }


        public BackUpInfoWindow(string title, DataType dataType, bool cacheData, Statistics statisticsClass)
        {
            InitializeComponent();
            DataContext = this;
            lblTitle.Content = title;
            stats = statisticsClass;
            this.dataType = dataType;
            this.cacheData = cacheData;
            this.title = title;
            GetAndDisplayData();
        }
        void GetAndDisplayData()
        {

            //Cache Data
            if (cacheData)
            {
                if (dataType == DataType.PropertiesWithBackups && CachedInfo.PropertiesWithBackups != null && CachedInfo.PropertiesWithBackups.Count > 0)
                {
                    foreach (var d in CachedInfo.PropertiesWithBackups)
                    {
                        listbxData.Items.Add(d);
                    }
                }
                else if (dataType == DataType.PropertiesWithNoBackups && CachedInfo.PropertiesWithoutBackups != null && CachedInfo.PropertiesWithoutBackups.Count > 0)
                {
                    foreach (var d in CachedInfo.PropertiesWithBackups)
                    {
                        listbxData.Items.Add(d);
                    }

                }
            }
            else
            {
                BackgroundWorker bg = new BackgroundWorker();
                bg.DoWork += new DoWorkEventHandler(BackGroundDoWork);
                bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackGroundWorkCompleted);
                if (!bg.IsBusy) { bg.RunWorkerAsync(); }
            }

        }
        private void BackGroundDoWork(object sender, DoWorkEventArgs e)
        {
            
          
            switch (dataType)
            {
                case DataType.PropertiesWithBackups:
                    stats.PropertieWithBackups(stats.BackupDirectory);
                    CachedInfo.PropertiesWithBackups = stats.PropertiesWithBackups;
                    break;
                case DataType.PropertiesWithNoBackups:
                    stats.PropertieWithoutBackups(stats.BackupDirectory);
                    CachedInfo.PropertiesWithoutBackups = stats.PropertiesWithoutBackups;
                    break;
            }




        }
        private void BackGroundWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listbxData.Items.Clear();

            switch (dataType)
            {
                case DataType.PropertiesWithBackups:
                    foreach (var value in CachedInfo.PropertiesWithBackups.Keys)
                    {
                        string sanitized = value.ToString().Substring(58, 5);
                        if (!listbxData.Items.Contains(sanitized)) { listbxData.Items.Add(sanitized); }
                      
                    }
                    break;
                case DataType.PropertiesWithNoBackups:
                    foreach (var value in CachedInfo.PropertiesWithoutBackups.Keys)
                    {
                        string sanitized = value.ToString().Substring(58, 5);
                        if (!listbxData.Items.Contains(sanitized)) { listbxData.Items.Add(sanitized); }

                    }
                    break;
            }
        }
    }
}


