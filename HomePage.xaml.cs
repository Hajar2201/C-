using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Windows;

namespace Dashboard_Admin
{
    public partial class HomePage : Window
    {
        private readonly string connectionString;

        public HomePage()
        {
            InitializeComponent();
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["MySQLConnectionString"];
            if (settings != null)
            {
                connectionString = settings.ConnectionString;
            }
            else
            {
                MessageBox.Show("bdd non co.");
            }
        }

        private void BtnDemandesConges_Click(object sender, RoutedEventArgs e)
        {
            
            // Redirection vers la page pour voir toutes les congés
            CongesPage congesPage = new CongesPage();
            congesPage.Show();
            this.Close();
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (UsersPage usersPage = new UsersPage())
                {
                    usersPage.Show();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur s'est produite : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btndepart_Click(object sender, RoutedEventArgs e)
        {


            depart depart = new depart();
            depart.Show();
            this.Close();
            
            
        }

        private void Btnarrivee_Click(object sender, RoutedEventArgs e)
        {


            arrivee arrivee = new arrivee();
            arrivee.Show();
            this.Close();


        }
    }

}
