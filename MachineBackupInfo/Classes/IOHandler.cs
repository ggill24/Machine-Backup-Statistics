using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MachineBackupInfo.Classes
{
    static class IOHandler
    {
        public static readonly string RootPath = Path.Combine(Path.GetTempPath() + "Machine Backup Info");
        public static readonly string DataFolder = Path.Combine(RootPath + @"\Data");

        public static void CreateRootPath()
        {
            if (!Directory.Exists(RootPath)) Directory.CreateDirectory(RootPath);
        }

        public static void CreateChildDirectories()
        {
            if (!Directory.Exists(DataFolder)) Directory.CreateDirectory(DataFolder);
            
        }

    }
}
