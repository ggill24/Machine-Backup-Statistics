using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace MachineBackupInfo.Classes
{
    class Statistics
    {
        private readonly string BackupDirectory = @"\\fileserver\public storage canada\public\machine backups";
       
        //Number of Backups(determined by folder count in directory)
        public int BackupTotal { get; set; }
        
        //Property Directory Count
        public int PropertyCount { get; set; }
       
        //User Directory Count
        public int UserBackups { get; set; }
        
        //Properties with backups
        public int PropertiesWithBackups { get; set; }
       
        //Properties without backups
        public int PropertiesWithNoBackups { get; set; }
        
        //Users with backups
        public int UsersWithBackups { get; set; }

        //Users without backups
        public int UsersWithoutBackups { get; set; }

        public Statistics()
        {
            BackupTotal = DirectoryCount(BackupDirectory);
        }
        
        //Gets number of Directories in specified path
        public int DirectoryCount(string path)
        {
            //Returns Directory Path or 0 if path does not exist
            return Directory.Exists(path) ? Directory.GetDirectories(path).Count() : 0;
        }
        
        //Gets number of Directories that are for properties
        public int PropertyDirectoryCount(string path)
        {
            return 
        }
    }
}

        

