using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Pak_Maker.Content
{
    public class ImageLoader
    {
        private static readonly string EncryptedBinId = "your bin"; //I used jsonbin 
        private static readonly string EncryptedMasterKey = "Access key from jsonbin";
        private static readonly string BinId = CryptoHelper.Decrypt(EncryptedBinId);
        private static readonly string MasterKey = CryptoHelper.Decrypt(EncryptedMasterKey);
        private static readonly string BaseUrl = "https://api.jsonbin.io/v3/b/" + BinId + "/latest";

        public List<string> BlunderPacks { get; private set; } = new();
        public List<string> EorPacks { get; private set; } = new();
        public List<string> PistolPacks { get; private set; } = new();


        public ObservableCollection<BitmapImage> BlunderImages { get; private set; } = new();
        public ObservableCollection<BitmapImage> EorImages { get; private set; } = new();
        public ObservableCollection<BitmapImage> PistolImages { get; private set; } = new();

        public async Task LoadAllImagesAsync()
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Add("X-Access-Key", MasterKey);

            try
            {
                string json = await client.GetStringAsync(BaseUrl);
                var document = JsonDocument.Parse(json);
                var root = document.RootElement.GetProperty("record");

                BlunderImages.Clear();
                EorImages.Clear();
                PistolImages.Clear();
                BlunderPacks.Clear();
                EorPacks.Clear();
                PistolPacks.Clear();

                await LoadCategoryImages(root, "Blunder", BlunderImages, BlunderPacks);
                await LoadCategoryImages(root, "Eor", EorImages, EorPacks);
                await LoadCategoryImages(root, "Pistol", PistolImages, PistolPacks);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading images: " + ex.Message);
            }
        }
        private async Task LoadCategoryImages(JsonElement root, string category, ObservableCollection<BitmapImage> imageList, List<string> packList)
        {
            if (root.TryGetProperty(category, out var items))
            {
                foreach (var item in items.EnumerateArray())
                {
                    if (item.TryGetProperty("url", out var urlProp) && item.TryGetProperty("pack", out var packProp))
                    {
                        BitmapImage image = await DownloadImageAsync(urlProp.GetString());
                        if (image != null)
                        {
                            imageList.Add(image);
                            packList.Add(packProp.GetString());
                        }
                    }
                }
            }
        }
        private async Task<BitmapImage> DownloadImageAsync(string url)
        {
            try
            {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36");
                byte[] data = await client.GetByteArrayAsync(url);

                BitmapImage bitmap = new();
                using MemoryStream ms = new(data);
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                bitmap.Freeze();

                return bitmap;
            }
            catch
            {
                return null; // Ignore failed images
            }
        }
    }
}

