using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Data.SQLite;
using System.IO;


namespace KChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbPath = Path.Combine(Constants.appDataFolder, Path.Combine(Constants.appFolder, Constants.dbName));

            var url = "http://192.168.0.102:8080/";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"Server running at {url}");
                Console.WriteLine("Creating a server database at :" + dbPath);
                if (!Directory.Exists(Path.GetDirectoryName(dbPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(dbPath));

                if (!File.Exists(dbPath))
                    SQLiteConnection.CreateFile(dbPath);

                using (DbHelper.Conn = new SQLiteConnection($"Data Source = {dbPath}; Version = 3; FailIfMissing = True"))
                {
                    DbHelper.Conn.Open();
                    DbHelper.CreateTable();

                    Console.WriteLine("Input 'quit' for exiting...");
                    string endString = string.Empty;
                    while (endString != "quit")
                    {
                        endString = Console.ReadLine().ToLower();
                    }
                }
            }
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR("/kchat", new HubConfiguration());
        }
    }
}
