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

        bool cacheData;

        string title { get; set; }

        List<Property> data = new List<Property>();

        public BackUpInfoWindow(string title, bool cacheData, Statistics statisticsClass)
        {
            InitializeComponent();
            DataContext = this;
            lblTitle.Content = title;
            stats = statisticsClass;
            this.cacheData = cacheData;
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

            if (cacheData && CachedInfo.PropertyData.Count <= 0)
            {
                data = stats.PropertyData(stats.BackupDirectory);
                CachedInfo.PropertyData = data;
            }
            else
            {
                data = stats.PropertyData(stats.BackupDirectory);
            }

        }
            
                
            
        
        private void DisplayData(bool cacheData)
        {
            List<Property> _data = new List<Property>();
            listbxFullPath.Items.Clear();
            listbxHasBackup.Items.Clear();
            listbxProperty.Items.Clear();
            if (cacheData)
            {
                _data = CachedInfo.PropertyData;
            }
            else
            {
                _data = data;
            }
            for(int i = 0; i < _data.Count; i++)
            {
                listbxFullPath.Items.Add(_data[i].FullPath);
                listbxHasBackup.Items.Add(_data[i].HasBackup);
                listbxProperty.Items.Add(_data[i].PropertyName);
            }
        }
        private void BackGroundWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DisplayData(cacheData);
        }
        private string SanitizeString(string _string, int startIndex, int length)
        {
            if(_string.Length >= startIndex && _string.Length >= startIndex + length)
            {
                string sanitized = _string.Substring(startIndex, length);
                return sanitized;
            }
            return _string;
        }
    }
}


