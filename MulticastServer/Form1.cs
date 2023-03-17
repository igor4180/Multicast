using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MulticastServer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			sender.IsBackground = true;
			sender.Start();
		}

		Thread sender = new Thread(new ThreadStart(Sender));
		static string message = "";
		private void Form1_Load(object sender, EventArgs e)
		{
			
		}
		static void Sender()
		{
			while (true)
			{
				Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 2);
				IPAddress dest = IPAddress.Parse("234.5.5.5");
				socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(dest));
				IPEndPoint point = new IPEndPoint(dest, 4567);
				socket.Connect(point);
				socket.Send(Encoding.UTF8.GetBytes(message));
				socket.Close();
				Thread.Sleep(1000);
			}
		}

		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{
			message = richTextBox1.Text;
		}
	}
		

}
