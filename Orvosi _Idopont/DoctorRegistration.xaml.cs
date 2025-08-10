using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Orvosi__Idopont
{
    public partial class DoctorRegistration : Window
    {
        public DoctorRegistration()
        {
            InitializeComponent();
        }

        private void Doc_information(object sender, RoutedEventArgs e)
        {
            string docadmininput = DocAdmin.Text;
            string docpassinput = Docpass.Text;

            string inputadmindoc = "doc@doc.com";
            string inputpassdoc = "doc123qwe";

            if (string.IsNullOrEmpty(docadmininput) || string.IsNullOrEmpty(docpassinput) ||
                string.IsNullOrEmpty(inputadmindoc) || string.IsNullOrEmpty(inputpassdoc))
            {
                MessageBox.Show("Please Enter your Doctor email & password");
                return;
            }

            if (docadmininput == inputadmindoc && docpassinput == inputpassdoc)
            {
                MessageBox.Show("Doctor registered");
                DoctorsInfo docpage = new DoctorsInfo();
                docpage.Show();
                this.Close();
            }
        }

        private void RemoveText(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb != null && (tb.Text == "Email" || tb.Text == "Password"))
            {
                tb.Text = "";
                tb.Foreground = new SolidColorBrush(Colors.AliceBlue);
            }
        }

        private void AddText(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb != null && string.IsNullOrEmpty(tb.Text))
            {
                if (tb.Name == "DocAdmin")
                {
                    tb.Text = "Email";
                    tb.Foreground = new SolidColorBrush(Colors.AliceBlue);
                }
                else if (tb.Name == "Docpass")
                {
                    tb.Text = "Password";
                    tb.Foreground = new SolidColorBrush(Colors.AliceBlue);
                }
            }
        }

        private void Doc_kosnane(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Doc_information(sender, e);
            }
        }
    }
}
