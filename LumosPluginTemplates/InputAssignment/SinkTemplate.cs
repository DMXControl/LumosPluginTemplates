using LumosLIB.Kernel;
using LumosPluginTemplates.MainSwitch;
using LumosProtobuf;
using LumosProtobuf.Input;
using org.dmxc.lumos.Kernel.Input.v2;
using System;

namespace LumosPluginTemplates.InputAssignment
{
    public class SinkTemplate : AbstractInputSink
    {
        public readonly string SerialNumber;
        private object valueCache;

        public SinkTemplate(string serialNumber, bool usePanelID = false) :
            base(getID(serialNumber), getDisplayName(), getCategory(serialNumber))

        {
            MainSwitchTemplate.getInstance().EnabledChanged += SinkTemplate_EnabledChanged; ;
            SerialNumber = serialNumber;
        }

        private void SinkTemplate_EnabledChanged(object sender, EventArgs e)
        {
            if (MainSwitchTemplate.getInstance().Enabled)
                UpdateValue(valueCache);
        }

        private static string getID(string serialNumber)
        {
            return string.Format("PluginTemplate-{0}-Sink", serialNumber);
        }

        private static string getDisplayName()
        {
            return "SinkTemplate";
        }

        private static ParameterCategory getCategory(string serialNumber)
        {
            return ParameterCategoryTools.FromNames("PluginTemplate", serialNumber);
        }

        public override EWellKnownInputType AutoGraphIOType
        {
            get { return EWellKnownInputType.Numeric; }
        }

        public override object Min => 0;

        public override object Max => 100;

        public override bool UpdateValue(object newValue)
        {
            valueCache = newValue;

            if (!MainSwitchTemplate.getInstance().Enabled)
                return true;

            return false;
        }
    }
}
