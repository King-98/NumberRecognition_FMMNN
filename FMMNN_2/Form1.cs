using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FMMNN_2
{
    public partial class Form1 : Form
    {
        Image image;
        Bitmap totalpicture;

        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = "All Files(*.*)|*.*|Bitmap File(*.bmp)|*.bmp|";
            name = name + "Gif File(*.gif)|*.gif|jpeg File(*.jpg)|*.jpg";
            openFileDialog1.Title = "타이틀";

            openFileDialog1.Filter = name;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strName = openFileDialog1.FileName;
                image = Image.FromFile(strName);
                totalpicture = new Bitmap(image);
            }

            pictureBox1.Image = new Bitmap(totalpicture, pictureBox1.Width, pictureBox1.Height);

            this.Invalidate();
        }

        private void fMMNNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = totalpicture;
            Run run = new Run();
            run.numFMMNN(bitmap);
            
            ImageProcessing ip = new ImageProcessing();
            pictureBox2.Image = new Bitmap(run.Convert(ip.roiarea(ip.maxmin(ip.GrayArray(totalpicture)))), pictureBox2.Width, pictureBox2.Height);
        }
    }
}
