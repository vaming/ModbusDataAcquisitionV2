using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using INIFILE;
using System.Runtime.InteropServices;
//using Modbus.Device;    //for modbus master
using System.Timers;
//using ModbusRTU;
using Modbus;
using Modbus.Device;
using System.Windows.Forms.DataVisualization.Charting;

namespace ModbusDataAcquisition
{

    public partial class ModbusDataAcquisition : Form
    {
        SerialPort serialPort = new SerialPort();
        ModbusSerialMaster master;
        private float[] voltageArrayCH1 = new float[120];
        private float[] voltageArrayCH2 = new float[120];
        private float[] voltageArrayCH3 = new float[120];
        private float[] voltageArrayCH4 = new float[120];
        private float[] voltageArrayCH5 = new float[120];
        private float[] voltageArrayCH6 = new float[120];


        System.Timers.Timer taskTimer = new System.Timers.Timer(1000);//定义一个1000ms的定时器


        public ModbusDataAcquisition()
        {
            InitializeComponent();
        }

        private void ModbusDataAcquisition_Load(object sender, EventArgs e)
        {
            

            #region 串口的参数载入            
            INIFILE.Profile.LoadProfile();//加载所有
            // 预置波特率
            switch (Profile.G_BAUDRATE)
            {
                case "4800":
                    cbBaudRate.SelectedIndex = 0;
                    break;
                case "9600":
                    cbBaudRate.SelectedIndex = 1;
                    break;
                case "19200":
                    cbBaudRate.SelectedIndex = 2;
                    break;
                case "38400":
                    cbBaudRate.SelectedIndex = 3;
                    break;
                case "115200":
                    cbBaudRate.SelectedIndex = 4;
                    break;
                default:
                    {
                        MessageBox.Show("波特率预置参数错误。");
                        return;
                    }
            }

            //预置数据位
            switch (Profile.G_DATABITS)
            {
                case "5":
                    cbDataBits.SelectedIndex = 0;
                    break;
                case "6":
                    cbDataBits.SelectedIndex = 1;
                    break;
                case "7":
                    cbDataBits.SelectedIndex = 2;
                    break;
                case "8":
                    cbDataBits.SelectedIndex = 3;
                    break;
                default:
                    {
                        MessageBox.Show("数据位预置参数错误。");
                        return;
                    }

            }
            //预置停止位
            switch (Profile.G_STOP)
            {
                case "1":
                    cbStop.SelectedIndex = 0;
                    break;
                case "1.5":
                    cbStop.SelectedIndex = 1;
                    break;
                case "2":
                    cbStop.SelectedIndex = 2;
                    break;
                default:
                    {
                        MessageBox.Show("停止位预置参数错误。");
                        return;
                    }
            }

            //预置校验位
            switch (Profile.G_PARITY)
            {
                case "NONE":
                    cbParity.SelectedIndex = 0;
                    break;
                case "ODD":
                    cbParity.SelectedIndex = 1;
                    break;
                case "EVEN":
                    cbParity.SelectedIndex = 2;
                    break;
                default:
                    {
                        MessageBox.Show("校验位预置参数错误。");
                        return;
                    }
            }

            //检查是否含有串口
            string[] str = SerialPort.GetPortNames();
            if (str == null)
            {
                MessageBox.Show("本机没有串口！", "Error");
                return;
            }

            //添加串口项目
            foreach (string s in System.IO.Ports.SerialPort.GetPortNames())
            {//获取有多少个COM口
                //System.Diagnostics.Debug.WriteLine(s);
                cbSerial.Items.Add(s);
            }

            //串口设置默认选择项
            cbSerial.SelectedIndex = 0;         //note：获得COM9口，但别忘修改

            serialPort.BaudRate = 9600;

            Control.CheckForIllegalCrossThreadCalls = false;    //这个类中我们不检查跨线程的调用是否合法(因为.net 2.0以后加强了安全机制,，不允许在winform中直接跨线程访问控件的属性)
            //serialPort.DataReceived += new SerialDataReceivedEventHandler(scom_DataReceived);
            //serialPort.ReceivedBytesThreshold = 128;//事件发生前内部输入缓冲区的字节数，每当缓冲区的字节达到此设定的值，就会触发对象的数据接收事件
            //准备就绪              
            serialPort.DtrEnable = true;
            serialPort.RtsEnable = true;
            //设置数据读取超时为0.5秒
            serialPort.ReadTimeout = 300;
            serialPort.WriteTimeout = 1000;
            serialPort.ReadBufferSize = 1024 * 1024 * 30;//设置串口缓存大小为30M
            serialPort.WriteBufferSize = 1024 * 1024 * 30;
            serialPort.Close();

            tbAddr.Text = "1";//预置地址
            tbTime.Text = "1000";//预置时间间隔


            //设置波形控件
            voltageChart.ChartAreas[0].AxisY.Maximum = 6;
            voltageChart.ChartAreas[0].AxisY.Minimum = -6;
            voltageChart.ChartAreas[0].AxisY.Interval = 1;
            voltageChart.ChartAreas[0].AxisX.Maximum = 120;
            voltageChart.ChartAreas[0].AxisX.Minimum = 0;
            voltageChart.ChartAreas[0].AxisX.Interval = 6;
            voltageChart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            voltageChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            voltageChart.Series["CH1"].Points.AddXY(0, 0);

            taskTimer.Elapsed += new System.Timers.ElapsedEventHandler(timerReadSCom);//定义定时执行的任务
            taskTimer.AutoReset = true;//不断执行
            taskTimer.Enabled = false;//关定时

            #endregion

        }

