using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Data.SQLite;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace KChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbPath = Path.Combine(Constants.appDataFolder, Path.Combine(Constants.appFolder, Constants.dbName));

            var url = "http://192.168.0.102:8080/";
            ServiceHost host;

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"Started SignalR is running at {url}");

                var wcfUrl = new Uri(url + "kwcf");
                using (host = new ServiceHost(typeof(KChatServer.WCF.KChatWcfService), wcfUrl))
                {
                    ServiceMetadataBehavior smb = new ServiceMetadataBehavior
                    {
                        HttpGetEnabled = true
                    };
                    smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                    host.Description.Behaviors.Add(smb);

                    host.Open();
                    Console.WriteLine($"WCF Service is running at {wcfUrl}.");

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

                    host.Close();
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
