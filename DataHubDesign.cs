using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlobHandles;
using BuildSoft.OscCore;
using RemoteWindow.TCPLib;

namespace VRCOSCDataHub
{
    public partial class DataHubDesign : Form
    {
        public DataHubDesign()
        {
            InitializeComponent();
        }

        private void ServerToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (ServerToggle.Checked)
            {
                TCP_Server = TCP_Server.CreateTCPServer((int)PortNumberChanger.Value);
            }
            else
            {
                TCP_Server?.Shutdown();
                TCP_Server = null;
            }
        }

        public static OscServer OSC_Server = new (9001);
        public static TCP_Server TCP_Server;

        private void DataHubDesign_Load(object sender, EventArgs e)
        {
            OSC_Server.AddMonitorCallback(OnOSCData);

            OSC_Server.Start();
        }

        private void OnOSCData(BlobString OSC_Address, OscMessageValues OSC_Values)
        {
            // Package The Data
            var ParameterInfo = (OSC_Address.ToString(), new List<object?>());

            for (int i = 0; i < OSC_Values.ElementCount; i++)
            {
                var value = OSC_Values.ReadValue(i);

                if (!ParameterInfo.Item2.Contains(value))
                {
                    ParameterInfo.Item2.Add(value);
                }
            }

            // Broadcast The Data
            TCP_Server.Send($"{ParameterInfo.Item1};{string.Join("|", ParameterInfo.Item2)}");
        }
    }

    public static class DataHubExtensions
    {
        public static object? ReadValue(this OscMessageValues value, int index) // Credit: VRCOSCLib
        {
            return value.GetTypeTag(index) switch
            {
                TypeTag.Float32 => value.ReadFloatElementUnchecked(index),
                TypeTag.Int32 => value.ReadIntElementUnchecked(index),
                TypeTag.True => true,
                TypeTag.False => false,
                TypeTag.AltTypeString or TypeTag.String => value.ReadStringElement(index),
                TypeTag.Float64 => value.ReadFloat64ElementUnchecked(index),
                TypeTag.Int64 => value.ReadInt64ElementUnchecked(index),
                TypeTag.Blob => value.ReadBlobElement(index),
                TypeTag.Color32 => value.ReadColor32ElementUnchecked(index),
                TypeTag.MIDI => value.ReadMidiElementUnchecked(index),
                TypeTag.AsciiChar32 => value.ReadAsciiCharElement(index),
                TypeTag.TimeTag => value.ReadTimestampElementUnchecked(index),
                TypeTag.Infinitum => double.PositiveInfinity,
                TypeTag.Nil => null,
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
