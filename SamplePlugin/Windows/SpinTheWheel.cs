using System;
using System.Collections.Generic;
using System.Numerics;
using Dalamud.Interface.Internal;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using SpinTheWheelPlugin.Objects;

namespace SpinTheWheel.Windows;

public class SpinWindow : Window, IDisposable
{
    private Plugin Plugin;

    private List<Wheel> wheels;

    private int inputWheelCount = 1;


    private List<SpinnerWindow> spinnerWindows;

    public SpinWindow(Plugin plugin) : base(
        "Spin the Wheel", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        wheels = new List<Wheel>();
        spinnerWindows = new List<SpinnerWindow>();



        this.Plugin = plugin;
        
    }


    public void Dispose()
    {
    }

    public override void Draw()
    {
        if (ImGui.Button("Create Wheel"))
        {
            spinnerWindows.Add(new SpinnerWindow(Plugin));
            spinnerWindows[spinnerWindows.Count - 1].WindowName = $"Spinner {spinnerWindows.Count}";

            Plugin.WindowSystem.AddWindow(spinnerWindows[spinnerWindows.Count-1]);
            spinnerWindows[spinnerWindows.Count-1].IsOpen = true;
        }

    }

}

