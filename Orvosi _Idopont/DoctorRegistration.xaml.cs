using System;
using System.Collections.Generic;
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

namespace Orvosi__Idopont
{
    /// <summary>
    /// Interaction logic for DoctorRegistration.xaml
    /// </summary>
    public partial class DoctorRegistration : Window
    {
        public DoctorRegistration()
        {
            InitializeComponent();
        }

        private void Doc_information(object s,EventArgs e)
        {
            string docadmininput = DocAdmin.Text;
            string docpassinput = Docpass.Text;

            string inputadmindoc = "doc@doc.com";
            string inputpassdoc = "doc123qwe";

            if(string.IsNullOrEmpty(docadmininput)||string.IsNullOrEmpty(docpassinput)||string.IsNullOrEmpty(inputadmindoc)||string.IsNullOrEmpty(inputpassdoc))
            {
                MessageBox.Show("Please Enter your Doctor email & password");
                return;
            }
            
            if(docadmininput==inputadmindoc && docpassinput == inputpassdoc)
            {
                MessageBox.Show("Doctor registered");
                DoctorsInfo docpage = new DoctorsInfo();
                docpage.Show();
                this.Close();
            }

        }

        private void RemoveText(object s,EventArgs e)
        {
            TextBox tb = s as TextBox;

            if(tb!= null &&(tb.Text=="Email")||tb.Text=="Password")
            {
                tb.Text = "";
                tb.Foreground = new SolidColorBrush(Colors.AliceBlue);
            } 
        }

        private void AddText(object s,EventArgs e)
        {
           TextBox tb = s as TextBox ;

            if(tb!=null && string.IsNullOrEmpty(tb.Text) )
            {
                if(tb.Name== "DocAdmin")
                {
                    tb.Text = "Email";
                    tb.Foreground = new SolidColorBrush(Colors.AliceBlue);
                }

                else if(tb.Name== "Docpass")
                {
                    tb.Text = "Password";
                    tb.Foreground = new SolidColorBrush(Colors.AliceBlue);
                }
            }

            

        }

        private void Doc_kosnane(object s,KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Doc_information(s, e);
            }
        }



    }
}
