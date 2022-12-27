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
    public partial class Consulta : Form
    {
        SqlConnection conexao = Banco.conexao;
        SqlCommand comando = new SqlCommand();
        public Consulta()
        {
            InitializeComponent();
        }

        public void ReiniciarDG1()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT funcionario_projeto.id_funcionario_projeto as 'ID', projeto.nome as 'Projeto', funcionario.nome as 'Funcionario', cliente.nome as 'Cliente', setor.descricao as 'Setor', supervisor.nome as 'Supervisor' FROM funcionario_projeto INNER JOIN projeto ON funcionario_projeto.id_projeto=projeto.id_projeto INNER JOIN funcionario ON funcionario_projeto.cpf_funcionario=funcionario.cpf INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor INNER JOIN supervisor ON setor.cpf_supervisor=supervisor.cpf", conexao);
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

            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);

            if (dataGridView1.Rows.Count == 0)
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        public void ReiniciarDG2()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT id_funcionario_especialidade as 'ID', funcionario.nome as 'Funcionario', especialidade.descricao as 'Especialidade' FROM funcionario_especialidade INNER JOIN funcionario ON funcionario_especialidade.cpf_funcionario=funcionario.cpf INNER JOIN especialidade ON funcionario_especialidade.id_especialidade=especialidade.id_especialidade", conexao);
            DataSet ds1 = new DataSet();

            try
            {
                conexao.Open();
                da1.Fill(ds1, "Tabela");
                dataGridView2.DataSource = ds1;
                dataGridView2.DataMember = "Tabela";
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro -> " + erro.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }

            dataGridView2.Sort(dataGridView2.Columns[0], ListSortDirection.Ascending);

            if (dataGridView2.Rows.Count == 0)
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }
        }

        private void Consulta_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT funcionario_projeto.id_funcionario_projeto as 'ID', projeto.nome as 'Projeto', funcionario.nome as 'Funcionario', cliente.nome as 'Cliente', setor.descricao as 'Setor', supervisor.nome as 'Supervisor' FROM funcionario_projeto INNER JOIN projeto ON funcionario_projeto.id_projeto=projeto.id_projeto INNER JOIN funcionario ON funcionario_projeto.cpf_funcionario=funcionario.cpf INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor INNER JOIN supervisor ON setor.cpf_supervisor=supervisor.cpf", conexao);
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

            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);

            SqlDataAdapter da1 = new SqlDataAdapter("SELECT id_funcionario_especialidade as 'ID', funcionario.nome as 'Funcionario', especialidade.descricao as 'Especialidade' FROM funcionario_especialidade INNER JOIN funcionario ON funcionario_especialidade.cpf_funcionario=funcionario.cpf INNER JOIN especialidade ON funcionario_especialidade.id_especialidade=especialidade.id_especialidade", conexao);
            DataSet ds1 = new DataSet();

            try
            {
                conexao.Open();
                da1.Fill(ds1, "Tabela");
                dataGridView2.DataSource = ds1;
                dataGridView2.DataMember = "Tabela";
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro -> " + erro.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }

            dataGridView2.Sort(dataGridView2.Columns[0], ListSortDirection.Ascending);

            if (dataGridView1.Rows.Count == 0)
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }

            if (dataGridView2.Rows.Count == 0)
            {
                button2.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
            }
        }

        private void Consulta_FormClosed(object sender, FormClosedEventArgs e)
        {
            Home.consulta = new Consulta();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string criterio = textBox1.Text;
            string comando = "";

            if(radioButton6.Checked)
            {
                comando = "SELECT funcionario_projeto.id_funcionario_projeto as 'ID', projeto.nome as 'Projeto', funcionario.nome as 'Funcionario', cliente.nome as 'Cliente', setor.descricao as 'Setor', supervisor.nome as 'Supervisor' FROM funcionario_projeto INNER JOIN projeto ON funcionario_projeto.id_projeto=projeto.id_projeto INNER JOIN funcionario ON funcionario_projeto.cpf_funcionario=funcionario.cpf INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor INNER JOIN supervisor ON setor.cpf_supervisor=supervisor.cpf WHERE funcionario_projeto.id_funcionario_projeto LIKE '%" + criterio + "%'";
            }
            if(radioButton1.Checked)
            {
                comando = "SELECT funcionario_projeto.id_funcionario_projeto as 'ID', projeto.nome as 'Projeto', funcionario.nome as 'Funcionario', cliente.nome as 'Cliente', setor.descricao as 'Setor', supervisor.nome as 'Supervisor' FROM funcionario_projeto INNER JOIN projeto ON funcionario_projeto.id_projeto=projeto.id_projeto INNER JOIN funcionario ON funcionario_projeto.cpf_funcionario=funcionario.cpf INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor INNER JOIN supervisor ON setor.cpf_supervisor=supervisor.cpf WHERE projeto.nome LIKE '%" + criterio + "%'";
            }
            if (radioButton2.Checked)
            {
                comando = "SELECT funcionario_projeto.id_funcionario_projeto as 'ID', projeto.nome as 'Projeto', funcionario.nome as 'Funcionario', cliente.nome as 'Cliente', setor.descricao as 'Setor', supervisor.nome as 'Supervisor' FROM funcionario_projeto INNER JOIN projeto ON funcionario_projeto.id_projeto=projeto.id_projeto INNER JOIN funcionario ON funcionario_projeto.cpf_funcionario=funcionario.cpf INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor INNER JOIN supervisor ON setor.cpf_supervisor=supervisor.cpf WHERE funcionario.nome LIKE '%" + criterio + "%'";
            }
            if (radioButton3.Checked)
            {
                comando = "SELECT funcionario_projeto.id_funcionario_projeto as 'ID', projeto.nome as 'Projeto', funcionario.nome as 'Funcionario', cliente.nome as 'Cliente', setor.descricao as 'Setor', supervisor.nome as 'Supervisor' FROM funcionario_projeto INNER JOIN projeto ON funcionario_projeto.id_projeto=projeto.id_projeto INNER JOIN funcionario ON funcionario_projeto.cpf_funcionario=funcionario.cpf INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor INNER JOIN supervisor ON setor.cpf_supervisor=supervisor.cpf WHERE cliente.nome LIKE '%" + criterio + "%'";
            }
            if (radioButton4.Checked)
            {
                comando = "SELECT funcionario_projeto.id_funcionario_projeto as 'ID', projeto.nome as 'Projeto', funcionario.nome as 'Funcionario', cliente.nome as 'Cliente', setor.descricao as 'Setor', supervisor.nome as 'Supervisor' FROM funcionario_projeto INNER JOIN projeto ON funcionario_projeto.id_projeto=projeto.id_projeto INNER JOIN funcionario ON funcionario_projeto.cpf_funcionario=funcionario.cpf INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor INNER JOIN supervisor ON setor.cpf_supervisor=supervisor.cpf WHERE setor.nome LIKE '%" + criterio + "%'";
            }
            if (radioButton5.Checked)
            {
                comando = "SELECT funcionario_projeto.id_funcionario_projeto as 'ID', projeto.nome as 'Projeto', funcionario.nome as 'Funcionario', cliente.nome as 'Cliente', setor.descricao as 'Setor', supervisor.nome as 'Supervisor' FROM funcionario_projeto INNER JOIN projeto ON funcionario_projeto.id_projeto=projeto.id_projeto INNER JOIN funcionario ON funcionario_projeto.cpf_funcionario=funcionario.cpf INNER JOIN cliente ON projeto.cnpj_cliente=cliente.cnpj INNER JOIN setor ON projeto.id_setor=setor.id_setor INNER JOIN supervisor ON setor.cpf_supervisor=supervisor.cpf WHERE supervisor.nome LIKE '%" + criterio + "%'";
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

            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string criterio = textBox2.Text;
            string comando = "";

            if (radioButton7.Checked)
            {
                comando = "SELECT id_funcionario_especialidade as 'ID', funcionario.nome as 'Funcionario', especialidade.descricao as 'Especialidade' FROM funcionario_especialidade INNER JOIN funcionario ON funcionario_especialidade.cpf_funcionario=funcionario.cpf INNER JOIN especialidade ON funcionario_especialidade.id_especialidade=especialidade.id_especialidade WHERE funcionario_especialidade.id_funcionario_especialidade LIKE '%" + criterio + "%'";
            }
            if (radioButton8.Checked)
            {
                comando = "SELECT id_funcionario_especialidade as 'ID', funcionario.nome as 'Funcionario', especialidade.descricao as 'Especialidade' FROM funcionario_especialidade INNER JOIN funcionario ON funcionario_especialidade.cpf_funcionario=funcionario.cpf INNER JOIN especialidade ON funcionario_especialidade.id_especialidade=especialidade.id_especialidade WHERE funcionario.nome LIKE '%" + criterio + "%'";
            }
            if (radioButton9.Checked)
            {
                comando = "SELECT id_funcionario_especialidade as 'ID', funcionario.nome as 'Funcionario', especialidade.descricao as 'Especialidade' FROM funcionario_especialidade INNER JOIN funcionario ON funcionario_especialidade.cpf_funcionario=funcionario.cpf INNER JOIN especialidade ON funcionario_especialidade.id_especialidade=especialidade.id_especialidade WHERE especialidade.descricao LIKE '%" + criterio + "%'";
            }

            SqlDataAdapter da = new SqlDataAdapter(comando, conexao);
            DataSet ds = new DataSet();

            try
            {
                conexao.Open();
                da.Fill(ds, "Tabela");
                dataGridView2.DataSource = ds;
                dataGridView2.DataMember = "Tabela";
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro -> " + erro.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }

            dataGridView2.Sort(dataGridView2.Columns[0], ListSortDirection.Ascending);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int espacoLinha = 30;

            var document = new PdfSharp.Pdf.PdfDocument();
            var page = document.AddPage();
            var graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
            var textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
            var font = new PdfSharp.Drawing.XFont("Calibri", 14);

            textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Center;
            textFormatter.DrawString("CONSULTA DE DADOS", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, espacoLinha, page.Width - 60, page.Height - 60));
            espacoLinha += 60;

            textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Left;

            for(int i = 0; i<dataGridView1.Rows.Count; i++)
            {
                textFormatter.DrawString("ID: " + dataGridView1.Rows[i].Cells[0].Value.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, espacoLinha, page.Width - 60, page.Height - 60));
                espacoLinha += 30;
                textFormatter.DrawString("Projeto: " + dataGridView1.Rows[i].Cells[1].Value.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, espacoLinha, page.Width - 60, page.Height - 60));
                espacoLinha += 30;
                textFormatter.DrawString("Funcionário: " + dataGridView1.Rows[i].Cells[2].Value.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, espacoLinha, page.Width - 60, page.Height - 60));
                espacoLinha += 30;
                textFormatter.DrawString("Cliente: " + dataGridView1.Rows[i].Cells[3].Value.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, espacoLinha, page.Width - 60, page.Height - 60));
                espacoLinha += 30;
                textFormatter.DrawString("Setor: " + dataGridView1.Rows[i].Cells[4].Value.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, espacoLinha, page.Width - 60, page.Height - 60));
                espacoLinha += 30;
                textFormatter.DrawString("Supervisor: " + dataGridView1.Rows[i].Cells[5].Value.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, espacoLinha, page.Width - 60, page.Height - 60));
                espacoLinha += 60;

                if (espacoLinha >= 600 && dataGridView1.Rows.Count >= 4)
                {
                    page = document.AddPage();
                    graphics.Dispose();
                    graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                    textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
                    espacoLinha = 90;
                }
            }

            document.Save("consultaProjeto.pdf");
            System.Diagnostics.Process.Start("consultaProjeto.pdf");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int espacoLinha = 30;

            var document = new PdfSharp.Pdf.PdfDocument();
            var page = document.AddPage();
            var graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
            var textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
            var font = new PdfSharp.Drawing.XFont("Calibri", 14);

            textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Center;
            textFormatter.DrawString("CONSULTA DE DADOS", font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, espacoLinha, page.Width - 60, page.Height - 60));
            espacoLinha += 60;

            textFormatter.Alignment = PdfSharp.Drawing.Layout.XParagraphAlignment.Left;

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                textFormatter.DrawString("ID: " + dataGridView2.Rows[i].Cells[0].Value.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, espacoLinha, page.Width - 60, page.Height - 60));
                espacoLinha += 30;
                textFormatter.DrawString("Funcionário: " + dataGridView2.Rows[i].Cells[1].Value.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, espacoLinha, page.Width - 60, page.Height - 60));
                espacoLinha += 30;
                textFormatter.DrawString("Especialidade: " + dataGridView2.Rows[i].Cells[2].Value.ToString(), font, PdfSharp.Drawing.XBrushes.Black, new PdfSharp.Drawing.XRect(30, espacoLinha, page.Width - 60, page.Height - 60));
                espacoLinha += 60;

                if (espacoLinha >= 810 && dataGridView2.Rows.Count >= 7)
                {
                    page = document.AddPage();
                    graphics.Dispose();
                    graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page);
                    textFormatter = new PdfSharp.Drawing.Layout.XTextFormatter(graphics);
                    espacoLinha = 90;
                }
            }

            document.Save("consultaFuncionario.pdf");
            System.Diagnostics.Process.Start("consultaFuncionario.pdf");
        }
    }
}
