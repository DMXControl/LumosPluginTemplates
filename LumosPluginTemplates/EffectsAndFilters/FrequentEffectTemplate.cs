using LumosLIB.Kernel.Log;
using LumosLIB.Tools;
using org.dmxc.lumos.Kernel.PropertyValue.Effect;
using System;

namespace LumosPluginTemplates.EffectsAndFilters
{
    public class FrequentEffectTemplate : AbstractFrequentFunctionEffect
    {
        private static readonly ILumosLog log = LumosLogger.getInstance(nameof(FrequentEffectTemplate));

        protected FrequentEffectTemplate(object amplitude, object frequency, object phase)
            : base(amplitude, frequency, phase)
        {

        }

        public FrequentEffectTemplate()
            : this(100.0, 0.2, 0.0)
        {

        }

        public override string Name
        {
            get { return "FrequentEffectTemplate"; }
        }

        protected override ILumosLog Log
        {
            get { return log; }
        }

        protected override double getNormedEffectValue(double timeInMsRelative)
        {
            double x = timeInMsRelative / 1000 + Convert.ToDouble(Phase) / 360;
            x %= 1.005;

            return x.Limit(0, 1);
        }

        protected override AbstractFrequentFunctionEffect cloneAbstractFrequentFunctionEffect()
        {
            FrequentEffectTemplate e = new FrequentEffectTemplate(Amplitude, Frequency, Phase);
            return e;
        }
    }
}

