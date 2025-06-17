using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;

namespace Dashboard_Admin
{
    public partial class UsersPage : Window, IDisposable
    {
        private readonly string connectionString;

        public UsersPage()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["MySQLConnectionString"];
            if (settings != null)
            {
                connectionString = settings.ConnectionString;
            }
            else
            {
                MessageBox.Show("bdd non co.");
            }
            InitializeComponent();
            LoadUsers();
           
        }

        private void LoadUsers()
        {

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM users";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridUser.ItemsSource = dataTable.DefaultView;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur de chargement des articles : " + ex.Message);
                }

            }
        }

        public void BtnRetour_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.Show();
            Close();
        }

        // Implémentation de l'interface IDisposable
        public void Dispose()
        {
            // Libérer les éventuelles ressources non managées ici
        }

        public void BtnDeconnexion_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            // Vérifier si un article est sélectionné
            if (dataGridUser.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur à supprimer.");
                return;
            }

            // Récupérer l'ID de l'user sélectionné
            DataRowView row = (DataRowView)dataGridUser.SelectedItem;
            int id = Convert.ToInt32(row["idUser"]);

            // Demander à l'utilisateur de confirmer la suppression
            MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet utilisateur ?", "Confirmation de suppression", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                // Supprimer l'article de la base de données
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM users WHERE idUser = @idUser";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@idUser", id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("L'article a été supprimé avec succès.");
                            // Actualiser la liste des articles après la suppression
                            LoadUsers();
                        }
                        else
                        {
                            MessageBox.Show("Aucun utilisateur n'a été supprimé.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de la suppression de l'article : " + ex.Message);
                    }
                }
            }
        }

        private void DataGridUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Votre logique pour gérer la sélection dans le DataGrid va ici
        }

        private void BtnModification_Click(object sender, RoutedEventArgs e)
        {
            // Vérifiez si un user est sélectionné
            if (dataGridUser.SelectedItem != null)
            {
                // Récupérez l'ID de user sélectionné
                DataRowView row = (DataRowView)dataGridUser.SelectedItem;
                int userId = Convert.ToInt32(row["idUser"]);
                // Ouvrez la fenêtre de modification de l'article avec l'ID de l'article
                ModifierUser modifierUser = new ModifierUser(userId);
                modifierUser.ShowDialog();
                // Après la modification de l'article, actualisez la liste des articles
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un article à modifier.");
            }
        }
    }
}