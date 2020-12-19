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
    public partial class OduncKitapListeleme : Form
    {
        public OduncKitapListeleme()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Anasayfa fr = new Anasayfa();
            fr.Show();
            this.Hide();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        DataSet daset = new DataSet();

        private void OduncKitapListeleme_Load(object sender, EventArgs e)
        {
            OduncKitaplisteleme();
            //form yüklenince comboboxın birinci seçeneği seçili gelsiin
            comboBox1.SelectedIndex = 0;
        }
        //method
        private void OduncKitaplisteleme()
        {
            
            SqlDataAdapter adapter = new SqlDataAdapter("select * from odunckitaplar", bgl.baglanti()); //form açıldığında kayıtlar görünsün
            adapter.Fill(daset, "odunckitaplar");  //kayıtları geçici tabloya aktarıyoruz
            dataGridView1.DataSource = daset.Tables["odunckitaplar"];
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            daset.Tables["odunckitaplar"].Clear();
            if (comboBox1.SelectedIndex==0)
            {
                OduncKitaplisteleme();
            }
            else if (comboBox1.SelectedIndex==1)
            {
                
                SqlDataAdapter adapter = new SqlDataAdapter("select * from odunckitaplar where '"+DateTime.Now.ToShortDateString()+"'>iadetarihi ", bgl.baglanti()); 
                adapter.Fill(daset, "odunckitaplar");  //kayıtları geçici tabloya aktarıyoruz
                dataGridView1.DataSource = daset.Tables["odunckitaplar"];
                
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                
                SqlDataAdapter adapter = new SqlDataAdapter("select * from odunckitaplar where '" + DateTime.Now.ToShortDateString() + "'<= iadetarihi ", bgl.baglanti());
                adapter.Fill(daset, "odunckitaplar");  //kayıtları geçici tabloya aktarıyoruz
                dataGridView1.DataSource = daset.Tables["odunckitaplar"];
                
            }
        }
        //worde aktar butonu
        private void button2_Click(object sender, EventArgs e)
        {
            int satırSayisi = dataGridView1.Rows.Count;
            int sutunSayisi = 10;

            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc";

            word.Application wordApp = new word.Application();
            word.Document wordDoc = wordApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            wordApp.Visible = true;

            object oRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oRng = wordDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            word.Paragraph paragraf;
            paragraf = wordDoc.Content.Paragraphs.Add(ref oRng);
            paragraf.Range.Text = "ÖDÜNÇ KİTAP LİSTESİ";
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
                for (int j = 0; j < 10; j++)
                {
                    tablo.Rows[i + 1].Cells[j + 1].Range.Text = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
        }

        //txt aktar butonu
        private void button5_Click(object sender, EventArgs e)
        {
            TextWriter sw = new StreamWriter(@"C:\Users\user\Desktop\\filtrelikitaplisteleme.txt");
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
                         + dataGridView1.Rows[i].Cells[7].Value.ToString() + "\t"
                          + dataGridView1.Rows[i].Cells[8].Value.ToString() + "\t"
                           + dataGridView1.Rows[i].Cells[9].Value.ToString() + "\t");

            }
            sw.Close();
            MessageBox.Show("Metin Belgesine Aktarım İşlemi Başarılı ", "Tebrikler");
        }
    }
}
