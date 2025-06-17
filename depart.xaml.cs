using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;

namespace Dashboard_Admin
{
    /// <summary>
    /// Logique d'interaction pour depart.xaml
    /// </summary>
    public partial class depart : Window
    {
        private readonly string connectionString;

        public depart()
        {
            InitializeComponent();

            // Load connection string from configuration
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["MySQLConnectionString"];
            if (settings != null)
                connectionString = settings.ConnectionString;

            LoadCongesdepart();
        }

        private void LoadCongesdepart()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                MessageBox.Show("Chaîne de connexion non définie.");
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM conges WHERE DATEDIFF(DateDeDebut, CURDATE()) = 2";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridDepart.ItemsSource = dataTable.DefaultView;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur de chargement des articles : " + ex.Message);
                }
            }
        }
    }
}
