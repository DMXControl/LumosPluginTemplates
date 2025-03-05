using LumosLIB.Kernel;
using LumosPluginTemplates.MainSwitch;
using LumosProtobuf;
using LumosProtobuf.Input;
using org.dmxc.lumos.Kernel.Input.v2;
using System;
using System.Timers;

namespace LumosPluginTemplates.InputAssignment
{
    public class SourceTemplate : AbstractInputSource
    {
        public string SerialNumber { get; private set; }

        private Random random = new();
        private Timer timer;

        public SourceTemplate(string serialNumber) :
            base(getID(serialNumber), getDisplayName(), getCategory(serialNumber), default)
        {
            MainSwitchTemplate.getInstance().EnabledChanged += SourceTemplate_EnabledChanged; ;
            AutofireChangedEvent = MainSwitchTemplate.getInstance().Enabled;
            SerialNumber = serialNumber;

            timer = new Timer(100);
            timer.Elapsed += Timer_Elapsed;

            if (AutofireChangedEvent)
                timer.Start();
        }

        private void SourceTemplate_EnabledChanged(object sender, EventArgs e)
        {
            AutofireChangedEvent = MainSwitchTemplate.getInstance().Enabled;
            if (AutofireChangedEvent)
                timer.Start();
            else
                timer.Stop();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.CurrentValue = random.Next((int)Min, (int)Max);
        }

        private static string getID(string serialNumber)
        {
            return string.Format("PluginTemplate-{0}-Source", serialNumber);
        }

        private static string getDisplayName()
        {
            return "SourceTemplate";
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
    }
}
