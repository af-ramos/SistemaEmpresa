using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace INFORMÁTICA__PROJETO_
{
    class Banco
    {
        public static SqlConnection conexao = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Informática.mdf;Integrated Security=True;Connect Timeout=30");
    }
}
