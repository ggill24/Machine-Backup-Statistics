using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineBackupInfo.Classes
{
    public class Property
    {
        public string PropertyName { get; set; }
        public DataType Type { get; set; }
        public bool HasBackup { get; set; }
        public double BackupSize { get; set; }
        public string FullPath { get; set; }

    }
}
