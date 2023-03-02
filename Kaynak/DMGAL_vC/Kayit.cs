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
using MySql.Data.MySqlClient;

namespace DMGAL_vC
{
    public partial class Kayit : Form
    {
        public Kayit()
        {
            InitializeComponent();
        }
        
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dr;

        void comboVeriGetir()
        {
            MySqlConnection conn = new MySqlConnection(MySQL.conString);
            comboBox2.Items.Clear();
            conn.Open();
            cmd = new MySqlCommand("Select * From Admin", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["kad"].ToString());
            }
            conn.Close();
        }

        private void Kayit_Load(object sender, EventArgs e)
        {
            try {
                comboBox1.Text = "Yetkili";
                comboVeriGetir();
            }
            catch { MessageBox.Show("Kayıt ekranı açılırken bir sorun oluştu!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text = comboBox2.SelectedItem.ToString();
        }

        private void pKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(MySQL.conString);
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    conn.Open();
                    cmd = new MySqlCommand("Insert Into Admin(kad,pass,perm) values('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "')", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Girdiğiniz kullanıcı bilgileri başarıyla kaydedildi.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    textBox2.Clear();
                    comboBox1.Text = "Yetkili";
                    comboVeriGetir();
                    textBox1.Focus();
                }
            } catch { MessageBox.Show("Kayıt sırasında hata meydana geldi, bağlantınızı kontrol edin!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(MySQL.conString);
                if (textBox3.Text != "")
                {
                    conn.Open();
                    cmd = new MySqlCommand("Delete From Admin where kad='" + textBox3.Text + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    textBox3.Clear();
                    comboVeriGetir();
                    textBox3.Focus();
                    MessageBox.Show("Seçtiğiniz kullanıcı kaydı başarıyla kaldırıldı.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch { MessageBox.Show("Kayıt sırasında hata meydana geldi, bağlantınızı kontrol edin!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
