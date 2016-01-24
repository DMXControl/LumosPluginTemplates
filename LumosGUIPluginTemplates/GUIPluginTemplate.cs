using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumosLIB.GUI.Windows;
using Lumos.GUI.BaseWindow;
using LumosLIB.Kernel.Log;

namespace LumosGUIPluginTemplates
{
    public class GUIPluginTemplate : IPluginWindow
    {
        private static readonly ILumosLog Log = LumosLogger.getInstance(typeof(GUIPluginTemplate));

        public GUIPluginTemplate() : base()
        {
        }

        public MenuType MainFormMenu
        {
            get
            {
                return MenuType.Windows;
            }
        }

        string IPluginWindow.PluginID
        {
            get { return "{6211303D-14D3-44BB-BC06-3ED46F954F55}"; }
        }

        string IPluginWindow.WindowID
        {
            get { return ((IPluginWindow)this).PluginID; }
        }

        public void initialize(IPluginWindowContext context)
        {
            Log.Info("initialize");
        }

        public void pluginDisabled()
        {
            Log.Info("disable");
        }

        public void pluginEnabled()
        {
            Log.Info("enable");
        }
    }
}
