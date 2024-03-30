using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZastitaProjekat
{
    public class SHA1Al
    {

        public static string CalculateSHA1(string input)
        {
            
            byte[] data = Encoding.UTF8.GetBytes(input);

            uint h0 = 0x67452301;
            uint h1 = 0xEFCDAB89;
            uint h2 = 0x98BADCFE;
            uint h3 = 0x10325476;
            uint h4 = 0xC3D2E1F0;

            
            byte[] paddedData = PadData(data);

            
            for (int i = 0; i < paddedData.Length; i += 64)
            {
                uint[] words = new uint[80];

                
                for (int j = 0; j < 16; j++)
                {
                    words[j] = BitConverter.ToUInt32(paddedData, i + j * 4);
                }

              
                for (int j = 16; j < 80; j++)
                {
                    words[j] = LeftRotate(words[j - 3] ^ words[j - 8] ^ words[j - 14] ^ words[j - 16], 1);
                }

                
                uint a = h0;
                uint b = h1;
                uint c = h2;
                uint d = h3;
                uint e = h4;

              
                for (int j = 0; j < 80; j++)
                {
                    uint f, k;

                    if (j < 20)
                    {
                        f = (b & c) | ((~b) & d);
                        k = 0x5A827999;
                    }
                    else if (j < 40)
                    {
                        f = b ^ c ^ d;
                        k = 0x6ED9EBA1;
                    }
                    else if (j < 60)
                    {
                        f = (b & c) | (b & d) | (c & d);
                        k = 0x8F1BBCDC;
                    }
                    else
                    {
                        f = b ^ c ^ d;
                        k = 0xCA62C1D6;
                    }

                    uint temp = LeftRotate(a, 5) + f + e + k + words[j];
                    e = d;
                    d = c;
                    c = LeftRotate(b, 30);
                    b = a;
                    a = temp;
                }

                
                h0 += a;
                h1 += b;
                h2 += c;
                h3 += d;
                h4 += e;
            }

            
            byte[] hashBytes = new byte[] { (byte)(h0 >> 24), (byte)(h0 >> 16), (byte)(h0 >> 8), (byte)h0,
                                        (byte)(h1 >> 24), (byte)(h1 >> 16), (byte)(h1 >> 8), (byte)h1,
                                        (byte)(h2 >> 24), (byte)(h2 >> 16), (byte)(h2 >> 8), (byte)h2,
                                        (byte)(h3 >> 24), (byte)(h3 >> 16), (byte)(h3 >> 8), (byte)h3,
                                        (byte)(h4 >> 24), (byte)(h4 >> 16), (byte)(h4 >> 8), (byte)h4 };

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        private static byte[] PadData(byte[] data)
        {
           
            int originalLength = data.Length;
            int paddedLength = (originalLength + 8 + 63) / 64 * 64;
            byte[] paddedData = new byte[paddedLength];
            Array.Copy(data, paddedData, originalLength);
            paddedData[originalLength] = 0x80;

            
            long bitLength = (long)originalLength * 8;
            Array.Copy(BitConverter.GetBytes(bitLength), 0, paddedData, paddedLength - 8, 8);

            return paddedData;
        }

        private static uint LeftRotate(uint value, int offset)
        {
            return (value << offset) | (value >> (32 - offset));
        }
    }
}

