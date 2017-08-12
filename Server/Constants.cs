using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KChatServer
{
    public static class Constants
    {
        public static string dbName = "KChat_Db.sqlite";
        public static string appFolder = "KChat";
        public static string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }
}
