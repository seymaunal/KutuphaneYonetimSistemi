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
    public partial class Kitap_Kayit : Form
    {
        public Kitap_Kayit()
        {
            InitializeComponent();
        }     

        private void Kitap_Kayit_Load(object sender, EventArgs e)
        {
           
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void button3_Click(object sender, EventArgs e)
        {
            Anasayfa fr = new Anasayfa();
            fr.Show();
            this.Hide();
        }
        //ekle butonu
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text!= "" && textBox5.Text!= "")
            {
                SqlCommand komut = new SqlCommand("insert into kitapkayit(serino,kitapadi,yazari,turu,sayfasayisi,yayinevi,basimyili,rafno,kayittarihi)values(@serino,@kitapadi,@yazari,@turu,@sayfasayisi,@yayinevi,@basimyili,@rafno,@kayittarihi)", bgl.baglanti());
                komut.Parameters.AddWithValue("@serino", textBox1.Text);
                komut.Parameters.AddWithValue("@kitapadi", textBox2.Text);
                komut.Parameters.AddWithValue("@yazari", textBox3.Text);
                komut.Parameters.AddWithValue("@turu", comboBox1.Text);
                komut.Parameters.AddWithValue("@sayfasayisi", textBox4.Text);
                komut.Parameters.AddWithValue("@yayinevi", textBox5.Text);
                komut.Parameters.AddWithValue("@basimyili", textBox6.Text);
                komut.Parameters.AddWithValue("@rafno", textBox7.Text);
                komut.Parameters.AddWithValue("@kayittarihi", DateTime.Now.ToShortDateString());

                komut.ExecuteNonQuery();

                MessageBox.Show("Kitap Kayıt İşlemi Yapıldı.", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);


                //kayıt işleminde sonra textlerin içinin temizlenmesi
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }

            }
            else
            {
                MessageBox.Show("Kitap Bilgilerini Eksiksiz Doldurmalısınız'", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            deneme deneme = new deneme();
            roman roman = new roman();
            oyku oyku = new oyku();
            siir siir = new siir();
            isletme isletme = new isletme();
            bilisim bilisim = new bilisim();

            if (comboBox1.SelectedIndex == 0)
            {
                roman.rafno = "Raf No'yu 51-100 arasında seçiniz.";
                label9.Text = roman.rafno.ToString();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                deneme.rafno = "Raf No'yu 1-50 arasında seçiniz";
                label9.Text = deneme.rafno.ToString();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                oyku.rafno = "Raf No'yu 101-150 arasında seçiniz";
                label9.Text = oyku.rafno.ToString();
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                siir.rafno = "Raf No'yu 151-200 arasında seçiniz";
                label9.Text = siir.rafno.ToString();
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                isletme.rafno = "Raf No'yu 250-300 arasında seçiniz";
                label9.Text = isletme.rafno.ToString();
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                bilisim.rafno = "Raf No'yu 350-400 arasında seçiniz";
                label9.Text = bilisim.rafno.ToString();
            }
        }

       
    }
}
