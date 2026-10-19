using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MicSwitch.MainWindow.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    internal enum MuteMode
    {
        [Description("단축키를 누를 때마다 음소거 켜기/끄기")]
        ToggleMute,
        [Description("단축키를 누르고 있는 동안만 마이크 켜기")]
        PushToTalk,
        [Description("단축키를 누르고 있는 동안만 마이크 끄기")]
        PushToMute
    }
}