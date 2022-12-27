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
    public partial class Setor : Form
    {

        SqlConnection conexao = Banco.conexao;
        SqlCommand comando = new SqlCommand();
        string cpfSupervisor;

        public Setor()
        {
            InitializeComponent();
        }

        public void ReiniciarDG()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT id_setor as 'ID', descricao as 'Descricao', supervisor.nome as 'Supervisor' FROM setor INNER JOIN supervisor ON setor.cpf_supervisor=supervisor.cpf", conexao);
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
            if (comboBox1.Text == "")
            {
                return true;
            }
            return false;
        }

        private void Setor_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            SqlDataAdapter da = new SqlDataAdapter("SELECT id_setor as 'ID', descricao as 'Descricao', supervisor.nome as 'Supervisor' FROM setor INNER JOIN supervisor ON setor.cpf_supervisor=supervisor.cpf",conexao);
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
                SqlDataAdapter daSupervisor = new SqlDataAdapter("SELECT * FROM supervisor", conexao);
                DataTable dtSupervisor = new DataTable();
                daSupervisor.Fill(dtSupervisor);

                for (int i = 0; i < dtSupervisor.Rows.Count; i++)
                {
                    comboBox1.Items.Add(dtSupervisor.Rows[i]["nome"].ToString());
                }
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

        public void atualizarCB()
        {
            comboBox1.Items.Clear();

            try
            {
                conexao.Open();
                SqlDataAdapter daSupervisor = new SqlDataAdapter("SELECT * FROM supervisor", conexao);
                DataTable dtSupervisor = new DataTable();
                daSupervisor.Fill(dtSupervisor);

                for (int i = 0; i < dtSupervisor.Rows.Count; i++)
                {
                    comboBox1.Items.Add(dtSupervisor.Rows[i]["nome"].ToString());
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
                SqlCommand comando = new SqlCommand("SELECT cpf FROM supervisor WHERE nome = @nome", conexao);
                comando.Parameters.AddWithValue("@nome", comboBox1.Text);
                cpfSupervisor = (string)comando.ExecuteScalar();
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

        public void LimparDados()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = "";
            
            comando.Parameters.Clear();
            textBox1.Focus();
            textBox1.Enabled = true;
            ReiniciarDG();

            Home.projeto.atualizarCB();
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

                if(eNumero)
                {
                    string descricao = textBox2.Text;
                    string cpfSupervisor = this.cpfSupervisor;

                    try
                    {
                        comando.Connection = conexao;
                        comando.CommandText = "INSERT INTO setor (id_setor, descricao, cpf_supervisor) VALUES (@id, @descricao, @cpf)";
                        comando.Parameters.AddWithValue("@id", id);
                        comando.Parameters.AddWithValue("@descricao", descricao);
                        comando.Parameters.AddWithValue("@cpf", cpfSupervisor);
                        conexao.Open();
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Setor Inserido!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void botaoExcluir_Click(object sender, EventArgs e)
        {
            if (verficarVazio())
            {
                MessageBox.Show("Insira os Dados Restantes", "FALTA DE DADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int id = int.Parse(textBox1.Text);

                try
                {
                    comando.Connection = conexao;
                    comando.CommandText = "DELETE FROM setor WHERE id_setor=@id";
                    comando.Parameters.AddWithValue("@id", id);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Setor Removido!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void botaoAtualizar_Click(object sender, EventArgs e)
        {
            if (verficarVazio())
            {
                MessageBox.Show("Insira os Dados Restantes", "FALTA DE DADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int id = int.Parse(textBox1.Text);
                string descricao = textBox2.Text;
                string cpfSupervisor = this.cpfSupervisor;

                try
                {
                    comando.Connection = conexao;
                    comando.CommandText = "UPDATE setor SET descricao=@descricao, cpf_supervisor=@cpf WHERE id_setor=@id";
                    comando.Parameters.AddWithValue("@id", id);
                    comando.Parameters.AddWithValue("@descricao", descricao);
                    comando.Parameters.AddWithValue("@cpf", cpfSupervisor);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Setor Alterado!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            textBox1.Text = (dataGridView1.Rows[e.RowIndex].Cells[0].Value).ToString();
            textBox2.Text = (dataGridView1.Rows[e.RowIndex].Cells[1].Value).ToString();
            comboBox1.Text = (dataGridView1.Rows[e.RowIndex].Cells[2].Value).ToString();
            textBox1.Enabled = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string criterio = textBox3.Text;
            string comando = "SELECT id_setor as 'ID', descricao as 'Descricao', supervisor.nome as 'Supervisor' FROM setor INNER JOIN supervisor ON setor.cpf_supervisor=supervisor.cpf WHERE setor.descricao LIKE '%" + criterio + "%'";

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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string criterio = textBox4.Text;
            string comando = "SELECT id_setor as 'ID', descricao as 'Descricao', supervisor.nome as 'Supervisor' FROM setor INNER JOIN supervisor ON setor.cpf_supervisor=supervisor.cpf WHERE supervisor.nome LIKE '%" + criterio + "%'";

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

        private void Setor_FormClosed(object sender, FormClosedEventArgs e)
        {
            Home.setor = new Setor();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string criterio = textBox4.Text;
            string comando = "SELECT id_setor as 'ID', descricao as 'Descricao', supervisor.nome as 'Supervisor' FROM setor INNER JOIN supervisor ON setor.cpf_supervisor=supervisor.cpf WHERE setor.id_setor = " + criterio;

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
