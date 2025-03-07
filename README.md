# Plugin Template for DMXControl 3

This repository contains both Kernel and GUI plugins to demonstrate how plugins for DMXControl 3 are written and which elements are required. Additionally, this repository includes examples of potentially useful classes and how to register their objects.

## Required Plugin Structure

For both the GUI and Kernel plugins, you need to create a class that inherits from `GuiPluginBase` for the GUI plugin or `KernelPluginBase` for the Kernel plugin. This class serves as the entry point for DMXControl 3 to your plugin, and all other plugin content can be initialized and started from there.

> [!CAUTION]
> You must change the GUID and the registered name to meaningful values in the constructor of the plugin class! The GUID must be unique so DMXControl 3 can distinguish between different plugins. The name is displayed in the plugins window, allowing users to identify your plugin easily.

## Folder Structure of DMXControl 3

All plugins must be added to the plugin folder in the Kernel or the GUI depending on the plugin. You can place your plugin directly in the plugin folder or within a separate sub-folder dedicated to your plugin.

- If you place the plugin directly in the plugin folder, ensure all your dependencies are moved to the `api-dlls` folder.
- If you use a separate folder for your plugin, you can either place your dependencies in the `api-dlls` folder or keep them within the plugin sub-folder.

In both cases, DMXControl 3 should locate and load the dependencies correctly.

## Function Calls
The following section lists all relevant function calls for the plugins:

| Function | Call Time | Mandatory | Kernel | GUI | Description |
| --- | --- | :---: | :---: | :---: | --- |
| `initializePlugin()` | Once at DMXControl 3 startup | ✅ | ✅ | ✅ | Perform the basic initialization of your plugin here. **This function is called even if the plugin is not activated. Avoid creating objects that are unnecessary while the plugin is deactivated.** |
| `startupPlugin()` | Each time the plugin is activated (when the checkbox in the plugin window is selected) | ✅ | ✅ | ✅ | Initialize all runtime data required for the plugin here. Also, check if the Kernel has already loaded a project. |
| `shutdownPlugin()` | Each time the plugin is deactivated (when the checkbox in the plugin window is cleared) | ✅ | ✅ | ✅ | Clean up all plugin data in this function to ensure no residual influence on DMXControl 3 while the plugin is deactivated. |
| `connectionEstablished()` | After the connection to the Kernel is established |  |  | ✅ | Use this function to load data from the Kernel. Ensure to check whether a project is already loaded. |
| `connectionClosing()` | Immediately after the connection is initiated for closure |  |  | ✅ | Remove all objects that depend on a connection to the Kernel. |
| `loadProject(LumosIOContext context)` | At the end of the project loading process |  | ✅ | ✅ | Load project-specific data stored in the project file here. |
| `saveProject(LumosIOContext context)` | At the end of the project saving process |  | ✅ | ✅ | Save project-specific data here so that it is included in the project. |
| `closeProject(LumosIOContext context)` | At the end of the project closing process |  | ✅ | ✅ | Unload all project-related data and objects here. |

> [!CAUTION]
> The `initializePlugin()` function is called even if the plugin is not activated. You should **not** create objects here that are unnecessary while the plugin is deactivated.

> [!IMPORTANT]
> The `loadProject(LumosIOContext context)` function is only called if the GUI is connected and the Kernel loads a project. If your plugin is enabled or a connection is established, you must manually check if a project is already loaded and handle any necessary logic accordingly.

## Useful Classes
The following section describes some classes that may be useful for your plugin:

| Class | Inheritance | Kernel/GUI | Location | Description |
| --- | --- | :---: | --- | --- |
| `PEBranchTemplate` | `AbstractExplorerBranch` | GUI | Project Explorer | Adds a new top-level entry in the Project Explorer. |
| `PENodeTemplate` | `AbstractGuiExplorerNode` | GUI | Project Explorer | Adds a new sub-node to a branch in the Project Explorer. Nested sub-nodes are also possible. |
| `DimCurveTemplate` | `IFadeCurve` | Kernel | Dimmer Curve | Defines a new dimmer curve, which can be used for the dimmer property of a device (in device settings) or the dimmer curve of the Chaser Filter. |
| `FrequentEffectTemplate` | `AbstractFrequentFunctionEffect` | Kernel | Effects and Filters | Defines a new 1D effect based on a physical function. |
| `MxMatrixEffectTemplate` | `AbstractMxEffect` | Kernel | Effects and Filters | Defines a new matrix effect. |
| `NodeTemplate` | `AbstractNode` or `AbstractNodeWithEnable` | Kernel | Input Assignment | Adds a new node to the Input Assignment, suitable for logic nodes independent of other objects like devices, cuelists etc. |
| `WrapperNodeTemplate` | **`IWrapperGraphNode`** with `AbstractNode` or `AbstractNodeWithEnable` | Kernel | Input Assignment | Adds a wrapper node to the Input Assignment, intended for nodes which are wrapper for other objects like devices, cuelists etc. |
| `SourceTemplate` | `AbstractInputSource` | Kernel | Input Assignment | Adds a new input source with a single input. Useful e.g. for external device inputs, similar to keyboard inputs. |
| `SinkTemplate` | `AbstractInputSink` | Kernel | Input Assignment | Adds a new sink with a single output. Useful for returning data e.g. to an external device. |
| `MainSwitchTemplate` | `IMainSwitch` | Kernel | Main Switch | Adds a new switch to the main switch feature in the left quick-access menu, allowing users to enable/disable parts of your plugin. |

> [!WARNING]
> In future versions of DMXControl 3, Input Sources and Input Sinks may be deprecated. Be aware of potential changes in upcoming releases.

## Logging
You can use DMXControl 3's logging system to write debug information. Examples are available for setting up the Lumos logger. Once the `Log` object is initialized, you can log messages using:

- `Log.Info()`
- `Log.Debug()`
- `Log.Warn()`
- `Log.Error()`

> [!IMPORTANT]
> Ensure proper error handling using `try ... catch ...` blocks where needed to prevent DMXControl 3 from crashing.

> [!NOTE]
> Using DMXControl 3's internal logging system is optional. You may implement your own logging mechanism. However, using the internal system facilitates easier debugging by aligning your plugin's log entries with those from other DMXControl 3 components.

