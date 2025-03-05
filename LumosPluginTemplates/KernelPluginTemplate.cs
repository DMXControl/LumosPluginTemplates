using LumosLIB.Kernel.Log;
using LumosPluginTemplates.InputAssignment;
using LumosPluginTemplates.MainSwitch;
using org.dmxc.lumos.Kernel.Input.v2;
using org.dmxc.lumos.Kernel.MainSwitch;
using org.dmxc.lumos.Kernel.Plugin;
using System;

namespace LumosPluginTemplates
{
    public class KernelPluginTemplate : KernelPluginBase
    {
        private static readonly ILumosLog Log = LumosLogger.getInstance(typeof(KernelPluginTemplate));

        //PLEASE CHANGE GUID AND NAME TO PREVENT CONFLICTS
        public KernelPluginTemplate() : base("{915DF045-5B8C-409C-A96C-8F73D5C69C1C}", "<ENTER-PLUGIN-NAME>")
        {
        }

        protected override void initializePlugin()
        {
            Log.Info("Initialize " + nameof(KernelPluginTemplate));
            MainSwitchManager.getInstance().RegisterMainSwitch(MainSwitchTemplate.getInstance());
        }

        protected override void startupPlugin()
        {
            Log.Info("Startup " + nameof(KernelPluginTemplate));

            var im = InputManager.getInstance();
            try
            {
                im.RegisterSource(new SourceTemplate("915DF04"));

                im.RegisterSink(new SinkTemplate("8F73D5C69C1C"));
            }
            catch (Exception ex)
            {
                Log.Error(string.Empty, ex);
            }
        }


        protected override void shutdownPlugin()
        {
            Log.Info("Shutdown " + nameof(KernelPluginTemplate));
        }
    }
}