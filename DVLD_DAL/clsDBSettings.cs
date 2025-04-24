using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DVLD_DAL
{
    public static class clsDBSettings
    {
        public static string CnnString(string DBName) =>
            ConfigurationManager.ConnectionStrings[DBName].ConnectionString;
    }
}