        #region 保存串口参数
        private void btnSave_Click(object sender, EventArgs e)
        {
            //设置各“串口设置”
            string strBaudRate = cbBaudRate.Text;
            string strDateBits = cbDataBits.Text;
            string strStopBits = cbStop.Text;
            Int32 iBaudRate = Convert.ToInt32(strBaudRate);
            Int32 iDateBits = Convert.ToInt32(strDateBits);

            Profile.G_BAUDRATE = iBaudRate + "";       //波特率
            Profile.G_DATABITS = iDateBits + "";       //数据位
            switch (cbStop.Text)            //停止位
            {
                case "1":
                    Profile.G_STOP = "1";
                    break;
                case "1.5":
                    Profile.G_STOP = "1.5";
                    break;
                case "2":
                    Profile.G_STOP = "2";
                    break;
                default:
                    MessageBox.Show("Error：参数不正确!", "Error");
                    break;
            }
            switch (cbParity.Text)             //校验位
            {
                case "无":
                    Profile.G_PARITY = "NONE";
                    break;
                case "奇校验":
                    Profile.G_PARITY = "ODD";
                    break;
                case "偶校验":
                    Profile.G_PARITY = "EVEN";
                    break;
                default:
                    MessageBox.Show("Error：参数不正确!", "Error");
                    break;
            }


            Profile.SaveProfile();
            Console.WriteLine(DateTime.Now.ToString() + " =>Save profile sucessfully!");
        }
        #endregion

