using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace KutuphaneSistemi
{
    class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=DESKTOP-D092U0J;Initial Catalog=KutuphaneSistemi;Integrated Security=True");
            baglan.Open();
            return baglan;

        }
    }
}
