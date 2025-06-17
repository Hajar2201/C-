using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using static Dashboard_Admin.CongesPage;

namespace Dashboard_Admin
{
    /// <summary>
    /// Logique d'interaction pour AjouterCongeWindow.xaml
    /// </summary>
    public partial class AjouterCongeWindow : Window
    {
            // Déclarations de vos contrôles pour l'utilisateur, la date de début, la date de fin, le nombre de jours et le statut
            // Assurez-vous que ces contrôles sont définis dans votre fichier XAML et qu'ils sont accessibles depuis cette classe

            public string Utilisateur { get; set; }
            public DateTime? DateDeDebut { get; set; }
            public DateTime? DateDeFin { get; set; }
            public int NombreJour { get; set; }
            public string Statut { get; set; }

            // Constructeur et autres membres de la classe

            // Méthode pour récupérer les informations saisies

            public AjouterCongeWindow GetConge()
            {
                return new AjouterCongeWindow
                {
                    Utilisateur = this.Utilisateur,
                    DateDeDebut = this.DateDeDebut,
                    DateDeFin = this.DateDeFin,
                    NombreJour = this.NombreJour,
                    Statut = this.Statut
                };
            }
        

        public AjouterCongeWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer les détails des conges saisis par l'utilisateur
            string Utilisateur = txtUtilisateur.Text;
            string DateDeDebut = txtDateDeDebut.Text;
            string DateDeFin = txtDateDeFin.Text;
            string Nombre_jour = txtNombre_jour.Text;
            // Autres détails de user...

            // Mettre à jour le conge dans la base de données
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO conges (Utilisateur, DateDeDebut, DateDeFin, Nombre_jour) "+"VALUES (@Utilisateur, @DateDeDebut, @DateDeFin, @Nombre_jour)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Utilisateur", Utilisateur);
                    cmd.Parameters.AddWithValue("@DateDeDebut", DateDeDebut);
                    cmd.Parameters.AddWithValue("@DateDeFin", DateDeFin);
                    cmd.Parameters.AddWithValue("@Nombre_jour", Nombre_jour);


                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Le congé a été ajouté avec succès !");
                        // Fermer la fenêtre de modification d'un conge
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de l'ajout du congé.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de l'ajout du congé : " + ex.Message);
                }
            }
    }

    }

}

