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
        private readonly Serverconnection connection = new Serverconnection();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void PDF_Generator(object sender, EventArgs e)
        {
            var document = new PDF_generator();
            document.GeneratePdfAndShow();
        }

        private async void Book_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(fullnameInput.Text) ||
                string.IsNullOrEmpty(emailinput.Text) ||
                string.IsNullOrEmpty(passwordinput.Password) ||
                string.IsNullOrEmpty(userinput.Text) ||
                roleinput.SelectedItem == null)
            {
                MessageBox.Show("Please enter all required fields.");
                return;
            }

            string role = (roleinput.SelectedItem as ComboBoxItem)?.Content.ToString();

            bool success = await connection.Registration(
        userinput.Text,                
        passwordinput.Password,        
        emailinput.Text,               
        role,                       
        fullnameInput.Text            
    );

            MessageBox.Show(success ? "Registration confirmed" : "Registration failed");
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = LoginUserInput.Text;
            string password = LoginPassInput.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your credentials.");
                return;
            }

            bool isLoggedIn = await connection.Login(username, password);

            MessageBox.Show(isLoggedIn ? "Login successful" : "Login failed");
        }

        private async void BookAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TimeslotInput.Text, out int timeslot))
            {
                MessageBox.Show("Please enter a valid timeslot ID.");
                return;
            }

            bool success = await connection.loginAppointment(timeslot, fullnameInput.Text);

            MessageBox.Show(success
                ? "Appointment booked successfully"
                : "Booking failed. Make sure you are logged in.");
        }

        public void Cancel_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Your appointment was cancelled.");
            this.Close();
        }
    }
}
