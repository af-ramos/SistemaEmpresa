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
    public partial class Cliente : Form
    {
        SqlConnection conexao = Banco.conexao;
        SqlCommand comando = new SqlCommand();
        public Cliente()
        {
            InitializeComponent();
        }

        public bool verficarVazio()
        {
            if(textBox1.Text == "")
            {
                return true;
            }
            if (textBox3.Text == "")
            {
                return true;
            }
            if (textBox5.Text == "")
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

        public void LimparDados()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = maskedTextBox1.Text = maskedTextBox2.Text = "";
            textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = textBox4.Enabled = textBox5.Enabled = maskedTextBox1.Enabled = maskedTextBox2.Enabled = true;
            comando.Parameters.Clear();
            this.ReiniciarDG();

            Home.projeto.atualizarCB();
        }

        public void ReiniciarDG()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT cnpj as 'CNPJ', nome as 'Nome', endereco as 'Endereco', tipo as 'Tipo', telefone as 'Telefone', email as 'Email' FROM cliente", conexao);
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

        private void botaoNovo_Click(object sender, EventArgs e)
        {
            LimparDados();
        }

        private void botaoInserir_Click(object sender, EventArgs e)
        {

            if(verficarVazio())
            {
                MessageBox.Show("Insira os Dados Restantes", "FALTA DE DADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string cnpj = maskedTextBox1.Text;
                    string nome = textBox1.Text;
                    string endereco = textBox3.Text;
                    string tipo = textBox5.Text;
                    string telefone = maskedTextBox2.Text;
                    string email = textBox2.Text;

                    comando.Connection = conexao;
                    comando.CommandText = "INSERT INTO cliente (cnpj, nome, endereco, tipo, telefone, email) VALUES (@cnpj, @nome, @endereco, @tipo, @telefone, @email)";
                    comando.Parameters.AddWithValue("@cnpj", cnpj);
                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@endereco", endereco);
                    comando.Parameters.AddWithValue("@tipo", tipo);
                    comando.Parameters.AddWithValue("@telefone", telefone);
                    comando.Parameters.AddWithValue("@email", email);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Cliente Inserido!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (verficarVazio())
            {
                MessageBox.Show("Insira os Dados Restantes", "FALTA DE DADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string cnpj = maskedTextBox1.Text;
                    string nome = textBox1.Text;
                    string endereco = textBox3.Text;
                    string tipo = textBox5.Text;
                    string telefone = maskedTextBox2.Text;
                    string email = textBox2.Text;

                    comando.Connection = conexao;
                    comando.CommandText = "UPDATE cliente SET nome=@nome, endereco=@endereco, tipo=@tipo, telefone=@telefone, email=@email WHERE cnpj=@cnpj";
                    comando.Parameters.AddWithValue("@cnpj", cnpj);
                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@tipo", tipo);
                    comando.Parameters.AddWithValue("@telefone", telefone);
                    comando.Parameters.AddWithValue("@endereco", endereco);
                    comando.Parameters.AddWithValue("@email", email);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Cliente Alterado!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (verficarVazio())
            {
                MessageBox.Show("Insira os Dados Restantes", "FALTA DE DADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string cnpj = maskedTextBox1.Text;

                    comando.Connection = conexao;
                    comando.CommandText = "DELETE FROM cliente WHERE cnpj=@cnpj";
                    comando.Parameters.AddWithValue("@cnpj", cnpj);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Cliente Removido!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Cliente_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT cnpj as 'CNPJ', nome as 'Nome', endereco as 'Endereco', tipo as 'Tipo', telefone as 'Telefone', email as 'Email' FROM cliente", conexao);
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
            string comando = "";
            string criterio = textBox4.Text;

            if (radioButton1.Checked)
            {
                comando = "SELECT cnpj as 'CNPJ', nome as 'Nome', endereco as 'Endereco', tipo as 'Tipo', telefone as 'Telefone', email as 'Email' FROM cliente WHERE cnpj LIKE '%" + criterio + "%'";
            }
            if (radioButton2.Checked)
            {
                comando = "SELECT cnpj as 'CNPJ', nome as 'Nome', endereco as 'Endereco', tipo as 'Tipo', telefone as 'Telefone', email as 'Email' FROM cliente WHERE nome LIKE '%" + criterio + "%'";
            }
            if (radioButton3.Checked)
            {
                comando = "SELECT cnpj as 'CNPJ', nome as 'Nome', endereco as 'Endereco', tipo as 'Tipo', telefone as 'Telefone', email as 'Email' FROM cliente WHERE endereco LIKE '%" + criterio + "%'";
            }
            if (radioButton4.Checked)
            {
                comando = "SELECT cnpj as 'CNPJ', nome as 'Nome', endereco as 'Endereco', tipo as 'Tipo', telefone as 'Telefone', email as 'Email' FROM cliente WHERE tipo LIKE '%" + criterio + "%'";
            }
            if (radioButton5.Checked)
            {
                comando = "SELECT cnpj as 'CNPJ', nome as 'Nome', endereco as 'Endereco', tipo as 'Tipo', telefone as 'Telefone', email as 'Email' FROM cliente WHERE telefone LIKE '%" + criterio + "%'";
            }
            if (radioButton6.Checked)
            {
                comando = "SELECT cnpj as 'CNPJ', nome as 'Nome', endereco as 'Endereco', tipo as 'Tipo', telefone as 'Telefone', email as 'Email' FROM cliente WHERE email LIKE '%" + criterio + "%'";
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
            textBox3.Text = (dataGridView1.Rows[e.RowIndex].Cells[2].Value).ToString();
            textBox5.Text = (dataGridView1.Rows[e.RowIndex].Cells[3].Value).ToString();
            maskedTextBox2.Text = (dataGridView1.Rows[e.RowIndex].Cells[4].Value).ToString();
            textBox2.Text = (dataGridView1.Rows[e.RowIndex].Cells[5].Value).ToString();
            maskedTextBox1.Enabled = false;
        }

        private void Cliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            Home.cliente = new Cliente();
        }
    }
}
