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

namespace INFORMÁTICA__PROJETO_
{
    public partial class Supervisor : Form
    {
        SqlConnection conexao = Banco.conexao;
        SqlCommand comando = new SqlCommand();
        public Supervisor()
        {
            InitializeComponent();
        }

        public void LimparDados()
        {
            maskedTextBox1.Text = maskedTextBox2.Text = textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = "";
            comando.Parameters.Clear();
            maskedTextBox1.Enabled = maskedTextBox2.Enabled = textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = true;
            this.ReiniciarDG();

            Home.setor.atualizarCB();
        }

        private void botaoNovo_Click(object sender, EventArgs e)
        {
            LimparDados();
        }

        private void botaoInserir_Click(object sender, EventArgs e)
        {
            if(verificarVazio())
            {
                MessageBox.Show("Insira os Dados Restantes", "FALTA DE DADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string cpf = maskedTextBox1.Text;
                    string nome = textBox1.Text;
                    string telefone = maskedTextBox2.Text;
                    string endereco = textBox3.Text;
                    string email = textBox2.Text;

                    comando.Connection = conexao;
                    comando.CommandText = "INSERT INTO supervisor (cpf, nome, endereco, telefone, email) VALUES (@cpf, @nome, @endereco, @telefone, @email)";
                    comando.Parameters.AddWithValue("@cpf", cpf);
                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@endereco", endereco);
                    comando.Parameters.AddWithValue("@telefone", telefone);
                    comando.Parameters.AddWithValue("@email", email);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Supervisor Inserido!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception erro)
                {
                    MessageBox.Show("Erro -> " + erro.Message, "ERRO | DADOS POSSIVELMENTE REPETIDOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conexao.Close();
                    LimparDados();
                }
            }
            
        }

        private void botaoAtualizar_Click(object sender, EventArgs e)
        {
            if (verificarVazio())
            {
                MessageBox.Show("Insira os Dados Restantes", "FALTA DE DADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string cpf = maskedTextBox1.Text;
                    string nome = textBox1.Text;
                    string telefone = maskedTextBox2.Text;
                    string endereco = textBox2.Text;
                    string email = textBox3.Text;

                    comando.Connection = conexao;
                    comando.CommandText = "UPDATE supervisor SET nome=@nome, endereco=@endereco, telefone=@telefone, email=@email WHERE cpf=@cpf";
                    comando.Parameters.AddWithValue("@cpf", cpf);
                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@telefone", telefone);
                    comando.Parameters.AddWithValue("@endereco", endereco);
                    comando.Parameters.AddWithValue("@email", email);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Supervisor Alterado!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception erro)
                {
                    MessageBox.Show("Erro -> " + erro.Message, "ERRO | DADOS POSSIVELMENTE REPETIDOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conexao.Close();
                    LimparDados();
                }
            }
            
        }

        private void botaoExcluir_Click(object sender, EventArgs e)
        {
            if (verificarVazio())
            {
                MessageBox.Show("Insira os Dados Restantes", "FALTA DE DADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string cpf = maskedTextBox1.Text;

                    comando.Connection = conexao;
                    comando.CommandText = "DELETE FROM supervisor WHERE cpf=@cpf";
                    comando.Parameters.AddWithValue("@cpf", cpf);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Supervisor Removido!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception erro)
                {
                    MessageBox.Show("Erro (Dado utilizado em uma tabela) -> " + erro.Message, "DADO SENDO UTILIZADO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    conexao.Close();
                    LimparDados();
                }
            }
            
        }

        public void ReiniciarDG()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT cpf as 'CPF', nome as 'Nome', endereco as 'Endereco', telefone as 'Telefone', email as 'Email' FROM supervisor", conexao);
            DataSet ds = new DataSet();

            try
            {
                conexao.Open();
                da.Fill(ds, "Tabela");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Tabela";
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro -> " + erro.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }
        }

        public bool verificarVazio()
        {
            if (textBox1.Text == "")
            {
                return true;
            }
            if (textBox3.Text == "")
            {
                return true;
            }
            if (textBox2.Text == "")
            {
                return true;
            }
            if (maskedTextBox1.Text == "")
            {
                return true;
            }
            if (maskedTextBox2.Text == "")
            {
                return true;
            }
            return false;
        }

        private void Supervisor_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT cpf as 'CPF', nome as 'Nome', endereco as 'Endereco', telefone as 'Telefone', email as 'Email' FROM supervisor", conexao);
            DataSet ds = new DataSet();

            try
            {
                conexao.Open();
                da.Fill(ds, "Tabela");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Tabela";
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro -> " + erro.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string criterio = textBox4.Text;
            string comando = "";

            if (radioButton1.Checked)
            {
                comando = "SELECT cpf as 'CPF', nome as 'Nome', endereco as 'Endereco', telefone as 'Telefone', email as 'Email' FROM supervisor WHERE cpf LIKE '%" + criterio + "%'";
            }
            if (radioButton2.Checked)
            {
                comando = "SELECT cpf as 'CPF', nome as 'Nome', endereco as 'Endereco', telefone as 'Telefone', email as 'Email' FROM supervisor WHERE nome LIKE '%" + criterio + "%'";
            }
            if (radioButton3.Checked)
            {
                comando = "SELECT cpf as 'CPF', nome as 'Nome', endereco as 'Endereco', telefone as 'Telefone', email as 'Email' FROM supervisor WHERE endereco LIKE '%" + criterio + "%'";
            }
            if (radioButton4.Checked)
            {
                comando = "SELECT cpf as 'CPF', nome as 'Nome', endereco as 'Endereco', telefone as 'Telefone', email as 'Email' FROM supervisor WHERE telefone LIKE '%" + criterio + "%'";
            }
            if (radioButton5.Checked)
            {
                comando = "SELECT cpf as 'CPF', nome as 'Nome', endereco as 'Endereco', telefone as 'Telefone', email as 'Email' FROM supervisor WHERE email LIKE '%" + criterio + "%'";
            }

            SqlDataAdapter da = new SqlDataAdapter(comando, conexao);
            DataSet ds = new DataSet();

            try
            {
                conexao.Open();
                da.Fill(ds, "Tabela");

                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Tabela";
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro -> " + erro.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            maskedTextBox1.Text = (dataGridView1.Rows[e.RowIndex].Cells[0].Value).ToString();
            textBox1.Text = (dataGridView1.Rows[e.RowIndex].Cells[1].Value).ToString();
            maskedTextBox2.Text = (dataGridView1.Rows[e.RowIndex].Cells[3].Value).ToString();
            textBox2.Text = (dataGridView1.Rows[e.RowIndex].Cells[4].Value).ToString();
            textBox3.Text = (dataGridView1.Rows[e.RowIndex].Cells[2].Value).ToString();
            maskedTextBox1.Enabled = false;
        }

        private void Supervisor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Home.supervisor = new Supervisor();
        }
    }
}
