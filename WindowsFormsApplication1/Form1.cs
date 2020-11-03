using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string[] portlar = SerialPort.GetPortNames();
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string sonuc = serialPort1.ReadExisting();
                textBox1.Text = sonuc + "";
            }
            catch (Exception ex)
            {
                
                timer1.Stop();

             
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string port in portlar)
            {
                comboBox1.Items.Add(port);
                comboBox1.SelectedIndex = 0;
            }
            comboBox2.Items.Add("9600");
            comboBox2.Items.Add("115200");
            comboBox2.SelectedIndex = 1;
            label2.Visible = true;
            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            using(OpenFileDialog ofd=new OpenFileDialog() )
            if (serialPort1.IsOpen==false)
            {
                if(comboBox1.Text=="")
                 return;
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                try
                {
                    serialPort1.Open();
                    label1.Visible = true;
                    label1.Text = "Baglanti acik";
                }
                catch (Exception hata)
                {
                   
                    
                }

               
            } 
            else
                {
                    label2.Text="Baglantı Kuruldu";
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if(serialPort1.IsOpen==true)
            {
                serialPort1.Close();
                label2.Text = "Baglantı Kapalı";

            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(serialPort1.IsOpen==true)
            {
                serialPort1.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {           

            System.IO.StreamWriter yaz;
            yaz = new System.IO.StreamWriter("rapor.txt", true);
            yaz.WriteLine(textBox1.Text);
            yaz.WriteLine();
            yaz.Close();
            MessageBox.Show("Kaydedildi");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
