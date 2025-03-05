using org.dmxc.lumos.Kernel.Scene.Fade.Curve;

namespace LumosPluginTemplates.DimmerCurve
{
    public class DimCurveTemplate : IFadeCurve
    {
        #region IFadeCurve Member

        public string Name
        {
            get { return "DimCurveTemplate"; }
        }

        public double getFadeValue(double fadePercent)
        {
            return fadePercent; // 1:1 linear curve
        }

        #endregion
    }
}