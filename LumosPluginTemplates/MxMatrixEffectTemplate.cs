using LumosLIB.Kernel.Log;
using LumosLIB.Tools;
using org.dmxc.lumos.Kernel.PropertyType;
using org.dmxc.lumos.Kernel.PropertyValue.Attachable;
using org.dmxc.lumos.Kernel.PropertyValue.Filter; //Wichtig!
using System;
using System.Collections.Generic;
using System.Text;

namespace LumosPluginTemplates
{
    public class MxMatrixEffectTemplate : AbstractMxEffect //Wichtig!
    {
        private static readonly ILumosLog log = LumosLogger.getInstance(typeof(MxMatrixEffectTemplate)); //Wichtig!
        private string attachable;
        protected MxMatrixEffectTemplate(object Speed)
            : base()
        {
        }
        public MxMatrixEffectTemplate()
            : this(1)
        {
        }
        public override string Name //Ein Muss!
        {
            get { return "MatrixEffectTemplate"; }
        }
        protected override AbstractFilter cloneAbstractFilter() //Ein Muss!
        {
            return new MxMatrixEffectTemplate(this.Speed);
        }
        protected override ILumosLog Log //Ein Muss!
        {
            get { return log; }
        }
        protected override bool processMatrix(Matrix input, out Matrix output, long time) //Ein Muss! Hier entsteht dein Bild!!!
        {
            output = Matrix.FromBounds(10, 10);
            return true;
        }
        protected override IEnumerable<AttachableParameter> ParametersInternal //Hier definierst du Parameter
        {
            get
            {
                try
                {
                    AttachableParameter[] items = new AttachableParameter[1];
                    items[0] = new org.dmxc.lumos.Kernel.PropertyValue.Attachable.AttachableParameter("MyAttachable", null, typeof(string), new string[] { "one", "two" });
                    return base.ParametersInternal.Append<AttachableParameter>(items);
                }
                catch (Exception e)
                {
                    log.Error(ExeptionString("Error at \"protected override IEnumerable<AttachableParameter> ParametersInternal\""), e);
                }
                return base.ParametersInternal;
            }
        }
        protected override bool setParameterInternal(string name, object value) //Hier werden Parameteränderungen ausgewertet
        {
            try
            {
                if (name.Equals("MyAttachable") && (value is String))
                {
                    this.attachable = (string)value;
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
        protected override object getParameterInternal(string name) //hier werden Parameter aus deinem Effect gelesen
        {
            try
            {
                if (name.Equals("MyAttachable"))
                {
                    return this.attachable;
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
            StringBuilder sb = new StringBuilder("Effect: " + this.Name);
            sb.AppendLine(Caption);
            for (int i = 0; i < ojb.Length; i++)
            {
                sb.AppendLine(ojb[i].ToString());
            }
            return sb.ToString();
        }
    }

}