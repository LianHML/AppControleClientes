using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AppControleClientes.View;
using AppControleClientes.Models;
using System.Reflection;

namespace AppControleClientes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=localhost;Port=5432;UserID=postgres;Password=H4t3b7p9;Database=postgre;"

            using var connection = new NpgsqlConnection(connectionString);

            string sqlCommand = "SELECT * FROM clientes";

            var command = new NpgsqlCommand(sqlCommand);

            var list = new List<Cliente>();

            command.Connection = connection;

            connection.Open();

            var reader = command.ExecuteReader();

            var dataTable = new DataTable();

            dataTable.Load(reader);

            foreach(DataRow dataRow in dataTable.Rows)
            {
                var tipo = typeof(Cliente);

                var objeto = Activator.CreateInstance<Cliente>();

                foreach (DataColumn column in dataRow.Table.Columns)
                {
                    foreach (var pro in tipo.GetProperties())
                    {
                        if (pro.Name.ToLower() == column.ColumnName.ToLower())
                        {
                            if (dataRow[column.ColumnName] != DBNull.Value)
                            {
                                object value = dataRow[column.ColumnName];

                                pro.SetValue(objeto, value, null);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }

                list.Add(objeto);
            }

            DtgClientes.ItemsSource = list;
        }

        private void BtnExcluirClientes_Click(object sender, RoutedEventArgs e)
        {
            if (DtgClientes.SelectedItem is Cliente clienteSelecionado)
            {
                string delete = $"DELETE FROM clientes WHERE id = {clienteSelecionado.Id}";
                var command = new NpgsqlCommand(delete);
                string connectionString = "Server=localhost;Port=5432;UserId=postgres;Password=H4t3b7p9;Database=postgres;";
                using var connection = new NpgsqlConnection(connectionString);
                command.Connection = connection;
                connection.Open();
                int quantidadeDeLinhasAfetadas = command.ExecuteNonQuery();
                if (quantidadeDeLinhasAfetadas > 0)
                {
                    BtnBuscar_Click(this, e);
                }
                else
                {

                }
            }
        }

        private void BtnCadastrarClientes_Click(object sender, RoutedEventArgs e)
        {
            var janelaCadastro = new CadastroClientes();
            _ = janelaCadastro.ShowDialog();
        }
    }
}
