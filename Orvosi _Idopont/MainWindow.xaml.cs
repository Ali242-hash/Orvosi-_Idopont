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
            Start();
        }

        async void Start()
        {
            try
            {
                if (UsersPanel == null)
                {
                    MessageBox.Show("Error: 'UsersPanel' not found.");
                    return;
                }

                UsersPanel.Children.Clear();

                List<Userprofile> list = await connection.GetUserprofiles();

                foreach (Userprofile profile in list)
                {
                    Grid grid = new Grid();
                    grid.Margin = new Thickness(5);
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });

                    Label onelabel = new Label()
                    {
                        Content = $"Name: {profile.Fullname}, Email: {profile.email}, Username: {profile.username}, Password: {profile.password}, Role: {profile.role}, TimeSlot: {profile.date}",
                        VerticalAlignment = VerticalAlignment.Center,
                        Padding = new Thickness(5)
                    };
                    Grid.SetColumn(onelabel, 0);

                    Button onebutton = new Button()
                    {
                        Content = "Torles",
                        Tag = profile.id,
                        Width = 80,
                        Height = 30,
                        Margin = new Thickness(5),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Grid.SetColumn(onebutton, 1);

                    onebutton.Click += DeleteEvent;

                    grid.Children.Add(onelabel);
                    grid.Children.Add(onebutton);

                    UsersPanel.Children.Add(grid);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in Start(): " + ex.Message);
            }
        }

        public void Cancel_click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MessageBox.Show("Your appointment has been canceled");
        }

        async void DeleteEvent(object s, RoutedEventArgs e)
        {
            Button button = s as Button;

            if (button != null)
            {
                await connection.Deleteone((int)button.Tag);
                Start();
            }
        }

        async void Book_Click(object sender, RoutedEventArgs e)
        {
            Userprofile oneUser = new Userprofile()
            {
                Fullname = FullnameInput.Text,
                email = emailinput.Text,
                username = userinput.Text,
                password = passwordinput.Text,
                date = DateTime.Now,
                role = roleinput.Text,
            };

            await connection.PostUserprofile(oneUser);

            Start();
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















    }
}