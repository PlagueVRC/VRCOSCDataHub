using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteWindow.TCPLib
{
    public class TCP_Client
    {
        public TcpClient client = new TcpClient();

        private Func<NetworkStream> NetStream;

        private bool IsReceiving = false;

        private List<byte> buffer = new List<byte>();

        public bool IsConnected;

        public event Action ConnectedEvent;

        public static TCP_Client CreateTCPClient(int Port = 9002)
        {
            var instance = new TCP_Client();

            Task.Run(() =>
            {
                instance.client.Connect(IPAddress.Parse("127.0.0.1"), Port);

                instance.NetStream = () => instance.client.GetStream();

                instance.IsConnected = true;

                instance.ConnectedEvent?.Invoke();

                while (true)
                {
                    if (instance.client.Available > 0 && !instance.IsReceiving)
                    {
                        instance.IsReceiving = true;

                        var localBuffer = new byte[instance.client.Available];

                        var receivedLength = instance.NetStream().Read(localBuffer, 0, localBuffer.Length);
                        instance.NetStream().Flush();

                        localBuffer = localBuffer.Take(receivedLength).ToArray();

                        if (localBuffer.Any(o => o != 0)) // owo
                        {
                            instance.buffer.AddRange(localBuffer);
                        }
                    }
                    else if (instance.client.Available <= 0)
                    {
                        if (instance.buffer.Count > 0)
                        {
                            try
                            {
                                instance.OnDataReceived(instance.buffer.ToArray());
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

            return instance;
        }

        public int SendToServer(string text) => SendToServer(Encoding.Default.GetBytes(text));

        public int SendToServer(byte[] data)
        {
            Task.Run(() =>
            {
                while (IsReceiving)
                {
                    // Hang
                }

                IsReceiving = true;

                NetStream().Write(data, 0, data.Length);

                IsReceiving = false;
            });

            return data.Length;
        }

        public void OnDataReceived(byte[] data) => HandleData(data);

        private bool IsHandling = false;

        public event Action<string> DataReceivedEvent;

        public void HandleData(byte[] data)
        {
            try
            {
                if (!IsHandling)
                {
                    IsHandling = true;

                    var text = Encoding.Default.GetString(data);

                    //MessageBox.Show(text);

                    DataReceivedEvent?.Invoke(text);

                    IsHandling = false;
                }
            }
            catch
            {
                IsHandling = false; // choccy milk
            }
        }
    }
}
