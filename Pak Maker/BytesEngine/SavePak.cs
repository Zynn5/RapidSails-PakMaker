using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pak_Maker.BytesEngine
{
    internal class SavePak
    {
        public void SavePakToPath(string path,string name, byte[] data)
        {
            string FullPath = path + "/" + name;
            try
            {
                File.WriteAllBytes(FullPath, data);
                if (File.Exists(FullPath))
                {
                    MessageBox.Show("Pak Saved to path!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: C11");
            }

        }
    }
}
