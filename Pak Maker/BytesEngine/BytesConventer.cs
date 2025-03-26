using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Pak_Maker.BytesEngine
{
    public class BytesConventer
    {
        public void ConvertFloatToBytes(byte[] bytes, float value, int[] offset)
        {
            byte[] Byte = BitConverter.GetBytes(value);


            for (int i = 0; i < 4; i++)
            {
                if (offset.Length > i)
                {
                    bytes[offset[i]] = Byte[i];
                }
            }
        }
        public void ConvertFloatToMoreBytes(byte[] bytes, float value, uint[,] offset, uint lenght)
        {
            byte[] Byte = BitConverter.GetBytes(value);

            for (int j = 0; j < lenght; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (offset.GetLength(0) > j && offset.GetLength(1) > i)
                    {
                        bytes[offset[j, i]] = Byte[i];
                    }
                }
            }
        }
        public void ConvertRGBAFloatToBytes(byte[] bytes, float R, float G, float B, float A, int[] offsetsR, int[] offsetsG, int[] offsetsB, int[] offsetsA)
        {
            byte[] byteR = BitConverter.GetBytes(R);
            byte[] byteG = BitConverter.GetBytes(G);
            byte[] byteB = BitConverter.GetBytes(B);
            byte[] byteA = BitConverter.GetBytes(A);


            for (int i = 0; i < 4; i++)
            {
                if (offsetsR.Length > i)
                {
                    bytes[offsetsR[i]] = byteR[i];
                }

                if (offsetsG.Length > i)
                {
                    bytes[offsetsG[i]] = byteG[i];
                }

                if (offsetsB.Length > i)
                {
                    bytes[offsetsB[i]] = byteB[i];
                }

                if (offsetsA.Length > i)
                {
                    bytes[offsetsA[i]] = byteA[i];
                }
            }
        }
        public void ConvertRGBFloatToBytes(byte[] bytes, float R, float G, float B, int[] offsetsR, int[] offsetsG, int[] offsetsB)
        {
            byte[] byteR = BitConverter.GetBytes(R);
            byte[] byteG = BitConverter.GetBytes(G);
            byte[] byteB = BitConverter.GetBytes(B);


            for (int i = 0; i < 4; i++)
            {
                if (offsetsR.Length > i)
                {
                    bytes[offsetsR[i]] = byteR[i];
                }

                if (offsetsG.Length > i)
                {
                    bytes[offsetsG[i]] = byteG[i];
                }

                if (offsetsB.Length > i)
                {
                    bytes[offsetsB[i]] = byteB[i];
                }

            }
        }
        public void ConvertBoolToBytes(byte[] bytes, bool value, int[] offset)
        {
            byte boolByte = (byte)(value ? 1 : 0);

            if (offset.Length > 0)
            {
                bytes[offset[0]] = boolByte;
            }
        }
        public void ConvertIntToMoreBytes(byte[] bytes, short value, uint[,] offset, uint length)
        {
            byte[] Byte = BitConverter.GetBytes(value); 

            for (int j = 0; j < length; j++)
            {
                for (int i = 0; i < 2; i++) 
                {
                    if (offset.GetLength(0) > j && offset.GetLength(1) > i)
                    {
                        bytes[offset[j, i]] = Byte[i];
                    }
                }
            }
        }
    }
}
