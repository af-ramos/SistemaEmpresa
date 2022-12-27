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
    public partial class Projeto : Form
    {
        SqlConnection conexao = Banco.conexao;
        SqlCommand comando = new SqlCommand();
        int idSetor;
        string cnpjCliente;

        public void LimparDados()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = maskedTextBox1.Text = maskedTextBox2.Text = "";
            comando.Parameters.Clear();
            textBox1.Enabled = true;
            textBox1.Focus();
            ReiniciarDG();

            Home.funcionarioProjeto.atualizarCB();
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
            if (comboBox2.Text == "")
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

        public void ReiniciarDG()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT id_projeto as 'ID', projeto.nome as 'Nome', data_inicial as 'Data Inicial', data_final as 'Data Final', setor.descricao as 'Setor', cliente.nome as 'Cliente' FROM projeto INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor", conexao);
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

        public Projeto()
        {
            InitializeComponent();
        }

        private void Projeto_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            SqlDataAdapter da = new SqlDataAdapter("SELECT id_projeto as 'ID', projeto.nome as 'Nome', data_inicial as 'Data Inicial', data_final as 'Data Final', setor.descricao as 'Setor', cliente.nome as 'Cliente' FROM projeto INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor", conexao);
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
                SqlDataAdapter daSetor = new SqlDataAdapter("SELECT * FROM setor", conexao);
                DataTable dtSetor = new DataTable();
                daSetor.Fill(dtSetor);

                for (int i = 0; i < dtSetor.Rows.Count; i++)
                {
                    comboBox1.Items.Add(dtSetor.Rows[i]["descricao"].ToString());
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
                SqlDataAdapter daCliente = new SqlDataAdapter("SELECT * FROM cliente", conexao);
                DataTable dtCliente = new DataTable();
                daCliente.Fill(dtCliente);

                for (int i = 0; i < dtCliente.Rows.Count; i++)
                {
                    comboBox2.Items.Add(dtCliente.Rows[i]["nome"].ToString());
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
                SqlDataAdapter daSetor = new SqlDataAdapter("SELECT * FROM setor", conexao);
                DataTable dtSetor = new DataTable();
                daSetor.Fill(dtSetor);

                for (int i = 0; i < dtSetor.Rows.Count; i++)
                {
                    comboBox1.Items.Add(dtSetor.Rows[i]["descricao"].ToString());
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
                SqlDataAdapter daCliente = new SqlDataAdapter("SELECT * FROM cliente", conexao);
                DataTable dtCliente = new DataTable();
                daCliente.Fill(dtCliente);

                for (int i = 0; i < dtCliente.Rows.Count; i++)
                {
                    comboBox2.Items.Add(dtCliente.Rows[i]["nome"].ToString());
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
                SqlCommand comando = new SqlCommand("SELECT id_setor FROM setor WHERE descricao = @descricao", conexao);
                comando.Parameters.AddWithValue("@descricao", comboBox1.Text);
                idSetor = (int)comando.ExecuteScalar();
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
                SqlCommand comando = new SqlCommand("SELECT cnpj FROM cliente WHERE nome = @nome", conexao);
                comando.Parameters.AddWithValue("@nome", comboBox2.Text);
                cnpjCliente = (string)comando.ExecuteScalar();
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
            int id_projeto = 0;
            if(verficarVazio())
            {
                MessageBox.Show("Insira os Dados Restantes", "FALTA DE DADOS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    id_projeto = int.Parse(textBox1.Text);
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
                    string nome = textBox2.Text;
                    string data_inicial = maskedTextBox1.Text;
                    string data_final = maskedTextBox2.Text;
                    int id_setor = idSetor;
                    string cnpj_cliente = cnpjCliente;

                    try
                    {
                        comando.Connection = conexao;
                        comando.CommandText = "INSERT INTO projeto (id_projeto, nome, data_inicial, data_final, id_setor, cnpj_cliente) VALUES (@id, @nome, @data_inicial, @data_final, @id_setor, @cnpj)";
                        comando.Parameters.AddWithValue("@id", id_projeto);
                        comando.Parameters.AddWithValue("@nome", nome);
                        comando.Parameters.AddWithValue("@data_inicial", data_inicial);
                        comando.Parameters.AddWithValue("@data_final", data_final);
                        comando.Parameters.AddWithValue("@id_setor", id_setor);
                        comando.Parameters.AddWithValue("@cnpj", cnpj_cliente);
                        conexao.Open();
                        comando.ExecuteNonQuery();
                        MessageBox.Show("Projeto Inserido!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                int id_projeto = int.Parse(textBox1.Text);
                string nome = textBox2.Text;
                string data_inicial = maskedTextBox1.Text;
                string data_final = maskedTextBox2.Text;
                int id_setor = idSetor;
                string cnpj_cliente = cnpjCliente;

                try
                {
                    comando.Connection = conexao;
                    comando.CommandText = "UPDATE projeto SET nome=@nome, data_inicial=@data_inicial, data_final=@data_final, id_setor=@id_setor, cnpj_cliente=@cnpj WHERE id_projeto = @id";
                    comando.Parameters.AddWithValue("@id", id_projeto);
                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@data_inicial", data_inicial);
                    comando.Parameters.AddWithValue("@data_final", data_final);
                    comando.Parameters.AddWithValue("@id_setor", id_setor);
                    comando.Parameters.AddWithValue("@cnpj", cnpj_cliente);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Projeto Alterado!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                int id = int.Parse(textBox1.Text);

                try
                {
                    comando.Connection = conexao;
                    comando.CommandText = "DELETE FROM projeto WHERE id_projeto=@id";
                    comando.Parameters.AddWithValue("@id", id);
                    conexao.Open();
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Projeto Removido!", "SUCESSO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string criterio = textBox3.Text;
            string comando = "";

            if (radioButton1.Checked)
            {
                comando = "SELECT id_projeto as 'ID', projeto.nome as 'Nome', data_inicial as 'Data Inicial', data_final as 'Data Final', setor.descricao as 'Setor', cliente.nome as 'Cliente' FROM projeto INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor WHERE projeto.id_projeto = " + criterio;
            }
            if (radioButton2.Checked)
            {
                comando = "SELECT id_projeto as 'ID', projeto.nome as 'Nome', data_inicial as 'Data Inicial', data_final as 'Data Final', setor.descricao as 'Setor', cliente.nome as 'Cliente' FROM projeto INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor WHERE projeto.nome LIKE '%" + criterio + "%'";
            }
            if (radioButton3.Checked)
            {
                comando = "SELECT id_projeto as 'ID', projeto.nome as 'Nome', data_inicial as 'Data Inicial', data_final as 'Data Final', setor.descricao as 'Setor', cliente.nome as 'Cliente' FROM projeto INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor WHERE projeto.data_inicial LIKE '%" + criterio + "%'";
            }
            if (radioButton4.Checked)
            {
                comando = "SELECT id_projeto as 'ID', projeto.nome as 'Nome', data_inicial as 'Data Inicial', data_final as 'Data Final', setor.descricao as 'Setor', cliente.nome as 'Cliente' FROM projeto INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor WHERE projeto.data_final LIKE '%" + criterio + "%'";
            }
            if (radioButton5.Checked)
            {
                comando = "SELECT id_projeto as 'ID', projeto.nome as 'Nome', data_inicial as 'Data Inicial', data_final as 'Data Final', setor.descricao as 'Setor', cliente.nome as 'Cliente' FROM projeto INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor WHERE setor.descricao LIKE '%" + criterio + "%'";
            }
            if (radioButton6.Checked)
            {
                comando = "SELECT id_projeto as 'ID', projeto.nome as 'Nome', data_inicial as 'Data Inicial', data_final as 'Data Final', setor.descricao as 'Setor', cliente.nome as 'Cliente' FROM projeto INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor WHERE cliente.nome LIKE '%" + criterio + "%'";
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
            textBox1.Text = (dataGridView1.Rows[e.RowIndex].Cells[0].Value).ToString();
            textBox2.Text = (dataGridView1.Rows[e.RowIndex].Cells[1].Value).ToString();
            maskedTextBox1.Text = (dataGridView1.Rows[e.RowIndex].Cells[2].Value).ToString();
            maskedTextBox2.Text = (dataGridView1.Rows[e.RowIndex].Cells[3].Value).ToString();
            comboBox1.Text = (dataGridView1.Rows[e.RowIndex].Cells[4].Value).ToString();
            comboBox2.Text = (dataGridView1.Rows[e.RowIndex].Cells[5].Value).ToString();
            textBox1.Enabled = false;
        }

        private void Projeto_FormClosed(object sender, FormClosedEventArgs e)
        {
            Home.projeto = new Projeto();
        }
    }
}
