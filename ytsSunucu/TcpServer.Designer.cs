using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Threading;

namespace ytsSunucu
{
    public class TcpServer : Component
	{
		private List<TcpServerConnection> connections;

		private TcpListener listener;

		private Thread listenThread;

		private Thread sendThread;

		private bool m_isOpen;

		private int m_port;

		private int m_maxSendAttempts;

		private int m_idleTime;

		private int m_maxCallbackThreads;

		private int m_verifyConnectionInterval;

		private Encoding m_encoding;

		private SemaphoreSlim sem;

		private bool waiting;

		private int activeThreads;

		private object activeThreadsLock = new object();

		private IContainer components = null;

		public List<TcpServerConnection> Connections
		{
			get
			{
				List<TcpServerConnection> rv = new List<TcpServerConnection>();
				rv.AddRange(this.connections);
				return rv;
			}
		}

		public Encoding Encoding
		{
			get
			{
				return this.m_encoding;
			}
			set
			{
				Encoding oldEncoding = this.m_encoding;
				this.m_encoding = value;
				foreach (TcpServerConnection client in this.connections)
				{
					if (client.Encoding == oldEncoding)
					{
						client.Encoding = this.m_encoding;
					}
				}
			}
		}

		public int IdleTime
		{
			get
			{
				return this.m_idleTime;
			}
			set
			{
				this.m_idleTime = value;
			}
		}

		[Browsable(false)]
		public bool IsOpen
		{
			get
			{
				return this.m_isOpen;
			}
			set
			{
				if (this.m_isOpen != value)
				{
					if (!value)
					{
						this.Close();
					}
					else
					{
						this.Open();
					}
				}
			}
		}

		public int MaxCallbackThreads
		{
			get
			{
				return this.m_maxCallbackThreads;
			}
			set
			{
				this.m_maxCallbackThreads = value;
			}
		}

		public int MaxSendAttempts
		{
			get
			{
				return this.m_maxSendAttempts;
			}
			set
			{
				this.m_maxSendAttempts = value;
			}
		}

		public int Port
		{
			get
			{
				return this.m_port;
			}
			set
			{
				if (value >= 0)
				{
					if (this.m_port != value)
					{
						if (this.m_isOpen)
						{
							throw new Exception("Invalid attempt to change port while still open.\nPlease close port before changing.");
						}
						this.m_port = value;
						if (this.listener != null)
						{
							this.listener.Server.Bind(new IPEndPoint(IPAddress.Any, this.m_port));
						}
						else
						{
							this.listener = new TcpListener(IPAddress.Any, this.m_port);
						}
					}
				}
			}
		}

		public int VerifyConnectionInterval
		{
			get
			{
				return this.m_verifyConnectionInterval;
			}
			set
			{
				this.m_verifyConnectionInterval = value;
			}
		}

		public TcpServer()
		{
			this.InitializeComponent();
			this.initialise();
		}

		public TcpServer(IContainer container)
		{
			container.Add(this);
			this.InitializeComponent();
			this.initialise();
		}

