using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteWindow.TCPLib
{
    public class TCP_Server
    {
        public TcpListener server;

        public Func<NetworkStream> NetStream;

        private bool IsReceiving = false;

        public bool IsBusy = false;

        private List<byte> buffer = new List<byte>();

        public event Action OnClientConnect;

        public static TCP_Server CreateTCPServer(int Port)
        {
            var instance = new TCP_Server();

            Task.Run(async () =>
            {
                instance.server = new TcpListener(IPAddress.Any, Port);

                instance.server.Start();

                while (true)
                {
                    if (instance.server.Pending())
                    {
                        Task.Run(async () =>
                        {
                            var client = await instance.server.AcceptTcpClientAsync();

                            instance.NetStream = () => client.GetStream();

                            instance.OnClientConnect?.Invoke();

                            while (client.Connected)
                            {
                                if (client.Available > 0 && !instance.IsReceiving)
                                {
                                    instance.IsReceiving = true;

                                    var localBuffer = new byte[client.Available];

                                    var receivedLength = instance.NetStream().Read(localBuffer, 0, localBuffer.Length);
                                    await instance.NetStream().FlushAsync();

                                    localBuffer = localBuffer.Take(receivedLength).ToArray();

                                    if (localBuffer.Any(o => o != 0))
                                    {
                                        instance.buffer.AddRange(localBuffer);
                                    }
                                }
                                else if (client.Available <= 0)
                                {
                                    if (instance.buffer.Count > 0)
                                    {
                                        try
                                        {
                                            instance.OnDataReceived(instance.buffer.ToArray(), client);
                                        }
                                        catch
                                        {
                                        }

                                        instance.buffer.Clear();
                                    }

                                    instance.IsReceiving = false;
                                }
                            }
                        });
                    }
                }
            });

            return instance;
        }

        public int Send(string text) => Send(Encoding.Default.GetBytes(text));

        public int Send(byte[] data)
        {
            Task.Run(() =>
            {
                while (IsReceiving)
                {
                    // Hang
                }

                NetStream().Write(data, 0, data.Length);
            });

            return data.Length;
        }

        public void Shutdown()
        {
            server.Stop();
            server.Server.Dispose();
        }

        public event Action<string> DataReceivedEvent;

        public async void OnDataReceived(byte[] data, TcpClient client)
        {
            var text = Encoding.Default.GetString(data);

            DataReceivedEvent?.Invoke(text);
        }
    }
}