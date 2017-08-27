using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Data.SQLite;
using System.IO;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace KChatServer
{
	class Program
	{
		static void Main(string[] args)
		{
			string dbPath = Path.Combine(Constants.appDataFolder, Path.Combine(Constants.appFolder, Constants.dbName));

			string ipAdddresSett = ConfigurationManager.AppSettings["ipAddress"];
			int ipPortSett = Int32.Parse(ConfigurationManager.AppSettings["ipPort"]);

			var url = $"http://{ipAdddresSett}:{ipPortSett}/";
			ServiceHost host;

			using (WebApp.Start<Startup>(url))
			{
				Console.WriteLine($"Started SignalR is running at {url}");

				var wcfHttpUrl = new Uri(url + "kwcf");
				var wcfTcpUrl = new Uri($"net.tcp://{ipAdddresSett}:{ipPortSett + 1}");

				Uri[] baseAdresses = { wcfTcpUrl, wcfHttpUrl };

				using (host = new ServiceHost(typeof(KChatServer.WCF.KChatWcfServiceImpl), baseAdresses))
				{
					ServiceMetadataBehavior smb = new ServiceMetadataBehavior
					{
						HttpGetEnabled = true
					};
					smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
					host.Description.Behaviors.Add(smb);
					NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None, true);
					//Updated: to enable file transefer of 64 MB
					tcpBinding.MaxBufferPoolSize = (int)67108864;
					tcpBinding.MaxBufferSize = 67108864;
					tcpBinding.MaxReceivedMessageSize = (int)67108864;
					tcpBinding.TransferMode = TransferMode.Buffered;
					tcpBinding.ReaderQuotas.MaxArrayLength = 67108864;
					tcpBinding.ReaderQuotas.MaxBytesPerRead = 67108864;
					tcpBinding.ReaderQuotas.MaxStringContentLength = 67108864;


					tcpBinding.MaxConnections = 100;

					tcpBinding.ReceiveTimeout = new TimeSpan(20, 0, 0);
					tcpBinding.ReliableSession.Enabled = true;
					tcpBinding.ReliableSession.InactivityTimeout = new TimeSpan(20, 0, 10);

					host.AddServiceEndpoint(typeof(WCF.IKWcfService), tcpBinding, "tcp");
					host.Open();
					Console.WriteLine($"WCF Service is running at {wcfHttpUrl}.");

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
