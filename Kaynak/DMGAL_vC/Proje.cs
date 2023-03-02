using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMGAL_vC
{
    public partial class Proje : UserControl
    {
        public Proje()
        {
            InitializeComponent();
        }

        public static string ProjeAdi = "";
        public void Proje_Click(EventHandler handler)
        {
            this.Click += handler;
            panel1.Click += handler;
            pictureBox1.Click += handler;
            label1.Click += handler;
            label2.Click += handler;
        }

        public string ProjeIsim
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public string LogoProjeIsim
        {
            get { return pictureBox1.Tag.ToString(); }
            set { pictureBox1.Tag = value; }
        }
        public string ProjeEkleyen
        {
            get { return label2.Text; }
            set { label2.Text = value; }
        }
        public string ProjeVersion
        {
            get { return label3.Text; }
            set { label3.Text = value; }
        }

        public string ProjeLogo
        {
            get { return pictureBox1.ImageLocation; }
            set { pictureBox1.ImageLocation = value; }
        }
        public Image ProjeLogos
        {
            get { return pictureBox1.Image; }
            set { pictureBox1.Image = value; }
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#3b3d41");
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#2f3136");
        }
    }
}
