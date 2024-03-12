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

    private List<Wheel> Wheels;


    private string result ="";

    private Random random;

    private int inputWheelCount = 1;

    private int inputSeed = 0;
    private string inputOptions = "";
    private bool inputUseSeed = false;

    public SpinWindow(Plugin plugin) : base(
        "Spin the Wheel", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

    
        this.Plugin = plugin;

    }

    public string ExecuteSpin()
    {
        inputOptions.Replace(" ", "");
        string[] brokenstring = inputOptions.Split(',');

        return brokenstring[DevSpin(0, brokenstring.Length)];
    }
    private int DevSpin(int min, int max)
    {

        if (inputUseSeed)
        {
            random = new Random(inputSeed);
        }
        else
        {
            random = new Random();
        }

        int resultListIndex = -1;        

        resultListIndex = random.Next(min,max);

        return resultListIndex;
    }
    public void Dispose()
    {
    }

    public override void Draw()
    {

        if (ImGui.InputInt("Wheel Count", ref inputWheelCount,1,1))
        {
            //count changed, clamp min max

            if (inputWheelCount < 1) inputWheelCount = 1;
            if (inputWheelCount > 5) inputWheelCount = 5;
        }

        ImGui.Checkbox("Use seed ", ref inputUseSeed);

        if (inputUseSeed)
        {
            ImGui.InputInt("Seed", ref inputSeed);
        }        

        if (ImGui.InputTextWithHint("Options", "Separate entries with a comma ( , )", ref inputOptions, 1024))
        {

        }

        if (ImGui.Button("Spin"))
        {
            result = ExecuteSpin();
        }


        ImGui.Spacing();
        ImGui.Text($"Result: {result}.");
        ImGui.Spacing();

    }

}

