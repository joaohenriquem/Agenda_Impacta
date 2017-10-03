using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Xml.Serialization;

using System.Data;

//Imports do banco de dados
using System.Data.SqlClient;

namespace TelaPrincipal
{
    /// <summary>
    /// Interaction logic for JanelaAdicionar.xaml
    /// </summary>
    public partial class JanelaAdicionar : Window
    {
        

        public Compromissos LcompAdd;
        public JanelaAdicionar()
        {
            InitializeComponent();
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Referenciar(Compromissos cs)
        {
            this.LcompAdd = cs;
        }

        public void Limpar() 
        {
            TB1A.Text = "";
            TB2AHInicial.Text = "";
            TB3AHFinal.Text = "";
            CheckBox1.IsChecked = false;
            DatePicker.ClearValue(DatePicker.SelectedDateProperty);
        }

        private void Adicionar(object sender, RoutedEventArgs e)
        {

            var conexao = new ConexaoSqlServer();
            string sql = "INSERT INTO COMPROMISSOS (NOME_COMPROMISSO, DATA_COMPROMISSO, HORA_INICIO_COMPROMISSO, HORA_FIM_COMPROMISSO, DESC_COMPROMISSO) VALUES ('" + TB1A + "','" + DatePicker.Text + "','" + TB2AHInicial.Text + "','" + TB3AHFinal.Text + "','" + Descricao.Text + "')";

            conexao.Atualiza(sql);
            MessageBox.Show("O compromisso foi adicionado com sucesso!");

            DialogResult = true;
            this.Close();

        }

        private void Alterar(object sender, RoutedEventArgs e)
        {

            var conexao = new ConexaoSqlServer();
            string sql = "UPDATE COMPROMISSOS SET NOME_COMPROMISSO='" + TB1A + "', DATA_COMPROMISSO='" + DatePicker.Text + "',HORA_INICIO_COMPROMISSO='" + TB2AHInicial.Text + "',HORA_FIM_COMPROMISSO='" + TB3AHFinal.Text + "', DESC_COMPROMISSO='" + Descricao.Text + "'";

            conexao.Atualiza(sql);
            MessageBox.Show("O compromisso foi alterado com sucesso!");

            DialogResult = true;
            this.Close();

        }

        private void Excluir(object sender, RoutedEventArgs e)
        {

            var conexao = new ConexaoSqlServer();
            string sql = "DELETE FROM COMPROMISSOS WHERE NOME_COMPROMISSO ='" + TB1A + "' AND  DATA_COMPROMISSO = '" + DatePicker.Text + "'";

            conexao.Atualiza(sql);
            MessageBox.Show("O compromisso foi excuido com sucesso!");

            DialogResult = true;
            this.Close();

        }

        
    }
}
