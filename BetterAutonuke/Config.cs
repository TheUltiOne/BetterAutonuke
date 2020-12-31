using Exiled.API.Interfaces;
using System.ComponentModel;

namespace BetterAutonuke
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("Sets the time until autonuke")]
        public float Time { get; set; } = 1140;

        [Description("Sets the time until autonuke")]
        public string Announcement { get; set; } = "ATTENTION ALL PERSONNEL AUTOMATIC WARHEAD DETONATION IN T MINUS {minutes} MINUTES EVACUATE THE FACILITY IT CANNOT BE DISABLED";

        [Description("Sets how many times the warhead alert will be shown")]
        public float Intervals { get; set; } = 3;
    }
}
