using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Orvosi__Idopont
{
    public partial class MainWindow : Window
    {
        Serverconnection connection = new Serverconnection("http://127.1.1.1:3000");

        public MainWindow()
        {
            InitializeComponent();
           
        }

      

        public void Cancel_click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MessageBox.Show("Your appointment has been canceled");
        }

       

        async void Book_Click(object sender, RoutedEventArgs e)
        {

            string rolechange = roleinput.Text.ToLower();

            if(rolechange!="patient" && rolechange!="doctor" && rolechange != "admin")
            {
                MessageBox.Show("Please select the valid roles ");
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

            await connection.PostUserprofile(oneUser);

            
        }

        private void Doc_Click(object sender, RoutedEventArgs e)
        {
            DoctorsInfo doctorsInfo = new DoctorsInfo();
            doctorsInfo.Show();
            this.Close();

        }


        private List<string> GenerateSlot(TimeSpan start, TimeSpan end, int munites)
        {
            var slots = new List<string>();

            for (TimeSpan time = start; time < end; time = time.Add(TimeSpan.FromMinutes(munites)))
            {
                DateTime timedate = DateTime.Today.Add(time);
                slots.Add(timedate.ToString("hh:mm tt"));
            };
            return slots;
        }

        private void Morning_Checked(object sender, RoutedEventArgs e)
        {
            TimeSlotsList.Items.Clear();

            var morningslots = GenerateSlot(TimeSpan.FromHours(9), TimeSpan.FromHours(12), 15);

            foreach (var item in morningslots)
            {
                TimeSlotsList.Items.Add(item);
            }
        }

        private void Afternoon_Checked(object sender, RoutedEventArgs e)
        {
            TimeSlotsList.Items.Clear();

            var afternoonSlot = GenerateSlot(TimeSpan.FromHours(13), TimeSpan.FromHours(17), 15);

            foreach (var item in afternoonSlot)
            {
                TimeSlotsList.Items.Add(item);
            }
        }

        private void Return_click(object sender, RoutedEventArgs e)
        {
            Administration adminwindow = new Administration();
            adminwindow.Show();
            this.Close();
        }






    }
}