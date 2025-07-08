using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Orvosi__Idopont
{
    /// <summary>
    /// Interaction logic for Administration.xaml
    /// </summary>
    public partial class Administration : Window
    {
        public Administration()
        {
            InitializeComponent();
        }

        private void Enter_admin(object sender, EventArgs e)
        {
            string adminmail = Adminemail.Text;
            string adminPass = AdminPass.Text;

            string inputadmin = "admin@admin.com";
            string inputpass = "admin123qwe";

          
            if (string.IsNullOrEmpty(adminmail) || string.IsNullOrEmpty(adminPass) ||
                adminmail == "Email" || adminPass == "Password")
            {
                MessageBox.Show("Please enter your credentials.");
                return;
            }

           
            if (adminmail == inputadmin && adminPass == inputpass)
            {
                MainWindow mainpage = new MainWindow();
                mainpage.Show();
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
    }
}
