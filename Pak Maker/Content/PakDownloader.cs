using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Net.Http;

namespace Pak_Maker.Content
{
    namespace Pak_Maker.Content
    {
        public class PakDownloader : ImageLoader
        {
            private readonly string _pakDirectory = SettingsManager.PakPath;  

            public async Task DownloadPakFileAsync(string packUrl, string category)
            {
                string fileName = GetPakFileName(category);
                string filePath = Path.Combine(_pakDirectory, fileName);

                try
                {
                    using HttpClient client = new();
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36");
                    byte[] fileData = await client.GetByteArrayAsync(packUrl);

                    await File.WriteAllBytesAsync(filePath, fileData);
                    MessageBox.Show($"{fileName} Downloaded to path!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: C11 or C13");
                }
            }

            private string GetPakFileName(string category)
            {
                return category switch
                {
                    "Blunder" => "BlunderbussSkin_p999.pak",
                    "Eor" => "EyeofReachSkin_p999.pak",
                    "Pistol" => "FlintlockSkin_p999.pak",
                    _ => throw new ArgumentException("Wrong file!", nameof(category))
                };
            }

            public static async Task DownloadPak(string path, string name, string url)
            {
                try
                {
                    string fullPath = Path.Combine(path, name);

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36");
                        byte[] fileBytes = await client.GetByteArrayAsync(url);

                        await File.WriteAllBytesAsync(fullPath, fileBytes);

                        MessageBox.Show("Pak saved to path!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: C11 or C13");
                }
            }
        }
    }

}