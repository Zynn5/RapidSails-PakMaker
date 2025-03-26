using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pak_Maker.Content
{
    public static class SettingsManager
    {
        private static readonly string IniFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PathToFiles.ini");
        private const string IniSection = "Settings";
        private const string IniKey = "PakPath";
        public static string PakPath { get; set; } = "D:\\Steam\\steamapps\\common\\Sea of Thieves\\Athena\\Content\\Paks";
        public static void LoadSettings()
        {
            if (File.Exists(IniFilePath))
            {
                var lines = File.ReadAllLines(IniFilePath);
                foreach (var line in lines)
                {
                    if (line.StartsWith($"{IniKey}="))
                    {
                        PakPath = line.Split('=')[1]; 
                        break;
                    }
                }
            }
        }
        public static void SaveSettings()
        {
            using (var writer = new StreamWriter(IniFilePath, false))
            {
                writer.WriteLine($"{IniKey}={PakPath}");
            }
        }
    }

}
