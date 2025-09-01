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
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        Serverconnection connection = new Serverconnection();
        public AdminDashboard()
        {
            InitializeComponent();
            Loading_Info();
            Load_Appointments();
        }

        private async void Load_Appointments()
        {
            List<Appointment> appointments = await connection.GetAppointments();

         /*   if (appointments == null)
            {
                MessageBox.Show("No appointments found");
                return;
            }

            if (appointments.Count == 0)
            {
                MessageBox.Show("No appointments found");
                return;
            }
         */

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
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4, GridUnitType.Star) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

                    StackPanel panel = new StackPanel() { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Left };
                    panel.Children.Add(new Label() { Content = $"Name: {profile.Fullname}  ", FontWeight = FontWeights.Bold });
                    panel.Children.Add(new Label() { Content = $"Username: {profile.Username}  ", FontWeight = FontWeights.Normal });
                    panel.Children.Add(new Label() { Content = $"Email: {profile.Email}  ", FontWeight = FontWeights.Normal });
                    panel.Children.Add(new Label() { Content = $"Role: {profile.Role}  ", FontWeight = FontWeights.Normal });

                    Grid.SetColumn(panel, 0);
                    grid.Children.Add(panel);

                    Button deleteButton = new Button()
                    {
                        Content = "Delete",
                        Tag = profile.Id,
                        BorderThickness = new Thickness(1),
                        BorderBrush = Brushes.Red,
                        Width = 80,
                        Height = 30,
                        Margin = new Thickness(5),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Background = Brushes.LightCoral,
                        Foreground = Brushes.White,
                        FontWeight = FontWeights.Bold
                    };
                    Grid.SetColumn(deleteButton, 1);
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
