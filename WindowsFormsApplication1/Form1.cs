using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using WaveConvertNet;

using System.Diagnostics;
using System.Collections;
using System.Globalization;





namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int ret = 0;
        string  file_log="match_seg.log";
        
        // 创建文件，准备写入
        FileStream fs_log = null;        
        StreamWriter wr_log = null;

             

        public Form1()
        {
            InitializeComponent();
        }

        // 输入目录 0216_808 
        private void button1_Click(object sender, EventArgs e)
        {
              
            FolderBrowserDialog fdlg = new FolderBrowserDialog();
            
            // 弹出对话框 选取目录 
            {  
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    // 目录的值  复制给 textBox11.Text 
                    textBox11.Text = fdlg.SelectedPath;
                    
                }
            }
        }

        // 选择输出目录  创建相同目录结构的新目录   存放最终结果
        private void button2_Click_1(object sender, EventArgs e)
        {

            FolderBrowserDialog fdlg = new FolderBrowserDialog();
            {

                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    // 复制给  textBox22 
                    textBox22.Text = fdlg.SelectedPath;

                }
            }
        }

        // 总的开始按钮  开始处理整个目录 
        private void button3_Click(object sender, EventArgs e)
        {

            // 判断  textBox11 和 textBox22 是否已经有值 
            if (Directory.Exists(textBox11.Text) == false)
            {
                MessageBox.Show("请选择输入目录 ");
                textBox11.Focus();
                return;
            }
            if (Directory.Exists(textBox22.Text) == false)
            {
                MessageBox.Show("请选择输出目录 ");
                textBox22.Focus();
                return;
            }

            // 输出 log 文件 
            fs_log = File.Open(file_log, FileMode.Create, FileAccess.Write);
            wr_log = new StreamWriter(fs_log);
            //wr.WriteLine(line);

         
           

            // 输入目录的信息
            textBoxLog.AppendText("目录1：\r\n");
            {
                DirectoryInfo dir = new DirectoryInfo(textBox11.Text);
                //FileInfo[] files = dir.GetFiles("*.wav", SearchOption.AllDirectories);
                
                // 当前目录下 所有子目录（speaker1  speaker2 …… ） 
                DirectoryInfo[] list_dir = dir.GetDirectories();  // //D:\20170222\0216_808\Speaker0109
                foreach (DirectoryInfo di_spk in list_dir)
                {
                    // 每一个 speaker 目录 
                    textBoxLog.AppendText("read dir1:"+di_spk.FullName + "\r\n");

                    // //  0216_808\Speaker0109 
                    DirectoryInfo di_out_spk = new DirectoryInfo(textBox22.Text + "\\" + di_spk.Name);
                    di_out_spk.Create();                   
                    textBoxLog.AppendText("Cread dir1:"+ di_out_spk.FullName + "\r\n");

                    // 为了创建 out 对应的子目录 
                    DirectoryInfo di_main = null; // 记录主wav 手机"下面的
                    FileInfo fi_main = null;  // 主wav 的路径
                    FileInfo fi_seg_main = null; // 主wav对应的seg 
                    DirectoryInfo[] list_dir_2345m = di_spk.GetDirectories();
                    foreach (DirectoryInfo di_2345m in list_dir_2345m) // 2m|3m|4m|5m|手机 
                    {
                        //  0216_808\Speaker0109\2m 
                        DirectoryInfo di_out_2345m = new DirectoryInfo(textBox22.Text + "\\" + di_spk.Name+"\\"+di_2345m.Name);
                        di_out_2345m.Create();
                        textBoxLog.AppendText("\tCread dir2:" + di_out_2345m.FullName + "\r\n");

                        // 2m|3m|4m|5m 中的每个wav 都与 对应 “手机”中的wav对齐
                        FileInfo[] list_wav = di_2345m.GetFiles("*.wav",SearchOption.AllDirectories);
                        foreach(FileInfo fi in list_wav)
                        {
                            // 0216_808\Speaker0109\2m\DUMP.wav 
                            DirectoryInfo di_out_wav = new DirectoryInfo(di_out_2345m.FullName +"\\"+fi.Name);
                            di_out_wav.Create();
                            textBoxLog.AppendText("\t\tCread dir3:" + di_out_wav.FullName + "\r\n");
                        }

                        // 是否是 main wav  
                        if (di_2345m.Name == "手机") // 0216_808\Speaker0109\手机 
                        {
                            di_main = di_2345m;
                        }
 
                    }
                    
                    // 手机下面应该 有且只有一个 wav 
                    FileInfo[] list_wav_main = di_main.GetFiles("*.wav",SearchOption.AllDirectories);
                    if (list_wav_main.Count() !=1 )
                    {
                        MessageBox.Show(di_main.FullName + "目录下有多个wav! ");
                        return;
                    }
                    fi_main = list_wav_main[0]; // 0216_808\Speaker0109\手机\EN-US_U0109_S0.txt\EN-US_U0109_S0.txt.wav
                    textBoxLog.AppendText("main wav is: " + fi_main.FullName + "\r\n");

                    // “手机”下面有且只有一个*.seg
                    FileInfo[] list_seg_main = di_main.GetFiles("*.seg", SearchOption.AllDirectories);
                    if (list_seg_main.Count() != 1)
                    {
                        MessageBox.Show(di_main.FullName + "目录下有多个 seg ! ");
                        return;
                    }
                    fi_seg_main = list_seg_main[0]; // 0216_808\Speaker0109\手机\EN-US_U0109_S0.txt\EN-US_U0109_S0.txt.seg
                    textBoxLog.AppendText("main seg is: " + fi_seg_main.FullName + "\r\n");

                    // 开始对齐 
                    foreach (DirectoryInfo di_2345m in list_dir_2345m) // 2m|3m|4m|5m|手机 
                    {
                        //  0216_808\Speaker0109\2m 
                        DirectoryInfo di_out_2345m = new DirectoryInfo(textBox22.Text + "\\" + di_spk.Name + "\\" + di_2345m.Name);
                        
                        // 2m 中的每个wav 都与 fi_main 对齐 
                        FileInfo[] list_wav = di_2345m.GetFiles("*.wav", SearchOption.AllDirectories);
                        foreach (FileInfo fi in list_wav)
                        {
                            // 0216_808\Speaker0109\2m\DUMP.wav 目录 
                            DirectoryInfo di_out_wav = new DirectoryInfo(di_out_2345m.FullName + "\\" + fi.Name);
                            
                            // 开始对齐 ！！！
                            string file_main = fi_main.FullName;
                            string file_res = fi.FullName;
                            textBoxLog.AppendText("file1:" + file_main + "\tfile2:" + file_res + "\r\n");
                            
                            int ret = 0;
                            double aa = 1.0;
                            double bb = 0.0;
                            ret = CPPDLL.speechMatch_a(file_main, file_res, ref aa, ref bb);
                            if(ret != 0)
                            {
                                textBoxLog.AppendText("speech match error !\r\n");
                                continue;
                            }
                            textBoxLog.AppendText("a=" + aa + "\tb=" + bb + "\r\n");    
                        
                            //  b=10.0 表示 file_main语音的第0秒 对应 file_res的第b=10.0 ，所以将file_res的前面b=10.0秒删除即可
                            //  b=-10.0 表示 file_main语音的第0秒 对应 file_res的第b=-10.0 ，所以将file_res开头增加b=10.0秒静音即可

                            WaveFile inWav = new WaveFile();
                            ret = inWav.OpenFile(file_res, WaveFormat.WF_NONE);
                            int datalength = (int)inWav.GetTotalSample();
                            int sampleRate = (int)inWav.GetSamplePerSec();
                            short[,] data = null;
                            datalength = inWav.GetData(ref data, datalength);
                            //float data_time = float(datalength) / float(sampleRate);
                            int num_chanel = inWav.GetChannelNum();

                            //// 输出wav 
                            string out_test_wav = di_out_wav.FullName + "\\test_out.wav";
                            WaveFile outWav = new WaveFile();
                            outWav.OpenFile(out_test_wav, inWav.WaveFileFormat, false, (UInt32)sampleRate, (ushort)data.GetLength(0));

                            short[,] data_new = null;
                            
                            if(bb>0) // 删除 file_res 前面 bb 秒
                            {
                                
                                int len_del = (int)(bb * sampleRate);
                                int len_copy = datalength - len_del;
                                data_new = new short[num_chanel, len_copy];
                                for (int ii = 0; ii < len_copy; ii++ )
                                {
                                    data_new[0,ii] = data[0,ii+len_del];
                                }                                
                                
                                outWav.PutData(data_new, len_copy);
                            }
                            else  // 在 file_res 前面 增加bb秒的静音 
                            {
                               
                                int len_add = (int)((0-bb) * sampleRate);
                                int len_copy = datalength + len_add;
                                data_new = new short[num_chanel, len_copy];
                                for (int ii = 0; ii < len_add;ii++ )
                                {
                                    data_new[0, ii] = 0;
                                }
                                for (int ii = len_add; ii < len_copy; ii++)
                                {
                                    data_new[0, ii] = data[0, ii - len_add];
                                }

                                outWav.PutData(data_new, len_copy);
                            }

                            // 读取 seg 文件 
                            StreamReader sr_seg = new StreamReader(fi_seg_main.FullName, Encoding.Default);
                            String line;
                            int num_line = 0;
                            textBoxLog.AppendText(fi_seg_main.FullName+" seg info:\r\n");
                            while ((line = sr_seg.ReadLine()) != null)
                            {
                                num_line += 1;
                                if (num_line == 1) continue;

                                
                                string line_str = line.ToString();
                                string[] str_split = line_str.Split(new string[] { "<->" }, 20,StringSplitOptions.RemoveEmptyEntries);
                                if(str_split.GetLength(0)<8)continue;

                                string name_wav = str_split[0];
                                string id = str_split[1];
                                double time_st = double.Parse(str_split[4]) / 1000.0;
                                double time_end = double.Parse(str_split[5]) / 1000.0;
                                //textBoxLog.AppendText("\t"+id+" "+time_st+" "+time_end+"\r\n");

                                

                                
                                int len_st = (int)((aa * time_st + bb)*sampleRate);
                                int len_end = (int)((aa * time_end + bb)*sampleRate);
                                // pcm 左边位置不够  或者 右边位置不够 
                                if (len_end < 0 || len_st < 0 || datalength < len_end)
                                {
                                    continue;
                                }

                                //// 输出 seg wav 
                                string out_seg_wav = di_out_wav.FullName + "\\" + id + ".wav";
                                WaveFile outWav_seg = new WaveFile();
                                outWav_seg.OpenFile(out_seg_wav, inWav.WaveFileFormat, false, (UInt32)sampleRate, (ushort)data.GetLength(0));
                                short[,] data_new_seg = null;
                                data_new_seg = new short[num_chanel, len_end-len_st];
                                for (int ii = len_st; ii < len_end; ii++)
                                {
                                    data_new_seg[0, ii-len_st] = data[0, ii];
                                }

                                outWav_seg.PutData(data_new_seg, len_end-len_st);
                                outWav_seg.FlushFile();
                                outWav_seg.CloseFile();
                                outWav_seg.DestoryData(ref data_new_seg);
                                
                            }
	                        
                            outWav.FlushFile();
	                        outWav.CloseFile();
	                        inWav.DestoryData(ref data);
                            inWav.DestoryData(ref data_new);
	                        inWav.CloseFile();



                        }

                    }

                    
                }
            }

            // 整个过程处理完毕！ 
            textBoxLog.AppendText("completed!\r\n");

            // 逐行将textBox1的内容写入到文件中
            foreach (string line in textBoxLog.Lines)
            {
                wr_log.WriteLine(line);
            }
            wr_log.Flush();

            wr_log.Close();
            fs_log.Close();
            fs_log = null;
            wr_log = null;
            
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBoxLog_TextChanged(object sender, EventArgs e)
        {

        }


    } // end class Form1 : Form 

    public class CPPDLL
    {
        [DllImport("speechMatchDLL.dll", EntryPoint = "speechMatch_a", CallingConvention = CallingConvention.Cdecl)]
        public static extern int speechMatch_a(string file1, string file2, ref double aa, ref double bb);
    }




}
