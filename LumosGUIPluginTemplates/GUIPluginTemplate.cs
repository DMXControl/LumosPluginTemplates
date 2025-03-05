using Lumos.GUI.Plugin;
using Lumos.GUI.Windows;
using LumosGUIPluginTemplates.ProjectExplorer;
using LumosLIB.Kernel.Log;

namespace LumosGUIPluginTemplates
{
    public class GUIPluginTemplate : GuiPluginBase
    {
        private static readonly ILumosLog Log = LumosLogger.getInstance(nameof(GUIPluginTemplate));

        // Important notice: If you want to add windows, please use WPF windows and not WinForms windows
        // as DMXControl 3 is currently transitioning to WPF.

        //PLEASE CHANGE GUID AND NAME TO PREVENT CONFLICTS
        public GUIPluginTemplate() : base("{40986B0E-7256-48E3-9A0F-2B7EB828D05B}", "<ENTER-GUI-PLUGIN-NAME>")
        {

        }

        protected override void initializePlugin()
        {
            Log.Info("Initialize " + nameof(GUIPluginTemplate));
            PEManager.getInstance().registerProjectExplorerBranch(new PEBranchTemplate());
        }

        protected override void startupPlugin()
        {
            Log.Info("Startup " + nameof(GUIPluginTemplate));
        }


        protected override void shutdownPlugin()
        {
            Log.Info("Shutdown " + nameof(GUIPluginTemplate));
        }

        public override void connectionEstablished()
        {
            base.connectionEstablished();
            Log.Info("ConnectionEstablished " + nameof(GUIPluginTemplate));
        }
        public override void connectionClosing()
        {
            base.connectionClosing();
            Log.Info("ConnectionClosing " + nameof(GUIPluginTemplate));
        }
    }
}
