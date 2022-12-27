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
    public partial class Funcionario_Projeto : Form
    {
        SqlConnection conexao = Banco.conexao;
        SqlCommand comando = new SqlCommand();
        int idProjeto;
        string cpfFuncionario;
        public Funcionario_Projeto()
        {
            InitializeComponent();
        }

        public bool verificarVazio()
        {
            if (textBox1.Text == "")
            {
                return true;
            }
            if (comboBox1.Text == "")
            {
                return true;
            }
            if (comboBox2.Text == "")
            {
                return true;
            }
            return false;
        }

        public void LimparDados()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = "";
            ReiniciarDG();
            textBox1.Enabled = true;
            textBox1.Focus();
            comando.Parameters.Clear();

            Home.consulta.ReiniciarDG1();
        }

        public void ReiniciarDG()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT id_funcionario_projeto as 'ID', projeto.nome as 'Projeto', funcionario.nome as 'Funcionario' FROM funcionario_projeto INNER JOIN projeto ON funcionario_projeto.id_projeto=projeto.id_projeto INNER JOIN funcionario ON funcionario_projeto.cpf_funcionario=funcionario.cpf", conexao);
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

        private void Funcionario_Projeto_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            SqlDataAdapter da = new SqlDataAdapter("SELECT id_funcionario_projeto as 'ID', projeto.nome as 'Projeto', funcionario.nome as 'Funcionario' FROM funcionario_projeto INNER JOIN projeto ON funcionario_projeto.id_projeto=projeto.id_projeto INNER JOIN funcionario ON funcionario_projeto.cpf_funcionario=funcionario.cpf", conexao);
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

            try
            {
                conexao.Open();
                SqlDataAdapter daFuncionario = new SqlDataAdapter("SELECT * FROM funcionario", conexao);
                DataTable dtFuncionario = new DataTable();
                daFuncionario.Fill(dtFuncionario);
           
                for (int i = 0; i < dtFuncionario.Rows.Count; i++)
                {
                    comboBox1.Items.Add(dtFuncionario.Rows[i]["nome"].ToString());
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro -> " + erro.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
                
            }

            try
            {
                conexao.Open();
                SqlDataAdapter daProjeto = new SqlDataAdapter("SELECT * FROM projeto", conexao);
                DataTable dtProjeto = new DataTable();
                daProjeto.Fill(dtProjeto);

                for (int i = 0; i < dtProjeto.Rows.Count; i++)
                {
                    comboBox2.Items.Add(dtProjeto.Rows[i]["nome"].ToString());
                }
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

        public void atualizarCB()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            try
            {
                conexao.Open();
                SqlDataAdapter daFuncionario = new SqlDataAdapter("SELECT * FROM funcionario", conexao);
                DataTable dtFuncionario = new DataTable();
                daFuncionario.Fill(dtFuncionario);

                for (int i = 0; i < dtFuncionario.Rows.Count; i++)
                {
                    comboBox1.Items.Add(dtFuncionario.Rows[i]["nome"].ToString());
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro -> " + erro.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
                
            }

            try
            {
                conexao.Open();
                SqlDataAdapter daProjeto = new SqlDataAdapter("SELECT * FROM projeto", conexao);
                DataTable dtProjeto = new DataTable();
                daProjeto.Fill(dtProjeto);

                for (int i = 0; i < dtProjeto.Rows.Count; i++)
                {
                    comboBox2.Items.Add(dtProjeto.Rows[i]["nome"].ToString());
                }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();
                SqlCommand comando = new SqlCommand("SELECT cpf FROM funcionario WHERE nome = @nome", conexao);
                comando.Parameters.AddWithValue("@nome", comboBox1.Text);
                cpfFuncionario = (string)comando.ExecuteScalar();
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conexao.Open();
                SqlCommand comando = new SqlCommand("SELECT id_projeto FROM projeto WHERE nome = @nome", conexao);
                comando.Parameters.AddWithValue("@nome", comboBox2.Text);
                idProjeto = (int)comando.ExecuteScalar();
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
            if(verificarVazio())
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

                if(eNumero)
                {
                    string cpfFuncionario = this.cpfFuncionario;
                    int idProjeto = this.idProjeto;

                    try
                    {
                        comando.Connection = conexao;
                        comando.CommandText = "INSERT INTO funcionario_projeto (id_funcionario_projeto, id_projeto, cpf_funcionario) VALUES (@id, @idProjeto, @cpfFuncionario)";
                        comando.Parameters.AddWithValue("@id", id);
                        comando.Parameters.AddWithValue("@cpfFuncionario", cpfFuncionario);
                        comando.Parameters.AddWithValue("@idProjeto", idProjeto);
                        conexao.Open();
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Funcionário & Projeto Inserido!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (verificarVazio())
            {
                MessageBox.Show("Insira os Dados Restantes", "FALTA DE DADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int id = int.Parse(textBox1.Text);
                string cpfFuncionario = this.cpfFuncionario;
                int idProjeto = this.idProjeto;

                try
                {
                    comando.Connection = conexao;
                    comando.CommandText = "UPDATE funcionario_projeto SET id_projeto=@idProjeto, cpfFuncionario=@cpfFuncionario WHERE id_funcionario_projeto=@id";
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@cpfFuncionario", cpfFuncionario);
                    comando.Parameters.AddWithValue("@idProjeto", idProjeto);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Funcionário & Projeto Alterado!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                int id = int.Parse(textBox1.Text);

                try
                {
                    comando.Connection = conexao;
                    comando.CommandText = "DELETE FROM funcionario_projeto WHERE id_funcionario_projeto=@id";
                    comando.Parameters.AddWithValue("@id", id);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Funcionário & Projeto Removido!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception erro)
                {
                    MessageBox.Show("Erro -> " + erro.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conexao.Close();
                    LimparDados();
                }
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string criterio = textBox2.Text;
            string comando = "SELECT id_funcionario_projeto as 'ID', projeto.nome as 'Projeto', funcionario.nome as 'Funcionario' FROM funcionario_projeto INNER JOIN projeto ON funcionario_projeto.id_projeto = projeto.id_projeto INNER JOIN funcionario ON funcionario_projeto.cpf_funcionario = funcionario.cpf WHERE funcionario.nome LIKE '%" + criterio + "%'";

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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string criterio = textBox3.Text;
            string comando = "SELECT id_funcionario_projeto as 'ID', projeto.nome as 'Projeto', funcionario.nome as 'Funcionario' FROM funcionario_projeto INNER JOIN projeto ON funcionario_projeto.id_projeto = projeto.id_projeto INNER JOIN funcionario ON funcionario_projeto.cpf_funcionario = funcionario.cpf WHERE projeto.nome LIKE '%" + criterio + "%'";

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
            textBox1.Text = (dataGridView1.Rows[e.RowIndex].Cells[0].Value).ToString();
            comboBox2.Text = (dataGridView1.Rows[e.RowIndex].Cells[1].Value).ToString();
            comboBox1.Text = (dataGridView1.Rows[e.RowIndex].Cells[2].Value).ToString();
            textBox1.Enabled = false;
        }

        private void Funcionario_Projeto_FormClosed(object sender, FormClosedEventArgs e)
        {
            Home.funcionarioProjeto = new Funcionario_Projeto();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string criterio = textBox3.Text;
            string comando = "SELECT id_funcionario_projeto as 'ID', projeto.nome as 'Projeto', funcionario.nome as 'Funcionario' FROM funcionario_projeto INNER JOIN projeto ON funcionario_projeto.id_projeto = projeto.id_projeto INNER JOIN funcionario ON funcionario_projeto.cpf_funcionario = funcionario.cpf WHERE funcionario_projeto.id_funcionario_projeto = " + criterio;

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
    }
}
