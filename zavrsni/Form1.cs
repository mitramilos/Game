using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;


namespace zavrsni
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        FilterInfoCollection kamerica;
        VideoCaptureDevice prednja;
        PictureBox[] pb;
        Random r = new Random();
        int pogodjeno;
        int stvoreni;

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.White;
            kamerica = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterinfo in kamerica)
                comboBox1.Items.Add(filterinfo.Name);
            comboBox1.SelectedIndex = 0;
            prednja = new VideoCaptureDevice();
            timer1.Interval = 1000;
            stvoreni = 0;

        }
        private void VideoCaptureDevice_frejm(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.BackgroundImage = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = textBox1.Text;
            prednja = new VideoCaptureDevice(kamerica[comboBox1.SelectedIndex].MonikerString);
            prednja.NewFrame += VideoCaptureDevice_frejm;
            prednja.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = pictureBox1.BackgroundImage;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PictureBox novi = new PictureBox();
            novi.Width = 40;
            novi.Height = 40;
            novi.BackgroundImage = pictureBox2.BackgroundImage;
            novi.BackgroundImageLayout = ImageLayout.Stretch;
            novi.Location = new Point(r.Next(panel1.Width - 40), r.Next(panel1.Height - 40));
            panel1.Controls.Add(novi);
            novi.Click += kliknutaslika;
            stvoreni++;
            if (stvoreni == 30)
            {
                krajigre();
            }
        }

        private void krajigre()
        {
            timer1.Stop();


            panel1.BackgroundImage = pictureBox1.BackgroundImage;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            label5.Text = "Ostvarili ste ukupno " + pogodjeno.ToString();
            label6.Text = "NEMOJTE DA SE NERVIRATE";
            label6.BackColor = Color.Red;
            
        }
        private void kliknutaslika(object sender, EventArgs e)
        {
            PictureBox pb=(PictureBox)sender;
            panel1.Controls.Remove(pb);
            pogodjeno++;
            label4.Text = "Pogodjenih:" + pogodjeno.ToString();

            if (pogodjeno == 5)
            {
                timer1.Interval = 800;
            }

            if (pogodjeno == 10)
            {
                timer1.Interval = 600;
            }
            if (pogodjeno == 15)
            {
                timer1.Interval = 400;
            }
            if (pogodjeno == 20)
            {
                timer1.Interval = 200;
            }
            if (pogodjeno == 25)
            {
                timer1.Interval = 100;
            }
            if (stvoreni == 30)
            {
                panel1.Controls.Remove(pb);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();
            label6.Text = "UZIVO PRENOS";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            krajigre();
        }
       
    }
}
