using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Orvosi__Idopont
{
    public partial class MainWindow : Window
    {
        Serverconnection connection = new Serverconnection();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void PDF_Generator(object s, EventArgs e)
        {
           var document = new PDF_generator();
            document.GeneratePdfAndShow();
        }

   
   

       private async void Book_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullnameInput.Text) ||
               string.IsNullOrWhiteSpace(passwordinput.Text) ||
                string.IsNullOrWhiteSpace(userinput.Text) ||
               string.IsNullOrWhiteSpace(emailinput.Text) ||
                 string.IsNullOrWhiteSpace(roleinput.Text))
            {
                MessageBox.Show("Please fill in all the boxes");
                return;
            }

         

            Userprofile oneUser = new Userprofile()
            {
                Fullname = FullnameInput.Text,
                Email = emailinput.Text,
                Username = userinput.Text,
                Password = passwordinput.Text,
                LétrehozásDátuma = DateTime.Now,
          
            };

           
            await connection.Registration(
                 userinput.Text,
                 passwordinput.Text,
                 emailinput.Text,
                 roleinput.Text,        
                FullnameInput.Text
            );

            

            MessageBox.Show("Your registration is confirmed");
        }


   
        public void Cancel_click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MessageBox.Show("Your appointment has been canceled");
        }

     

 




    }
}
