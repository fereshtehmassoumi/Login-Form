using Microsoft.Data.Sqlite;
using System.IO;
namespace WinFormsApp3
{

    public partial class Form1 : Form

    {
        public Form1()
        {
            InitializeComponent();
            EnsureDtabase();
        }


        private readonly String dbpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.db");
        private void EnsureDtabase()
        {
            using var connection = new SqliteConnection($"Data Source={dbpath}");
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
        CREATE TABLE IF NOT EXISTS Users (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Email TEXT NOT NULL UNIQUE,
            Password TEXT NOT NULL
        );
    ";
            cmd.ExecuteNonQuery();
        }
        private void SeedUser()
        {
            using var connection = new SqliteConnection($"Data Source ={dbpath}");
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"...";
        }



        private bool CheckLogin(string username, string password)
        {
            using var connection = new SqliteConnection($"Data Source={dbpath}");
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
        SELECT COUNT(1)
        FROM Users
        WHERE Username = $u AND Password = $p;
    ";
            cmd.Parameters.AddWithValue("$u", username);
            cmd.Parameters.AddWithValue("$p", password);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }
        private void RegisterUser(string email, string password)
        {
            using var connection = new SqliteConnection($"Data Source={dbpath}");
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO Users (Email, Password)
                VALUES ($e, $p);
            ";
            cmd.Parameters.AddWithValue("$e", email);
            cmd.Parameters.AddWithValue("$p", password);

            cmd.ExecuteNonQuery();
        }

        private bool UserExists(string email)
        {
            using var connection = new SqliteConnection($"Data Source={dbpath}");
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT COUNT(1) FROM Users WHERE Email = $e;";
            cmd.Parameters.AddWithValue("$e", email);

            long count = (long)cmd.ExecuteScalar();
            return count > 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //string name=;
            Form2 wf = new Form2(textBox1.Text);
            wf.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();
            string pass = textBox2.Text;

            if (email == "" || pass == "")
            {
                MessageBox.Show("Bitte E-Mail und Passwort eingeben!");
                return;
            }

            if (UserExists(email))
            {
                MessageBox.Show("Benutzer existiert bereits!");
                return;
            }

            RegisterUser(email, pass);
            MessageBox.Show("Registrierung erfolgreich!");
        }
    }
}
