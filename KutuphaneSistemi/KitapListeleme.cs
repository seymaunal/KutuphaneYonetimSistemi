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
using Microsoft.Office.Interop.Word;
using word = Microsoft.Office.Interop.Word;
using System.IO;


namespace KutuphaneSistemi
{
    public partial class KitapListeleme : Form
    {
        public KitapListeleme()
        {
            InitializeComponent();
        }

        //iptal butonu
        private void button3_Click(object sender, EventArgs e)
        {
            Anasayfa fr = new Anasayfa();
            fr.Show();
            this.Hide();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        DataSet daset = new DataSet();
        

        private void kitaplistele()
        {
             SqlDataAdapter adapter = new SqlDataAdapter("select * from kitapkayit", bgl.baglanti());
            adapter.Fill(daset, "kitapkayit");
            dataGridView1.DataSource = daset.Tables["kitapkayit"];
        }

        private void KitapListeleme_Load(object sender, EventArgs e)
        {
            kitaplistele();
        }

        //sil butonu
        private void button3_Click_1(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu Kaydı Silmek İstiyor Musunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {
               
                SqlCommand komut = new SqlCommand("delete from kitapkayit where serino=@serino", bgl.baglanti());
                komut.Parameters.AddWithValue("@serino", dataGridView1.CurrentRow.Cells["serino"].Value.ToString());
                komut.ExecuteNonQuery();
                
                MessageBox.Show("Silme İşlemi Başarıyla Gerçekleşti.", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                daset.Tables["kitapkayit"].Clear();
                kitaplistele();

                //işlem bittikten sonra texboxların içinin temizlenmesi
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
        }

        //güncelle butonu
        private void button1_Click(object sender, EventArgs e)
        {
            
            SqlCommand komut = new SqlCommand("update kitapkayit set kitapadi=@kitapadi,yazari=@yazari,turu=@turu,sayfasayisi=@sayfasayisi,yayinevi=@yayinevi,basimyili=@basimyili,rafno=@rafno where serino=@serino", bgl.baglanti());
            komut.Parameters.AddWithValue("@serino", textBox1.Text);
            komut.Parameters.AddWithValue("@kitapadi", textBox2.Text);
            komut.Parameters.AddWithValue("@yazari", textBox3.Text);
            komut.Parameters.AddWithValue("@turu", comboBox1.Text);
            komut.Parameters.AddWithValue("@sayfasayisi", textBox4.Text);
            komut.Parameters.AddWithValue("@yayinevi", textBox5.Text);
            komut.Parameters.AddWithValue("@basimyili", textBox6.Text);
            komut.Parameters.AddWithValue("@rafno", textBox7.Text);
            komut.ExecuteNonQuery();

            
            MessageBox.Show("Güncelleme İşlemi Başarıyla Gerçekleşti.", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);

            daset.Tables["kitapkayit"].Clear();
            kitaplistele();

            //işlem bittikten sonra texboxların içinin temizlenmesi
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
        }

        //kitapseri no ara textboxı
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["kitapkayit"].Clear();
            
            SqlDataAdapter adapter = new SqlDataAdapter("select * from kitapkayit where serino like '%" + textBox8.Text + "%'", bgl.baglanti());
            adapter.Fill(daset, "kitapkayit");
            dataGridView1.DataSource = daset.Tables["kitapkayit"];
            
        }

        //kitapseri no textboxı
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            SqlCommand komut = new SqlCommand("select * from kitapkayit where serino like '" + textBox1.Text + "'", bgl.baglanti());
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox2.Text = read["kitapadi"].ToString();
                textBox3.Text = read["yazari"].ToString();
                comboBox1.Text = read["turu"].ToString();
                textBox4.Text = read["sayfasayisi"].ToString();
                textBox5.Text = read["yayinevi"].ToString();
                textBox6.Text = read["basimyili"].ToString();
                textBox7.Text = read["rafno"].ToString();

            }
            
        }

        //datagridview deki verilere tıklanınca textboxlara geçmesini sağlıyor
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["serino"].Value.ToString();
        }

        //worde aktar butonu
        private void button4_Click(object sender, EventArgs e)
        {
            
            int satırSayisi = dataGridView1.Rows.Count;
            int sutunSayisi = 9;

            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc";

            word.Application wordApp = new word.Application();
            word.Document wordDoc = wordApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            wordApp.Visible = true;

            object oRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            
            word.Paragraph paragraf;
            paragraf = wordDoc.Content.Paragraphs.Add(ref oRng);
            paragraf.Range.Text = "KİTAP LİSTESİ";
            paragraf.Range.Font.Shadow = 0;
            paragraf.Range.Font.Size = 16;
            paragraf.Range.Paragraphs.LeftIndent = 50;
            paragraf.Format.SpaceAfter = 10;
            paragraf.Range.InsertParagraphAfter();

            word.Range wrdRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            word.Table tablo = wordDoc.Tables.Add(wrdRng, satırSayisi, sutunSayisi, ref oMissing, ref oMissing);

            tablo.Borders.InsideLineStyle = word.WdLineStyle.wdLineStyleSingle;
            tablo.Borders.OutsideLineStyle = word.WdLineStyle.wdLineStyleSingle;

            tablo.Range.ParagraphFormat.SpaceAfter = 10;
            
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    tablo.Rows[i + 1].Cells[j + 1].Range.Text = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
        
        }
        //txt aktar butonu
        private void button5_Click(object sender, EventArgs e)
        {


            TextWriter sw = new StreamWriter(@"C:\Users\user\Desktop\\kitaplisteleme.txt");
            int rowcount = dataGridView1.Rows.Count;
            for (int i = 0; i < rowcount - 1; i++)
            {
                sw.WriteLine(dataGridView1.Rows[i].Cells[0].Value.ToString() + "\t"
                    + dataGridView1.Rows[i].Cells[1].Value.ToString() + "\t"
                    + dataGridView1.Rows[i].Cells[2].Value.ToString() + "\t"
                     + dataGridView1.Rows[i].Cells[3].Value.ToString() + "\t"
                      + dataGridView1.Rows[i].Cells[4].Value.ToString() + "\t"
                       + dataGridView1.Rows[i].Cells[5].Value.ToString() + "\t"
                        + dataGridView1.Rows[i].Cells[6].Value.ToString() + "\t"
                         + dataGridView1.Rows[i].Cells[7].Value.ToString() + "\t");
                
            }
            sw.Close();
            MessageBox.Show("Metin Belgesine Aktarım İşlemi Başarılı ", "Tebrikler");


        }
    }
}
