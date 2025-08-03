using Mono.Data.Sqlite;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace XrCode
{
    public class ConfigUtil
    {
        public static object GetArrayData111(SqliteDataReader reader, int index, int len)
        {
            //�޸Ķ�ȡ���int[]����
            byte[] vs = (byte[])reader[index];
            return ConvertByteToInt(vs);
        }

        /// <summary>
        /// byte[]תint[]
        /// </summary>
        /// <param name="vs"></param>
        /// <returns></returns>
        private static int[] ConvertByteToInt(byte[] vs)

        {
            int[] ints = new int[vs.Length / 4];
            Buffer.BlockCopy(vs, 0, ints, 0, vs.Length - 1);
            int[] endInt = new int[ints[0]];
            for (int i = 1; i < ints.Length; i++)
            {
                endInt[i - 1] = ints[i];
            }
            return endInt;
        }

        /// <summary>
        /// byte[]תint[]
        /// </summary>
        /// <param name="vs"></param>
        /// <returns></returns>
        public static object GetArrayData11(SqliteDataReader reader, int index, int len)
        {
            var str = reader[index].ToString();
            //var content = reader.GetBytes(0, len, buffer, 0, len);
            var content = reader.GetValue(index);
            byte[] aa = content as byte[];
            //ͷ��Ϊint���ͣ���¼���鳤�ȣ��̶�����ռ4�ֽ�
            int intLen = 4;
            byte[] countArray = new byte[4];
            Buffer.BlockCopy(aa, 0, countArray, 0, intLen);
            int count = BitConverter.ToInt32(countArray, 0);

            switch (len)
            {
                case 11:    //int
                    int[] objArray = new int[count];
                    for (int i = 0; i < objArray.Length; i++)
                    {
                        byte[] tempArray = new byte[intLen];
                        //ʵ���ϵ�0����λ���ȵ�ʱ�ڴ洢��������ĳ��ȣ���������ݲ���Ҫ������������
                        Buffer.BlockCopy(aa, (i + 1) * intLen, tempArray, 0, intLen);
                        int v = BitConverter.ToInt32(tempArray, 0);
                        objArray[i] = v;
                    }
                    return objArray;

                case 12:    //string
                    string[] strArray = new string[count];
                    //������ʼ������ͷ���ֽ�
                    int currentIndex = intLen;

                    break;

            }
            return null;
        }

        /// <summary>
        /// byte[]תint[]
        /// </summary>
        /// <param name="vs"></param>
        /// <returns></returns>
        public static object GetArrayData(SqliteDataReader reader, int index, int len)
        {
            //var content = reader.GetBytes(0, len, buffer, 0, len);
            byte[] content = (byte[])reader.GetValue(index);
            //ͷ��Ϊint���ͣ���¼���鳤�ȣ��̶�����ռ4�ֽ�
            int intLen = 4;
            byte[] countArray = new byte[4];
            Buffer.BlockCopy(content, 0, countArray, 0, intLen);
            int count = BitConverter.ToInt32(countArray, 0);

            switch (len)
            {
                case 11:    //int
                    int[] intArray = new int[count];
                    Buffer.BlockCopy(content, intLen, intArray, 0, content.Length - intLen);
                    return intArray;
                case 12:    //string
                    string[] strArray = new string[count];
                    return strArray;
                case 13:    //float
                    float[] flArray = new float[count];
                    Buffer.BlockCopy(content, intLen, flArray, 0, content.Length - intLen);
                    return flArray;
                case 14:    //bool
                    bool[] boolArray = new bool[count];
                    Buffer.BlockCopy(content, intLen, boolArray, 0, content.Length - intLen);
                    return boolArray;
                case 15:    //short
                    short[] shortArray = new short[count];
                    Buffer.BlockCopy(content, intLen, shortArray, 0, content.Length - intLen);
                    return shortArray;
            }
            return null;
        }
    }
}