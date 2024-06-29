using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    internal class CheckSum
    {
        public static byte MyCRC8(string data)
        {
            byte crc = 0x00;

            byte[] bytes = data.Select(c => Convert.ToByte(c)).AsEnumerable().ToArray();

            return crc;
        }

        public static byte CRC8(string data)
        {
            byte crc = 0x00;

            byte[] bytes = data.Select(c => Convert.ToByte(c)).AsEnumerable().ToArray();

            for (int i = 0; i < bytes.Length; i++)
            {
                crc ^= (byte)data[i];

                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x80) > 0)
                        crc = (byte)((crc << 1) ^ 0x07);
                    else
                        crc <<= 1;
                }
            }

            return crc;
        }

        public static bool CheckCRC8(string data, byte crc) => CRC8(data).Equals(crc);
    }
}
