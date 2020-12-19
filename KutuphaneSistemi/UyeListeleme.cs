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
    public partial class UyeListeleme : Form
    {
        public UyeListeleme()
        {
            InitializeComponent();
        }

        //iptal butonu
        private void button2_Click(object sender, EventArgs e)
        {
            Anasayfa fr = new Anasayfa();
            fr.Show();
            this.Hide();
        }

        //datagridview deki verilere tıklanınca textboxlara geçmesini sağlıyor
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
        }
        
        sqlbaglantisi bgl = new sqlbaglantisi();
        DataSet daset = new DataSet();

        //üye tc no textboxı
        private void textBox1_TextChanged(object sender, EventArgs e)
        {           
            SqlCommand komut = new SqlCommand("select * from uyekayit where tc like'"+textBox1.Text+"'", bgl.baglanti());
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                textBox2.Text = read["adsoyad"].ToString();
                dateTimePicker1.Text = read["dogumtarihi"].ToString();
                textBox4.Text = read["telefon"].ToString();
                textBox5.Text = read["email"].ToString();
            }            
        }
        
        //uyetc ara textboxı
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["uyekayit"].Clear();
           
            SqlDataAdapter adapter = new SqlDataAdapter("select * from uyekayit where tc like '%"+textBox6.Text+"%'", bgl.baglanti());
            adapter.Fill(daset,"uyekayit");
            dataGridView1.DataSource = daset.Tables["uyekayit"];          
        }

        //sil butonu
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu Kaydı Silmek İstiyor Musunuz?", "Sil",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
            if (dialog==DialogResult.Yes)
            {
              
                SqlCommand komut = new SqlCommand("delete from uyekayit where tc=@tc", bgl.baglanti());
                komut.Parameters.AddWithValue("@tc", dataGridView1.CurrentRow.Cells["tc"].Value.ToString());
                komut.ExecuteNonQuery();
                
                MessageBox.Show("Silme İşlemi Başarıyla Gerçekleşti.", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                daset.Tables["uyekayit"].Clear();
                uyelistele();

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

        private void uyelistele()
        {
            
            SqlDataAdapter adapter = new SqlDataAdapter("select * from uyekayit",bgl.baglanti());
            adapter.Fill(daset,"uyekayit");
            dataGridView1.DataSource = daset.Tables["uyekayit"];
            
        }

        private void UyeListeleme_Load(object sender, EventArgs e)
        {
            uyelistele();
        }

        //güncelle butonu
        private void button1_Click(object sender, EventArgs e)
        {
            
            SqlCommand komut = new SqlCommand("update uyekayit set adsoyad=@adsoyad,dogumtarihi=@dogumtarihi,telefon=@telefon,email=@email where tc=@tc",bgl.baglanti());
            komut.Parameters.AddWithValue("@tc", textBox1.Text);
            komut.Parameters.AddWithValue("@adsoyad", textBox2.Text);
            komut.Parameters.AddWithValue("@dogumtarihi", dateTimePicker1.Text);
            komut.Parameters.AddWithValue("@telefon", textBox4.Text);
            komut.Parameters.AddWithValue("@email", textBox5.Text);
            komut.ExecuteNonQuery();

            
            MessageBox.Show("Güncelleme İşlemi Başarıyla Gerçekleşti.", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);

            daset.Tables["uyekayit"].Clear();
            uyelistele();

            //işlem bittikten sonra texboxların içinin temizlenmesi
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }

        }
        //worde aktar butonu
        private void button4_Click(object sender, EventArgs e)
        {
            int satırSayisi = dataGridView1.Rows.Count;
            int sutunSayisi = 5;

            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc";

            word.Application wordApp = new word.Application();
            word.Document wordDoc = wordApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            wordApp.Visible = true;

            object oRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            word.Paragraph paragraf;
            paragraf = wordDoc.Content.Paragraphs.Add(ref oRng);
            paragraf.Range.Text = "ÜYE LİSTESİ";
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

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    tablo.Rows[i + 1].Cells[j + 1].Range.Text = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
        }

        //txt aktar butonu
        private void button5_Click(object sender, EventArgs e)
        {
            TextWriter sw = new StreamWriter(@"C:\Users\user\Desktop\\uyelisteleme.txt");
            int rowcount = dataGridView1.Rows.Count;
            for (int i = 0; i < rowcount - 1; i++)
            {
                sw.WriteLine(dataGridView1.Rows[i].Cells[0].Value.ToString() + "\t"
                    + dataGridView1.Rows[i].Cells[1].Value.ToString() + "\t"
                    + dataGridView1.Rows[i].Cells[2].Value.ToString() + "\t"
                     + dataGridView1.Rows[i].Cells[3].Value.ToString() + "\t"
                      + dataGridView1.Rows[i].Cells[4].Value.ToString() + "\t");
                
            }
            sw.Close();
            MessageBox.Show("Metin Belgesine Aktarım İşlemi Başarılı ", "Tebrikler");
        }

        
    }
}
