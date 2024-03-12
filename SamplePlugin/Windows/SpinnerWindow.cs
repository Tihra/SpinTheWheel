using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using SpinTheWheelPlugin.Objects;


namespace SpinTheWheel.Windows
{
    class SpinnerWindow : Window, IDisposable
    {
        private Plugin plugin;
        private Wheel wheel;

        public SpinnerWindow(Plugin plugin) : base(
        "Spin the Wheel", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
        {
            this.SizeConstraints = new WindowSizeConstraints
            {
                MinimumSize = new Vector2(375, 160),
                MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
            };

            this.Size = new Vector2(375, 160);

            wheel = new Wheel();
        }
        public override void Draw()
        {

            wheel.Draw();

        }

        public void Dispose()
        {

        }
    }


}
