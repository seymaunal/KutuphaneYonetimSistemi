using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneSistemi
{
    public partial class OduncKitapİade : Form
    {
        public OduncKitapİade()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Anasayfa fr = new Anasayfa();
            fr.Show();
            this.Hide();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        DataSet daset = new DataSet();

        private void OduncKitaplisteleme()
        {            
            SqlDataAdapter adapter = new SqlDataAdapter("select * from odunckitaplar", bgl.baglanti()); //form açıldığında kayıtlar görünsün
            adapter.Fill(daset, "odunckitaplar");  //kayıtları geçici tabloya aktarıyoruz
            dataGridView1.DataSource = daset.Tables["odunckitaplar"];                     
        }

        private void OduncKitapİade_Load(object sender, EventArgs e)
        {
            OduncKitaplisteleme();
        }
        //Tc'ye Göre Ara textboxı
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["odunckitaplar"].Clear();
            
            SqlDataAdapter adapter = new SqlDataAdapter("select *from odunckitaplar where tc like '%"+textBox1.Text+"%'",bgl.baglanti());
            adapter.Fill(daset, "odunckitaplar");
            dataGridView1.DataSource = daset.Tables["odunckitaplar"];
           
            if (textBox1.Text == "") 
            {
                daset.Tables["odunckitaplar"].Clear();
                OduncKitaplisteleme();               
            }
        }
        //Seri No'ya Göre Ara textboxı
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["odunckitaplar"].Clear();
            
            SqlDataAdapter adapter = new SqlDataAdapter("select * from odunckitaplar where serino like '%" + textBox2.Text + "%'", bgl.baglanti());
            adapter.Fill(daset, "odunckitaplar");
            dataGridView1.DataSource = daset.Tables["odunckitaplar"];
            
            if (textBox2.Text == "")
            {
                daset.Tables["odunckitaplar"].Clear();
                OduncKitaplisteleme();
            }
        }
        //teslim al butonu
        private void button2_Click(object sender, EventArgs e)
        {            
            SqlCommand komut = new SqlCommand("delete from odunckitaplar where tc=@tc AND serino=@serino", bgl.baglanti());
            komut.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells["tc"].Value.ToString());
            komut.Parameters.AddWithValue("@serino", dataGridView1.CurrentRow.Cells["serino"].Value.ToString());
            komut.ExecuteNonQuery();
            MessageBox.Show("Kitap(lar) Teslim Alındı", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
          
            daset.Tables["odunckitaplar"].Clear();
            OduncKitaplisteleme();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
