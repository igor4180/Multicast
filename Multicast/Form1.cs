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

namespace Multicast
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			Thread receiverThread = new Thread(new ThreadStart(Receiver));
			receiverThread.IsBackground = true;
			receiverThread.Start();
		}
		delegate void receiverDelegate(string text);
		void ReciverDel(string text)
		{
			richTextBox1.Text = text;
		}
		void Receiver()
		{
			while (true)
			{
				Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				IPEndPoint point = new IPEndPoint(IPAddress.Any, 4567);
				socket.Bind(point);
				IPAddress address = IPAddress.Parse("234.5.5.5");
				socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(address, IPAddress.Any));
				byte[] buffer = new byte[1024];
				socket.Receive(buffer);
				richTextBox1.Invoke(new receiverDelegate(ReciverDel), Encoding.UTF8.GetString(buffer));
				socket.Close();

			}
		}
	}
}
