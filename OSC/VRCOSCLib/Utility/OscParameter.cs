using System;
using System.Collections.Generic;
using System.Text;
using ParamChangedHandler = BuildSoft.VRChat.Osc.OscParameterChangedEventHandler<BuildSoft.VRChat.Osc.IReadOnlyOscParameterCollection>;

namespace BuildSoft.VRChat.Osc;
public static partial class OscParameter
{
    private static OscParameterCollection? _parameters;
    public static OscParameterCollection Parameters => _parameters ??= new();

    public static event ParamChangedHandler ValueChanged
    {
        add => Parameters.ValueChanged += value;
        remove => Parameters.ValueChanged -= value;
    }

    static OscParameter()
    {
        OscUtility.RegisterMonitorCallback(ReceiveMessage);
    }

    public static void Initialize()
    {

    }
}
