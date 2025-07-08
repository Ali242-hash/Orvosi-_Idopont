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

      
        private void Enter_admin(object sender,EventArgs e)
        {
           string inputemail = Adminemail.Text;
           string inputpass = AdminPass.Text;

           string  adminemail = "admin@admin.com";
           string adminpass = "admin123qwe";

            if(string.IsNullOrEmpty(inputemail) || string.IsNullOrEmpty(inputpass) || adminemail=="Email" || adminpass == "Password")
            {
                MessageBox.Show("Please Enter your credential");
            }

            else if(inputemail==adminemail && inputpass == adminpass)
            {
                MainWindow mainpage = new MainWindow();
                mainpage.Show();
                this.Close();
            }

            else
            {
                MessageBox.Show("Please Enter correct information");
            }

        }

        private void RemoveText(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if(tb != null && tb.Text=="Email" || tb.Text=="Password" )
            {
                tb.Text = "";
                tb.Foreground = new SolidColorBrush(Colors.LightBlue);
            }
        }

        private void AddText(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if(tb!=null && string.IsNullOrEmpty(tb.Text) )
            {
                if (tb.Name == "Adminemail")
                    tb.Text = "Email";
                else if(tb.Name == "AdminPass")
                {
                    tb.Text = "Password";
                    tb.Foreground = new SolidColorBrush(Colors.LightBlue);
                }
            }
           
            
        }

}
}