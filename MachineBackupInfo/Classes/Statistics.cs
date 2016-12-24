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

        //Users with backups
        public int UsersWithBackups { get; set; }

        //Users without backups
        public int UsersWithoutBackups { get; set; }

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
        //Retrieves backup information for all properties
        public List<Property> PropertyData(string path)
        {
            Property prop = new Property();
            List<Property> data = new List<Property>();
            //Gets root directory path of Properties (Example: .\P0010 but not the children)
            var propDirectories = Directory.GetDirectories(path).Where(x => Propertyrgx.IsMatch(x)).ToArray();

            if (propDirectories == null) return data;

            //Directories in parent
            foreach (var di in propDirectories)
            {
                //Main/Slave folder (some properties will have more (ex: 3 computers)
                string[] children = Directory.GetDirectories(di);

                if (children == null) return data;

                foreach (var c in children)
                {
                    string child = c.ToString().ToLowerInvariant();

                    if (child.Contains(DataType.Main.ToString().ToLowerInvariant()))
                    {
                        prop.Type = DataType.Main;
                    }
                    else if(child.Contains(DataType.Slave.ToString().ToLowerInvariant()))
                    {
                        prop.Type = DataType.Slave;
                    }
                    else
                    {
                        prop.Type = DataType.Other;
                    }

                    prop.FullPath = child;
                    prop.PropertyName = PropertyName(child);

                    if (Directory.GetDirectories(child).Count() > 0)
                    {
                        string[] infant = Directory.GetDirectories(child);

                        foreach (var p in infant)
                        {
                            if (Directory.Exists(p))
                            {
                                string[] files = Directory.GetFiles(p);

                                if(files == null) { continue; }

                                DirectoryInfo dInfo = new DirectoryInfo(p);

                                FileInfo[] fiInfo = dInfo.GetFiles();

                                bool containsBackup = fiInfo.Any(x => x.Name.Contains(".vbk"));

                                if (!containsBackup) { prop.BackupSize = 0; prop.HasBackup = false; continue; }

                                //TODO: get filesize of every backupfile and not just one
                                long filesize = fiInfo.Where(x => x.Name.Contains(".vbk")).FirstOrDefault().Length;

                                prop.BackupSize = Math.Round((filesize / 1024f) / 1024f, 0);
                                prop.HasBackup = true;
                               
                            }
                        }
                    }
                    else
                    {
                        prop.HasBackup = false;
                        prop.BackupSize = 0;
                    }
                    data.Add(new Property{ PropertyName = prop.PropertyName, BackupSize = prop.BackupSize, FullPath = prop.FullPath, HasBackup = prop.HasBackup });

                }
            }
            return data;
        }
        private string PropertyName(string path)
        {
            try
            {
                string propName = path.Substring(58, 5);
                return propName;
            }
            catch(IndexOutOfRangeException)
            {
                return path;
            }
            catch(Exception)
            {
                return path;
            }
        }
    }
}
 


                
                       
              

        

