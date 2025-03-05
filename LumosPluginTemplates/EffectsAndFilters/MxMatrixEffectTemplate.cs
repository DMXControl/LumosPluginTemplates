using LumosLIB.Kernel.Log;
using org.dmxc.lumos.Kernel.PropertyType;
using org.dmxc.lumos.Kernel.PropertyValue.Attachable;
using org.dmxc.lumos.Kernel.PropertyValue.Filter; //Important!
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumosPluginTemplates.EffectsAndFilters
{
    public class MxMatrixEffectTemplate : AbstractMxEffect
    {
        private static readonly ILumosLog log = LumosLogger.getInstance(typeof(MxMatrixEffectTemplate)); //Important
        private string attachable;
        protected MxMatrixEffectTemplate(object Speed)
            : base(Speed)
        {

        }
        public MxMatrixEffectTemplate()
            : this(1)
        {

        }
        public override string Name //Required!
        {
            get { return "MatrixEffectTemplate"; }
        }

        protected override AbstractMxEffect cloneAbstractMxEffect()
        {
            return new MxMatrixEffectTemplate(Speed);
        }

        protected override ILumosLog Log //Required!
        {
            get { return log; }
        }
        protected override bool processMatrix(Matrix input, out Matrix output, long time) //Required! Here, the matrix is rendered!!!
        {
            output = Matrix.FromBounds(10, 10);
            return true;
        }
        protected override IEnumerable<AttachableParameter> ParametersInternal //Here, parameters are defined
        {
            get
            {
                try
                {
                    var items = new AttachableParameter("MyAttachable", null, typeof(string), new string[] { "one", "two" });
                    return base.ParametersInternal.Append(items);
                }
                catch (Exception e)
                {
                    log.Error(ExeptionString("Error at \"protected override IEnumerable<AttachableParameter> ParametersInternal\""), e);
                }
                return base.ParametersInternal;
            }
        }
        protected override bool setParameterInternal(string name, object value) //Here, parameter changes are evaluated
        {
            try
            {
                if (name.Equals("MyAttachable") && value is string)
                {
                    attachable = (string)value;
                    return true;
                }
            }
            catch (Exception e)
            {
                log.Error(ExeptionString("Error at \"protected override bool setParameterInternal(string name, object value)\"",
                    "string name = " + name,
                    "object value = " + value.ToString()), e);
            }
            return base.setParameterInternal(name, value);
        }
        protected override object getParameterInternal(string name) //Here, parameter values are read out from the effect
        {
            try
            {
                if (name.Equals("MyAttachable"))
                {
                    return attachable;
                }
            }
            catch (Exception e)
            {
                log.Error(ExeptionString("Error at \"protected override object getParameterInternal(string name)\"", "string name = " + name), e);
            }
            return base.getParameterInternal(name);
        }
        private string ExeptionString(string Caption, params object[] ojb)
        {
            StringBuilder sb = new StringBuilder("Effect: " + Name);
            sb.AppendLine(Caption);
            for (int i = 0; i < ojb.Length; i++)
            {
                sb.AppendLine(ojb[i].ToString());
            }
            return sb.ToString();
        }
    }

}