
using System;
using System.Text;

namespace PM.CloudPlatform.ForkliftManager.Apis.Extensions
{
    /// <summary>
    /// Hex拓展
    /// <remark>
    /// 作者: Powers
    /// 创始时间: 2021-02-25
    /// 描述:
    ///     拓展了一些Hex方法
    /// </remark>
    /// <para>
    /// 新增:
    ///     2021/03/15  Powers  新增了ReadBcd和WriteBcd方法
    /// 提供Bcd编码与string/byte[]之间的转换
    /// </para>
    /// </summary>
    //public static class HexExtensions
    //{
    //    /// <summary>
    //    /// 将byte[]转为hex字符串
    //    /// </summary>
    //    /// <param name="source"></param>
    //    /// <returns></returns>
    //    public static string ToHexString(this byte[] source)
    //    {
    //        return HexUtil.DoHexDump(source, 0, source.Length).ToUpper();
    //    }

    //    /// <summary>
    //    /// 把号码用BCD进行压缩编码。
    //    /// </summary>
    //    /// <param name="Num8BitByte">The num8 bit byte.</param>
    //    /// <returns></returns>
    //    public static byte[] ByteArrayToBcd(this byte[] Num8BitByte)//8位的ascii码
    //    {
    //        byte[] Num4bitByte = new byte[8];

    //        Num4bitByte = BitConverter.GetBytes(0xffffffffffffffff);
    //        for (int i = 0; i < Num8BitByte.Length; i++)
    //        {
    //            byte num = Convert.ToByte(Num8BitByte[i] - 0x30);
    //            if (i % 2 == 0)
    //            {
    //                Num4bitByte[i / 2] = Convert.ToByte((Num4bitByte[i / 2] & 0xF0) | num);
    //            }
    //            else
    //            {
    //                Num4bitByte[i / 2] = Convert.ToByte((Num4bitByte[i / 2] & 0x0F) | (num << 4));
    //            }
    //        }

    //        return Num4bitByte;
    //    }

    //    /// <summary>
    //    /// BCDs to string.
    //    /// </summary>
    //    /// <param name="bcdNum">The BCD num.</param>
    //    /// <returns></returns>
    //    public static string BcdToString(this byte[] bcdNum)
    //    {
    //        string retString = "";
    //        byte[] byteChar = new byte[bcdNum.Length];
    //        byteChar = BitConverter.GetBytes(0xffffffffffffffff);
    //        byte tempHigh = 0, tempLow = 0;
    //        int i = 0;
    //        while (tempHigh != 0x0F && tempLow != 0xF0)
    //        {
    //            tempHigh = Convert.ToByte(bcdNum[i] & 0xF0);//取出高四位；
    //            tempHigh = Convert.ToByte(tempHigh >> 4);
    //            tempLow = Convert.ToByte((bcdNum[i] & 0x0F) << 4);
    //            byteChar[i] = Convert.ToByte(tempLow | tempHigh);
    //            i++;
    //        }
    //        string[] HexString = BitConverter.ToString(byteChar).Trim().Split('-');
    //        foreach (string str in HexString)
    //        {
    //            retString += str.Trim();
    //        }
    //        int LastIndex = retString.IndexOf('F');
    //        return retString = retString.Substring(0, LastIndex);
    //    }

    //    /// <summary>
    //    /// ReadHexStringLittle
    //    /// </summary>
    //    /// <param name="bytes">bytes</param>
    //    /// <param name="offset">offset</param>
    //    /// <param name="data">data</param>
    //    /// <param name="len">len</param>
    //    /// <returns></returns>
    //    public static int WriteHexStringLittle(byte[] bytes, int offset, string data, int len)
    //    {
    //        if (data == null) data = "";
    //        data = data.Replace(" ", "");
    //        int startIndex = 0;
    //        if (data.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
    //        {
    //            startIndex = 2;
    //        }
    //        int length = len;
    //        if (length == -1)
    //        {
    //            length = (data.Length - startIndex) / 2;
    //        }
    //        int noOfZero = length * 2 + startIndex - data.Length;
    //        if (noOfZero > 0)
    //        {
    //            data = data.Insert(startIndex, new string('0', noOfZero));
    //        }
    //        int byteIndex = 0;
    //        while (startIndex < data.Length && byteIndex < length)
    //        {
    //            bytes[offset + byteIndex] = Convert.ToByte(data.Substring(startIndex, 2), 16);
    //            startIndex += 2;
    //            byteIndex++;
    //        }
    //        return length;
    //    }

    //    /// <summary>
    //    /// 16进制字符串转16进制数组
    //    /// </summary>
    //    /// <param name="hexString">hexString</param>
    //    /// <returns></returns>
    //    public static byte[] ToHexBytes(this string hexString)
    //    {
    //        hexString = hexString.Replace(" ", "");
    //        byte[] buf = new byte[hexString.Length / 2];
    //        ReadOnlySpan<char> readOnlySpan = hexString.AsSpan();
    //        for (int i = 0; i < hexString.Length; i++)
    //        {
    //            if (i % 2 == 0)
    //            {
    //                buf[i / 2] = Convert.ToByte(readOnlySpan.Slice(i, 2).ToString(), 16);
    //            }
    //        }
    //        return buf;
    //    }

