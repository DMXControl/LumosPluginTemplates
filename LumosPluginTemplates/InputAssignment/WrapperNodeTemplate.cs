using LumosLIB.Kernel.Log;
using LumosLIB.Tools;
using LumosProtobuf;
using org.dmxc.lumos.Kernel.Devices;
using org.dmxc.lumos.Kernel.Input.v2;
using org.dmxc.lumos.Kernel.Input.v2.InputClass;
using org.dmxc.lumos.Kernel.Input.v2.Worker;
using org.dmxc.lumos.Kernel.Project;
using org.dmxc.lumos.Kernel.User;
using System;
using System.ComponentModel;
using System.Linq;
using T = LumosLIB.Tools.I18n.DummyT; //Translation only works for already existing translations

namespace LumosPluginTemplates.InputAssignment
{
    public class WrapperNodeTemplate : AbstractNode, IWrapperGraphNode
    {
        private static readonly ILumosLog log = LumosLogger.getInstance(typeof(WrapperNodeTemplate));

        public static readonly string NAME = T._("WrapperNodeTemplate");
        public static readonly string WRAPPERNODE_TYPE = "__WRAPPERNODETEMPLATE";

        public string MemberInputName
        {
            get { return inputDevice.Name; }
        }

        public string MemberInputValue
        {
            get { return inputDevice.Value?.ToString() ?? null; }
        }

        private IGraphNodeInputPort inputDevice,
            inputSelect;

        private IGraphNodeOutputPort outputName,
            outputID,
            outputSelected;

        private IDevice _device;

        private IDevice Device
        {
            get { return this._device; }
            set
            {
                if (this._device != value)
                {
                    if (this._device != null)
                        this._device.PropertyChanged -= Device_PropertyChanged;

                    this._device = value;
                    if (this._device != null)
                        this._device.PropertyChanged += Device_PropertyChanged;

                    this.outputID.Value = this._device?.ID;
                    this.setOutputName();
                }
            }
        }

        private bool setOutputName()
        {
            if (this.outputName == null)
                return false;

            if (string.Equals(this.outputName.Value, this.Device?.Name))
                return false;

            this.outputName.Value = this.Device?.Name;
            return true;
        }

        private bool setOutputSelected()
        {
            if (this.outputSelected == null)
                return false;

            var uc = this.UserContextToUse;
            if (this.Device == null)
                this.outputSelected.Value = null;
            else if (uc != null)
            {
                var device = uc.SelectedDeviceGroup?.DevicesWithoutSubgroups?.FirstOrDefault(d => d.ID.Equals(this.Device?.ID));

                bool result = device != null;
                if (bool.Equals(this.outputSelected.Value, result))
                    return false;
                this.outputSelected.Value = result;
            }

            return true;
        }

        private void select(string op = "")
        {
            if (this.Device == null)
                return;

            if ("T".Equals(op))
            {
                if (this.outputSelected?.Value == null)
                    return;
                if ((bool)this.outputSelected?.Value)
                    this.select("-");
                else
                    this.select("+");
                return;
            }


            if (!string.IsNullOrEmpty(op))
                SelectedDeviceGroupNode.SelectDevice(op + this.Device.ID, this.UserContextToUse);
            else
            {
                bool? value = ConvertToBoolResult(inputSelect?.Value);
                if (value == true)
                    SelectedDeviceGroupNode.SelectDevice(op + this.Device.ID, this.UserContextToUse);
            }
        }

        public WrapperNodeTemplate(GraphNodeID id)
            : base(id, WRAPPERNODE_TYPE, KnownCategories.WRAPPER)
        {
            this.Name = NAME;
        }

        protected override void OnAddedToGraph()
        {
            base.OnAddedToGraph();
            this.setDeviceInternal();
            AssignEvents(true);
        }

        protected override void OnRemovedFromGraph()
        {
            base.OnRemovedFromGraph();
            AssignEvents(false);
        }

