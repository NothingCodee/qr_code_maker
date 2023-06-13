using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;
using System.IO;
using System.Drawing.Imaging;

namespace QrCode_Olusturucu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int width, height, margin;

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            logo_yolu = openFileDialog1.FileName;
            pictureBox2.ImageLocation = openFileDialog1.FileName;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(pictureBox1 != null)
            {
                Image img = pictureBox1.Image;
                Bitmap bmp = new Bitmap(img.Width, img.Height);
                Graphics gra = Graphics.FromImage(bmp);
                gra.DrawImageUnscaled(img, new Point(0, 0));
                gra.Dispose();

                string belgelerim = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                bmp.Save(belgelerim + "\\qrcode.jpg", ImageFormat.Jpeg);
                bmp.Dispose();
                label7.Text = "QrCode Masaüstüne Kaydedildi!";
            }
            else
            {
                MessageBox.Show("QrCode oluşturmadınız!");
            }
        }

        string logo_yolu = null;
        private void button2_Click(object sender, EventArgs e)
        {
            width = Convert.ToInt32(textBox2.Text);
            height = Convert.ToInt32(textBox3.Text);
            margin = Convert.ToInt32(textBox4.Text);
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            EncodingOptions encodingOptions = new EncodingOptions() { Width = width, Height = height, Margin = margin, PureBarcode = false};
            encodingOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            barcodeWriter.Renderer = new BitmapRenderer();
            barcodeWriter.Options = encodingOptions;
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            Bitmap bitmap = barcodeWriter.Write(textBox1.Text);
            if(logo_yolu != null)
            {
                Bitmap logo = new Bitmap(logo_yolu);
                Graphics g = Graphics.FromImage(bitmap);
                g.DrawImage(logo, new Point((bitmap.Width - logo.Width) / 2, (bitmap.Height - logo.Height) / 2));
                pictureBox1.Image = bitmap;
            }
            else
            {
                pictureBox1.Image = bitmap;
            }
        }
    }
}
