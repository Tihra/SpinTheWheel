using FFXIVClientStructs.Havok;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpinTheWheelPlugin.Objects
{
    internal class Wheel
    {
        

        public int InputSeed = 0;
        public bool InputbUseSeed = false;
        public string InputOptions = "";
        public string outputResult = "Spin the Wheel";

        private Random random;
        private string processedString = "";

        public Wheel()
        {

        }
        ~Wheel()
        {

        }

        public void Draw()
        {
      
            ImGui.Checkbox("Use seed ", ref this.InputbUseSeed);

            if (this.InputbUseSeed)
            {
                ImGui.InputInt("Seed",ref this.InputSeed);
            }

            ImGui.InputTextWithHint("Options","Separate options with a comma", ref this.InputOptions, 1024);


            if (ImGui.Button("Spin"))
            {
                this.outputResult = SpinRandom();
            }

            ImGui.Text($"Result: {this.outputResult}");
        }

        public string SpinRandom()
        {
            if (InputbUseSeed)
            {
                this.random = new Random(this.InputSeed);
            }
            else
            {
                this.random = new Random();
            }

            processedString = this.InputOptions.Replace(" ", "");
            string[] options = processedString.Split(',');

            

            return options[random.Next(0,options.Length)];
        }
    }
}
