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
    /// <summary>
    /// Logique d'interaction pour Modifierconges.xaml
    /// </summary>
    public partial class Modifierconges : Window
    {
            private int IdConges;
            public Modifierconges(int id)
            {
                InitializeComponent();
                this.IdConges = id;
            }


            private void BtnModification_Click(object sender, RoutedEventArgs e)
            {
                // Récupérer les détails des conges saisis par l'utilisateur
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
                        string query = "UPDATE conges SET DateDeDebut = @DateDeDebut, DateDeFin = @DateDeFin, Nombre_jour = @Nombre_jour WHERE IdConges = @IdConges";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@DateDeDebut", DateDeDebut);
                        cmd.Parameters.AddWithValue("@DateDeFin", DateDeFin);
                    cmd.Parameters.AddWithValue("@Nombre_jour", Nombre_jour);
                    cmd.Parameters.AddWithValue("@IdConges", IdConges);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Le congé a été modifié avec succès !");
                            // Fermer la fenêtre de modification d'un conge
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Erreur lors de la modification du congé.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur lors de la modification du congé : " + ex.Message);
                    }
                }
            }
        }
    }

