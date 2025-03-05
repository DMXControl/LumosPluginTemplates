using Lumos.GUI.Plugin;
using LumosLIB.Kernel.Log;

namespace LumosGUIPluginTemplates
{
    public class GUIPluginTemplate : GuiPluginBase
    {
        private static readonly ILumosLog Log = LumosLogger.getInstance(nameof(GUIPluginTemplate));

        public GUIPluginTemplate() : base("<ENTER-UNIQUE-GUI-PLUGIN-GUID>", "<ENTER-GUI-PLUGIN-NAME>")
        {

        }

        protected override void initializePlugin()
        {
            Log.Info("Initialize " + nameof(GUIPluginTemplate));
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
