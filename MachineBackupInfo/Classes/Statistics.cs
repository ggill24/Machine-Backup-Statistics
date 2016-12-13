using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace MachineBackupInfo.Classes
{
    class Statistics
    {
        public readonly string BackupDirectory = @"\\fileserver\public storage canada\public\machine backups";

        //Examples of what this regex will match:
        //P0009, P9, P009, P0009 (Basically matches Property Numbers)
        Regex Propertyrgx = new Regex("^.*[Pp]{1}[0-9]{1,4}");

        //Number of Backups(determined by folder count in directory)
        public int BackupTotal { get; set; }

        //Property Directory Count
        public int PropertyTotal { get; set; }

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
            /*BackupTotal = DirectoryCount(BackupDirectory);
            PropertyTotal = PropertyDirectoryCount(BackupDirectory);*/
           // var t = PropertiesWithbackups(BackupDirectory);

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
            return Directory.Exists(path) ? Directory.GetDirectories(path).Where(x => Propertyrgx.IsMatch(x)).Count() : 0;
        }
        //Gets number of Properties that have a backup file
        public Dictionary<string, bool> PropertiesContainingBackups(string path)
        {
            // if (!Directory.Exists(path)) return 0;

            Dictionary<string, bool> backups = new Dictionary<string, bool>();

            //Gets root directory path of Properties (Example: .\P0010 but not the children)
            var propDirectories = Directory.GetDirectories(path).Where(x => Propertyrgx.IsMatch(x)).ToArray();

            //Directories in parent
            foreach (var di in propDirectories)
            {
                //Main/Slave folder (some properties will have more (ex: 3 computers)
                string[] children = Directory.GetDirectories(di);

                foreach (var c in children)
                {
                    string child = c;

                    if (Directory.GetDirectories(child).Count() > 0)
                    {
                        string[] infant = Directory.GetDirectories(child);

                        foreach (var p in infant)
                        {
                            if (Directory.Exists(p))
                            {
                                string[] files = Directory.GetFiles(p);
                                bool containsBackup = files.Any(x => x.Contains(".vbk"));
                                backups.Add(p, containsBackup);
                            }
                        }
                    }
                }
            }
            return backups;
        }
    }
}



                
                       
              

        

