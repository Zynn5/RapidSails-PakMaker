using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pak_Maker.Content.Pak_Maker.Content;
using System.Windows;
using Pak_Maker.VM;
using Pak_Maker.Views;
using Pak_Maker.Content;
using System.Reflection;

namespace Pak_Maker.BytesEngine
{
    public class FinishPak
    {
        public static async Task FinishPakFile(PakManagerVM values) //Im lazy i moved all offsets here and other variables but I will move it to data next time.
        {
            LoadingPakWindow loadingWindow = new LoadingPakWindow();
            ImageLoader imageLoader = new ImageLoader();
            PakManagerWindow pakManagerWindow = new PakManagerWindow(values);
            MainWindow pakbytes = new MainWindow();
            BytesConventer paks = new BytesConventer();
            SavePak savePak = new SavePak();

            bool enableFieldOfView = values.EnableFieldOfView;
            bool enableStaticFov = values.EnableStaticFov;

            float playerFov = values.PlayerFov;
            float sprintFov = values.SprintFov / playerFov * 78;
            float blunderbussFov = values.BlunderbussFov / playerFov * 78;
            float eyeOfReachFov = values.EyeOfReachFov / playerFov * 78;
            float pistolFov = values.PistolFov / playerFov * 78; ;
            float cannonFov = values.CannonFov;
            float wheelFov = values.WheelFov;
            float mapFov = values.MapFov;

            bool quickswap = values.Quickswap;
            bool fastShot = values.FastShot;
            bool fastReload = values.FastReload;
            bool instantZoom = values.InstantZoom;
            bool noSpreadPistol = values.NoSpreadPistol;
            bool reducedSpreadBlunder = values.ReducedSpreadBlunder;
            bool wallbang = values.Wallbang;

            bool enableColorBullets = values.EnableColorBullets;
            float bulletColorR = values.BulletR;
            float bulletColorG = values.BulletG;
            float bulletColorB = values.BulletB;
            float bulletColorA = values.BulletA;


            bool removeUnderwaterFog = values.RemoveUnderwaterFog;
            bool removeStorm = values.RemoveStorm;
            bool removeClouds = values.RemoveClouds;
            bool removeFog = values.RemoveFog;
            bool enableSkyColor = values.EnableSkyColor;
            bool enableWaterColor = values.EnableWaterColor;
            float waterColorR = values.WaterR;
            float waterColorG = values.WaterG;
            float waterColorB = values.WaterB;
            float skyColorR = values.SkyR;
            float skyColorG = values.SkyG;
            float skyColorB = values.SkyB;

            bool enableSunColor = values.EnableSunColor;
            bool enableMoonColor = values.EnableMoonColor;
            bool enableFogColor = values.EnableFogColor;
            float SunR = values.SunR;
            float SunG = values.SunG;
            float SunB = values.SunB;
            float SunA = values.SunA;
            float MoonR = values.MoonR;
            float MoonG = values.MoonG;
            float MoonB = values.MoonB;
            float MoonA = values.MoonA;
            float FogR = values.FogR;
            float FogG = values.FogG;
            float FogB = values.FogB;
            float FogA = values.FogA;




            Application.Current.Windows.OfType<PakManagerWindow>().FirstOrDefault()?.Close();
            pakManagerWindow.Hide();
            loadingWindow.Show();
            
            await Task.Run(async () =>
            {
            
                PakBytes pakData = await PakBytes.DownloadAsync("PUT YOUR LINK TO PAK HERE");
                PakBytes skyColorData = await PakBytes.DownloadAsync("PUT YOUR LINK TO PAK HERE");
                PakBytes waterColorData = await PakBytes.DownloadAsync("PUT YOUR LINK TO PAK HERE");
                PakBytes sunColorData = await PakBytes.DownloadAsync("PUT YOUR LINK TO PAK HERE");
                PakBytes moonColorData = await PakBytes.DownloadAsync("PUT YOUR LINK TO PAK HERE");
                PakBytes fogColorData = await PakBytes.DownloadAsync("PUT YOUR LINK TO PAK HERE");

                byte[] rawDataPak;
                byte[] rawDataSkyPak;
                byte[] rawDataWaterPak;
                byte[] rawDataSunPak;
                byte[] rawDataMoonPak;
                byte[] rawDataFogPak;

                rawDataWaterPak = waterColorData.Data;
                rawDataPak = pakData.Data;
                rawDataSkyPak = skyColorData.Data;
                rawDataSunPak = sunColorData.Data;
                rawDataMoonPak = moonColorData.Data;
                rawDataFogPak = fogColorData.Data;

                //Fov
                int[] fovSprintOffsets = { 0x0001B71A, 0x0001B71B, 0x0001B71C, 0x0001B71D };
                int[] fovBlunderOffsets = { 0x00057A59, 0x00057A5A, 0x00057A5B, 0x00057A5C };
                int[] fovSniperOffsets = { 0x0005F7ED, 0x0005F7EE, 0x0005F7EF, 0x0005F7F0 };
                int[] fovPistolOffsets = { 0x00065F56, 0x00065F57, 0x00065F58, 0x00065F59 };
                int[] fovWheelOffsets = { 0x0000272F, 0x00002730, 0x00002731, 0x00002732 };
                int[] fovCannonOffsets = { 0x00002DBF, 0x00002DC0, 0x00002DC1, 0x00002DC2 };
                int[] fovMapOffsets = { 0x00002EFB, 0x00002EFC, 0x00002EFD, 0x00002EFE };

                uint[,] QuickSwap =
                {
                     { 0x00057C4C, 0x00057C4D, 0x00057C4E, 0x00057C4F },
                     { 0x0005F9CB, 0x0005F9CC, 0x0005F9CD, 0x0005F9CE },
                     { 0x000660FF, 0x00066100, 0x00066101, 0x00066102 }
                };
                int[] InstantZoom = { 0x0005F755, 0x0005F756, 0x0005F757, 0x0005F758 };
                uint[,] FastShot =
                {
                    { 0x00057502, 0x00057503, 0x00057504, 0x00057505 },
                    { 0x0005F1CE, 0x0005F1CF, 0x0005F1D0, 0x0005F1D1 },
                    { 0x000659A4, 0x000659A5, 0x000659A6, 0x000659A7 }
                };
                uint[,] FastReload =
                {
                    { 0x0005751E, 0x0005751F, 0x00057520, 0x00057521 },
                    { 0x0005F1EA, 0x0005F1EB, 0x0005F1EC, 0x0005F1ED },
                    { 0x000659C0, 0x000659C1, 0x000659C2, 0x000659C3 }
                };
                int[] PistolStraightAngle = { 0x00065A44, 0x00065A45, 0x00065A46, 0x00065A47 };
                uint[,] PistolStraight =
                {
                    { 0x00065A28, 0x00065A29 },
                    { 0x00065B08, 0x00065B09 }
                };
                int[] BlunderAngle = { 0x000576A6, 0x000576A7, 0x000576A8, 0x000576A9 };

                //BulletColor
                uint[,] BulletColorR =
                {
                    { 0x00057822, 0x00057823, 0x00057824, 0x00057825 },
                    { 0x0005F6F0, 0x0005F6F1, 0x0005F6F2, 0x0005F6F3 },
                    { 0x00065EC6, 0x00065EC7, 0x00065EC8, 0x00065EC9 }
                }; 
                uint[,] BulletColorG =
                {
                    { 0x00057826, 0x00057827, 0x00057828, 0x00057829 },
                    { 0x0005F6F4, 0x0005F6F5, 0x0005F6F6, 0x0005F6F7 },
                    { 0x00065ECA, 0x00065ECB, 0x00065ECC, 0x00065ECD }
                };
                uint[,] BulletColorB =
                {
                    { 0x0005782A, 0x0005782B, 0x0005782C, 0x0005782D },
                    { 0x0005F6F8, 0x0005F6F9, 0x0005F6FA, 0x0005F6FB },
                    { 0x00065ECE, 0x00065ECF, 0x00065ED0, 0x00065ED1 }
                };
                uint[,] BulletColorA =
                {
                    { 0x0005782E, 0x0005782F, 0x00057830, 0x00057831 },
                    { 0x0005F6FC, 0x0005F6FD, 0x0005F6FE, 0x0005F6FF },
                    { 0x00065ED2, 0x00065ED3, 0x00065ED4, 0x00065ED5 }
                };

                //SkyColor
                uint[,] SkyColorR = {
                    { 0x000002E3, 0x000002E4, 0x000002E5, 0x000002E6 },
                    { 0x00000757, 0x00000758, 0x00000759, 0x0000075A },
                    { 0x00000BCB, 0x00000BCC, 0x00000BCD, 0x00000BCE },
                    { 0x0000103F, 0x00001040, 0x00001041, 0x00001042 },
                    { 0x000014B3, 0x000014B4, 0x000014B5, 0x000014B6 },
                    { 0x00001927, 0x00001928, 0x00001929, 0x0000192A },
                    { 0x00001D9B, 0x00001D9C, 0x00001D9D, 0x00001D9E },
                    { 0x0000220F, 0x00002210, 0x00002211, 0x00002212 },
                    { 0x00002683, 0x00002684, 0x00002685, 0x00002686 },
                    { 0x00002AF7, 0x00002AF8, 0x00002AF9, 0x00002AFA },
                    { 0x00002F6B, 0x00002F6B, 0x00002F6C, 0x00002F6D },
                    { 0x000033DF, 0x000033E0, 0x000033E1, 0x000033E2 },
                    { 0x00003853, 0x00003854, 0x00003855, 0x00003856 }
                }; //14
                uint[,] SkyColorG = {
                    { 0x0000035A, 0x0000035B, 0x0000035C, 0x0000035D },
                    { 0x000007CE, 0x000007CF, 0x000007D0, 0x000007D1 },
                    { 0x00000C42, 0x00000C43, 0x00000C44, 0x00000C45 },
                    { 0x000010B6, 0x000010B7, 0x000010B8, 0x000010B9 },
                    { 0x0000152A, 0x0000152B, 0x0000152C, 0x0000152D },
                    { 0x0000199E, 0x0000199F, 0x000019A0, 0x000019A1 },
                    { 0x00001E12, 0x00001E13, 0x00001E14, 0x00001E15 },
                    { 0x00002286, 0x00002287, 0x00002288, 0x00002289 },
                    { 0x000026F9, 0x000026FA, 0x000026FB, 0x000026FC },
                    { 0x00002B6E, 0x00002B6F, 0x00002B70, 0x00002B71 },
                    { 0x00002FE2, 0x00002FE3, 0x00002FE4, 0x00002FE5 },
                    { 0x00003456, 0x00003457, 0x00003458, 0x00003459 },
                    { 0x000038CA, 0x000038CB, 0x000038CC, 0x000038CD }
                };
                uint[,] SkyColorB = {
                    { 0x000003D1, 0x000003D2, 0x000003D3, 0x000003D4 },
                    { 0x00000845, 0x00000846, 0x00000847, 0x00000848 },
                    { 0x00000CB9, 0x00000CBA, 0x00000CBB, 0x00000CBC },
                    { 0x0000112D, 0x0000112E, 0x0000112F, 0x00001130 },
                    { 0x000015A1, 0x000015A2, 0x000015A3, 0x000015A4 },
                    { 0x00001A15, 0x00001A16, 0x00001A17, 0x00001A18 },
                    { 0x00001E89, 0x00001E8A, 0x00001E8B, 0x00001E8C },
                    { 0x000022FD, 0x000022FE, 0x000022FF, 0x00002300 },
                    { 0x00002771, 0x00002772, 0x00002773, 0x00002774 },
                    { 0x00002BE5, 0x00002BE6, 0x00002BE7, 0x00002BE8 },
                    { 0x00003059, 0x0000305A, 0x0000305B, 0x0000305C },
                    { 0x000034CD, 0x000034CE, 0x000034CF, 0x000034D0 },
                    { 0x00003941, 0x00003942, 0x00003943, 0x00003944 }
                };
                //WaterColor
                uint[,] WaterColorR = {
                    { 0x000002E9, 0x000002EA, 0x000002EB, 0x000002EC },
                    { 0x000006EC, 0x000006ED, 0x000006EE, 0x000006EF },
                    { 0x00000AE1, 0x00000AE2, 0x00000AE3, 0x00000AE4 },
                    { 0x00000EE4, 0x00000EE5, 0x00000EE6, 0x00000EE7 },
                    { 0x000012E1, 0x000012E2, 0x000012E3, 0x000012E4 },
                    { 0x000016F0, 0x000016F1, 0x000016F2, 0x000016F3 },
                    { 0x00001F8F, 0x00001F90, 0x00001F91, 0x00001F92 },
                    { 0x00002392, 0x00002393, 0x00002394, 0x00002395 },
                    { 0x00002787, 0x00002788, 0x00002789, 0x0000278A },
                    { 0x00002B8A, 0x00002B8B, 0x00002B8C, 0x00002B8D },
                    { 0x00002F87, 0x00002F88, 0x00002F89, 0x00002F8A },
                    { 0x00003396, 0x00003397, 0x00003398, 0x00003399 },
                    { 0x00003C35, 0x00003C36, 0x00003C37, 0x00003C38 },
                    { 0x00004038, 0x00004039, 0x0000403A, 0x0000403B },
                    { 0x0000442D, 0x0000442E, 0x0000442F, 0x00004430 },
                    { 0x00004830, 0x00004831, 0x00004832, 0x00004833 },
                    { 0x00004C2D, 0x00004C2E, 0x00004C2F, 0x00004C30 },
                    { 0x0000503C, 0x0000503D, 0x0000503E, 0x0000503F },
                    { 0x000058DB, 0x000058DC, 0x000058DD, 0x000058DE },
                    { 0x00005CDE, 0x00005CDF, 0x00005CE0, 0x00005CE1 },
                    { 0x000060D3, 0x000060D4, 0x000060D5, 0x000060D6 },
                    { 0x000064D6, 0x000064D7, 0x000064D8, 0x000064D9 },
                    { 0x000068D3, 0x000068D4, 0x000068D5, 0x000068D6 },
                    { 0x00006CE2, 0x00006CE3, 0x00006CE4, 0x00006CE5 },
                    { 0x00007581, 0x00007582, 0x00007583, 0x00007584 },
                    { 0x00007984, 0x00007985, 0x00007986, 0x00007987 },
                    { 0x00007D79, 0x00007D7A, 0x00007D7B, 0x00007D7C },
                    { 0x0000817C, 0x0000817D, 0x0000817E, 0x0000817F },
                    { 0x00008579, 0x0000857A, 0x0000857B, 0x0000857C },
                    { 0x00008988, 0x00008989, 0x0000898A, 0x0000898B },
                    { 0x00009227, 0x00009228, 0x00009229, 0x0000922A },
                    { 0x0000962A, 0x0000962B, 0x0000962C, 0x0000962D },
                    { 0x00009A1F, 0x00009A20, 0x00009A21, 0x00009A22 },
                    { 0x00009E22, 0x00009E23, 0x00009E24, 0x00009E25 },
                    { 0x0000A21F, 0x0000A220, 0x0000A221, 0x0000A222 },
                    { 0x0000A62E, 0x0000A62F, 0x0000A630, 0x0000A631 },
                    { 0x0000AECD, 0x0000AECE, 0x0000AECF, 0x0000AED0 },
                    { 0x0000B2D0, 0x0000B2D1, 0x0000B2D2, 0x0000B2D3 },
                    { 0x0000B6C5, 0x0000B6C6, 0x0000B6C7, 0x0000B6C8 },
                    { 0x0000BAC8, 0x0000BAC9, 0x0000BACA, 0x0000BACB },
                    { 0x0000BEC5, 0x0000BEC6, 0x0000BEC7, 0x0000BEC8 },
                    { 0x0000C2D4, 0x0000C2D5, 0x0000C2D6, 0x0000C2D7 },
                    { 0x0000CB73, 0x0000CB74, 0x0000CB75, 0x0000CB76 },
                    { 0x0000CF76, 0x0000CF77, 0x0000CF78, 0x0000CF79 },
                    { 0x0000D36B, 0x0000D36C, 0x0000D36D, 0x0000D36E },
                    { 0x0000D76E, 0x0000D76F, 0x0000D770, 0x0000D771 },
                    { 0x0000DB6B, 0x0000DB6C, 0x0000DB6D, 0x0000DB6E },
                    { 0x0000DF7A, 0x0000DF7B, 0x0000DF7C, 0x0000DF7D },
                    { 0x0000E819, 0x0000E81A, 0x0000E81B, 0x0000E81C },
                    { 0x0000EC1C, 0x0000EC1D, 0x0000EC1E, 0x0000EC1F },
                    { 0x0000F011, 0x0000F012, 0x0000F013, 0x0000F014 },
                    { 0x0000F414, 0x0000F415, 0x0000F416, 0x0000F417 },
                    { 0x0000F811, 0x0000F812, 0x0000F813, 0x0000F814 },
                    { 0x0000FC20, 0x0000FC21, 0x0000FC22, 0x0000FC23 }
                }; //57
                uint[,] WaterColorG = {
                    {0x00000360, 0x00000361, 0x00000362, 0x00000363},
                    {0x00000763, 0x00000764, 0x00000765, 0x00000766},
                    {0x00000B58, 0x00000B59, 0x00000B5A, 0x00000B5B},
                    {0x00000F5B, 0x00000F5C, 0x00000F5D, 0x00000F5E},
                    {0x00001358, 0x00001359, 0x0000135A, 0x0000135B},
                    {0x00001767, 0x00001768, 0x00001769, 0x0000176A},
                    {0x00002006, 0x00002007, 0x00002008, 0x00002009},
                    {0x00002409, 0x0000240A, 0x0000240B, 0x0000240C},
                    {0x000027FE, 0x000027FF, 0x00002800, 0x00002801},
                    {0x00002C01, 0x00002C02, 0x00002C03, 0x00002C04},
                    {0x00002FFE, 0x00002FFF, 0x00003000, 0x00003001},
                    {0x0000340D, 0x0000340E, 0x0000340F, 0x00003410},
                    {0x00003CAC, 0x00003CAD, 0x00003CAE, 0x00003CAF},
                    {0x000040AF, 0x000040B0, 0x000040B1, 0x000040B2},
                    {0x000044A4, 0x000044A5, 0x000044A6, 0x000044A7},
                    {0x000048A7, 0x000048A8, 0x000048A9, 0x000048AA},
                    {0x00004CA4, 0x00004CA5, 0x00004CA6, 0x00004CA7},
                    {0x000050B3, 0x000050B4, 0x000050B5, 0x000050B6},
                    {0x00005952, 0x00005953, 0x00005954, 0x00005955},
                    {0x00005D55, 0x00005D56, 0x00005D57, 0x00005D58},
                    {0x0000614A, 0x0000614B, 0x0000614C, 0x0000614D},
                    {0x0000654D, 0x0000654E, 0x0000654F, 0x00006550},
                    {0x0000694A, 0x0000694B, 0x0000694C, 0x0000694D},
                    {0x00006D59, 0x00006D5A, 0x00006D5B, 0x00006D5C},
                    {0x000075F8, 0x000075F9, 0x000075FA, 0x000075FB},
                    {0x000079FB, 0x000079FC, 0x000079FD, 0x000079FE},
                    {0x00007DF0, 0x00007DF1, 0x00007DF2, 0x00007DF3},
                    {0x000081F3, 0x000081F4, 0x000081F5, 0x000081F6},
                    {0x000085F0, 0x000085F1, 0x000085F2, 0x000085F3},
                    {0x000089FF, 0x00008A00, 0x00008A01, 0x00008A02},
                    {0x0000929E, 0x0000929F, 0x000092A0, 0x000092A1},
                    {0x000096A1, 0x000096A2, 0x000096A3, 0x000096A4},
                    {0x00009A96, 0x00009A97, 0x00009A98, 0x00009A99},
                    {0x00009E99, 0x00009E9A, 0x00009E9B, 0x00009E9C},
                    {0x0000A296, 0x0000A297, 0x0000A298, 0x0000A299},
                    {0x0000A6A5, 0x0000A6A6, 0x0000A6A7, 0x0000A6A8},
                    {0x0000AF44, 0x0000AF45, 0x0000AF46, 0x0000AF47},
                    {0x0000B347, 0x0000B348, 0x0000B349, 0x0000B34A},
                    {0x0000B73C, 0x0000B73D, 0x0000B73E, 0x0000B73F},
                    {0x0000BB3F, 0x0000BB40, 0x0000BB41, 0x0000BB42},
                    {0x0000BF3C, 0x0000BF3D, 0x0000BF3E, 0x0000BF3F},
                    {0x0000C34B, 0x0000C34C, 0x0000C34D, 0x0000C34E},
                    {0x0000CBEA, 0x0000CBEB, 0x0000CBEC, 0x0000CBED},
                    {0x0000CFED, 0x0000CFEE, 0x0000CFEF, 0x0000CFF0},
                    {0x0000D3E2, 0x0000D3E3, 0x0000D3E4, 0x0000D3E5},
                    {0x0000D7E5, 0x0000D7E6, 0x0000D7E7, 0x0000D7E8},
                    {0x0000DBE2, 0x0000DBE3, 0x0000DBE4, 0x0000DBE5},
                    {0x0000DFF1, 0x0000DFF2, 0x0000DFF3, 0x0000DFF4},
                    {0x0000E890, 0x0000E891, 0x0000E892, 0x0000E893},
                    {0x0000EC93, 0x0000EC94, 0x0000EC95, 0x0000EC96},
                    {0x0000F088, 0x0000F089, 0x0000F08A, 0x0000F08B},
                    {0x0000F48B, 0x0000F48C, 0x0000F48D, 0x0000F48E},
                    {0x0000F888, 0x0000F889, 0x0000F88A, 0x0000F88B},
                    {0x0000FC97, 0x0000FC98, 0x0000FC99, 0x0000FC9A}
                };
                uint[,] WaterColorB =
                {
                    {0x000003D7, 0x000003D8, 0x000003D9, 0x000003DA},
                    {0x000007DA, 0x000007DB, 0x000007DC, 0x000007DD},
                    {0x00000BCF, 0x00000BD0, 0x00000BD1, 0x00000BD2},
                    {0x00000FD2, 0x00000FD3, 0x00000FD4, 0x00000FD5},
                    {0x000013CF, 0x000013D0, 0x000013D1, 0x000013D2},
                    {0x000017DE, 0x000017DF, 0x000017E0, 0x000017E1},
                    {0x0000207D, 0x0000207E, 0x0000207F, 0x00002080},
                    {0x00002480, 0x00002481, 0x00002482, 0x00002483},
                    {0x00002875, 0x00002876, 0x00002877, 0x00002878},
                    {0x00002C78, 0x00002C79, 0x00002C7A, 0x00002C7B},
                    {0x00003075, 0x00003076, 0x00003077, 0x00003078},
                    {0x00003484, 0x00003485, 0x00003486, 0x00003487},
                    {0x00003D23, 0x00003D24, 0x00003D25, 0x00003D26},
                    {0x00004126, 0x00004127, 0x00004128, 0x00004129},
                    {0x0000451B, 0x0000451C, 0x0000451D, 0x0000451E},
                    {0x0000491E, 0x0000491F, 0x00004920, 0x00004921},
                    {0x00004D1B, 0x00004D1C, 0x00004D1D, 0x00004D1E},
                    {0x0000512A, 0x0000512B, 0x0000512C, 0x0000512D},
                    {0x000059C9, 0x000059CA, 0x000059CB, 0x000059CC},
                    {0x00005DCC, 0x00005DCD, 0x00005DCE, 0x00005DCF},
                    {0x000061C1, 0x000061C2, 0x000061C3, 0x000061C4},
                    {0x000065C4, 0x000065C5, 0x000065C6, 0x000065C7},
                    {0x000069C1, 0x000069C2, 0x000069C3, 0x000069C4},
                    {0x00006DD0, 0x00006DD1, 0x00006DD2, 0x00006DD3},
                    {0x0000766F, 0x00007670, 0x00007671, 0x00007672},
                    {0x00007A72, 0x00007A73, 0x00007A74, 0x00007A75},
                    {0x00007E67, 0x00007E68, 0x00007E69, 0x00007E6A},
                    {0x0000826A, 0x0000826B, 0x0000826C, 0x0000826D},
                    {0x00008667, 0x00008668, 0x00008669, 0x0000866A},
                    {0x00008A76, 0x00008A77, 0x00008A78, 0x00008A79},
                    {0x00009315, 0x00009316, 0x00009317, 0x00009318},
                    {0x00009718, 0x00009719, 0x0000971A, 0x0000971B},
                    {0x00009B0D, 0x00009B0E, 0x00009B0F, 0x00009B10},
                    {0x00009F10, 0x00009F11, 0x00009F12, 0x00009F13},
                    {0x0000A30D, 0x0000A30E, 0x0000A30F, 0x0000A310},
                    {0x0000A71C, 0x0000A71D, 0x0000A71E, 0x0000A71F},
                    {0x0000AFBB, 0x0000AFBC, 0x0000AFBD, 0x0000AFBE},
                    {0x0000B3BE, 0x0000B3BF, 0x0000B3C0, 0x0000B3C1},
                    {0x0000B7B3, 0x0000B7B4, 0x0000B7B5, 0x0000B7B6},
                    {0x0000BBB6, 0x0000BBB7, 0x0000BBB8, 0x0000BBB9},
                    {0x0000BFB3, 0x0000BFB4, 0x0000BFB5, 0x0000BFB6},
                    {0x0000C3C2, 0x0000C3C3, 0x0000C3C4, 0x0000C3C5},
                    {0x0000CC61, 0x0000CC62, 0x0000CC63, 0x0000CC64},
                    {0x0000D064, 0x0000D065, 0x0000D066, 0x0000D067},
                    {0x0000D459, 0x0000D45A, 0x0000D45B, 0x0000D45C},
                    {0x0000D85C, 0x0000D85D, 0x0000D85E, 0x0000D85F},
                    {0x0000DC59, 0x0000DC5A, 0x0000DC5B, 0x0000DC5C},
                    {0x0000E068, 0x0000E069, 0x0000E06A, 0x0000E06B},
                    {0x0000E907, 0x0000E908, 0x0000E909, 0x0000E90A},
                    {0x0000ED0A, 0x0000ED0B, 0x0000ED0C, 0x0000ED0D},
                    {0x0000F0FF, 0x0000F100, 0x0000F101, 0x0000F102},
                    {0x0000F502, 0x0000F503, 0x0000F504, 0x0000F505},
                    {0x0000F8FF, 0x0000F900, 0x0000F901, 0x0000F902},
                    {0x0000FD0E, 0x0000FD0F, 0x0000FD10, 0x0000FD11}
                };

                uint[,] SunColorR = {
                    { 0x000002E1, 0x000002E2, 0x000002E3, 0x000002E4 },
                    { 0x00000CE2, 0x00000CE3, 0x00000CE4, 0x00000CE5 },
                    { 0x000013DD, 0x000013DE, 0x000013DF, 0x000013E0 },
                    { 0x000017D8, 0x000017D9, 0x000017DA, 0x000017DB },
                    { 0x000021D9, 0x000021DA, 0x000021DB, 0x000021DC },
                    { 0x000028D4, 0x000028D5, 0x000028D6, 0x000028D7 },
                    { 0x00002CCF, 0x00002CD0, 0x00002CD1, 0x00002CD2 },
                    { 0x000036D0, 0x000036D1, 0x000036D2, 0x000036D3 },
                    { 0x00003DCB, 0x00003DCC, 0x00003DCD, 0x00003DCE },
                    { 0x000041C6, 0x000041C7, 0x000041C8, 0x000041C9 },
                    { 0x00004BC7, 0x00004BC8, 0x00004BC9, 0x00004BCA },
                    { 0x000052C2, 0x000052C3, 0x000052C4, 0x000052C5 },
                    { 0x000056BD, 0x000056BE, 0x000056BF, 0x000056C0 },
                    { 0x000060BE, 0x000060BF, 0x000060C0, 0x000060C1 },
                    { 0x000067B9, 0x000067BA, 0x000067BB, 0x000067BC },
                    { 0x00006BB4, 0x00006BB5, 0x00006BB6, 0x00006BB7 },
                    { 0x000075B5, 0x000075B6, 0x000075B7, 0x000075B8 },
                    { 0x00007CB0, 0x00007CB1, 0x00007CB2, 0x00007CB3 },
                    { 0x000080AB, 0x000080AC, 0x000080AD, 0x000080AE },
                    { 0x00008AAC, 0x00008AAD, 0x00008AAE, 0x00008AAF },
                    { 0x000091A7, 0x000091A8, 0x000091A9, 0x000091AA },
                    { 0x000095A2, 0x000095A3, 0x000095A4, 0x000095A5 },
                    { 0x00009FA3, 0x00009FA4, 0x00009FA5, 0x00009FA6 },
                    { 0x0000A69E, 0x0000A69F, 0x0000A6A0, 0x0000A6A1 },
                    { 0x0000AA99, 0x0000AA9A, 0x0000AA9B, 0x0000AA9C },
                    { 0x0000B49A, 0x0000B49B, 0x0000B49C, 0x0000B49D },
                    { 0x0000BB95, 0x0000BB96, 0x0000BB97, 0x0000BB98 },
                    { 0x0000BF90, 0x0000BF91, 0x0000BF92, 0x0000BF93 },
                    { 0x0000C991, 0x0000C992, 0x0000C993, 0x0000C994 },
                    { 0x0000D08C, 0x0000D08D, 0x0000D08E, 0x0000D08F },
                    { 0x0000D487, 0x0000D488, 0x0000D489, 0x0000D48A },
                    { 0x0000DE88, 0x0000DE89, 0x0000DE8A, 0x0000DE8B },
                    { 0x0000E583, 0x0000E584, 0x0000E585, 0x0000E586 },
                    { 0x0000E97E, 0x0000E97F, 0x0000E980, 0x0000E981 },
                    { 0x0000F37F, 0x0000F380, 0x0000F381, 0x0000F382 },
                    { 0x0000FA7A, 0x0000FA7B, 0x0000FA7C, 0x0000FA7D },
                    { 0x0000FE75, 0x0000FE76, 0x0000FE77, 0x0000FE78 },
                    { 0x00010876, 0x00010877, 0x00010878, 0x00010879 },
                    { 0x00010F71, 0x00010F72, 0x00010F73, 0x00010F74 }
                    }; //42
                uint[,] SunColorG = {
                    {0x00000358, 0x00000359, 0x0000035A, 0x0000035B},
                    {0x00000D59, 0x00000D5A, 0x00000D5B, 0x00000D5C},
                    {0x00001454, 0x00001455, 0x00001456, 0x00001457},
                    {0x0000184F, 0x00001850, 0x00001851, 0x00001852},
                    {0x00002250, 0x00002251, 0x00002252, 0x00002253},
                    {0x0000294B, 0x0000294C, 0x0000294D, 0x0000294E},
                    {0x00002D46, 0x00002D47, 0x00002D48, 0x00002D49},
                    {0x00003747, 0x00003748, 0x00003749, 0x0000374A},
                    {0x00003E42, 0x00003E43, 0x00003E44, 0x00003E45},
                    {0x0000423D, 0x0000423E, 0x0000423F, 0x00004240},
                    {0x00004C3E, 0x00004C3F, 0x00004C40, 0x00004C41},
                    {0x00005339, 0x0000533A, 0x0000533B, 0x0000533C},
                    {0x00005734, 0x00005735, 0x00005736, 0x00005737},
                    {0x00006135, 0x00006136, 0x00006137, 0x00006138},
                    {0x00006830, 0x00006831, 0x00006832, 0x00006833},
                    {0x00006C2B, 0x00006C2C, 0x00006C2D, 0x00006C2E},
                    {0x0000762C, 0x0000762D, 0x0000762E, 0x0000762F},
                    {0x00007D27, 0x00007D28, 0x00007D29, 0x00007D2A},
                    {0x00008122, 0x00008123, 0x00008124, 0x00008125},
                    {0x00008B23, 0x00008B24, 0x00008B25, 0x00008B26},
                    {0x0000921E, 0x0000921F, 0x00009220, 0x00009221},
                    {0x00009619, 0x0000961A, 0x0000961B, 0x0000961C},
                    {0x0000A01A, 0x0000A01B, 0x0000A01C, 0x0000A01D},
                    {0x0000A715, 0x0000A716, 0x0000A717, 0x0000A718},
                    {0x0000AB10, 0x0000AB11, 0x0000AB12, 0x0000AB13},
                    {0x0000B511, 0x0000B512, 0x0000B513, 0x0000B514},
                    {0x0000BC0C, 0x0000BC0D, 0x0000BC0E, 0x0000BC0F},
                    {0x0000C007, 0x0000C008, 0x0000C009, 0x0000C00A},
                    {0x0000CA08, 0x0000CA09, 0x0000CA0A, 0x0000CA0B},
                    {0x0000D103, 0x0000D104, 0x0000D105, 0x0000D106},
                    {0x0000D4FE, 0x0000D4FF, 0x0000D500, 0x0000D501},
                    {0x0000DEFF, 0x0000DF00, 0x0000DF01, 0x0000DF02},
                    {0x0000E5FA, 0x0000E5FB, 0x0000E5FC, 0x0000E5FD},
                    {0x0000E9F5, 0x0000E9F6, 0x0000E9F7, 0x0000E9F8},
                    {0x0000F3F6, 0x0000F3F7, 0x0000F3F8, 0x0000F3F9},
                    {0x0000FAF1, 0x0000FAF2, 0x0000FAF3, 0x0000FAF4},
                    {0x0000FEEC, 0x0000FEED, 0x0000FEEE, 0x0000FEEF},
                    {0x000108ED, 0x000108EE, 0x000108EF, 0x000108F0},
                    {0x00010FE8, 0x00010FE9, 0x00010FEA, 0x00010FEB}
                    };
                uint[,] SunColorB = {
                    {0x000003CF, 0x000003D0, 0x000003D1, 0x000003D2},
                    {0x00000DD0, 0x00000DD1, 0x00000DD2, 0x00000DD3},
                    {0x000014CB, 0x000014CC, 0x000014CD, 0x000014CE},
                    {0x000018C6, 0x000018C7, 0x000018C8, 0x000018C9},
                    {0x000022C7, 0x000022C8, 0x000022C9, 0x000022CA},
                    {0x000029C2, 0x000029C3, 0x000029C4, 0x000029C5},
                    {0x00002DBD, 0x00002DBE, 0x00002DBF, 0x00002DC0},
                    {0x000037BE, 0x000037BF, 0x000037C0, 0x000037C1},
                    {0x00003EB9, 0x00003EBA, 0x00003EBB, 0x00003EBC},
                    {0x000042B4, 0x000042B5, 0x000042B6, 0x000042B7},
                    {0x00004CB5, 0x00004CB6, 0x00004CB7, 0x00004CB8},
                    {0x000053B0, 0x000053B1, 0x000053B2, 0x000053B3},
                    {0x000057AB, 0x000057AC, 0x000057AD, 0x000057AE},
                    {0x000061AC, 0x000061AD, 0x000061AE, 0x000061AF},
                    {0x000068A7, 0x000068A8, 0x000068A9, 0x000068AA},
                    {0x00006CA2, 0x00006CA3, 0x00006CA4, 0x00006CA5},
                    {0x000076A3, 0x000076A4, 0x000076A5, 0x000076A6},
                    {0x00007D9E, 0x00007D9F, 0x00007DA0, 0x00007DA1},
                    {0x00008199, 0x0000819A, 0x0000819B, 0x0000819C},
                    {0x00008B9A, 0x00008B9B, 0x00008B9C, 0x00008B9D},
                    {0x00009295, 0x00009296, 0x00009297, 0x00009298},
                    {0x00009690, 0x00009691, 0x00009692, 0x00009693},
                    {0x0000A091, 0x0000A092, 0x0000A093, 0x0000A094},
                    {0x0000A78C, 0x0000A78D, 0x0000A78E, 0x0000A78F},
                    {0x0000AB87, 0x0000AB88, 0x0000AB89, 0x0000AB8A},
                    {0x0000B588, 0x0000B589, 0x0000B58A, 0x0000B58B},
                    {0x0000BC83, 0x0000BC84, 0x0000BC85, 0x0000BC86},
                    {0x0000C07E, 0x0000C07F, 0x0000C080, 0x0000C081},
                    {0x0000CA7F, 0x0000CA80, 0x0000CA81, 0x0000CA82},
                    {0x0000D17A, 0x0000D17B, 0x0000D17C, 0x0000D17D},
                    {0x0000D575, 0x0000D576, 0x0000D577, 0x0000D578},
                    {0x0000DF76, 0x0000DF77, 0x0000DF78, 0x0000DF79},
                    {0x0000E671, 0x0000E672, 0x0000E673, 0x0000E674},
                    {0x0000EA6C, 0x0000EA6D, 0x0000EA6E, 0x0000EA6F},
                    {0x0000F46D, 0x0000F46E, 0x0000F46F, 0x0000F470},
                    {0x0000FB68, 0x0000FB69, 0x0000FB6A, 0x0000FB6B},
                    {0x0000FF63, 0x0000FF64, 0x0000FF65, 0x0000FF66},
                    {0x00010964, 0x00010965, 0x00010966, 0x00010967},
                    {0x0001105F, 0x00011060, 0x00011061, 0x00011062}
                    };
                uint[,] SunColorA = {
                    { 0x000006D3, 0x000006D4, 0x000006D5, 0x000006D6 },
                    { 0x000009DF, 0x000009E0, 0x000009E1, 0x000009E2 },
                    { 0x000010DC, 0x000010DD, 0x000010DE, 0x000010DF },
                    { 0x00001BCA, 0x00001BCB, 0x00001BCC, 0x00001BCD },
                    { 0x00001ED6, 0x00001ED7, 0x00001ED8, 0x00001ED9 },
                    { 0x000025D3, 0x000025D4, 0x000025D5, 0x000025D6 },
                    { 0x000030C1, 0x000030C2, 0x000030C3, 0x000030C4 },
                    { 0x000033CD, 0x000033CE, 0x000033CF, 0x000033D0 },
                    { 0x00003ACA, 0x00003ACB, 0x00003ACC, 0x00003ACD },
                    { 0x000045B8, 0x000045B9, 0x000045BA, 0x000045BB },
                    { 0x000048C4, 0x000048C5, 0x000048C6, 0x000048C7 },
                    { 0x00004FC1, 0x00004FC2, 0x00004FC3, 0x00004FC4 },
                    { 0x00005AAF, 0x00005AB0, 0x00005AB1, 0x00005AB2 },
                    { 0x00005DBB, 0x00005DBC, 0x00005DBD, 0x00005DBE },
                    { 0x000064B8, 0x000064B9, 0x000064BA, 0x000064BB },
                    { 0x00006FA6, 0x00006FA7, 0x00006FA8, 0x00006FA9 },
                    { 0x000072B2, 0x000072B3, 0x000072B4, 0x000072B5 },
                    { 0x000079AF, 0x000079B0, 0x000079B1, 0x000079B2 },
                    { 0x0000849D, 0x0000849E, 0x0000849F, 0x000084A0 },
                    { 0x000087A9, 0x000087AA, 0x000087AB, 0x000087AC },
                    { 0x00008EA6, 0x00008EA7, 0x00008EA8, 0x00008EA9 },
                    { 0x00009994, 0x00009995, 0x00009996, 0x00009997 },
                    { 0x00009CA0, 0x00009CA1, 0x00009CA2, 0x00009CA3 },
                    { 0x0000A39D, 0x0000A39E, 0x0000A39F, 0x0000A3A0 },
                    { 0x0000AE8B, 0x0000AE8C, 0x0000AE8D, 0x0000AE8E },
                    { 0x0000B197, 0x0000B198, 0x0000B199, 0x0000B19A },
                    { 0x0000B894, 0x0000B895, 0x0000B896, 0x0000B897 },
                    { 0x0000C382, 0x0000C383, 0x0000C384, 0x0000C385 },
                    { 0x0000C68E, 0x0000C68F, 0x0000C690, 0x0000C691 },
                    { 0x0000CD8B, 0x0000CD8C, 0x0000CD8D, 0x0000CD8E },
                    { 0x0000D879, 0x0000D87A, 0x0000D87B, 0x0000D87C },
                    { 0x0000DB85, 0x0000DB86, 0x0000DB87, 0x0000DB88 },
                    { 0x0000E282, 0x0000E283, 0x0000E284, 0x0000E285 },
                    { 0x0000ED70, 0x0000ED71, 0x0000ED72, 0x0000ED73 },
                    { 0x0000F07C, 0x0000F07D, 0x0000F07E, 0x0000F07F },
                    { 0x0000F779, 0x0000F77A, 0x0000F77B, 0x0000F77C },
                    { 0x00010267, 0x00010268, 0x00010269, 0x0001026A },
                    { 0x00010573, 0x00010574, 0x00010575, 0x00010576 },
                    { 0x00010C70, 0x00010C71, 0x00010C72, 0x00010C73 }
                    };

                uint[,] MoonColorR = {
                    { 0x000002DD, 0x000002DE, 0x000002DF, 0x000002E0 },
                    { 0x000006D4, 0x000006D5, 0x000006D6, 0x000006D7 },
                    { 0x00000ACB, 0x00000ACC, 0x00000ACD, 0x00000ACE },
                    { 0x00000EC2, 0x00000EC3, 0x00000EC4, 0x00000EC5 },
                    { 0x000012B9, 0x000012BA, 0x000012BB, 0x000012BC },
                    { 0x000016B0, 0x000016B1, 0x000016B2, 0x000016B3 },
                    { 0x00001AA7, 0x00001AA8, 0x00001AA9, 0x00001AAA },
                    { 0x00001E9E, 0x00001E9F, 0x00001EA0, 0x00001EA1 },
                    { 0x00002295, 0x00002296, 0x00002297, 0x00002298 },
                    { 0x0000268C, 0x0000268D, 0x0000268E, 0x0000268F },
                    { 0x00002A83, 0x00002A84, 0x00002A85, 0x00002A86 },
                    { 0x00002E7A, 0x00002E7B, 0x00002E7C, 0x00002E7D },
                    { 0x00003271, 0x00003272, 0x00003273, 0x00003274 }
                    }; //12 
                uint[,] MoonColorG = {
                    { 0x00000354, 0x00000355, 0x00000356, 0x00000357 },
                    { 0x0000074B, 0x0000074C, 0x0000074D, 0x0000074E },
                    { 0x00000B42, 0x00000B43, 0x00000B44, 0x00000B45 },
                    { 0x00000F39, 0x00000F3A, 0x00000F3B, 0x00000F3C },
                    { 0x00001330, 0x00001331, 0x00001332, 0x00001333 },
                    { 0x00001727, 0x00001728, 0x00001729, 0x0000172A },
                    { 0x00001B1E, 0x00001B1F, 0x00001B20, 0x00001B21 },
                    { 0x00001F15, 0x00001F16, 0x00001F17, 0x00001F18 },
                    { 0x0000230C, 0x0000230D, 0x0000230E, 0x0000230F },
                    { 0x00002703, 0x00002704, 0x00002705, 0x00002706 },
                    { 0x00002AFA, 0x00002AFB, 0x00002AFC, 0x00002AFD },
                    { 0x00002EF1, 0x00002EF2, 0x00002EF3, 0x00002EF4 },
                    { 0x000032E8, 0x000032E9, 0x000032EA, 0x000032EB }
                    };
                uint[,] MoonColorB = {
                    { 0x000003CB, 0x000003CC, 0x000003CD, 0x000003CE },
                    { 0x000007C2, 0x000007C3, 0x000007C4, 0x000007C5 },
                    { 0x00000BB9, 0x00000BBA, 0x00000BBB, 0x00000BBC },
                    { 0x00000FB0, 0x00000FB1, 0x00000FB2, 0x00000FB3 },
                    { 0x000013A7, 0x000013A8, 0x000013A9, 0x000013AA },
                    { 0x0000179E, 0x0000179F, 0x000017A0, 0x000017A1 },
                    { 0x00001B95, 0x00001B96, 0x00001B97, 0x00001B98 },
                    { 0x00001F8C, 0x00001F8D, 0x00001F8E, 0x00001F8F },
                    { 0x00002383, 0x00002384, 0x00002385, 0x00002386 },
                    { 0x0000277A, 0x0000277B, 0x0000277C, 0x0000277D },
                    { 0x00002B71, 0x00002B72, 0x00002B73, 0x00002B74 },
                    { 0x00002F68, 0x00002F69, 0x00002F6A, 0x00002F6B },
                    { 0x0000335F, 0x00003360, 0x00003361, 0x00003362 }
                    };

                uint[,] FogColorR = {
                    { 0x000002D5, 0x000002D6, 0x000002D7, 0x000002D8 },
                    { 0x000006CA, 0x000006CB, 0x000006CC, 0x000006CD },
                    { 0x00000EC3, 0x00000EC4, 0x00000EC5, 0x00000EC6 },
                    { 0x000012B8, 0x000012B9, 0x000012BA, 0x000012BB },
                    { 0x00001AB1, 0x00001AB2, 0x00001AB3, 0x00001AB4 },
                    { 0x00001EA6, 0x00001EA7, 0x00001EA8, 0x00001EA9 },
                    { 0x0000269F, 0x000026A0, 0x000026A1, 0x000026A2 },
                    { 0x00002A94, 0x00002A95, 0x00002A96, 0x00002A97 },
                    { 0x0000328D, 0x0000328E, 0x0000328F, 0x00003290 },
                    { 0x00003682, 0x00003683, 0x00003684, 0x00003685 },
                    { 0x00003E7B, 0x00003E7C, 0x00003E7D, 0x00003E7E },
                    { 0x00004270, 0x00004271, 0x00004272, 0x00004273 },
                    { 0x00004A69, 0x00004A6A, 0x00004A6B, 0x00004A6C },
                    { 0x00004E5E, 0x00004E5F, 0x00004E60, 0x00004E61 },
                    { 0x00005657, 0x00005658, 0x00005659, 0x0000565A },
                    { 0x00005A4C, 0x00005A4D, 0x00005A4E, 0x00005A4F },
                    { 0x00006245, 0x00006246, 0x00006247, 0x00006248 },
                    { 0x0000663A, 0x0000663B, 0x0000663C, 0x0000663D },
                    { 0x00006E33, 0x00006E34, 0x00006E35, 0x00006E36 },
                    { 0x00007228, 0x00007229, 0x0000722A, 0x0000722B },
                    { 0x00007A21, 0x00007A22, 0x00007A23, 0x00007A24 },
                    { 0x00007E16, 0x00007E17, 0x00007E18, 0x00007E19 },
                    { 0x0000860F, 0x00008610, 0x00008611, 0x00008612 },
                    { 0x00008A04, 0x00008A05, 0x00008A06, 0x00008A07 },
                    { 0x000091FD, 0x000091FE, 0x000091FF, 0x00009200 },
                    { 0x000095F2, 0x000095F3, 0x000095F4, 0x000095F5 }
                    }; //24
                uint[,] FogColorG = {
                    { 0x0000034C, 0x0000034D, 0x0000034E, 0x0000034F },
                    { 0x00000741, 0x00000742, 0x00000743, 0x00000744 },
                    { 0x00000F3A, 0x00000F3B, 0x00000F3C, 0x00000F3D },
                    { 0x0000132F, 0x00001330, 0x00001331, 0x00001332 },
                    { 0x00001B28, 0x00001B29, 0x00001B2A, 0x00001B2B },
                    { 0x00001F1D, 0x00001F1E, 0x00001F1F, 0x00001F20 },
                    { 0x00002716, 0x00002717, 0x00002718, 0x00002719 },
                    { 0x00002B0B, 0x00002B0C, 0x00002B0D, 0x00002B0E },
                    { 0x00003304, 0x00003305, 0x00003306, 0x00003307 },
                    { 0x000036F9, 0x000036FA, 0x000036FB, 0x000036FC },
                    { 0x00003EF2, 0x00003EF3, 0x00003EF4, 0x00003EF5 },
                    { 0x000042E7, 0x000042E8, 0x000042E9, 0x000042EA },
                    { 0x00004AE0, 0x00004AE1, 0x00004AE2, 0x00004AE3 },
                    { 0x00004ED5, 0x00004ED6, 0x00004ED7, 0x00004ED8 },
                    { 0x000056CE, 0x000056CF, 0x000056D0, 0x000056D1 },
                    { 0x00005AC3, 0x00005AC4, 0x00005AC5, 0x00005AC6 },
                    { 0x000062BC, 0x000062BD, 0x000062BE, 0x000062BF },
                    { 0x000066B1, 0x000066B2, 0x000066B3, 0x000066B4 },
                    { 0x00006EAA, 0x00006EAB, 0x00006EAC, 0x00006EAD },
                    { 0x0000729F, 0x000072A0, 0x000072A1, 0x000072A2 },
                    { 0x00007A98, 0x00007A99, 0x00007A9A, 0x00007A9B },
                    { 0x00007E8D, 0x00007E8E, 0x00007E8F, 0x00007E90 },
                    { 0x00008686, 0x00008687, 0x00008688, 0x00008689 },
                    { 0x00008A7B, 0x00008A7C, 0x00008A7D, 0x00008A7E },
                    { 0x00009274, 0x00009275, 0x00009276, 0x00009277 },
                    { 0x00009669, 0x0000966A, 0x0000966B, 0x0000966C }
                    };
                uint[,] FogColorB = {
                    { 0x000003C3, 0x000003C4, 0x000003C5, 0x000003C6 },
                    { 0x000007B8, 0x000007B9, 0x000007BA, 0x000007BB },
                    { 0x00000FB1, 0x00000FB2, 0x00000FB3, 0x00000FB4 },
                    { 0x000013A6, 0x000013A7, 0x000013A8, 0x000013A9 },
                    { 0x00001B9F, 0x00001BA0, 0x00001BA1, 0x00001BA2 },
                    { 0x00001F94, 0x00001F95, 0x00001F96, 0x00001F97 },
                    { 0x0000278D, 0x0000278E, 0x0000278F, 0x00002790 },
                    { 0x00002B82, 0x00002B83, 0x00002B84, 0x00002B85 },
                    { 0x0000337B, 0x0000337C, 0x0000337D, 0x0000337E },
                    { 0x00003770, 0x00003771, 0x00003772, 0x00003773 },
                    { 0x00003F69, 0x00003F6A, 0x00003F6B, 0x00003F6C },
                    { 0x0000435E, 0x0000435F, 0x00004360, 0x00004361 },
                    { 0x00004B57, 0x00004B58, 0x00004B59, 0x00004B5A },
                    { 0x00004F4C, 0x00004F4D, 0x00004F4E, 0x00004F4F },
                    { 0x00005745, 0x00005746, 0x00005747, 0x00005748 },
                    { 0x00005B3A, 0x00005B3B, 0x00005B3C, 0x00005B3D },
                    { 0x00006333, 0x00006334, 0x00006335, 0x00006336 },
                    { 0x00006728, 0x00006729, 0x0000672A, 0x0000672B },
                    { 0x00006F21, 0x00006F22, 0x00006F23, 0x00006F24 },
                    { 0x00007316, 0x00007317, 0x00007318, 0x00007319 },
                    { 0x00007B0F, 0x00007B10, 0x00007B11, 0x00007B12 },
                    { 0x00007F04, 0x00007F05, 0x00007F06, 0x00007F07 },
                    { 0x000086FD, 0x000086FE, 0x000086FF, 0x00008700 },
                    { 0x00008AF2, 0x00008AF3, 0x00008AF4, 0x00008AF5 },
                    { 0x000092EB, 0x000092EC, 0x000092ED, 0x000092EE },
                    { 0x000096E0, 0x000096E1, 0x000096E2, 0x000096E3 }
                    };



                if (removeUnderwaterFog == true)
                {
                    try
                    {
                        PakDownloader.DownloadPak(values.UserInput, "RapidSailsUnderwaterFogRemoval_p999.pak", "PUT YOUR LINK TO PAK HERE");
                    }
                    catch
                    {
                        MessageBox.Show("Error: C11");
                    }
                }
                if (removeStorm == true)
                {
                    try
                    {
                        PakDownloader.DownloadPak(values.UserInput, "RapidSailsStormRemoval_p999.pak", "PUT YOUR LINK TO PAK HERE");
                    }
                    catch
                    {
                        MessageBox.Show("Error: C11");
                    }
                }
                if (removeClouds == true)
                {
                    try
                    {
                        PakDownloader.DownloadPak(values.UserInput, "RapidSailsCloudsRemoval_p999.pak", "PUT YOUR LINK TO PAK HERE");
                    }
                    catch
                    {
                        MessageBox.Show("Error: C11");
                    }
                }
                if (enableSunColor == true)
                {
                    paks.ConvertFloatToMoreBytes(rawDataSunPak, SunR, SunColorR, 42);
                    paks.ConvertFloatToMoreBytes(rawDataSunPak, SunG, SunColorG, 42);
                    paks.ConvertFloatToMoreBytes(rawDataSunPak, SunB, SunColorB, 42);
                    paks.ConvertFloatToMoreBytes(rawDataSunPak, (SunA / 20), SunColorA, 42);
                    try
                    {
                        savePak.SavePakToPath(values.UserInput, "RapidSailsSunColor_p999.pak", rawDataSunPak);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: C11");
                    }
                }
                if (enableFogColor == true)
                {
                    paks.ConvertFloatToMoreBytes(rawDataFogPak, FogR, FogColorR, 24);
                    paks.ConvertFloatToMoreBytes(rawDataFogPak, FogG, FogColorG, 24);
                    paks.ConvertFloatToMoreBytes(rawDataFogPak, FogB, FogColorB, 24);

                    try
                    {
                        savePak.SavePakToPath(values.UserInput, "RapidSailsFogColor_p999.pak", rawDataFogPak);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: C24");
                    }
                }
                if ( enableMoonColor == true )
                {
                    paks.ConvertFloatToMoreBytes(rawDataMoonPak, MoonR, MoonColorR, 12);
                    paks.ConvertFloatToMoreBytes(rawDataMoonPak, MoonG, MoonColorG, 12);
                    paks.ConvertFloatToMoreBytes(rawDataMoonPak, MoonB, MoonColorB, 12);

                    try
                    {
                        savePak.SavePakToPath(values.UserInput, "RapidSailsMoonColor_p999.pak", rawDataMoonPak);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: C12");
                    }
                }
                if (enableFieldOfView == true && enableStaticFov == false)
                {
                    paks.ConvertFloatToBytes(rawDataPak, sprintFov, fovSprintOffsets);
                    paks.ConvertFloatToBytes(rawDataPak, blunderbussFov, fovBlunderOffsets);
                    paks.ConvertFloatToBytes(rawDataPak, eyeOfReachFov, fovSniperOffsets);
                    paks.ConvertFloatToBytes(rawDataPak, pistolFov, fovPistolOffsets);
                    paks.ConvertFloatToBytes(rawDataPak, cannonFov, fovCannonOffsets);
                    paks.ConvertFloatToBytes(rawDataPak, wheelFov, fovWheelOffsets);
                    paks.ConvertFloatToBytes(rawDataPak, mapFov, fovMapOffsets);
                }
                else if (enableFieldOfView == true && enableStaticFov == true)
                {
                    paks.ConvertFloatToBytes(rawDataPak, 78.0f, fovSprintOffsets);
                    paks.ConvertFloatToBytes(rawDataPak, 78.0f, fovBlunderOffsets);
                    paks.ConvertFloatToBytes(rawDataPak, 78.0f, fovSniperOffsets);
                    paks.ConvertFloatToBytes(rawDataPak, 78.0f, fovPistolOffsets);
                    paks.ConvertFloatToBytes(rawDataPak, playerFov, fovCannonOffsets);
                    paks.ConvertFloatToBytes(rawDataPak, playerFov, fovWheelOffsets);
                    paks.ConvertFloatToBytes(rawDataPak, playerFov, fovMapOffsets);
                }

                if (quickswap == true)
                {
                    try
                    {
                        PakDownloader.DownloadPak(values.UserInput, "RapidSailsEngine_p999.pak", "PUT YOUR LINK TO PAK HERE");
                    }
                    catch
                    {
                        MessageBox.Show("Error: C11");
                    }
                    paks.ConvertFloatToMoreBytes(rawDataPak, 0.1f, QuickSwap, 3);
                }

                if (fastShot == true)
                {
                    paks.ConvertFloatToMoreBytes(rawDataPak, 0.0f, FastShot, 3);
                }

                if (noSpreadPistol)
                {
                    paks.ConvertIntToMoreBytes(rawDataPak, 0, PistolStraight, 2);
                    paks.ConvertFloatToBytes(rawDataPak, 0.0f , PistolStraightAngle);
                }
                if (reducedSpreadBlunder)
                {
                    paks.ConvertFloatToBytes(rawDataPak, 8.0f, BlunderAngle);
                }

                if (wallbang)
                {
                    try
                    {
                        PakDownloader.DownloadPak(values.UserInput, "RapidSailsWallbang_p999.pak", "PUT YOUR LINK TO PAK HERE");
                    }
                    catch
                    {
                        MessageBox.Show("Error: C11");
                    }
                }

                if (fastReload == true)
                {
                    paks.ConvertFloatToMoreBytes(rawDataPak, 0.01f, FastReload, 3);
                }

                if (enableColorBullets == true)
                {
                    paks.ConvertFloatToMoreBytes(rawDataPak, bulletColorR, BulletColorR, 3);
                    paks.ConvertFloatToMoreBytes(rawDataPak, bulletColorG, BulletColorG, 3);
                    paks.ConvertFloatToMoreBytes(rawDataPak, bulletColorB, BulletColorB, 3);
                    paks.ConvertFloatToMoreBytes(rawDataPak, bulletColorA, BulletColorA, 3);
                }

                if (removeFog == true)
                {
                    try
                    {
                        PakDownloader.DownloadPak(values.UserInput, "RapidSailsNoFog_p999.pak", "PUT YOUR LINK TO PAK HERE");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error: C11");
                    }
                }

                if (enableSkyColor == true)
                {
                    paks.ConvertFloatToMoreBytes(rawDataSkyPak, skyColorR, SkyColorR, 13);
                    paks.ConvertFloatToMoreBytes(rawDataSkyPak, skyColorG, SkyColorG, 13);
                    paks.ConvertFloatToMoreBytes(rawDataSkyPak, skyColorB, SkyColorB, 13);
                    try
                    {
                        savePak.SavePakToPath(values.UserInput, "RapidSailsSkyColor_p999.pak", rawDataSkyPak);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: C11");
                    }
                }
                if (enableWaterColor == true)
                {
                    paks.ConvertFloatToMoreBytes(rawDataWaterPak, waterColorR, WaterColorR, 13);
                    paks.ConvertFloatToMoreBytes(rawDataWaterPak, waterColorG, WaterColorG, 13);
                    paks.ConvertFloatToMoreBytes(rawDataWaterPak, waterColorB, WaterColorB, 13);
                    try
                    {
                        savePak.SavePakToPath(values.UserInput, "RapidSailsWaterColor_p999.pak", rawDataWaterPak);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: C11");
                    }
                }


                try
                {
                    savePak.SavePakToPath(values.UserInput, "RapidSailsPak_p99999.pak", rawDataPak);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: C11");
                }

            });

            Application.Current.Windows.OfType<LoadingPakWindow>().FirstOrDefault()?.Close();
            loadingWindow.Hide();
            pakManagerWindow.Show();
           
        }
    }
}
