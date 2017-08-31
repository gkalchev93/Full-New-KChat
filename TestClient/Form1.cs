using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR.Client;

namespace TestClient
{
	public partial class Form1 : Form
	{




		public Form1()
		{
			InitializeComponent();
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			//List<SignalRClient> testClients = new List<SignalRClient>();
			int i = 0;
			while (true)
			{
				try
				{
					tbNumber.Text = i.ToString();
					var tmp = new ChatService();
					await tmp.ConnectAsync();
					await tmp.LoginAsync(Path.GetRandomFileName(), File.ReadAllBytes(@"C:\Users\turbo\Desktop\Full New KChat\TestClient\spongebob_4_150x150_png_by_somemilk.png"));
					i++;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					break;
				}
			}

		}



		private void button2_Click(object sender, EventArgs e)
		{

		}

		private async void button3_Click(object sender, EventArgs e)
		{
			var tmp = new ChatService();
			await tmp.ConnectAsync();
			await tmp.LoginAsync(Path.GetRandomFileName(), File.ReadAllBytes(@"C:\Users\turbo\Desktop\Full New KChat\TestClient\spongebob_4_150x150_png_by_somemilk.png"));

			StringBuilder msg = new StringBuilder("a");
			while (true)
			{
				tbMsgLen.Text = msg.Length.ToString();
				await tmp.SendUnicastMessageAsync("Gosho", msg.ToString());
				msg.Append('a', 100);
				Thread.Sleep(500);
				Application.DoEvents();
			}
		}
	}


}
