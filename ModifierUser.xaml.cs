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

namespace Dashboard_Admin
{
  
    public partial class ModifierUser : Window
    {
        private int userId;
        public ModifierUser(int id)
        {
            InitializeComponent();
            this.userId = id;
        }


        private void BtnModification_Click(object sender, RoutedEventArgs e)
        {
            // Récupérer les détails de user saisis par l'utilisateur
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            // Autres détails de user...

            // Mettre à jour l'utilisateur dans la base de données
            using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE users SET username = @username, password = @password WHERE idUser = @idUser";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@idUser", userId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("L'user a été modifié avec succès !");
                        // Fermer la fenêtre de modification d'un utilisateur
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la modification de l'utilisateur.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors de la modification de l'utilisateur : " + ex.Message);
                }
            }
        }
    }
}
