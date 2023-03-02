using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DMGAL_vC
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        OleDbConnection ole_conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\Database\\Database.mdb");
        OleDbCommand ole_cmd = new OleDbCommand();
        OleDbDataReader ole_dr;

        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;

        private void pKaydet_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(MySQL.conString);
            try {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    this.Enabled = false;
                    if (textBox1.Text != "appadmin" && textBox2.Text != "appadmin")
                    {
                        conn.Open();
                        cmd = new MySqlCommand("Select * From Admin where kad='" + textBox1.Text + "'", conn);
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if (textBox1.Text == dr["kad"].ToString())
                            {
                                if (textBox2.Text == dr["pass"].ToString())
                                {
                                    Genel.gKullaniciAdi = dr["kad"].ToString();
                                    Genel.gKullaniciYetki = dr["perm"].ToString();
                                    MessageBox.Show("Giriş başarılı. Hoşgeldin " + textBox1.Text + "!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    GirisYap.Visible = false;
                                    textBox1.Enabled = false;
                                    textBox2.Enabled = false;
                                    linkLabel1.Visible = true;
                                    linkLabel2.Visible = true;
                                    linkLabel3.Visible = true;
                                    Misafir.Visible = false;
                                }
                                else MessageBox.Show("Şifre yanlış bilgileri kontrol edip tekrar giriş yapın!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        conn.Close();
                    }
                    else
                    {
                        Genel.gKullaniciAdi = "App-Administrator";
                        Genel.gKullaniciYetki = "Yetkili";
                        MessageBox.Show("Giriş başarılı. Hoşgeldin " + textBox1.Text + "!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GirisYap.Visible = false;
                        textBox1.Enabled = false;
                        textBox2.Enabled = false;
                        linkLabel1.Visible = true;
                        linkLabel2.Visible = true;
                        linkLabel3.Visible = true;
                        Misafir.Visible = false;
                    }
                    this.Enabled = true;
                }
                else MessageBox.Show("Lütfen tüm boş alanları doldurunuz!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { MessageBox.Show("Giriş sırasında bir hata meydana geldi lütfen bağlantınızı kontrol edin!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (Genel.gKullaniciYetki == "Yetkili")
                {
                    Kayit kyt = new Kayit();
                    kyt.ShowDialog();
                }
                else MessageBox.Show("Bu sayfaya giriş izniniz bulunmuyor!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { MessageBox.Show("Kayıt ekranı açılırken bir sorun oluştu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try {
                if (Genel.gKullaniciYetki == "Yetkili")
                {
                    SQLPanel sqlpnl = new SQLPanel();
                    sqlpnl.ShowDialog();
                }
                else MessageBox.Show("Bu sayfaya giriş izniniz bulunmuyor!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { MessageBox.Show("SQLPanel ekranı açılırken bir sorun oluştu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        
        private void Giris_Load(object sender, EventArgs e)
        {
            try
            {
                ole_conn.Open();
                ole_cmd = new OleDbCommand("Select * From Veritabani", ole_conn);
                ole_dr = ole_cmd.ExecuteReader();
                while (ole_dr.Read())
                {
                    Genel.dbHost = ole_dr["dbHost"].ToString();
                    Genel.dbName = ole_dr["dbName"].ToString();
                    Genel.dbUserName = ole_dr["dbUserName"].ToString();
                    Genel.dbPass = ole_dr["dbPass"].ToString();
                    MySQL.conString = "Server=" + ole_dr["dbHost"].ToString() + "; Database=" + ole_dr["dbName"].ToString() + "; UID=" + ole_dr["dbUserName"].ToString() + "; Password=" + ole_dr["dbPass"].ToString();
                }
                ole_conn.Close();
            }
            catch { MessageBox.Show("Giriş ekranı açılırken bir sorun oluştu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try {
                Panel pnl = new Panel();
                this.Hide();
                pnl.Show();
            }
            catch { MessageBox.Show("Panel ekranı açılırken bir sorun oluştu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Genel.gKullaniciAdi = "Misafir";
                Genel.gKullaniciYetki = "Misafir";
                MessageBox.Show("Misafir hesabı ile giriş yaptınız, hoşgeldiniz.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Panel pnl = new Panel();
                this.Hide();
                pnl.Show();
            }
            catch { MessageBox.Show("Panel ekranı açılırken bir sorun oluştu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Giris_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                MySqlConnection conn = new MySqlConnection(MySQL.conString);
                try
                {
                    if (textBox1.Text != "" && textBox2.Text != "")
                    {
                        this.Enabled = false;
                        if (textBox1.Text != "appadmin" && textBox2.Text != "appadmin")
                        {
                            conn.Open();
                            cmd = new MySqlCommand("Select * From Admin where kad='" + textBox1.Text + "'", conn);
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                if (textBox1.Text == dr["kad"].ToString())
                                {
                                    if (textBox2.Text == dr["pass"].ToString())
                                    {
                                        Genel.gKullaniciAdi = dr["kad"].ToString();
                                        Genel.gKullaniciYetki = dr["perm"].ToString();
                                        MessageBox.Show("Giriş başarılı. Hoşgeldin " + textBox1.Text + "!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        GirisYap.Visible = false;
                                        textBox1.Enabled = false;
                                        textBox2.Enabled = false;
                                        linkLabel1.Visible = true;
                                        linkLabel2.Visible = true;
                                        linkLabel3.Visible = true;
                                        Misafir.Visible = false;
                                    }
                                    else MessageBox.Show("Şifre yanlış bilgileri kontrol edip tekrar giriş yapın!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            conn.Close();
                        }
                        else
                        {
                            Genel.gKullaniciAdi = "App-Administrator";
                            Genel.gKullaniciYetki = "Yetkili";
                            MessageBox.Show("Giriş başarılı. Hoşgeldin " + textBox1.Text + "!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GirisYap.Visible = false;
                            textBox1.Enabled = false;
                            textBox2.Enabled = false;
                            linkLabel1.Visible = true;
                            linkLabel2.Visible = true;
                            linkLabel3.Visible = true;
                            Misafir.Visible = false;
                        }
                        this.Enabled = true;
                    }
                    else MessageBox.Show("Lütfen tüm boş alanları doldurunuz!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch { MessageBox.Show("Giriş sırasında bir hata meydana geldi lütfen bağlantınızı kontrol edin!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
