using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INFORMÁTICA__PROJETO_
{
    public partial class Home : Form
    {
        public static Funcionario_Projeto funcionarioProjeto = new Funcionario_Projeto();
        public static Projeto projeto = new Projeto();
        public static Setor setor = new Setor();
        public static Supervisor supervisor = new Supervisor();
        public static Funcionario_Especialidade funcionarioEspecialidade = new Funcionario_Especialidade();
        public static Funcionario funcionario = new Funcionario();
        public static Especialidade especialidade = new Especialidade();
        public static Cliente cliente = new Cliente();
        public static Consulta consulta = new Consulta();
        public Home()
        {
            InitializeComponent();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cliente.MdiParent = this;
            cliente.Show();
        }

        private void especialidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            especialidade.MdiParent = this;
            especialidade.Show();
        }

        private void funcionárioToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            funcionario.MdiParent = this;
            funcionario.Show();
        }

        private void funcionárioEspecialidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            funcionarioEspecialidade.MdiParent = this;
            funcionarioEspecialidade.Show();
        }

        private void supervisorToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            supervisor.MdiParent = this;
            supervisor.Show();
        }

        private void setorToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            setor.MdiParent = this;
            setor.Show();
        }

        private void projetoToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            projeto.MdiParent = this;
            projeto.Show();
        }

        private void funcionárioEProjetoToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            funcionarioProjeto.MdiParent = this;
            funcionarioProjeto.Show();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void consultaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            consulta.MdiParent = this;
            consulta.Show();
        }
    }
}
