using System;
using System.Collections.Generic;
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

   



        private void Enter_admin(object sender, EventArgs e)
        {
            string adminmail = Adminemail.Text;
            string adminPass = AdminPass.Text;

            if (string.IsNullOrEmpty(adminmail) || string.IsNullOrEmpty(adminPass) ||
                adminmail == "Email" || adminPass == "Password")
            {
                MessageBox.Show("Please enter your credentials.");
                return;
            }

            if (adminmail == "admin@admin.com" && adminPass == "admin123qwe")
            {
                MessageBox.Show("Admin registered");
                AdminDashboard Register_Client = new AdminDashboard();
                Register_Client.Show();
                this.Close();

        
            }
            else
            {
                MessageBox.Show("Please enter correct information.");
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

        private void Admin_Click(object s, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Enter_admin(s, e);
            }
        }

        private void Doc_Click(object sender, RoutedEventArgs e)
        {
            DoctorsInfo doctorsInfo = new DoctorsInfo();
            doctorsInfo.Show();
            this.Close();
        }

       

        private void Return_click(object sender, RoutedEventArgs e)
        {
            Administration adminwindow = new Administration();
            adminwindow.Show();
            this.Close();
        }



    }
}