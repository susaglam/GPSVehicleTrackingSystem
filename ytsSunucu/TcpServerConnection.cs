using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace ytsSunucu
{
    public class TcpServerConnection
	{
		private TcpClient m_socket;

		private List<byte[]> messagesToSend;

		private int attemptCount;

		private Thread m_thread = null;

		private DateTime m_lastVerifyTime;

		private System.Text.Encoding m_encoding;

		public Thread CallbackThread
		{
			get
			{
				return this.m_thread;
			}
			set
			{
				if (!this.canStartNewThread())
				{
					throw new Exception("Cannot override TcpServerConnection Callback Thread. The old thread is still running.");
				}
				this.m_thread = value;
			}
		}

		public System.Text.Encoding Encoding
		{
			get
			{
				return this.m_encoding;
			}
			set
			{
				this.m_encoding = value;
			}
		}

		public DateTime LastVerifyTime
		{
			get
			{
				return this.m_lastVerifyTime;
			}
		}

		public TcpClient Socket
		{
			get
			{
				return this.m_socket;
			}
			set
			{
				this.m_socket = value;
			}
		}

		public TcpServerConnection(TcpClient sock, System.Text.Encoding encoding)
		{
			this.m_socket = sock;
			this.messagesToSend = new List<byte[]>();
			this.attemptCount = 0;
			this.m_lastVerifyTime = DateTime.UtcNow;
			this.m_encoding = encoding;
		}

		private bool canStartNewThread()
		{
			bool flag;
			if (this.m_thread != null)
			{
				flag = ((this.m_thread.ThreadState & (ThreadState.Stopped | ThreadState.Aborted)) == ThreadState.Running ? false : (this.m_thread.ThreadState & ThreadState.Unstarted) == ThreadState.Running);
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		public bool connected()
		{
			bool connected;
			try
			{
				connected = this.m_socket.Connected;
			}
			catch (Exception exception)
			{
				connected = false;
			}
			return connected;
		}

		public void forceDisconnect()
		{
			lock (this.m_socket)
			{
				this.m_socket.Close();
			}
		}

		public bool hasMoreWork()
		{
			bool flag;
			if (this.messagesToSend.Count > 0)
			{
				flag = true;
			}
			else
			{
				flag = (this.Socket.Available <= 0 ? false : this.canStartNewThread());
			}
			return flag;
		}

		public bool processOutgoing(int maxSendAttempts)
		{
			bool count;
			lock (this.m_socket)
			{
				if (!this.m_socket.Connected)
				{
					this.messagesToSend.Clear();
					count = false;
				}
				else if (this.messagesToSend.Count != 0)
				{
					lock (this.messagesToSend)
					{
						NetworkStream stream = this.m_socket.GetStream();
						try
						{
							stream.Write(this.messagesToSend[0], 0, (int)this.messagesToSend[0].Length);
							this.messagesToSend.RemoveAt(0);
							this.attemptCount = 0;
						}
						catch (IOException oException)
						{
							this.attemptCount = this.attemptCount + 1;
							if (this.attemptCount >= maxSendAttempts)
							{
								this.messagesToSend.RemoveAt(0);
								this.attemptCount = 0;
							}
						}
						catch (ObjectDisposedException objectDisposedException)
						{
							this.m_socket.Close();
							count = false;
							return count;
						}
					}
					count = this.messagesToSend.Count != 0;
					return count;
				}
				else
				{
					count = false;
				}
			}
			return count;
		}

		public void sendData(string data)
		{
			byte[] array = Encoding.ASCII.GetBytes(data);
			lock (this.messagesToSend)
			{
				this.messagesToSend.Add(array);
			}
		}

		public bool verifyConnected()
		{
			bool connected = (this.m_socket.Client.Available != 0 || !this.m_socket.Client.Poll(1, SelectMode.SelectRead) ? true : this.m_socket.Client.Available != 0);
			this.m_lastVerifyTime = DateTime.UtcNow;
			return connected;
		}
	}
}