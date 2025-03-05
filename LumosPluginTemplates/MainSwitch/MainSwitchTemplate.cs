using LumosToolsLIB.Tools;
using org.dmxc.lumos.Kernel.MainSwitch;
using System;

namespace LumosPluginTemplates.MainSwitch
{
    public class MainSwitchTemplate : IMainSwitch
    {
        private static readonly MainSwitchTemplate instance = new();
        public static MainSwitchTemplate getInstance() => instance;
        private bool _enabled;
        public event EventHandler<EventArgs> EnabledChanged;

        public string ID => "MainSwitchTemplate";

        public string Name => "Template";

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    EnabledChanged?.InvokeFailSafe(this, EventArgs.Empty);
                }
            }
        }
    }
}
