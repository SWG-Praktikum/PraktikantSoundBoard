using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PraktikantSoundBoard
{
    public partial class Form1 : Form
    {
        string targetDirectory = Environment.CurrentDirectory + @"\SoundBoard";

        public Form1()
        {
            InitializeComponent();

            if (!Directory.Exists(targetDirectory))
                Directory.CreateDirectory(targetDirectory);

            foreach (string fileName in Directory.GetFiles(targetDirectory))
            {
                if (fileName.ToLower().EndsWith(".mp3"))
                {
                    ConvertMp3ToWav(fileName, fileName.Replace(".mp3", ".wav"));
                }
            }

            int amount = 0;
            foreach (string fileName in Directory.GetFiles(targetDirectory))
            {
                if (fileName.ToLower().EndsWith(".wav"))
                {
                    Button newButton = new Button();
                    newButton.Width = 100;
                    newButton.Height = 50;
                    newButton.Location = new Point(110 * (amount % 5), 60 * (amount / 5));
                    newButton.Text = Path.GetFileName(fileName);
                    newButton.Click += btn_sound1_Click;
                    this.Controls.Add(newButton);

                    amount++;
                }
            }

            if (amount >= 5)
                this.Width = 440 + 100 + 18;
            else
                this.Width = 110 * (amount - 1) + 100 + 18;

            this.Height = 60 * ((amount - 1) / 5) + 50 + 40;
        }

        private void btn_sound1_Click(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Path.Combine(targetDirectory, ((Button)sender).Text ));
            player.Play();
        }

        private static void ConvertMp3ToWav(string _inPath_, string _outPath_)
        {
            using (Mp3FileReader mp3 = new Mp3FileReader(_inPath_))
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                {
                    WaveFileWriter.CreateWaveFile(_outPath_, pcm);
                }
            }
        }
    }
}
