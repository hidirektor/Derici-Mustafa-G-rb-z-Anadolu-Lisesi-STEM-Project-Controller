using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMGAL_vC
{
    public partial class Panel : Form
    {
        public Panel()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;

        ArrayList liste = new ArrayList();
        ArrayList liste2 = new ArrayList();

        private void prj_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            MySqlConnection conn = new MySqlConnection(MySQL.conString);
            try {
                if (sender.GetType() == typeof(Label))
                {
                    Label p = (Label)sender;
                    if (p.Tag.ToString() == "Başlık")
                    {
                        pSilinen.Text = p.Text; label_pBaslik.Text = p.Text;
                        pCalistir.Visible = true;
                        conn.Open();
                        cmd = new MySqlCommand("Select * From Veriler where pBaslik='" + p.Text + "'", conn);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            label_pEkleyen.Text = dr["pEkleyen"].ToString();
                            label_pSurumu.Text = dr["pSurumu"].ToString();
                            label_pBaslama.Text = dr["pBaslama"].ToString();
                            label_pBitis.Text = dr["pBitis"].ToString();
                            label_pGuncelleyen.Text = dr["pGuncelleyen"].ToString();
                            if (dr["pYolu"].ToString() != "") label_pYolu.Text = dr["pYolu"].ToString();
                            else if (dr["pYolu"].ToString() == "") label_pYolu.Text = "None";
                            if (dr["pLogoLink"].ToString() != "") label_pLogoLink.Text = dr["pLogoLink"].ToString();
                            else if (dr["pLogoLink"].ToString() == "") label_pLogoLink.Text = "None";
                            as_pAciklama.Text = dr["pAciklama"].ToString();
                            if (pKaydet.Text == "Proje Güncelle")
                            {
                                pBaslik.Text = p.Text;
                                pEkleyen.Text = dr["pEkleyen"].ToString();
                                pSurumu.Text = dr["pSurumu"].ToString();
                                pBaslama.Text = dr["pBaslama"].ToString();
                                pBitis.Text = dr["pBitis"].ToString();
                                pGuncelleyen.Text = Genel.gKullaniciAdi;
                                pYolu.Text = dr["pYolu"].ToString();
                                pLogoLink.Text = dr["pLogoLink"].ToString();
                                pAciklama.Text = dr["pAciklama"].ToString();
                            }
                        }
                        conn.Close();
                    }
                }
                if (sender.GetType() == typeof(PictureBox))
                {
                    PictureBox p = (PictureBox)sender;
                    pSilinen.Text = p.Tag.ToString();
                    label_pBaslik.Text = p.Tag.ToString();
                    pCalistir.Visible = true;
                    conn.Open();
                    cmd = new MySqlCommand("Select * From Veriler where pBaslik='" + p.Tag.ToString() + "'", conn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        label_pEkleyen.Text = dr["pEkleyen"].ToString();
                        label_pSurumu.Text = dr["pSurumu"].ToString();
                        label_pBaslama.Text = dr["pBaslama"].ToString();
                        label_pBitis.Text = dr["pBitis"].ToString();
                        label_pGuncelleyen.Text = dr["pGuncelleyen"].ToString();
                        if (dr["pYolu"].ToString() != "") label_pYolu.Text = dr["pYolu"].ToString();
                        else if (dr["pYolu"].ToString() == "") label_pYolu.Text = "None";
                        if (dr["pLogoLink"].ToString() != "") label_pLogoLink.Text = dr["pLogoLink"].ToString();
                        else if (dr["pLogoLink"].ToString() == "") label_pLogoLink.Text = "None";
                        as_pAciklama.Text = dr["pAciklama"].ToString();
                        if (pKaydet.Text == "Proje Güncelle")
                        {
                            pBaslik.Text = p.Tag.ToString();
                            pEkleyen.Text = dr["pEkleyen"].ToString();
                            pSurumu.Text = dr["pSurumu"].ToString();
                            pBaslama.Text = dr["pBaslama"].ToString();
                            pBitis.Text = dr["pBitis"].ToString();
                            pGuncelleyen.Text = Genel.gKullaniciAdi;
                            pYolu.Text = dr["pYolu"].ToString();
                            pLogoLink.Text = dr["pLogoLink"].ToString();
                            pAciklama.Text = dr["pAciklama"].ToString();
                        }
                    } conn.Close();
                }
            }
            catch { MessageBox.Show("Proje seçilirken bir hata meydana geldi!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            timer2.Enabled = true;
        }

        void anaSayfaYenile()
        {
            pSilinen.Clear();
            label_pBaslik.Text = "None";
            label_pEkleyen.Text = "None";
            label_pSurumu.Text = "None";
            label_pBaslama.Text = "None";
            label_pBitis.Text = "None";
            label_pGuncelleyen.Text = "None";
            label_pYolu.Text = "None";
            label_pLogoLink.Text = "None";
            as_pAciklama.Clear();

            pBaslik.Clear();
            pEkleyen.Text = Genel.gKullaniciAdi;
            pSurumu.Clear();
            pBaslama.Text = DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year;
            pBitis.Clear();
            pGuncelleyen.Text = Genel.gKullaniciAdi;
            pYolu.Clear();
            pLogoLink.Clear();
            pAciklama.Clear();

            pCalistir.Visible = false;
        }
        void veriGetir()
        {
            MySqlConnection conn = new MySqlConnection(MySQL.conString);
            veriTemizle();
            conn.Open();
            cmd = new MySqlCommand("Select * From Veriler", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                liste.Clear();
                foreach (Control c in panel1.Controls)
                {
                    if (c.GetType() == typeof(Proje))
                    {
                        liste.Add(c.Name.ToString());
                    }
                }
                Proje prj = new Proje();
                prj.Name = "proje" + (liste.Count + 1);
                prj.ProjeIsim = dr["pBaslik"].ToString();
                prj.ProjeEkleyen = "by " + dr["pEkleyen"].ToString();
                prj.LogoProjeIsim = dr["pBaslik"].ToString();
                prj.ProjeVersion = "Version: " + dr["pSurumu"].ToString();
                if (dr["pLogoLink"].ToString() != "") prj.ProjeLogo = dr["pLogoLink"].ToString();
                else prj.ProjeLogos = ımageList1.Images[0];
                if (liste.Count == 0) prj.Location = new Point(9, panel1.Location.Y + 10);
                else prj.Location = new Point(9, ((Proje)panel1.Controls[liste[liste.Count - 1].ToString()]).Location.Y + 70);
                prj.BringToFront();
                prj.Proje_Click(new EventHandler(prj_Click));
                panel1.Controls.Add(prj);
                liste.Clear();
                foreach (Control c in panel1.Controls)
                {
                    if (c.GetType() == typeof(Proje))
                    {
                        liste.Add(c.Name.ToString());
                    }
                }
            }
            conn.Close();
            anaSayfaYenile();
            AnaPanel2.Visible = false;
            AnaPanel1.Visible = true;
            AnaPanel1.BringToFront();
            AnaPanel1.Dock = DockStyle.Fill;
        }

        void veriTemizle()
        {
            for (int i = 0; i < liste.Count; i++)
            {
                panel1.Controls.Remove((Proje)panel1.Controls[liste[i].ToString()]);
            }
        }
        
        private void pKaydet_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            MySqlConnection conn = new MySqlConnection(MySQL.conString);
            try
            {
                if (pBaslik.Text != "" && pAciklama.Text != "")
                {
                    if (pLogoLink.Text != "")
                    {
                        if (pLogoLink.Text.Contains("http://") || pLogoLink.Text.Contains("https://"))
                        {
                            if (pKaydet.Text == "Proje Kaydet")
                            {
                                conn.Open();
                                cmd = new MySqlCommand("Insert Into Veriler(pBaslik,pEkleyen,pSurumu,pBaslama,pBitis,pGuncelleyen,pYolu,pLogoLink,pAciklama,pIsim) values('" + pBaslik.Text + "', '" + pEkleyen.Text + "', '" + pSurumu.Text + "', '" + pBaslama.Text + "', '" + pBitis.Text + "', '" + pGuncelleyen.Text + "', '" + pYolu.Text + "', '" + pLogoLink.Text + "', '" + pAciklama.Text + "', '" + ("proje" + (liste.Count + 1)) + "')", conn);
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                veriGetir();
                                MessageBox.Show(pBaslik.Text + " Başarıyla veritabanına eklendi!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (pKaydet.Text == "Proje Güncelle")
                            {
                                conn.Open();
                                cmd = new MySqlCommand("Update Veriler set pSurumu='" + pSurumu.Text + "', pBitis='" + pBitis.Text + "', pGuncelleyen='" + pGuncelleyen.Text + "', pYolu='" + pYolu.Text + "', pLogoLink='" + pLogoLink.Text + "', pAciklama='" + pAciklama.Text + "' where pBaslik='" + pBaslik.Text + "'", conn);
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                veriGetir();
                                MessageBox.Show(pBaslik.Text + " Başarıyla veritabanında güncellendi!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else MessageBox.Show("Lütfen logonuzu resim linki ile oluşturunuz!\nYazdığınız link 'http://' veya 'https://' içermelidir.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (pKaydet.Text == "Proje Kaydet")
                        {
                            conn.Open();
                            cmd = new MySqlCommand("Insert Into Veriler(pBaslik,pEkleyen,pSurumu,pBaslama,pBitis,pGuncelleyen,pYolu,pLogoLink,pAciklama,pIsim) values('" + pBaslik.Text + "', '" + pEkleyen.Text + "', '" + pSurumu.Text + "', '" + pBaslama.Text + "', '" + pBitis.Text + "', '" + pGuncelleyen.Text + "', '" + pYolu.Text + "', '" + pLogoLink.Text + "', '" + pAciklama.Text + "', '" + ("proje" + (liste.Count + 1)) + "')", conn);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            veriGetir();
                            MessageBox.Show(pBaslik.Text + " Başarıyla veritabanına eklendi!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (pKaydet.Text == "Proje Güncelle")
                        {
                            conn.Open();
                            cmd = new MySqlCommand("Update Veriler set pSurumu='" + pSurumu.Text + "', pBitis='" + pBitis.Text + "', pGuncelleyen='" + pGuncelleyen.Text + "', pYolu='" + pYolu.Text + "', pLogoLink='" + pLogoLink.Text + "', pAciklama='" + pAciklama.Text + "' where pBaslik='" + pBaslik.Text + "'", conn);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            veriGetir();
                            MessageBox.Show(pBaslik.Text + " Başarıyla veritabanında güncellendi!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else MessageBox.Show("Lütfen tüm alanları doldurunuz!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch { MessageBox.Show("Proje kaydedilirken bir hata meydana geldi!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            timer2.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void pSil_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            MySqlConnection conn = new MySqlConnection(MySQL.conString);
            try {
                if (pSilinen.Text != "")
                {
                    liste2.Clear();

                    conn.Open();
                    cmd = new MySqlCommand("Delete From Veriler where pBaslik='" + pSilinen.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                
                    conn.Open();
                    MySqlCommand komut = new MySqlCommand("Select * From Veriler", conn);
                    dr = komut.ExecuteReader();
                    while (dr.Read())
                    {
                        liste2.Add(dr["pIsim"].ToString());
                    }
                    conn.Close();

                    for (int i = 0; i < liste2.Count; i++)
                    {
                        conn.Open();
                        cmd = new MySqlCommand("Update Veriler set pIsim='" + ("proje" + (i+1)) + "' where pIsim='" + liste2[i].ToString() + "'", conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    veriGetir();
                }
            }
            catch { MessageBox.Show("Proje silinirken bir hata meydana geldi!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            timer2.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try {
                veriGetir();
                label2.Text = DateTime.Now.ToString();
                label1.Text = DateTime.Now.ToString();
                timer2.Enabled = true;
                if (Genel.gKullaniciYetki == "Yetkili")
                {
                    pEkle.Visible = true;
                    pGuncelle.Visible = true;
                    pSil.Visible = true;
                    pSilinen.Visible = true;
                    pSilinenTablo.Visible = true;
                    SeciliProje.Visible = true;
                }
                else if (Genel.gKullaniciYetki == "Misafir") aSayfa.Text = "Sayfayı Yenile";
                else Application.Exit();
            }
            catch { MessageBox.Show("Panel açılırken bir hata meydana geldi!\nBağlantı hatası yaşıyo olabilirsiniz.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
        }

        private void aSayfa_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            try {
                anaSayfaYenile();
                AnaPanel2.Visible = false;
                AnaPanel1.Visible = true;
                AnaPanel1.BringToFront();
                AnaPanel1.Dock = DockStyle.Fill;
            }
            catch { MessageBox.Show("AnaSayfa yenilenirken bir hata meydana geldi!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            timer2.Enabled = true;
        }
        private void pEkle_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            try {
                pBaslik.ForeColor = Color.LightSkyBlue;
                pBaslik.ReadOnly = false;
                pKaydet.Text = "Proje Kaydet";
                pKaydet.ForeColor = Color.Lime;
                anaSayfaYenile();
                AnaPanel1.Visible = false;
                AnaPanel2.Visible = true;
                AnaPanel2.BringToFront();
                AnaPanel2.Dock = DockStyle.Fill;
            }
            catch { MessageBox.Show("Proje ekleme ekranında bir hata oluştu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            timer2.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
            label1.Text = DateTime.Now.ToString();
        }

        private void pGuncelle_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            try {
                pBaslik.ForeColor = Color.SteelBlue;
                pBaslik.ReadOnly = true;
                pKaydet.Text = "Proje Güncelle";
                pKaydet.ForeColor = Color.DodgerBlue;
                anaSayfaYenile();
                AnaPanel1.Visible = false;
                AnaPanel2.Visible = true;
                AnaPanel2.BringToFront();
                AnaPanel2.Dock = DockStyle.Fill;
            }
            catch { MessageBox.Show("Proje güncelleme ekranında bir hata oluştu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            timer2.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void pCalistir_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            try {
                if (label_pYolu.Text != "None")
                    System.Diagnostics.Process.Start(label_pYolu.Text);
            } catch { MessageBox.Show("Proje başlatılamıyor dosya yolu değiştirilmiş veya silinmiş olabilir.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            timer2.Enabled = true;
        }

        private void YolSec_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            try {
                OpenFileDialog file = new OpenFileDialog();
                file.Filter = "Tüm Dosyalar|*|EXE|*.exe|JAR|*.jar|JAVA|*.java|TXT|*.txt";
                file.FilterIndex = 1;
                file.RestoreDirectory = true;
                file.CheckFileExists = false;
                file.Title = "Başlatmasını istediğiniz uygulamayı seçiniz..";
                file.Multiselect = false;
                if (file.ShowDialog() == DialogResult.OK)
                {
                    pYolu.Clear();
                    pYolu.Text = file.FileName;
                }
            }
            catch { MessageBox.Show("Dosya seçiminde bir hata oluştu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            timer2.Enabled = true;
        }

        private void Panel_FormClosed(object sender, FormClosedEventArgs e)
        {
            Giris grs = new Giris();
            grs.Show();
            this.Hide();
        }

        private void pYoluSil_Click(object sender, EventArgs e)
        {
            pYolu.Clear();
        }
        
        void veriKontrol()
        {
            MySqlConnection conn = new MySqlConnection(MySQL.conString);
            conn.Open();
            cmd = new MySqlCommand("Select * From Veriler", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (!(liste.Contains(dr["pIsim"].ToString())))
                {

                    liste.Clear();
                    foreach (Control c in panel1.Controls)
                    {
                        if (c.GetType() == typeof(Proje))
                        {
                            liste.Add(c.Name.ToString());
                        }
                    }
                    Proje prj = new Proje();
                    prj.Name = dr["pIsim"].ToString();
                    prj.ProjeIsim = dr["pBaslik"].ToString();
                    prj.ProjeEkleyen = "by " + dr["pEkleyen"].ToString();
                    prj.LogoProjeIsim = dr["pBaslik"].ToString();
                    prj.ProjeVersion = "Version: " + dr["pSurumu"].ToString();
                    if (dr["pLogoLink"].ToString() != "") prj.ProjeLogo = dr["pLogoLink"].ToString();
                    else prj.ProjeLogos = ımageList1.Images[0];
                    if (liste.Count == 0) prj.Location = new Point(9, panel1.Location.Y + 10);
                    else prj.Location = new Point(9, ((Proje)panel1.Controls[liste[liste.Count - 1].ToString()]).Location.Y + 70);
                    prj.BringToFront();
                    prj.Proje_Click(new EventHandler(prj_Click));
                    panel1.Controls.Add(prj);
                    liste.Clear();
                    foreach (Control c in panel1.Controls)
                    {
                        if (c.GetType() == typeof(Proje))
                        {
                            liste.Add(c.Name.ToString());
                        }
                    }
                }
            }
            conn.Close();
        }

        ArrayList liste3 = new ArrayList();
        void silinenKontrol()
        {
            MySqlConnection conn = new MySqlConnection(MySQL.conString);
            liste.Clear();
            foreach (Control c in panel1.Controls)
            {
                if (c.GetType() == typeof(Proje))
                {
                    liste.Add(c.Name.ToString());
                }
            }

            liste3.Clear();
            conn.Open();

            cmd = new MySqlCommand("Select * From Veriler", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                liste3.Add(dr["pIsim"].ToString());
            }

            for (int i = 0; i < liste.Count; i++)
            {
                if (!(liste3.Contains(liste[i].ToString())))
                {
                    panel1.Controls.Remove((Proje)panel1.Controls[liste[i].ToString()]);
                    veriGetir();
                }
            }

            conn.Close();
        }
        

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                veriKontrol();
                silinenKontrol();
            }
            catch { MessageBox.Show("Kritik bir bağlantı hatası oluşmuş olabilir program kapatılıcaktır!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Giris grs = new Giris();
            grs.Show();
            this.Hide();
        }
        

        private void pAciklama_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                MessageBox.Show("1");
            }
        }
    }
}