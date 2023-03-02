using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMGAL_vC
{
    public partial class SQLPanel : Form
    {
        public SQLPanel()
        {
            InitializeComponent();
        }

        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\Database\\Database.mdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataReader dr;

        void reDb()
        {
            conn.Open();
            cmd = new OleDbCommand("Select * From Veritabani", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Genel.dbHost = dr["dbHost"].ToString();
                Genel.dbName = dr["dbName"].ToString();
                Genel.dbUserName = dr["dbUserName"].ToString();
                Genel.dbPass = dr["dbPass"].ToString();
                MySQL.conString = "Server=" + dr["dbHost"].ToString() + "; Database=" + dr["dbName"].ToString() + "; UID=" + dr["dbUserName"].ToString() + "; Password=" + dr["dbPass"].ToString();
            }
            conn.Close();
        }

        private void pKaydet_Click(object sender, EventArgs e)
        {
            if (dbHost.Text != "" && dbName.Text != "" && dbUserName.Text != "" && dbPass.Text != "")
            {
                this.Enabled = false;
                try
                {
                    conn.Open();
                    cmd = new OleDbCommand("Delete From Veritabani", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    conn.Open();
                    cmd = new OleDbCommand("Insert Into Veritabani(dbHost,dbName,dbUserName,dbPass) values('" + dbHost.Text + "', '" + dbName.Text + "', '" + dbUserName.Text + "','" + dbPass.Text + "')", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    reDb();
                    MessageBox.Show("SQL Veritabanı bilgileri kaydedildi.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch { MessageBox.Show("Veritabanı bağlantısını güncelleme sırasında bir hata meydana geldi!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
                this.Enabled = false;
            }
        }

        private void SQLPanel_Load(object sender, EventArgs e)
        {
            try {
                reDb();
                dbHost.Text = Genel.dbHost;
                dbName.Text = Genel.dbName;
                dbUserName.Text = Genel.dbUserName;
                dbPass.Text = Genel.dbPass;
            }
            catch { MessageBox.Show("SQLPanel açılırken bir hata meydana geldi bu sorunu yöneticilere bildirin!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error); this.Close(); }
        }
    }
}
