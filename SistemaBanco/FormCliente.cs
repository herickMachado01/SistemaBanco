using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaBanco
{
    public partial class FormCliente : Form
    {
        private Cliente clienteSelecionado;

        public FormCliente()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (clienteSelecionado == null)
            {
                Cliente cliente = new Cliente();
                cliente.cpf = txtCPF.Text;
                cliente.nome = txtNome.Text;
                cliente.telefone = txtTel.Text;

                bool result = FormContainer.agencia.addCliente(cliente);
                if (result)
                {
                    MessageBox.Show("Cliente cadastrado com sucesso!", "Info");
                    txtCPF.Clear();
                    txtNome.Clear();
                    txtTel.Clear();
                    txtCPF.Focus();
                    carregarDados();
                }
                else
                {
                    MessageBox.Show(FormContainer.agencia.erro, "Info");
                }
            }
            else
            {
                clienteSelecionado.cpf = txtCPF.Text;
                clienteSelecionado.nome = txtNome.Text;
                clienteSelecionado.telefone = txtTel.Text;

                bool result = FormContainer.agencia.atualizarCliente(clienteSelecionado);
                if (result)
                {
                    MessageBox.Show("Cliente atualizado com sucesso!", "Info");
                    txtCPF.Clear();
                    txtNome.Clear();
                    txtTel.Clear();
                    txtCPF.Focus();
                    clienteSelecionado = null;
                    btnCadastrar.Text = "Cadastrar";
                    carregarDados();
                }
                else
                {
                    MessageBox.Show(FormContainer.agencia.erro, "Info");
                }
            }
        }

        private void carregarDados()
        {
            dtvCliente.Rows.Clear();
            foreach (Cliente c in FormContainer.agencia.exibirClientes())
            {
                dtvCliente.Rows.Add(c.cpf, c.nome, c.telefone, "📝", "❌");
            }
        }

        private void dtvCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string cpf = dtvCliente.Rows[e.RowIndex].Cells[0].Value.ToString();

            int funcao = e.ColumnIndex;
            if (funcao == 3)  
            {
                clienteSelecionado = FormContainer.agencia.consultarPorCPF(cpf);
                if (clienteSelecionado != null)
                {
                    txtCPF.Text = clienteSelecionado.cpf;
                    txtNome.Text = clienteSelecionado.nome;
                    txtTel.Text = clienteSelecionado.telefone;
                    btnCadastrar.Text = "Atualizar";
                }
            }
            if (funcao == 4) 
            {
                if (FormContainer.agencia.removerCliente(cpf))
                {
                    carregarDados();
                }
                else
                {
                    MessageBox.Show(FormContainer.agencia.erro, "Erro de exclusão");
                }
            }
        }
    }
}
