namespace ModbusDataAcquisition
{
    partial class ModbusDataAcquisition
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.communicationSetting = new System.Windows.Forms.GroupBox();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.tbAddr = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.cbStop = new System.Windows.Forms.ComboBox();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.cbSerial = new System.Windows.Forms.ComboBox();
            this.btnOpenCloseSCom = new System.Windows.Forms.Button();
            this.voltageChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.communicationSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.voltageChart)).BeginInit();
            this.SuspendLayout();
            // 
            // communicationSetting
            // 
            this.communicationSetting.Controls.Add(this.tbTime);
            this.communicationSetting.Controls.Add(this.btnSave);
            this.communicationSetting.Controls.Add(this.label7);
            this.communicationSetting.Controls.Add(this.tbAddr);
            this.communicationSetting.Controls.Add(this.label6);
            this.communicationSetting.Controls.Add(this.label5);
            this.communicationSetting.Controls.Add(this.label4);
            this.communicationSetting.Controls.Add(this.label3);
            this.communicationSetting.Controls.Add(this.label2);
            this.communicationSetting.Controls.Add(this.label1);
            this.communicationSetting.Controls.Add(this.cbParity);
            this.communicationSetting.Controls.Add(this.cbDataBits);
            this.communicationSetting.Controls.Add(this.cbStop);
            this.communicationSetting.Controls.Add(this.cbBaudRate);
            this.communicationSetting.Controls.Add(this.cbSerial);
            this.communicationSetting.Controls.Add(this.btnOpenCloseSCom);
            this.communicationSetting.Location = new System.Drawing.Point(13, 4);
            this.communicationSetting.Name = "communicationSetting";
            this.communicationSetting.Size = new System.Drawing.Size(180, 359);
            this.communicationSetting.TabIndex = 0;
            this.communicationSetting.TabStop = false;
            this.communicationSetting.Text = "通信设置";
            // 
            // tbTime
            // 
            this.tbTime.Location = new System.Drawing.Point(75, 275);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(81, 21);
            this.tbTime.TabIndex = 14;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(90, 318);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "保存参数";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 278);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 24);
            this.label7.TabIndex = 13;
            this.label7.Text = "数据间隔\r\n（ms）";
            // 
            // tbAddr
            // 
            this.tbAddr.Location = new System.Drawing.Point(74, 233);
            this.tbAddr.Name = "tbAddr";
            this.tbAddr.Size = new System.Drawing.Size(81, 21);
            this.tbAddr.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "从站地址";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 199);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "校验位";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "数据位";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "停止位";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "波特率";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "通信口";
            // 
            // cbParity
            // 
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Items.AddRange(new object[] {
            "无",
            "奇校验",
            "偶校验"});
            this.cbParity.Location = new System.Drawing.Point(74, 191);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(81, 20);
            this.cbParity.TabIndex = 5;
            // 
            // cbDataBits
            // 
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cbDataBits.Location = new System.Drawing.Point(74, 150);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(81, 20);
            this.cbDataBits.TabIndex = 4;
            // 
            // cbStop
            // 
            this.cbStop.FormattingEnabled = true;
            this.cbStop.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.cbStop.Location = new System.Drawing.Point(74, 107);
            this.cbStop.Name = "cbStop";
            this.cbStop.Size = new System.Drawing.Size(81, 20);
            this.cbStop.TabIndex = 3;
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200",
            "38400",
            "115200"});
            this.cbBaudRate.Location = new System.Drawing.Point(74, 69);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(81, 20);
            this.cbBaudRate.TabIndex = 2;
            // 
            // cbSerial
            // 
            this.cbSerial.FormattingEnabled = true;
            this.cbSerial.Location = new System.Drawing.Point(74, 29);
            this.cbSerial.Name = "cbSerial";
            this.cbSerial.Size = new System.Drawing.Size(81, 20);
            this.cbSerial.TabIndex = 1;
            // 
            // btnOpenCloseSCom
            // 
            this.btnOpenCloseSCom.Location = new System.Drawing.Point(9, 318);
            this.btnOpenCloseSCom.Name = "btnOpenCloseSCom";
            this.btnOpenCloseSCom.Size = new System.Drawing.Size(75, 23);
            this.btnOpenCloseSCom.TabIndex = 0;
            this.btnOpenCloseSCom.Text = "打开通信";
            this.btnOpenCloseSCom.UseVisualStyleBackColor = true;
            this.btnOpenCloseSCom.Click += new System.EventHandler(this.btnOpenCloseSCom_Click);
            // 
            // voltageChart
            // 
            chartArea1.Name = "ChartArea1";
            this.voltageChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.voltageChart.Legends.Add(legend1);
            this.voltageChart.Location = new System.Drawing.Point(211, 12);
            this.voltageChart.Name = "voltageChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "CH1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "CH2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "CH3";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "CH4";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Legend = "Legend1";
            series5.Name = "CH5";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Legend = "Legend1";
            series6.Name = "CH6";
            this.voltageChart.Series.Add(series1);
            this.voltageChart.Series.Add(series2);
            this.voltageChart.Series.Add(series3);
            this.voltageChart.Series.Add(series4);
            this.voltageChart.Series.Add(series5);
            this.voltageChart.Series.Add(series6);
            this.voltageChart.Size = new System.Drawing.Size(1217, 700);
            this.voltageChart.TabIndex = 1;
            this.voltageChart.Text = "电压监测值";
            // 
            // ModbusDataAcquisition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1440, 724);
            this.Controls.Add(this.voltageChart);
            this.Controls.Add(this.communicationSetting);
            this.Name = "ModbusDataAcquisition";
            this.Text = "数据采集器";
            this.Load += new System.EventHandler(this.ModbusDataAcquisition_Load);
            this.communicationSetting.ResumeLayout(false);
            this.communicationSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.voltageChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox communicationSetting;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbAddr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.ComboBox cbDataBits;
        private System.Windows.Forms.ComboBox cbStop;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.ComboBox cbSerial;
        private System.Windows.Forms.Button btnOpenCloseSCom;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataVisualization.Charting.Chart voltageChart;
    }
}

