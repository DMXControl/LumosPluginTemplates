using Lumos.GUI;
using Lumos.GUI.Facade;
using Lumos.GUI.GuiActions;
using Lumos.GUI.Windows;
using Lumos.GUI.Windows.ProjectExplorer;
using LumosLIB.Kernel.Log;
using LumosLIB.Tools.I18n;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LumosGUIPluginTemplates.ProjectExplorer
{
    class PENodeTemplate : AbstractGuiExplorerNode
    {
        private static readonly ILumosLog log = LumosLogger.getInstance(nameof(PENodeTemplate));

        private int id;
        private string name, color;
        private Random random = new Random();

        public PENodeTemplate()
           : base(KnownIcons.COLOR)
        {
            id = random.Next(0, 100);
            name = "PENodeTemplate" + id;
            //random selection of one of the following color strings:
            color = new string[] { "red", "green", "blue", "yellow", "orange", "purple", "pink", "brown", "black", "white" }[random.Next(0, 10)];
        }

        public override string ID
        {
            get { return id.ToString(); }
        }

        public string Color
        {
            get { return color; }
        }

        public override bool SetNameAllowed => true;

        public override string DisplayName
        {
            get { return name; }
            set
            {
                if (SetNameAllowed)
                {
                    name = value;
                }
            }
        }

        public override IConfigurable PropertiesElement => null;

        public override EPropertiesMode PropertiesMode => EPropertiesMode.CONFIG_PROPERTIES;

        public override ENodeOptions NodeOptions => base.NodeOptions;

        protected override void OnInTreeChanged()
        {
            base.OnInTreeChanged();
        }

        public override ReadOnlyCollection<ActionItemMetadata> MenuItems
        {
            get
            {
                List<ActionItemMetadata> tmp = new List<ActionItemMetadata>()
                {
                    new ActionItemMetadata("ActEditTemplate", this.ID, "", KnownIcons.PLUGIN + "_edit", EActionItemDisplayType.TOOL_STRIP | EActionItemDisplayType.CONTEXT_MENU, 0, "0_TemplateBranch", null, () => nodeDoubleClick())
                    {
                        DisplayNameFunction = async () => T._("Edit Template")
                    }
                };
                return tmp.AsReadOnly();
            }
        }

        protected override async Task<EDoubleClickResult> nodeDoubleClickInternal()
        {
            return EDoubleClickResult.Ok;
        }

        public override async Task<bool> deleteItem(DeletionContext context)
        {
            try
            {
                if (this.ParentBranch is PEBranchTemplate)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return false;
        }
    }
}
