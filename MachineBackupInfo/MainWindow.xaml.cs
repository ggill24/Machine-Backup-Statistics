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
            stats = new Statistics();
            DataContext = stats;
        }

        private async void MetroWindow_ContentRendered(object sender, EventArgs e)
        {
            Dictionary<string, bool> test = new Dictionary<string, bool>();
            await new Task(() =>
             {
                 var result = stats.PropertiesContainingBackups(stats.BackupDirectory);
                 test = result;

             }).ConfigureAwait(false);
            foreach(var t in test)
            {
                listBxPropertiesWithBackups.Items.Add(t.Value + " " + t.Key);
            }
            
           
           
        }
       
    }
}


 
