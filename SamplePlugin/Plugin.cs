using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System.IO;
using Dalamud.Interface.Windowing;
using Dalamud.Plugin.Services;
using SpinTheWheel.Windows;

namespace SpinTheWheel
{
    public sealed class Plugin : IDalamudPlugin
    {
        public string Name => "Spin the Wheel";
        private const string SpinCommand = "/stw";

        private DalamudPluginInterface PluginInterface { get; init; }
        private ICommandManager CommandManager { get; init; }
        public Configuration Configuration { get; init; }
        public WindowSystem WindowSystem = new("Spin the Wheel");

        private ConfigWindow ConfigWindow { get; init; }
   
        private SpinWindow SpinWindow { get; init; }

        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
            [RequiredVersion("1.0")] ICommandManager commandManager)
        {
            this.PluginInterface = pluginInterface;
            this.CommandManager = commandManager;

            this.Configuration = this.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            this.Configuration.Initialize(this.PluginInterface);


            ConfigWindow = new ConfigWindow(this);
    
            SpinWindow = new SpinWindow(this);
            
            WindowSystem.AddWindow(ConfigWindow);

            WindowSystem.AddWindow(SpinWindow);


            this.CommandManager.AddHandler(SpinCommand, new CommandInfo(OnSpinCommand)
            {
                HelpMessage = "Display the Spin Wheel Window"
            });

            this.PluginInterface.UiBuilder.Draw += DrawUI;
            this.PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;
        }

        public void Dispose()
        {
            this.WindowSystem.RemoveAllWindows();
            
            ConfigWindow.Dispose();

            SpinWindow.Dispose();
            

            this.CommandManager.RemoveHandler(SpinCommand);
        }


        private void OnSpinCommand(string command, string args)
        {
            // in response to the slash command, just display our main ui
            SpinWindow.IsOpen = true;
            
        }

        private void DrawUI()
        {
            this.WindowSystem.Draw();
        }

        public void DrawConfigUI()
        {
            ConfigWindow.IsOpen = true;
        }
    }
}
