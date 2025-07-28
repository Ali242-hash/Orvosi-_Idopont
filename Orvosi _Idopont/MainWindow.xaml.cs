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

        private void Window_key(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var allkey = Keyboard.FocusedElement;

                if (allkey == Docpage)
                    Doc_Click(sender, e);
                else if (allkey == Bookpage)
                    Book_Click(sender, e);
                else if (allkey == cancelconfirm)
                    Cancel_click(sender, e);
                else if (allkey == adminpage)
                    Return_click(sender, e);
            }
        }

        public void Cancel_click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MessageBox.Show("Your appointment has been canceled");
        }

        async void Book_Click(object sender, RoutedEventArgs e)
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

            string rolechange = roleinput.Text.ToLower();

            if (rolechange != "patient" && rolechange != "doctor" && rolechange != "admin")
            {
                MessageBox.Show("Please select a valid role");
                return;
            }

            Userprofile oneUser = new Userprofile()
            {
                Fullname = FullnameInput.Text,
                email = emailinput.Text,
                username = userinput.Text,
                password = passwordinput.Text,
                létrehozásDátuma = DateTime.Now,
                role = rolechange,
            };

            await connection.Registration(
                       userinput.Text,
                        passwordinput.Text,
                       emailinput.Text,
                        rolechange,
                    FullnameInput.Text
               );

            MessageBox.Show("Your registration is confirmed");
        }

        private void Doc_Click(object sender, RoutedEventArgs e)
        {
            DoctorsInfo doctorsInfo = new DoctorsInfo();
            doctorsInfo.Show();
            this.Close();
        }

        private List<string> GenerateSlot(TimeSpan start, TimeSpan end, int minutes)
        {
            var slots = new List<string>();

            for (TimeSpan time = start; time < end; time = time.Add(TimeSpan.FromMinutes(minutes)))
            {
                DateTime timedate = DateTime.Today.Add(time);
                slots.Add(timedate.ToString("hh:mm tt"));
            }
            return slots;
        }

   

        

        private void Return_click(object sender, RoutedEventArgs e)
        {
            Administration adminwindow = new Administration();
            adminwindow.Show();
            this.Close();
        }
    }
}
