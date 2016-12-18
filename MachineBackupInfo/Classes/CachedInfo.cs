using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineBackupInfo.Classes
{
    public static class CachedInfo
    {
        public static Dictionary<string, bool> PropertiesWithBackups;
        public static Dictionary<string, bool> PropertiesWithoutBackups;

    }
}
