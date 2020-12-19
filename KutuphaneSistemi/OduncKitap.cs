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
    public partial class OduncKitap : Form
    {
        public OduncKitap()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        DataSet daset = new DataSet();

        private void button2_Click(object sender, EventArgs e)
        {
            Anasayfa fr = new Anasayfa();
            fr.Show();
            this.Hide();
        }  
        //sepettekileri listelemek için method oluşturuldu.
        private void sepetListele()
        {            
            SqlDataAdapter adapter = new SqlDataAdapter("select * from sepet",bgl.baglanti());
            adapter.Fill(daset, "sepet");
            dataGridView1.DataSource = daset.Tables["sepet"];            
        }


        //ekle butonu
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "")
            {
                
                SqlCommand komut = new SqlCommand("insert into sepet(serino,kitapadi,yazari,turu,yayinevi,teslimtarihi,iadetarihi) values (@serino,@kitapadi,@yazari,@turu,@yayinevi,@teslimtarihi,@iadetarihi)", bgl.baglanti());
                komut.Parameters.AddWithValue("@serino", textBox4.Text);
                komut.Parameters.AddWithValue("@kitapadi", textBox5.Text);
                komut.Parameters.AddWithValue("@yazari", textBox6.Text);
                komut.Parameters.AddWithValue("@turu", comboBox1.Text);
                komut.Parameters.AddWithValue("@yayinevi", textBox7.Text);
                komut.Parameters.AddWithValue("@teslimtarihi", dateTimePicker1.Text);
                komut.Parameters.AddWithValue("@iadetarihi", dateTimePicker2.Text);

                komut.ExecuteNonQuery();
                

                MessageBox.Show("Kitap(lar) Sepete Eklendi", "Ekleme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                daset.Tables["sepet"].Clear(); //tabloyu temizler
                sepetListele(); //listeleme
            }
            else
            {
                MessageBox.Show("Kitap Bilgilerini Eksiksiz Doldurmalısınız'", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            foreach (Control item in groupBox2.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        private void OduncKitap_Load(object sender, EventArgs e)
        {
            sepetListele();
        }

        //tc no textboxı
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            SqlCommand komut = new SqlCommand("select * from uyekayit where tc like'"+textBox1.Text+"'",bgl.baglanti());
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox2.Text = read["adsoyad"].ToString();
                textBox3.Text = read["telefon"].ToString();
            }

            
            if (textBox1.Text=="")
            {
                foreach (Control item in  groupBox1.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                    
                }
            }
                       
        }

        //serino textboxı
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
            SqlCommand komut = new SqlCommand("select * from kitapkayit where serino like '"+textBox4.Text+"'", bgl.baglanti());
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read()) //kayıtlar okunduğu sürece demek
            {
                textBox5.Text = read["kitapadi"].ToString();
                textBox6.Text = read["yazari"].ToString();
                comboBox1.Text = read["turu"].ToString();
                textBox7.Text = read["yayinevi"].ToString();
                
            }
            
            if (textBox4.Text=="")
            {
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
        }
            
        //sil butonu
        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu Kaydı Silmek İstiyor Musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog==DialogResult.Yes)
            {
                
                SqlCommand komut = new SqlCommand("delete from sepet where serino='" + dataGridView1.CurrentRow.Cells["serino"].Value.ToString() + "'", bgl.baglanti());
                komut.ExecuteNonQuery();
                
                MessageBox.Show("Silme İşlemi Başarıyla Gerçekleşti.", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                daset.Tables["sepet"].Clear();
                sepetListele();
            }
            

        }
        //ödünç ver butonu
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!=""&&textBox2.Text!=""&&textBox3.Text!="")
            {
                for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                {                    
                    SqlCommand komut = new SqlCommand("insert into odunckitaplar(tc,adsoyad,telefon,serino,kitapadi,yazari,turu,yayinevi,teslimtarihi,iadetarihi) values (@tc,@adsoyad,@telefon,@serino,@kitapadi,@yazari,@turu,@yayinevi,@teslimtarihi,@iadetarihi)", bgl.baglanti());
                    komut.Parameters.AddWithValue("@tc", textBox1.Text);
                    komut.Parameters.AddWithValue("@adsoyad", textBox2.Text);
                    komut.Parameters.AddWithValue("@telefon", textBox3.Text);
                    komut.Parameters.AddWithValue("@serino", dataGridView1.Rows[i].Cells["serino"].Value.ToString());
                    komut.Parameters.AddWithValue("@kitapadi", dataGridView1.Rows[i].Cells["kitapadi"].Value.ToString());
                    komut.Parameters.AddWithValue("@yazari", dataGridView1.Rows[i].Cells["yazari"].Value.ToString());
                    komut.Parameters.AddWithValue("@turu", dataGridView1.Rows[i].Cells["turu"].Value.ToString());
                    komut.Parameters.AddWithValue("@yayinevi", dataGridView1.Rows[i].Cells["yayinevi"].Value.ToString());
                    komut.Parameters.AddWithValue("@teslimtarihi", dataGridView1.Rows[i].Cells["teslimtarihi"].Value.ToString());
                    komut.Parameters.AddWithValue("@iadetarihi", dataGridView1.Rows[i].Cells["iadetarihi"].Value.ToString());
                    komut.ExecuteNonQuery();
                    
                    MessageBox.Show("Ödünç Verme İşlemi Başarıyla Gerçekleşti.", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    daset.Tables["sepet"].Clear();
                    sepetListele();
                    textBox1.Text = "";
                }                
            }
            else
            {
                MessageBox.Show("Üye Bilgilerini Eksiksiz Doldurmalısınız'", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
