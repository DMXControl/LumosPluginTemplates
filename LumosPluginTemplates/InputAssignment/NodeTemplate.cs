using LumosLIB.Kernel;
using LumosLIB.Tools;
using org.dmxc.lumos.Kernel.Input.v2;
using org.dmxc.lumos.Kernel.Input.v2.Worker;
using System.Collections.Generic;
using System.Linq;
using T = LumosLIB.Tools.I18n.DummyT;

namespace LumosPluginTemplates.InputAssignment
{
    public class NodeTemplate : AbstractNodeWithEnable
    {
        public static readonly string NAME = T._("NodeTemplate");
        public static readonly string NODE_TYPE = "__NODETEMPLATE";

        public NodeTemplate(GraphNodeID id) : base(id, NODE_TYPE, KnownCategories.LOGIC)
        {
            this.Name = NAME;
        }

        protected override void AddDefaultPortsInternal()
        {
            var inputs = InputsWithoutEnable;

            for (int k = inputs.Count(); k < 2; k++)
            {
                var i = AddInputPort(name: k.ToString());
            }

            inputs.ForEach(i => i.DefaultParameterType = typeof(bool));
            inputs.Where(c => c.Value == null)
                .ForEach(c => c.Value = false);

            while (Outputs.Count < InputsWithoutEnable.Count())
            {
                var o = AddOutputPort(name: Outputs.Count.ToString());
            }
        }

        protected override void processInternalIfEnabled(NodeProcessContext context)
        {
            base.processInternalIfEnabled(context);

            var inputs = InputsWithoutEnable.ToList();
            var outputs = Outputs.ToList();

            foreach (var (i, o) in inputs.Zip(outputs, (i, o) => (i, o)))
            {
                o.Value = i.Value;
            }
        }

        #region Parameters

        protected override IEnumerable<GenericParameter> ParametersInternal
        {
            get
            {
                yield return ParameterTools.GetParameterFromEnum<ELogicOperator, GenericParameter>((name, displayName, type, enumValues)
                    => new GenericParameter(name, GraphNodeParameters.GraphNodeParameterType, type, enumValues: enumValues)
                    {
                        DisplayName = displayName,
                        [GraphNodeParameters.PARA_META_SHOWINTITLE] = "true",
                    });
                yield return new GenericParameter(GraphNodeParameters.PARA_INPUT_COUNT, GraphNodeParameters.GraphNodeParameterType, typeof(int))
                {
                    [GraphNodeParameters.PARA_META_REPAINTNODE] = "true",
                };
            }
        }

        protected override object getParameterInternal(GenericParameter parameter)
        {
            if (parameter.Name == GraphNodeParameters.PARA_INPUT_COUNT)
                return (uint)InputsWithoutEnable.Count();
            return base.getParameterInternal(parameter);
        }

        protected override bool setParameterInternal(GenericParameter parameter, object value)
        {
            if (parameter.Name == GraphNodeParameters.PARA_INPUT_COUNT)
            {
                var target = LumosTools.TryConvertToUInt32(value);
                if (target == null || !target.Value.Between(2, 12)) return false;

                var cnt = InputsWithoutEnable.Count();
                if (cnt > target) //Remove
                {
                    for (int i = cnt - 1; i >= target; i--)
                    {
                        var p = InputsWithoutEnable.Last();
                        RemovePort(p);

                        var o = Outputs.Last();
                        RemovePort(o);
                    }
                }
                else if (cnt < target) //Add
                {
                    do
                    {
                        var x = AddInputPort(name: cnt.ToString());
                        x.DefaultParameterType = typeof(bool);
                        x.Value = false;

                        var y = AddOutputPort(name: cnt.ToString());
                        y.Value = false;
                        cnt++;
                    } while (cnt < target);
                }

                return true;
            }
            return base.setParameterInternal(parameter, value);
        }

        protected override bool testParameterInternal(GenericParameter parameter, object value)
        {
            if (parameter.Name == GraphNodeParameters.PARA_INPUT_COUNT)
                return LumosTools.TryConvertToUInt32(value, out var val) && val.Between(2, 12);

            return base.testParameterInternal(parameter, value);
        }

        #endregion
    }
}
