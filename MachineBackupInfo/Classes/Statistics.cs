using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace MachineBackupInfo.Classes
{
    public class Statistics
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
        public int PropertiesWithBackupsAmt { get; set; }

        //Number of properties without backups
        public int PropertiesWithNoBackupsAmt { get; set; }

        //Properties without backups
        public Dictionary<string, bool> PropertiesWithoutBackups { get; set; }

        //Properties with backups
        public Dictionary<string, bool> PropertiesWithBackups { get; set; }

        //Users with backups
        public int UsersWithBackups { get; set; }

        //Users without backups
        public int UsersWithoutBackups { get; set; }

        //Will Reuse Statistics Saved In Memory The First Time The Program Is Ran But Will Not Be Real Time (Will Speed Loading By A Lot Though)
        public bool CacheData { get; set; }



        public Statistics()
        {
            PropertiesWithoutBackups = new Dictionary<string, bool>();
            PropertiesWithBackups = new Dictionary<string, bool>();

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
        public void PropertieWithBackups(string path)
        {

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

                                if (containsBackup)
                                {
                                    PropertiesWithBackupsAmt++;
                                    PropertiesWithBackups.Add(p, containsBackup);
                                }
                            }
                        }
                    }
                }
            }
        }
        //Gets number of Properties that have don't hav a backup file
        public void PropertieWithoutBackups(string path)
        {

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

                    if (Directory.GetDirectories(child).Count() <= 0)
                    {
                        string[] infant = Directory.GetDirectories(child);

                        foreach (var p in infant)
                        {
                            if (Directory.Exists(p))
                            {
                                string[] files = Directory.GetFiles(p);
                                bool containsBackup = files.Any(x => x.Contains(".vbk"));

                                if (!containsBackup)
                                {
                                    PropertiesWithNoBackupsAmt++;
                                    PropertiesWithoutBackups.Add(p, containsBackup);
                                }
                            }
                        }
                    }
                    else
                    {
                        if(!PropertiesWithoutBackups.ContainsKey(child))
                        {
                            PropertiesWithoutBackups.Add(child, false);
                        }
                    }
                }
            }
        }
    }
}

     



                
                       
              

        