    //    /// <summary>
    //    /// 将字符串的电话号转为BCD编码(8421)
    //    /// </summary>
    //    /// <param name="value">字符串</param>
    //    /// <param name="len">编码长度</param>
    //    /// <returns>BCD编码</returns>
    //    public static string WriteBcd(this string value, int len = 20)
    //    {
    //        string bcdText = value ?? "";
    //        int startIndex = 0;
    //        int noOfZero = len - bcdText.Length;
    //        if (noOfZero > 0)
    //        {
    //            bcdText = bcdText.Insert(startIndex, new string('0', noOfZero));
    //        }
    //        return bcdText;
    //    }

    //    /// <summary>
    //    /// 读取BCD(8421)
    //    /// </summary>
    //    /// <param name="data">要读取的数据</param>
    //    /// <param name="len">长度</param>
    //    /// <param name="trim">是否去除0</param>
    //    /// <returns></returns>
    //    public static string ReadBcd(this string data, int len = 20, bool trim = true)
    //    {
    //        int count = len / 2;
    //        var readOnlySpan = new ReadOnlySpan<byte>(data.ToHexBytes()).Slice(0);
    //        StringBuilder bcdSb = new StringBuilder(count);
    //        for (int i = 0; i < count; i++)
    //        {
    //            bcdSb.Append(readOnlySpan[i].ToString("X2"));
    //        }
    //        if (trim)
    //        {
    //            return bcdSb.ToString().TrimStart('0');
    //        }
    //        else
    //        {
    //            return bcdSb.ToString();
    //        }
    //    }

    //    /// <summary>
    //    /// 读取BCD
    //    /// </summary>
    //    /// <param name="data">要读取的数据</param>
    //    /// <param name="len">长度</param>
    //    /// <param name="trim">是否去除0</param>
    //    /// <returns></returns>
    //    public static string ReadBcd(this byte[] data, int len = 20, bool trim = true)
    //    {
    //        int count = len / 2;
    //        var readOnlySpan = new ReadOnlySpan<byte>(data).Slice(0);
    //        StringBuilder bcdSb = new StringBuilder(count);
    //        for (int i = 0; i < count; i++)
    //        {
    //            bcdSb.Append(readOnlySpan[i].ToString("X2"));
    //        }
    //        if (trim)
    //        {
    //            return bcdSb.ToString().TrimStart('0');
    //        }
    //        else
    //        {
    //            return bcdSb.ToString();
    //        }
    //    }

    //    /// <summary>
    //    /// ReadHexStringLittle
    //    /// </summary>
    //    /// <param name="read">read</param>
    //    /// <param name="offset">offset</param>
    //    /// <param name="len">len</param>
    //    /// <returns></returns>
    //    public static string ReadHexStringLittle(ReadOnlySpan<byte> read, ref int offset, int len)
    //    {
    //        //ReadOnlySpan<byte> source = read.Slice(offset, len);
    //        string hex = HexUtil.DoHexDump(read, offset, len);
    //        offset += len;
    //        return hex;
    //    }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <param name="format"></param>
    //    /// <returns></returns>
    //    public static string ReadNumber(this byte value, string format = "X2")
    //    {
    //        return value.ToString(format);
    //    }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <param name="format"></param>
    //    /// <returns></returns>
    //    public static string ReadNumber(this int value, string format = "X8")
    //    {
    //        return value.ToString(format);
    //    }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <param name="format"></param>
    //    /// <returns></returns>
    //    public static string ReadNumber(this uint value, string format = "X8")
    //    {
    //        return value.ToString(format);
    //    }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <param name="format"></param>
    //    /// <returns></returns>
    //    public static string ReadNumber(this long value, string format = "X16")
    //    {
    //        return value.ToString(format);
    //    }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <param name="format"></param>
    //    /// <returns></returns>
    //    public static string ReadNumber(this ulong value, string format = "X16")
    //    {
    //        return value.ToString(format);
    //    }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <param name="format"></param>
    //    /// <returns></returns>
    //    public static string ReadNumber(this short value, string format = "X4")
    //    {
    //        return value.ToString(format);
    //    }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <param name="format"></param>
    //    /// <returns></returns>
    //    public static string ReadNumber(this ushort value, string format = "X4")
    //    {
    //        return value.ToString(format);
    //    }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <returns></returns>
    //    public static ReadOnlySpan<char> ReadBinary(this ushort value)
    //    {
    //        return System.Convert.ToString(value, 2).PadLeft(16, '0').AsSpan();
    //    }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <returns></returns>
    //    public static ReadOnlySpan<char> ReadBinary(this short value)
    //    {
    //        return System.Convert.ToString(value, 2).PadLeft(16, '0').AsSpan();
    //    }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <returns></returns>
    //    public static ReadOnlySpan<char> ReadBinary(this uint value)
    //    {
    //        return System.Convert.ToString(value, 2).PadLeft(32, '0').AsSpan();
    //    }

    //    /// <summary>
    //    /// /
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <returns></returns>
    //    public static ReadOnlySpan<char> ReadBinary(this int value)
    //    {
    //        return System.Convert.ToString(value, 2).PadLeft(32, '0').AsSpan();
    //    }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <returns></returns>
    //    public static ReadOnlySpan<char> ReadBinary(this byte value)
    //    {
    //        return System.Convert.ToString(value, 2).PadLeft(8, '0').AsSpan();
    //    }
    //}
}