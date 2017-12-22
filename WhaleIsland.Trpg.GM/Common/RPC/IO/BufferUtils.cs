using System;
using System.Text;

namespace WhaleIsland.Trpg.GM.Common.RPC.IO
{
    /// <summary>
    ///
    /// </summary>
    public static class BufferUtils
    {
        /// <summary>
        /// 将1个2维数据包整合成以个一维数据包
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static public Byte[] MergeBytes(params Byte[][] args)
        {
            Int32 length = 0;
            foreach (byte[] tempbyte in args)
            {
                length += tempbyte != null ? tempbyte.Length : 0;  //计算数据包总长度
            }

            Byte[] bytes = new Byte[length]; //建立新的数据包

            Int32 tempLength = 0;

            foreach (byte[] tempByte in args)
            {
                if (tempByte == null) continue;
                tempByte.CopyTo(bytes, tempLength);
                tempLength += tempByte.Length;  //复制数据包到新数据包
            }

            return bytes;

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Byte[] GetBytes(byte[] data)
        {
            return GetBytes(data, 0, data.Length);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <param name="pos"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        static public Byte[] GetBytes(byte[] data, int pos, int count)
        {
            var buffer = new byte[count];
            Buffer.BlockCopy(data, pos, buffer, 0, count);
            return buffer;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetBytes(Int16 data)
        {
            return BitConverter.GetBytes(data);
        }


        /// <summary>
        /// 将一个 64 位带符号整数值转换成一个BYTE[]4字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetBytes(long data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// 将一个32位整形转换成一个BYTE[]4字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetBytes(Int32 data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// 将一个64位整型转换成以个BYTE[] 8字节
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetBytes(UInt64 data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetBytes(bool data)
        {
            return BitConverter.GetBytes(data);
        }

        /// <summary>
        /// 将一个 1位CHAR转换成1位的BYTE
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetBytes(Char data)
        {
            Byte[] bytes = new Byte[] { (Byte)data };
            return bytes;
        }

        /// <summary>
        /// 将一个BYTE[]数据包添加首位长度
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] AppendHeadBytes(Byte[] data)
        {
            return MergeBytes(
                GetBytes(data.Length),
                data
                );
        }

        /// <summary>
        /// 将一个字符串转换成BYTE[]，BYTE[]的首位是字符串的长度
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetBytes(String data)
        {
            Byte[] bytes = Encoding.UTF8.GetBytes(data);

            return MergeBytes(
                GetBytes(bytes.Length),
                bytes
                );
        }

        /// <summary>
        /// 将一个DATATIME转换成为BYTE[]数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public Byte[] GetBytes(DateTime data)
        {
            return GetBytes(data.ToString());
        }
    }

}
