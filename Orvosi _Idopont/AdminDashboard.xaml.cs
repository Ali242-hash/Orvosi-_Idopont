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
    public partial class AdminDashboard : Window
    {
        Serverconnection connection = new Serverconnection();
        public AdminDashboard()
        {
            InitializeComponent();
            Loading_Info();
            Load_Appointments();
        }

   

        async void Loading_Info()
        {
            try
            {
                if (Patientinfo == null)
                {
                    MessageBox.Show("The information of patient is missing");
                    return;
                }

                Patientinfo.Children.Clear();
                List<Userprofile> list = await connection.GetUserprofiles();

                foreach (Userprofile profile in list)
                {
                    Grid grid = new Grid();
                    grid.Margin = new Thickness(5);

                    for (int i = 0; i < 7; i++)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                    }

                    Label nameLabel = new Label() { Content = profile.Fullname, FontWeight = FontWeights.Bold, HorizontalAlignment = HorizontalAlignment.Center };
                    Grid.SetColumn(nameLabel, 0);
                    grid.Children.Add(nameLabel);

                    Label usernameLabel = new Label() { Content = profile.Username, HorizontalAlignment = HorizontalAlignment.Center };
                    Grid.SetColumn(usernameLabel, 1);
                    grid.Children.Add(usernameLabel);

                    Label emailLabel = new Label() { Content = profile.Email, HorizontalAlignment = HorizontalAlignment.Center };
                    Grid.SetColumn(emailLabel, 2);
                    grid.Children.Add(emailLabel);

                    Label roleLabel = new Label() { Content = profile.Role, HorizontalAlignment = HorizontalAlignment.Center };
                    Grid.SetColumn(roleLabel, 3);
                    grid.Children.Add(roleLabel);

                    Button activateButton = new Button()
                    {
                        Content = "Activate",
                        Tag = profile.Id,
                        Width = 80,
                        Margin = new Thickness(5),
                        Background = Brushes.LightGreen,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Grid.SetColumn(activateButton, 4);
                    grid.Children.Add(activateButton);
                    activateButton.Click += async (s, e) =>
                    {
                        await connection.ActivateUser((int)activateButton.Tag);
                        Loading_Info();
                    };

                    Button deactivateButton = new Button()
                    {
                        Content = "Deactivate",
                        Tag = profile.Id,
                        Width = 80,
                        Margin = new Thickness(5),
                        Background = Brushes.OrangeRed,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Grid.SetColumn(deactivateButton, 5);
                    grid.Children.Add(deactivateButton);
                    deactivateButton.Click += async (s, e) =>
                    {
                        await connection.DeactivateUser((int)deactivateButton.Tag);
                        Loading_Info();
                    };

                    Button deleteButton = new Button()
                    {
                        Content = "Delete",
                        Tag = profile.Id,
                        Width = 80,
                        Margin = new Thickness(5),
                        Background = Brushes.LightCoral,
                        Foreground = Brushes.White,
                        FontWeight = FontWeights.Bold,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Grid.SetColumn(deleteButton, 6);
                    grid.Children.Add(deleteButton);
                    deleteButton.Click += DeleteEvent;

                    Patientinfo.Children.Add(grid);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Load_Appointments()
        {
            List<Appointment> appointments = await connection.GetAppointmentsHistory();

            foreach (Appointment app in appointments)
            {
                Grid grid = new Grid();
                grid.Margin = new Thickness(5);
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                StackPanel panel = new StackPanel() { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
                panel.Children.Add(new Label() { Content = $"Timeslot ID: {app.TimeslotId}  ", FontWeight = FontWeights.Bold });
                panel.Children.Add(new Label() { Content = $"Patient Name: {app.Név}  ", FontWeight = FontWeights.Normal });
                panel.Children.Add(new Label() { Content = $"Patient ID: {app.PaciensId}  ", FontWeight = FontWeights.Normal });
                panel.Children.Add(new Label() { Content = $"Created: {app.LétrehozásDátuma}  ", FontWeight = FontWeights.Normal });

                Grid.SetColumn(panel, 0);
                grid.Children.Add(panel);

                Patientinfo.Children.Add(grid);
            }
        }

        async void DeleteEvent(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                await connection.Deleteone((int)button.Tag);
                Loading_Info();
            }
        }
    }
}
