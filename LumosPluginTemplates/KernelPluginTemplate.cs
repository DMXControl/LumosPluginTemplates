using LumosLIB.Kernel.Log;
using org.dmxc.lumos.Kernel.Plugin;

namespace LumosPluginTemplates
{
    public class KernelPluginTemplate : KernelPluginBase
    {
        private static readonly ILumosLog Log = LumosLogger.getInstance(typeof(KernelPluginTemplate));
        public KernelPluginTemplate() : base("{5B98C51F-C9AD-4C55-AF2D-1C3F9CE80D9C}", "KernelPluginTemplate") //GUID bitte unbedingt gegen einen eigenen tauschen, um überschneidungen zu entgehen!!!!!!!!!!!!!!!!
        {
        }

        protected override void initializePlugin()
        {
            Log.Info("initialize");
        }

        protected override void shutdownPlugin()
        {
            Log.Info("is shuting down!");
        }

        protected override void startupPlugin()
        {
            Log.Info("is starting up!");
        }
    }
}