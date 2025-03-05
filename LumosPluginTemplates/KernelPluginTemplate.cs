using LumosLIB.Kernel.Log;
using org.dmxc.lumos.Kernel.Plugin;

namespace LumosPluginTemplates
{
    public class KernelPluginTemplate : KernelPluginBase
    {
        private static readonly ILumosLog Log = LumosLogger.getInstance(typeof(KernelPluginTemplate));

        //PLEASE CHANGE GUID AND NAME TO PREVENT CONFLICTS
        public KernelPluginTemplate() : base("<ENTER-UNIQUE-PLUGIN-GUID>", "<ENTER-PLUGIN-NAME>")
        {
        }

        protected override void initializePlugin()
        {
            Log.Info("Initialize " + nameof(KernelPluginTemplate));
        }

        protected override void startupPlugin()
        {
            Log.Info("Startup " + nameof(KernelPluginTemplate));
        }


        protected override void shutdownPlugin()
        {
            Log.Info("Shutdown " + nameof(KernelPluginTemplate));
        }
    }
}