        #region 串口的打开与关闭
        private void btnOpenCloseSCom_Click(object sender, EventArgs e)
        {
            
            if (!serialPort.IsOpen)
            {
                try
                {
                    //设置串口号
                    string serialName = cbSerial.SelectedItem.ToString();
                    serialPort.PortName = serialName;

                    //设置各“串口设置”
                    string strBaudRate = cbBaudRate.Text;
                    string strDateBits = cbDataBits.Text;
                    string strStopBits = cbStop.Text;
                    Int32 iBaudRate = Convert.ToInt32(strBaudRate);
                    Int32 iDateBits = Convert.ToInt32(strDateBits);

                    serialPort.BaudRate = iBaudRate;       //波特率
                    serialPort.DataBits = iDateBits;       //数据位
                    switch (cbStop.Text)            //停止位
                    {
                        case "1":
                            serialPort.StopBits = StopBits.One;
                            break;
                        case "1.5":
                            serialPort.StopBits = StopBits.OnePointFive;
                            break;
                        case "2":
                            serialPort.StopBits = StopBits.Two;
                            break;
                        default:
                            MessageBox.Show("Error：参数不正确!", "Error");
                            break;
                    }
                    switch (cbParity.Text)             //校验位
                    {
                        case "无":
                            serialPort.Parity = Parity.None;
                            break;
                        case "奇校验":
                            serialPort.Parity = Parity.Odd;
                            break;
                        case "偶校验":
                            serialPort.Parity = Parity.Even;
                            break;
                        default:
                            MessageBox.Show("Error：参数不正确!", "Error");
                            break;
                    }

                    if (serialPort.IsOpen == true)//如果打开状态，则先关闭一下
                    {
                        serialPort.Close();
                    }
                    //状态栏设置
                    //tsSpNum.Text = "串口号：" + serialPort.PortName + "|";
                    //tsBaudRate.Text = "波特率：" + serialPort.BaudRate + "|";
                    //tsDataBits.Text = "数据位：" + serialPort.DataBits + "|";
                    //tsStopBits.Text = "停止位：" + serialPort.StopBits + "|";
                    //tsParity.Text = "校验位：" + serialPort.Parity + "|";

                    //设置必要控件不可用
                    cbSerial.Enabled = false;
                    cbBaudRate.Enabled = false;
                    cbDataBits.Enabled = false;
                    cbStop.Enabled = false;
                    cbParity.Enabled = false;


                    serialPort.Open();     //打开串口

                    // create modbus-rtu master
                    master = ModbusSerialMaster.CreateRtu(serialPort);
                    master.Transport.Retries = 0;//不重试
                    master.Transport.ReadTimeout = 1000;//超时时间，单位ms

                    //master = ModbusSerialMaster.CreateRtu(serialPort);//创建RTU通信
                    //master.Transport.Retries = 0;   //don't have to do retries
                    //master.Transport.ReadTimeout = 30; //milliseconds
                    Console.WriteLine(DateTime.Now.ToString() + " =>Open " + serialPort.PortName + " sucessfully!");


                    ///代码测试区

                    

                    //创建定时任务
                    taskTimer.Interval = Convert.ToInt16(tbTime.Text);//重新设置间隔时间
                    taskTimer.Enabled = true;//启用定时


                    btnOpenCloseSCom.Text = "关闭串口";
                    //打开定时器
                    //  taskTimer.Enabled = true;
                    //tsTips.Text = "串口打开成功";


                    

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Error");
                    //tmSend.Enabled = false;
                    cbSerial.Enabled = true;
                    cbBaudRate.Enabled = true;
                    cbDataBits.Enabled = true;
                    cbStop.Enabled = true;
                    cbParity.Enabled = true;
                    taskTimer.Enabled = false;//关定时
                    return;
                }
            }
            else
            {

                //恢复控件功能
                //设置必要控件不可用
                cbSerial.Enabled = true;
                cbBaudRate.Enabled = true;
                cbDataBits.Enabled = true;
                cbStop.Enabled = true;
                cbParity.Enabled = true;
                taskTimer.Enabled = false;//先不启用定时

                serialPort.Close();                    //关闭串口
                btnOpenCloseSCom.Text = "打开串口";
                // tmSend.Enabled = false;         //关闭计时器
                // taskTimer.Enabled = false;//关闭计时器
                //tsTips.Text = "串口已关闭";
                Console.WriteLine(DateTime.Now.ToString() + " =>Disconnect " + serialPort.PortName);
            }
        }
        #endregion


        #region RTU数据的读取
        void timerReadSCom(object sender, ElapsedEventArgs e)
        {
            
            try
            {
                byte slaveID = Convert.ToByte(tbAddr.Text);
                ushort startAddress = 32;//对应0x20H
                ushort numofPoints = 12;
                ushort[] valueTemp = master.ReadHoldingRegisters(slaveID, startAddress, numofPoints);
                float[] voltageValue = dataProcess(valueTemp);           
                string hexstr1 = string.Join(",", voltageValue);                
                Console.WriteLine(DateTime.Now.ToString() + " =>" + hexstr1);

                voltageArrayCH1[voltageArrayCH1.Length - 1] = voltageValue[0];
                voltageArrayCH2[voltageArrayCH2.Length - 1] = voltageValue[1];
                voltageArrayCH3[voltageArrayCH3.Length - 1] = voltageValue[2];
                voltageArrayCH4[voltageArrayCH4.Length - 1] = voltageValue[3];
                voltageArrayCH5[voltageArrayCH5.Length - 1] = voltageValue[4];
                voltageArrayCH6[voltageArrayCH6.Length - 1] = voltageValue[5];

                Array.Copy(voltageArrayCH1, 1, voltageArrayCH1, 0, voltageArrayCH1.Length - 1);
                Array.Copy(voltageArrayCH2, 1, voltageArrayCH2, 0, voltageArrayCH2.Length - 1);
                Array.Copy(voltageArrayCH3, 1, voltageArrayCH3, 0, voltageArrayCH3.Length - 1);
                Array.Copy(voltageArrayCH4, 1, voltageArrayCH4, 0, voltageArrayCH4.Length - 1);
                Array.Copy(voltageArrayCH5, 1, voltageArrayCH5, 0, voltageArrayCH5.Length - 1);
                Array.Copy(voltageArrayCH6, 1, voltageArrayCH6, 0, voltageArrayCH6.Length - 1);


                if (voltageChart.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate { UpdateVoltageChart(); });
                }
                else
                {
                    //......
                }


            }
            catch (Exception exception)
            {
                //Connection exception
                //No response from server.
                //The server maybe close the com port, or response timeout.
                if (exception.Source.Equals("System"))
                {
                    Console.WriteLine(DateTime.Now.ToString() + " " + exception.Message);
                }
                
               
                }
            }
        #endregion

