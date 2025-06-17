using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Configuration;
using System.Windows.Controls;
using static Dashboard_Admin.MainWindow;

namespace Dashboard_Admin
{
    public partial class CongesPage : Window, IDisposable
    {
        private readonly string connectionString;
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["MySQLConnectionString"];

        public CongesPage()
        {
            if (settings != null)
            {
                connectionString = settings.ConnectionString;
            }
            else
            {
                MessageBox.Show("bdd non co.");
            }
            InitializeComponent();
            LoadConges();

        }

        private void LoadConges()
        {


            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM conges";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridConge.ItemsSource = dataTable.DefaultView;
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

        private void dataGridConge_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        public void BtnDeconnexion_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }


        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            var ajouterCongeWindow = new AjouterCongeWindow();
            ajouterCongeWindow.ShowDialog();

            if (ajouterCongeWindow.DialogResult == true)
            {
                AjouterCongeWindow nouveauConge = ajouterCongeWindow.GetConge();

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO conges (Utilisateur, DateDeDebut, DateDeFin, Nombre_jour, Statut) " +
                                       "VALUES (@Utilisateur, @DateDeDebut, @DateDeFin, @Nombre_jour, @Statut)";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Utilisateur", nouveauConge.Utilisateur);
                        command.Parameters.AddWithValue("@DateDeDebut", nouveauConge.DateDeDebut);
                        command.Parameters.AddWithValue("@DateDeFin", nouveauConge.DateDeFin);
                        command.Parameters.AddWithValue("@Nombre_jour", nouveauConge.NombreJour);
                        command.Parameters.AddWithValue("@Statut", nouveauConge.Statut);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Le congé a été ajouté avec succès !");
                        LoadConges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de l'ajout du congé : " + ex.Message);
                }
            }
        }

        /*public void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            var ajouterCongeWindow = new AjouterCongeWindow();
            ajouterCongeWindow.ShowDialog();

            if (ajouterCongeWindow.DialogResult == true)
            {
                // Récupérer le nouveau congé à partir de la fenêtre AjouterCongeWindow
                AjouterCongeWindow nouveauConge = ajouterCongeWindow.GetConge();

                try
                {
                    // Insérer les données du nouveau congé dans la base de données
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO archives (Utilisateur, DateDeDebut, DateDeFin, Nombre_jour, Statut) " +
                                       "VALUES (@Utilisateur, @DateDeDebut, @DateDeFin, @Nombre_jour, @Statut)";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Utilisateur", nouveauConge.Utilisateur);
                        command.Parameters.AddWithValue("@DateDeDebut", nouveauConge.DateDeDebut);
                        command.Parameters.AddWithValue("@DateDeFin", nouveauConge.DateDeFin);
                        command.Parameters.AddWithValue("@Nombre_jour", nouveauConge.NombreJour);
                        command.Parameters.AddWithValue("@Statut", nouveauConge.Statut);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Le congé a été ajouté avec succès !");

                        // Rafraîchir les données dans le DataGrid après l'ajout
                        LoadConges();
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Erreur lors de l'ajout du congé : " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Une erreur s'est produite : " + ex.Message);
                }
            }
        }*/




        public void BtnSupprimer_Click(object sender, RoutedEventArgs e) {
        // Vérifier si le conges est sélectionné


        // Récupérer l'ID de le conge sélectionné
          DataRowView row = (DataRowView)dataGridConge.SelectedItem;
        int id = Convert.ToInt32(row["IdConges"]);

        // Demander à l'utilisateur de confirmer la suppression
        MessageBoxResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce conges ?", "Confirmation de suppression", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            // Supprimer le conge de la base de données
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM conges WHERE idConges = @idConges";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@idConges", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("L'article a été supprimé avec succès.");
                        // Actualiser la liste des conges après la suppression
                        LoadConges();
                    }
                    else
                    {
                        MessageBox.Show("Aucun conges n'a été supprimé.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la suppression de l'article : " + ex.Message);
                }
            }
        }
    }

    private void BtnModification_Click(object sender, RoutedEventArgs e)
    {


        // Vérifiez si un conges est sélectionné
        if (dataGridConge.SelectedItem != null)
        {
            // Récupérez l'ID du conges sélectionné
            DataRowView row = (DataRowView)dataGridConge.SelectedItem;
            int IdConges = Convert.ToInt32(row["IdConges"]);
                // Ouvrez la fenêtre de modification de l'article avec l'ID de l'article
                Modifierconges modifierConges = new Modifierconges(IdConges);
            modifierConges.ShowDialog();
                // Après la modification de l'article, actualisez la liste des articles
                LoadConges();
        }
        else
        {
            MessageBox.Show("Veuillez sélectionner un article à modifier.");
        }
    }

}
    
}

