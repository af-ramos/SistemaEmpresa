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
    public partial class Funcionario_Especialidade : Form
    {
        SqlConnection conexao = Banco.conexao;
        SqlCommand comando = new SqlCommand();
        string cpfFuncionario;
        int idEspecialidade;
        public Funcionario_Especialidade()
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
            textBox1.Text = textBox2.Text = "";
            comando.Parameters.Clear();
            textBox1.Enabled = true;
            textBox1.Focus();
            ReiniciarDG();

            Home.consulta.ReiniciarDG2();
        }

        public void ReiniciarDG()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT id_funcionario_especialidade as 'ID', funcionario.nome as 'Funcionario', especialidade.descricao as 'Especialidade' FROM funcionario_especialidade INNER JOIN funcionario ON funcionario_especialidade.cpf_funcionario=funcionario.cpf INNER JOIN especialidade ON funcionario_especialidade.id_especialidade=especialidade.id_especialidade", conexao);
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

        private void Funcionario_Especialidade_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            SqlDataAdapter da = new SqlDataAdapter("SELECT id_funcionario_especialidade as 'ID', funcionario.nome as 'Funcionario', especialidade.descricao as 'Especialidade' FROM funcionario_especialidade INNER JOIN funcionario ON funcionario_especialidade.cpf_funcionario=funcionario.cpf INNER JOIN especialidade ON funcionario_especialidade.id_especialidade=especialidade.id_especialidade", conexao);
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
                SqlDataAdapter daEspecialidade = new SqlDataAdapter("SELECT * FROM especialidade", conexao);
                DataTable dtEspecialidade = new DataTable();
                daEspecialidade.Fill(dtEspecialidade);

                for (int i = 0; i < dtEspecialidade.Rows.Count; i++)
                {
                    comboBox2.Items.Add(dtEspecialidade.Rows[i]["descricao"].ToString());
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
                SqlDataAdapter daEspecialidade = new SqlDataAdapter("SELECT * FROM especialidade", conexao);
                DataTable dtEspecialidade = new DataTable();
                daEspecialidade.Fill(dtEspecialidade);

                for (int i = 0; i < dtEspecialidade.Rows.Count; i++)
                {
                    comboBox2.Items.Add(dtEspecialidade.Rows[i]["descricao"].ToString());
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

        private void botaoNovo_Click(object sender, EventArgs e)
        {
            LimparDados();
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
                SqlCommand comando = new SqlCommand("SELECT id_especialidade FROM especialidade WHERE descricao = @descricao", conexao);
                comando.Parameters.AddWithValue("@descricao", comboBox2.Text);
                idEspecialidade = (int)comando.ExecuteScalar();
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

        private void botaoInserir_Click(object sender, EventArgs e)
        {
            bool eNumero;
            int id = 0;
            if (verificarVazio())
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
                    int idEspecialidade = this.idEspecialidade;

                    try
                    {
                        comando.Connection = conexao;
                        comando.CommandText = "INSERT INTO funcionario_especialidade (id_funcionario_especialidade, cpf_funcionario, id_especialidade) VALUES (@id, @cpfFuncionario, @idEspecialidade)";
                        comando.Parameters.AddWithValue("@id", id);
                        comando.Parameters.AddWithValue("@cpfFuncionario", cpfFuncionario);
                        comando.Parameters.AddWithValue("@idEspecialidade", idEspecialidade);
                        conexao.Open();
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Funcionario & Especialidade Inserida!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                int idEspecialidade = this.idEspecialidade;

                try
                {
                    comando.Connection = conexao;
                    comando.CommandText = "UPDATE funcionario_especialidade SET cpf_funcionario=@cpfFuncionario, id_especialidade=@idEspecialidade WHERE id_funcionario_especialidade=@id";
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@cpfFuncionario", cpfFuncionario);
                    comando.Parameters.AddWithValue("@idEspecialidade", idEspecialidade);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Funcionario & Especialidade Atualizada!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    comando.CommandText = "DELETE FROM funcionario_especialidade WHERE id_funcionario_especialidade=@id";
                    comando.Parameters.AddWithValue("@id", id);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Funcionario & Especialidade Removida!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string comando = "SELECT id_funcionario_especialidade as 'ID', funcionario.nome as 'Funcionario', especialidade.descricao as 'Especialidade' FROM funcionario_especialidade INNER JOIN funcionario ON funcionario_especialidade.cpf_funcionario = funcionario.cpf INNER JOIN especialidade ON funcionario_especialidade.id_especialidade = especialidade.id_especialidade WHERE funcionario.nome LIKE '%" + criterio + "%'";

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
            comboBox1.Text = (dataGridView1.Rows[e.RowIndex].Cells[1].Value).ToString();
            comboBox2.Text = (dataGridView1.Rows[e.RowIndex].Cells[2].Value).ToString();
            textBox1.Enabled = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string criterio = textBox3.Text;
            string comando = "SELECT id_funcionario_especialidade as 'ID', funcionario.nome as 'Funcionario', especialidade.descricao as 'Especialidade' FROM funcionario_especialidade INNER JOIN funcionario ON funcionario_especialidade.cpf_funcionario = funcionario.cpf INNER JOIN especialidade ON funcionario_especialidade.id_especialidade = especialidade.id_especialidade WHERE especialidade.descricao LIKE '%" + criterio + "%'";

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

        private void Funcionario_Especialidade_FormClosed(object sender, FormClosedEventArgs e)
        {
            Home.funcionarioEspecialidade = new Funcionario_Especialidade();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string criterio = textBox3.Text;
            string comando = "SELECT id_funcionario_especialidade as 'ID', funcionario.nome as 'Funcionario', especialidade.descricao as 'Especialidade' FROM funcionario_especialidade INNER JOIN funcionario ON funcionario_especialidade.cpf_funcionario = funcionario.cpf INNER JOIN especialidade ON funcionario_especialidade.id_especialidade = especialidade.id_especialidade WHERE funcionario_especialidade.id_funcionario_especialidade = " + criterio;

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