        private void DeviceChanged(object sender, DeviceEventArgs e)
        {
            if (this.IsDisposed)
            {
                AssignEvents(false);
                return;
            }

            switch (e.Type)
            {
                case EChangeType.Added:
                    if (this.Device == null)
                    {
                        this.setDeviceInternal();
                        if (this.Device != null)
                            this.OnProcessRequested();
                    }
                    break;
                case EChangeType.Removed:
                    if (Equals(this.Device?.ID, e.Device.ID))
                    {
                        this.Device = null;
                        this.OnProcessRequested();
                    }
                    break;
            }
        }

        private void DeviceNameOrNumberChanged(object sender, DeviceEventArgs e)
        {
            if (this.Device == null || ReferenceEquals(this.Device, e.Device))
            {
                var a = this.Device;
                this.setDeviceInternal();
                if (!ReferenceEquals(a, this.Device))
                    this.OnProcessRequested();
            }
        }

        private void DeviceGroupSelected(object sender, DeviceGroupEventArgs args)
        {
            if (sender != UserContextToUse)
                return;

            if (this.IsDisposed)
            {
                AssignEvents(false);
                return;
            }

            if (this.Device == null)
                return;

            if (this.setOutputSelected())
                this.OnProcessRequested();
        }

        private void Device_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case nameof(IDevice.Name):
                    if (this.setOutputName())
                        this.OnProcessRequested();
                    break;
                default:
                    break;
            }
        }

        public override void InitAfterLoadOrCreate(bool load)
        {
            base.InitAfterLoadOrCreate(load);
            if (load)
                this.setDeviceInternal();
        }

        public override void AddDefaultPorts()
        {
            inputDevice = Inputs.SingleOrDefault(c => c.Name == "Device") ?? AddInputPort(name: "Device");
            inputDevice.ShortName = T._("Device");
            inputDevice.DefaultParameterType = typeof(NameNumberIDValue);
            inputDevice.Value = inputDevice.Value ?? String.Empty;
            inputDevice.InputValueChanged += (o, value) => { this.setDeviceInternal(); };

            inputSelect = Inputs.SingleOrDefault(c => c.Name == "SELECT") ?? AddInputPort(name: "SELECT");
            inputSelect.ShortName = T._("SELECT");
            inputSelect.DefaultParameterType = typeof(bool);
            inputSelect.ShowAsParameter = false;
            inputSelect.InputValueChanged += (o, value) => { if (LumosTools.TryConvertToBool(value) ?? false) this.select(); };

            outputName = Outputs.SingleOrDefault(c => c.Name == "Name") ?? AddOutputPort(name: "Name");
            outputName.ShortName = T._("Name");
            outputID = Outputs.SingleOrDefault(c => c.Name == "ID") ?? AddOutputPort(name: "ID");
            outputID.ShortName = T._("ID");
            outputSelected = Outputs.SingleOrDefault(c => c.Name == "Selected") ?? AddOutputPort(name: "Selected");
            outputSelected.ShortName = T._("Selected");
        }

        private void setDeviceInternal()
        {
            this.Device = GetDevice(inputDevice.Value);
        }

        protected override void DisposeHook()
        {
            base.DisposeHook();
            AssignEvents(false);
            this.Device = null;
        }

        private void AssignEvents(bool register)
        {
            var dm = DeviceManager.getInstance();
            var um = UserManager.getInstance();

            um.DeviceGroupSelected -= DeviceGroupSelected;
            dm.DeviceChanged -= DeviceChanged;
            dm.DeviceNameChanged -= DeviceNameOrNumberChanged;
            dm.DeviceNumberChanged -= DeviceNameOrNumberChanged;


            if (register)
            {
                um.DeviceGroupSelected += DeviceGroupSelected;
                dm.DeviceChanged += DeviceChanged;
                dm.DeviceNameChanged += DeviceNameOrNumberChanged;
                dm.DeviceNumberChanged += DeviceNameOrNumberChanged;
            }
        }
    }
}