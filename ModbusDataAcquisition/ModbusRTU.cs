using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusRTU
{
    class ModbusRTU
    {
        //字符串转16进制字节
        public static byte[] stringToHex(string inputString)
        {
            inputString = inputString.Trim();//移除空白
            inputString = inputString.Replace(',', ' ');    //去掉英文逗号
            inputString = inputString.Replace('，', ' '); //去掉中文逗号
            inputString = inputString.Replace("0x", "");   //去掉0x
            inputString = inputString.Replace("0X", "");   //去掉0X
            string[] strArray = inputString.Split(' ');

            int byteBufferLength = strArray.Length;
            for (int i = 0; i < strArray.Length; i++)//获取非空字符数组的数量
            {
                if (strArray[i] == "")
                {
                    byteBufferLength--;
                }
            }
            // int temp = 0;
            byte[] byteBuffer = new byte[byteBufferLength];
            int j = 0;
            for (int i = 0; i < strArray.Length; i++)        //对获取的字符做相加运算
            {

                //   Byte[] bytesOfStr = Encoding.Default.GetBytes(strArray[i]);

                int decNum = 0;
                if (strArray[i] == "")
                {
                    //ii--;     //加上此句是错误的，下面的continue以延缓了一个ii，不与i同步
                    continue;
                }
                else
                {
                    decNum = Convert.ToInt32(strArray[i], 16); //atrArray[i] == 12时，temp == 18 
                }

                try    //防止输错，使其只能输入一个字节的字符
                {
                    byteBuffer[j] = Convert.ToByte(decNum);

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("字节越界，请逐个字节输入！", "Error");
                    //tmSend.Enabled = false;
                    // return;
                }
                j++;
            }
            return byteBuffer;
        }

        //读输入寄存器
        public static void ReadInputRegisters(uint address, uint function,uint startAddress,uint numofPoint)
        {

        }


        public static void ConvertHexToSingle()
        {
            //ffloat转换成hex

            float num1 = 999999f;

            // Convert to IEEE 754

            uint num2 = BitConverter.ToUInt32(BitConverter.GetBytes(num1), 0);

            //hex转换成float

            // Hex representation

            byte[] byteArray = BitConverter.GetBytes(num1);

            Array.Reverse(byteArray);

            // Convert back to float

            float num3 = BitConverter.ToSingle(BitConverter.GetBytes(num2), 0);
        }


        /// <summary>
        /// 将二进制值转ASCII格式十六进制字符串
        /// </summary>
        /// <paramname="data">二进制值</param>
        /// <paramname="length">定长度的二进制</param>
        /// <returns>ASCII格式十六进制字符串</returns>
        public static string toHexString(int data, int length)
        {
            string result = "";
            if (data > 0)
                result = Convert.ToString(data, 16).ToUpper();
            if (result.Length < length)
            {
                // 位数不够补0
                StringBuilder msg = new StringBuilder(0);
                msg.Length = 0;
                msg.Append(result);
                for (; msg.Length < length; msg.Insert(0, "0")) ;
                result = msg.ToString();
            }
            return result;
        }
        ///<summary>
        /// 将浮点数转ASCII格式十六进制字符串（符合IEEE-754标准（32））
        /// </summary>
        /// <paramname="data">浮点数值</param>
        /// <returns>十六进制字符串</returns>
        public static string FloatToIntString(float data)
        {
            byte[] intBuffer = BitConverter.GetBytes(data);
            StringBuilder stringBuffer = new StringBuilder(0);
            for (int i = 0; i < intBuffer.Length; i++)
            {
                stringBuffer.Insert(0, toHexString(intBuffer[i] & 0xff, 2));
            }
            return stringBuffer.ToString();
        }

        /// <summary>
        /// 将ASCII格式十六进制字符串转浮点数（符合IEEE-754标准（32））
        /// </summary>
        /// <param name="data">16进制字符串</param>
        /// <returns></returns>
        public static float StringToFloat(String data)
        {
            if (data.Length < 8 || data.Length > 8)
            {
                //throw new NotEnoughDataInBufferException(data.length(), 8);
                return 0;
            }
            else
            {
                byte[] intBuffer = new byte[4];
                // 将16进制串按字节逆序化（一个字节2个ASCII码）
                for (int i = 0; i < 4; i++)
                {
                    intBuffer[i] = Convert.ToByte(data.Substring((3 - i) * 2, 2), 16);
                }
                return BitConverter.ToSingle(intBuffer, 0);
            }
        }
        /// <summary>
        /// 将byte数组转为浮点数
        /// </summary>
        /// <param name="bResponse">byte数组</param>
        /// <returns></returns>
        public static float ByteToFloat(byte[] bResponse)
        {
            if (bResponse.Length < 4 || bResponse.Length > 4)
            {
                //throw new NotEnoughDataInBufferException(data.length(), 8);
                return 0;
            }
            else
            {
                byte[] intBuffer = new byte[4];
                //将byte数组的前后两个字节的高低位换过来
                intBuffer[0] = bResponse[3];
                intBuffer[1] = bResponse[2];
                intBuffer[2] = bResponse[1];
                intBuffer[3] = bResponse[0];
                return BitConverter.ToSingle(intBuffer, 0);
            }
        }
       


    }
}
