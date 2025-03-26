using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pak_Maker.BytesEngine
{
    internal class PakBytes
    {
        public byte[] Data { get; private set; }

        private PakBytes(byte[] data)
        {
            Data = data;
        }
        public PakBytes()
        {
            Data = new byte[0];  
        }

        public static async Task<PakBytes> DownloadAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36");
                byte[] data = await client.GetByteArrayAsync(url);
                return new PakBytes(data);
            }
        }
    }
}

