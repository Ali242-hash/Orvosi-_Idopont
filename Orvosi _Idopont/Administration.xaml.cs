using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Orvosi__Idopont
{
    public partial class Administration : Window
    {
        Serverconnection connection = new Serverconnection();
        public Administration()
        {
            InitializeComponent();
        }

        private async Task Enter_admin()
        {
            string adminmail = Adminemail.Text.Trim();
            string adminPass = AdminPass.Text.Trim();

            if (string.IsNullOrEmpty(adminmail) || string.IsNullOrEmpty(adminPass) ||
                adminmail == "Email" || adminPass == "Password")
            {
                MessageBox.Show("Please enter your credentials.");
                return;
            }

            try
            {
      
                bool success = await connection.Login(adminmail, adminPass);

                if (success)
                {
                    MessageBox.Show("Admin registered");
                    AdminDashboard adminDashboard = new AdminDashboard();
                    adminDashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid credential");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hiba {ex.Message}");
            }
        }


        private void RemoveText(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && (tb.Text == "Email" || tb.Text == "Password"))
            {
                tb.Text = "";
                tb.Foreground = new SolidColorBrush(Colors.Firebrick);
            }
        }

        private void AddText(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb.Name == "Adminemail")
                {
                    tb.Text = "Email";
                    tb.Foreground = new SolidColorBrush(Colors.Firebrick);
                }
                else if (tb.Name == "AdminPass")
                {
                    tb.Text = "Password";
                    tb.Foreground = new SolidColorBrush(Colors.Firebrick);
                }
            }
        }

        private async void Admin_Click(object s, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await Enter_admin();
            }
        }

        private async void Enter_admin(object sender, RoutedEventArgs e)
        {
            await Enter_admin();
        }

        private void Doc_Click(object sender, RoutedEventArgs e)
        {
            DoctorsInfo doctorsInfo = new DoctorsInfo();
            doctorsInfo.Show();
            this.Close();
        }

        private void Return_click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Histroy_click(object sender, RoutedEventArgs e)
        {
            AdminHistory histroy = new AdminHistory();
            histroy.Show();
            this.Close();
        }
    }
}
