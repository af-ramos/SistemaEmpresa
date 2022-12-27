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
    public partial class Especialidade : Form
    {
        SqlConnection conexao = Banco.conexao;
        SqlCommand comando = new SqlCommand();

        public Especialidade()
        {
            InitializeComponent();
        }

        public bool verficarVazio()
        {
            if (textBox1.Text == "")
            {
                return true;
            }
            if (textBox2.Text == "")
            {
                return true;
            }
            return false;
        }

        public void LimparDados()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = "";
            comando.Parameters.Clear();
            textBox1.Enabled = textBox2.Enabled = true;
            this.ReiniciarDataGrid();

            Home.funcionarioEspecialidade.atualizarCB();
        }

        public void ReiniciarDataGrid()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT id_especialidade as 'ID', descricao as 'Descrição' FROM especialidade", conexao);
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
            bool eNumero;
            int id = 0;
            if(verficarVazio())
            {
                MessageBox.Show("Insira os Dados Restantes", "FALTA DE DADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    id = int.Parse(textBox1.Text);
                    eNumero = true;
                }
                catch (Exception erro)
                {
                    MessageBox.Show("Erro -> " + erro.Message, "ID NÃO NUMÉRICO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    eNumero = false;
                    textBox1.Text = "";
                }
                if (eNumero)
                {
                    try
                    {
                        string descricao = textBox2.Text;

                        comando.Connection = conexao;
                        comando.CommandText = "INSERT INTO especialidade (id_especialidade, descricao) VALUES (@id, @descricao)";
                        comando.Parameters.AddWithValue("@id", id);
                        comando.Parameters.AddWithValue("@descricao", descricao);
                        conexao.Open();
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Especialidade Inserida!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    int id = int.Parse(textBox1.Text);
                    string descricao = textBox2.Text;

                    comando.Connection = conexao;
                    comando.CommandText = "UPDATE especialidade SET descricao=@descricao WHERE id_especialidade=@id";
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@descricao", descricao);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Especialidade Alterada!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if(verficarVazio())
            {
                MessageBox.Show("Insira os Dados Restantes", "FALTA DE DADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    int id = int.Parse(textBox1.Text);

                    comando.Connection = conexao;
                    comando.CommandText = "DELETE FROM especialidade WHERE id_especialidade=@id";
                    comando.Parameters.AddWithValue("@id", id);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Especialidade Removida!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Especialidade_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT id_especialidade as 'ID', descricao as 'Descrição' FROM especialidade", conexao);
            DataSet ds = new DataSet();

            try
            {
                conexao.Open();
                da.Fill(ds, "Tabela");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Tabela";
            }
            catch(Exception erro)
            {
                MessageBox.Show("Erro -> " + erro.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string criterio = textBox3.Text;
            string comando = "";

            if (radioButton1.Checked)
            {
                comando = "SELECT id_especialidade as 'ID', descricao as 'Descrição' FROM especialidade WHERE id = " + criterio;
            }
            if (radioButton2.Checked)
            {
                comando = "SELECT id_especialidade as 'ID', descricao as 'Descrição' FROM especialidade WHERE descricao LIKE '%" + criterio + "%'";
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
            catch(Exception erro)
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
            textBox1.Text = (dataGridView1.Rows[e.RowIndex].Cells[0].Value).ToString();
            textBox2.Text = (dataGridView1.Rows[e.RowIndex].Cells[1].Value).ToString();
            textBox1.Enabled = false;
        }

        private void Especialidade_FormClosed(object sender, FormClosedEventArgs e)
        {
            Home.especialidade = new Especialidade();
        }
    }
}
