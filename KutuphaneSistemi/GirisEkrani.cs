using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace KutuphaneSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void button1_Click(object sender, EventArgs e)
        {
            
            SqlCommand komut = new SqlCommand("Select * from admin where admin_ad=@adi AND admin_sifre=@sifresi", bgl.baglanti());
            komut.Parameters.AddWithValue("adi", textBox1.Text);
            komut.Parameters.AddWithValue("sifresi", textBox2.Text);

            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                MessageBox.Show("Giriş Başarılı", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AcilisEkrani fr = new AcilisEkrani();
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı!","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
                     
        }

        //Mouse butonun üzerine geldiğinde rengi aliceblue olacak.
        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.AliceBlue;
        }
        //Mouse butonun üzeinden ayrıldığında rengi yine eski rengine dönecek.
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.BurlyWood;
        }
    }
}
