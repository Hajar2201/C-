using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }

    private void BtnConnect_Click(object sender, RoutedEventArgs e)
    {
        string connectionString = "Server=localhost;Database=nom_de_votre_base_de_donnees;User Id=" + txtUsername.Text + ";Password=" + txtPassword.Password + ";";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                MessageBox.Show("Connexion réussie !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connexion : " + ex.Message);
            }
        }
    }
}