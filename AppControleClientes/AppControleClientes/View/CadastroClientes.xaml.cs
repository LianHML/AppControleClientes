using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppControleClientes.View
{
    /// <summary>
    /// Lógica interna para CadastroClientes.xaml
    /// </summary>
    public partial class CadastroClientes : Window
    {
        public CadastroClientes()
        {
            InitializeComponent();
        }

        private void BtnVoltar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string nome = TxtNome.Text;
            string empresa = TxtEmpresa.Text;
            string comando = $"insert into clientes(nome, empresa) values('{nome}', '{empresa}')";
            var command = new NpgsqlCommand(comando);
            string connectionString = "Server=localhost;Port=5432;UserId=postgres;Password=H4t3b7p9Database=postgres;";
            using var connection = new NpgsqlConnection(connectionString);
            command.Connection = connection;
            connection.Open();
            int quantidadeDeLinhasAfetadas = command.ExecuteNonQuery();
            if (quantidadeDeLinhasAfetadas > 0)
            {

            }
            else
            {

            }
        }
    }
}
