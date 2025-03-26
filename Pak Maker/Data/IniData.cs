using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pak_Maker.Data
{
    public class IniData
    {

        private static readonly string iniFilePath = "ValueData.ini";

        public static void SaveDataToIni(
            bool enableFieldOfView, bool enableStaticFov, float fovPlayer, float fovSprint,
            float fovBlunderbuss, float fovEyeOfReach, float fovPistol, float fovCannon,
            float fovWheel, float fovMap, bool quickswap, bool fastShot, bool fastReload,
            bool instantZoom, bool enableColorBullets, float bulletColorR, float bulletColorG,
            float bulletColorB, float bulletColorA, bool removeFog, bool enableSkyColor,
            float skyColorR, float skyColorG, float skyColorB)
        {
            StringBuilder iniContent = new StringBuilder();

            iniContent.AppendLine("[FieldOfView]");
            iniContent.AppendLine($"EnableFieldOfView={enableFieldOfView}");
            iniContent.AppendLine($"EnableStaticFov={enableStaticFov}");
            iniContent.AppendLine($"FovPlayer={fovPlayer.ToString(CultureInfo.InvariantCulture)}");
            iniContent.AppendLine($"FovSprint={fovSprint.ToString(CultureInfo.InvariantCulture)}");
            iniContent.AppendLine($"FovBlunderbuss={fovBlunderbuss.ToString(CultureInfo.InvariantCulture)}");
            iniContent.AppendLine($"FovEyeOfReach={fovEyeOfReach.ToString(CultureInfo.InvariantCulture)}");
            iniContent.AppendLine($"FovPistol={fovPistol.ToString(CultureInfo.InvariantCulture)}");
            iniContent.AppendLine($"FovCannon={fovCannon.ToString(CultureInfo.InvariantCulture)}");
            iniContent.AppendLine($"FovWheel={fovWheel.ToString(CultureInfo.InvariantCulture)}");
            iniContent.AppendLine($"FovMap={fovMap.ToString(CultureInfo.InvariantCulture)}");

            iniContent.AppendLine("\n[Misc]");
            iniContent.AppendLine($"Quickswap={quickswap}");
            iniContent.AppendLine($"FastShot={fastShot}");
            iniContent.AppendLine($"FastReload={fastReload}");
            iniContent.AppendLine($"InstantZoom={instantZoom}");
            iniContent.AppendLine($"EnableColorBullets={enableColorBullets}");

            iniContent.AppendLine("\n[BulletColor]");
            iniContent.AppendLine($"BulletColorR={bulletColorR.ToString(CultureInfo.InvariantCulture)}");
            iniContent.AppendLine($"BulletColorG={bulletColorG.ToString(CultureInfo.InvariantCulture)}");
            iniContent.AppendLine($"BulletColorB={bulletColorB.ToString(CultureInfo.InvariantCulture)}");
            iniContent.AppendLine($"BulletColorA={bulletColorA.ToString(CultureInfo.InvariantCulture)}");

            iniContent.AppendLine("\n[Environment]");
            iniContent.AppendLine($"RemoveFog={removeFog}");
            iniContent.AppendLine($"EnableSkyColor={enableSkyColor}");
            iniContent.AppendLine($"SkyColorR={skyColorR.ToString(CultureInfo.InvariantCulture)}");
            iniContent.AppendLine($"SkyColorG={skyColorG.ToString(CultureInfo.InvariantCulture)}");
            iniContent.AppendLine($"SkyColorB={skyColorB.ToString(CultureInfo.InvariantCulture)}");

            File.WriteAllText(iniFilePath, iniContent.ToString());
            MessageBox.Show("Saved settings!");
        }

        public static void LoadDataFromIni(out bool enableFieldOfView, out bool enableStaticFov,
            out float fovPlayer, out float fovSprint, out float fovBlunderbuss, out float fovEyeOfReach,
            out float fovPistol, out float fovCannon, out float fovWheel, out float fovMap,
            out bool quickswap, out bool fastShot, out bool fastReload, out bool instantZoom,
            out bool enableColorBullets, out float bulletColorR, out float bulletColorG,
            out float bulletColorB, out float bulletColorA, out bool removeFog,
            out bool enableSkyColor, out float skyColorR, out float skyColorG, out float skyColorB)
        {
            enableFieldOfView = false;
            enableStaticFov = false;
            fovPlayer = 90f;
            fovSprint = 100f;
            fovBlunderbuss = 0f;
            fovEyeOfReach = 0f;
            fovPistol = 0f;
            fovCannon = 0f;
            fovWheel = 0f;
            fovMap = 0f;
            quickswap = false;
            fastShot = false;
            fastReload = false;
            instantZoom = false;
            enableColorBullets = false;
            bulletColorR = 1.0f;
            bulletColorG = 1.0f;
            bulletColorB = 1.0f;
            bulletColorA = 1.0f;
            removeFog = false;
            enableSkyColor = false;
            skyColorR = 0f;
            skyColorG = 0f;
            skyColorB = 0f;

            if (!File.Exists(iniFilePath))
                return;

            string[] lines = File.ReadAllLines(iniFilePath);
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("["))
                    continue;

                string[] keyValue = line.Split('=');
                if (keyValue.Length != 2)
                    continue;

                string key = keyValue[0].Trim();
                string value = keyValue[1].Trim();

                switch (key)
                {
                    case "EnableFieldOfView": enableFieldOfView = bool.Parse(value); break;
                    case "EnableStaticFov": enableStaticFov = bool.Parse(value); break;
                    case "FovPlayer": fovPlayer = float.Parse(value, CultureInfo.InvariantCulture); break;
                    case "FovSprint": fovSprint = float.Parse(value, CultureInfo.InvariantCulture); break;
                    case "Quickswap": quickswap = bool.Parse(value); break;
                    case "FastShot": fastShot = bool.Parse(value); break;
                    case "RemoveFog": removeFog = bool.Parse(value); break;
                    case "BulletColorR": bulletColorR = float.Parse(value, CultureInfo.InvariantCulture); break;
                    case "SkyColorB": skyColorB = float.Parse(value, CultureInfo.InvariantCulture); break;
                }
            }
        }
    }

}