		public void Close()
		{
			if (this.m_isOpen)
			{
				lock (this)
				{
					this.m_isOpen = false;
					foreach (TcpServerConnection conn in this.connections)
					{
						conn.forceDisconnect();
					}
					try
					{
						if (this.listenThread.IsAlive)
						{
							this.listenThread.Interrupt();
							Thread.Yield();
							if (this.listenThread.IsAlive)
							{
								this.listenThread.Abort();
							}
						}
					}
					catch (SecurityException securityException)
					{

					}
					try
					{
						if (this.sendThread.IsAlive)
						{
							this.sendThread.Interrupt();
							Thread.Yield();
							if (this.sendThread.IsAlive)
							{
								this.sendThread.Abort();
							}
						}
					}
					catch (SecurityException securityException1)
					{
					}
				}
				this.listener.Stop();
				this.listenThread = null;
				this.sendThread = null;
				GC.Collect();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		public List<string> getConnString()
		{
			List<string> _list = new List<string>();
			foreach (TcpServerConnection conn in this.connections)
			{
				_list.Add(conn.Socket.Client.RemoteEndPoint.ToString());
			}
			return _list;
		}

		private void InitializeComponent()
		{
		}

		private void initialise()
		{
			this.connections = new List<TcpServerConnection>();
			this.listener = null;
			this.listenThread = null;
			this.sendThread = null;
			this.m_port = -1;
			this.m_maxSendAttempts = 3;
			this.m_isOpen = false;
			this.m_idleTime = 50;
			this.m_maxCallbackThreads = 100;
			this.m_verifyConnectionInterval = 100;
			this.m_encoding = System.Text.Encoding.ASCII;
			this.sem = new SemaphoreSlim(0);
			this.waiting = false;
			this.activeThreads = 0;
		}

		public void OnPosEvent(string data)
		{
		}

		public void Open()
		{
			lock (this)
			{
				if (!this.m_isOpen)
				{
					if (this.m_port < 0)
					{
						throw new Exception("Invalid port");
					}
					try
					{
						this.listener.Start(5);
					}
					catch (Exception exception)
					{
						this.listener.Stop();
						this.listener = new TcpListener(IPAddress.Any, this.m_port);
						this.listener.Start(5);
					}
					this.m_isOpen = true;
					this.listenThread = new Thread(new ThreadStart(this.runListener));
					this.listenThread.Start();
					this.sendThread = new Thread(new ThreadStart(this.runSender));
					this.sendThread.Start();
				}
			}
		}

		private bool processConnection(TcpServerConnection conn)
		{
			bool moreWork = false;
			if (conn.processOutgoing(this.m_maxSendAttempts))
			{
				moreWork = true;
			}
			if ((this.OnDataAvailable == null || this.activeThreads >= this.m_maxCallbackThreads ? false : conn.Socket.Available > 0))
			{
				lock (this.activeThreadsLock)
				{
					this.activeThreads = this.activeThreads + 1;
				}
				conn.CallbackThread = new Thread(() => this.OnDataAvailable(conn));
				conn.CallbackThread.Start();
				Thread.Yield();
			}
			return moreWork;
		}

		private void runListener()
		{
			while (true)
			{
				if ((!this.m_isOpen ? true : this.m_port < 0))
				{
					break;
				}
				try
				{
					if (!this.listener.Pending())
					{
						Thread.Sleep(this.m_idleTime);
					}
					else
					{
						TcpClient socket = this.listener.AcceptTcpClient();
						TcpServerConnection tcpServerConnection = new TcpServerConnection(socket, this.m_encoding);						
						if (this.OnConnect != null)
						{
							lock (this.activeThreadsLock)
							{
								this.activeThreads = this.activeThreads + 1;
							}
							tcpServerConnection.CallbackThread = new Thread(() => this.OnConnect(tcpServerConnection));
							tcpServerConnection.CallbackThread.Start();
						}
						lock (this.connections)
						{
							this.connections.Add(tcpServerConnection);
						}
					}
				}
				catch (ThreadInterruptedException threadInterruptedException)
				{
				}
				catch (Exception exception)
				{
					Exception e = exception;
					if ((!this.m_isOpen ? false : this.OnError != null))
					{
						this.OnError(this, e);
					}
				}
			}
		}

		private void runSender()
		{
			bool flag;
			while (true)
			{
				if ((!this.m_isOpen ? true : this.m_port < 0))
				{
					break;
				}
				try
				{
					bool moreWork = false;
					for (int i = 0; i < this.connections.Count; i++)
					{
						if (this.connections[i].CallbackThread != null)
						{
							try
							{
								this.connections[i].CallbackThread = null;
								lock (this.activeThreadsLock)
								{
									this.activeThreads = this.activeThreads - 1;
								}
							}
							catch (Exception exception)
							{
							}
						}
						if (this.connections[i].CallbackThread == null)
						{
							if (!this.connections[i].connected())
							{
								flag = false;
							}
							else
							{
								flag = (this.connections[i].LastVerifyTime.AddMilliseconds((double)this.m_verifyConnectionInterval) > DateTime.UtcNow ? true : this.connections[i].verifyConnected());
							}
							if (!flag)
							{
								lock (this.connections)
								{
									this.connections.RemoveAt(i);
									i--;
								}
							}
							else
							{
								moreWork = (moreWork ? true : this.processConnection(this.connections[i]));
							}
						}
					}
					if (!moreWork)
					{
						Thread.Yield();
						lock (this.sem)
						{
							foreach (TcpServerConnection conn in this.connections)
							{
								if (conn.hasMoreWork())
								{
									moreWork = true;
									break;
								}
							}
						}
						if (!moreWork)
						{
							this.waiting = true;
							this.sem.Wait(this.m_idleTime);
							this.waiting = false;
						}
					}
				}
				catch (ThreadInterruptedException threadInterruptedException)
				{
				}
				catch (Exception exception1)
				{
					Exception e = exception1;
					if ((!this.m_isOpen ? false : this.OnError != null))
					{
						this.OnError(this, e);
					}
				}
			}
		}

		public void Send(string data, string cihaz = "")
		{
			lock (this.sem)
			{
				foreach (TcpServerConnection conn in this.connections)
				{
					string ipp = conn.Socket.Client.RemoteEndPoint.ToString();
					if (ipp == cihaz)
					{
						conn.sendData(data);
					}
					else if (cihaz == "")
					{
						conn.sendData(data);
					}
				}
				Thread.Yield();
				if (this.waiting)
				{
					this.sem.Release();
					this.waiting = false;
				}
			}
		}

		public void setEncoding(System.Text.Encoding encoding, bool changeAllClients)
		{
			System.Text.Encoding oldEncoding = this.m_encoding;
			this.m_encoding = encoding;
			if (changeAllClients)
			{
				foreach (TcpServerConnection client in this.connections)
				{
					client.Encoding = this.m_encoding;
				}
			}
		}

		public event tcpServerConnectionChanged OnConnect;

		public event tcpServerConnectionChanged OnDataAvailable;

		public event tcpServerError OnError;
	}
}