        private void UpdateVoltageChart()
        {
            voltageChart.Series["CH1"].Points.Clear();
            voltageChart.Series["CH2"].Points.Clear();
            voltageChart.Series["CH3"].Points.Clear();
            voltageChart.Series["CH4"].Points.Clear();
            voltageChart.Series["CH5"].Points.Clear();
            voltageChart.Series["CH6"].Points.Clear();



            for (int i = 0; i < voltageArrayCH1.Length - 1; ++i)
            {
                voltageChart.Series["CH1"].Points.AddY(voltageArrayCH1[i]);
                voltageChart.Series["CH2"].Points.AddY(voltageArrayCH2[i]);
                voltageChart.Series["CH3"].Points.AddY(voltageArrayCH3[i]);
                voltageChart.Series["CH4"].Points.AddY(voltageArrayCH4[i]);
                voltageChart.Series["CH5"].Points.AddY(voltageArrayCH5[i]);
                voltageChart.Series["CH6"].Points.AddY(voltageArrayCH6[i]);
            }
        }


        #region 一些数据转换的函数
        /// <summary>
        /// 将二进制值转ASCII格式十六进制字符串
        /// </summary>
        /// <paramname="data">二进制值</param>
        /// <paramname="length">定长度的二进制</param>
        /// <returns>ASCII格式十六进制字符串</returns>
        public static float[] dataProcess(ushort[] data)
        {

            byte[] byteArray1 = BitConverter.GetBytes(data[0]);
            byte[] byteArray2 = BitConverter.GetBytes(data[1]);
            byte[] byteArray3 = BitConverter.GetBytes(data[2]);
            byte[] byteArray4 = BitConverter.GetBytes(data[3]);
            byte[] byteArray5 = BitConverter.GetBytes(data[4]);
            byte[] byteArray6 = BitConverter.GetBytes(data[5]);
            byte[] byteArray7 = BitConverter.GetBytes(data[6]);
            byte[] byteArray8 = BitConverter.GetBytes(data[7]);
            byte[] byteArray9 = BitConverter.GetBytes(data[8]);
            byte[] byteArray10 = BitConverter.GetBytes(data[9]);
            byte[] byteArray11 = BitConverter.GetBytes(data[10]);
            byte[] byteArray12 = BitConverter.GetBytes(data[11]);


            byte[] intBufferCH1 = new byte[4];
            byte[] intBufferCH2 = new byte[4];
            byte[] intBufferCH3 = new byte[4];
            byte[] intBufferCH4 = new byte[4];
            byte[] intBufferCH5 = new byte[4];
            byte[] intBufferCH6 = new byte[4];

            intBufferCH1[0] = byteArray1[1];
            intBufferCH1[1] = byteArray1[0];
            intBufferCH1[2] = byteArray2[1];
            intBufferCH1[3] = byteArray2[0];
            intBufferCH2[0] = byteArray3[1];
            intBufferCH2[1] = byteArray3[0];
            intBufferCH2[2] = byteArray4[1];
            intBufferCH2[3] = byteArray4[0];
            intBufferCH3[0] = byteArray5[1];
            intBufferCH3[1] = byteArray5[0];
            intBufferCH3[2] = byteArray6[1];
            intBufferCH3[3] = byteArray6[0];
            intBufferCH4[0] = byteArray7[1];
            intBufferCH4[1] = byteArray7[0];
            intBufferCH4[2] = byteArray8[1];
            intBufferCH4[3] = byteArray8[0];
            intBufferCH5[0] = byteArray9[1];
            intBufferCH5[1] = byteArray9[0];
            intBufferCH5[2] = byteArray10[1];
            intBufferCH5[3] = byteArray10[0];
            intBufferCH6[0] = byteArray11[1];
            intBufferCH6[1] = byteArray11[0];
            intBufferCH6[2] = byteArray12[1];
            intBufferCH6[3] = byteArray12[0];

            float CH1 = ByteToFloat(intBufferCH1);
            float CH2 = ByteToFloat(intBufferCH2);
            float CH3 = ByteToFloat(intBufferCH3);
            float CH4 = ByteToFloat(intBufferCH4);
            float CH5 = ByteToFloat(intBufferCH5);
            float CH6 = ByteToFloat(intBufferCH6);
            float[] result = { CH1, CH2, CH3, CH4, CH5, CH6 };            
            return result;
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
                //将byte数组的位置调换
                intBuffer[0] = bResponse[3];
                intBuffer[1] = bResponse[2];
                intBuffer[2] = bResponse[1];
                intBuffer[3] = bResponse[0];
                return BitConverter.ToSingle(intBuffer, 0);
                
            }
        }
#endregion 数据转换结束

    }

    }
