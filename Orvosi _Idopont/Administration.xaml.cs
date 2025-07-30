using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Orvosi__Idopont
{
    public partial class Administration : Window
    {
        Serverconnection connection = new Serverconnection();
        public Administration()
        {
            InitializeComponent();
            Loadinginfo();
        }

        async void Loadinginfo()
        {
            try
            {
                if (Patientinfo == null)
                {
                    MessageBox.Show("There is no patient information on this list");
                    return;
                }
                Patientinfo.Children.Clear();
                List<Userprofile> list = await connection.GetUserprofiles();
                foreach (Userprofile profile in list)
                {
                    
                    Grid grid = new Grid();
                    grid.Margin = new Thickness(5);
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1,GridUnitType.Star) });

                    StackPanel panelgrid = new StackPanel() { Orientation=Orientation.Vertical};
                    panelgrid.Children.Add(new Label() { Content = $"Name: {profile.Fullname}" });
                    panelgrid.Children.Add(new Label() { Content=$"Username: {profile.username}"});
                    panelgrid.Children.Add(new Label() { Content = $"Password: {profile.password}" });
                    panelgrid.Children.Add(new Label() { Content = $"Role: {profile.role}" });
                    panelgrid.Children.Add(new Label() { Content = $"Date: {profile.létrehozásDátuma}" });

                    Grid.SetColumn(panelgrid, 0);
                    grid.Children.Add(panelgrid);

                    Button onebutton = new Button()
                    {
                        Content = "Torles",
                        Tag = profile.id,
                        BorderThickness = new Thickness(5),
                        BorderBrush=new SolidColorBrush(Colors.AliceBlue),
                        Width = 80,
                        Height = 30,
                        Margin = new Thickness(5),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    Grid.SetColumn(onebutton, 1);
                    grid.Children.Add(onebutton);
                    onebutton.Click += DeleteEvent;

                    Patientinfo.Children.Add(grid);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        async void DeleteEvent(object s, RoutedEventArgs e)
        {
            Button button = s as Button;
            if (button != null)
            {
                await connection.Deleteone((int)button.Tag);
                Loadinginfo();
            }
        }

        private void Enter_admin(object sender, EventArgs e)
        {
            string adminmail = Adminemail.Text;
            string adminPass = AdminPass.Text;

            string inputadmin = "admin@admin.com";
            string inputpass = "admin123qwe";

            if (string.IsNullOrEmpty(adminmail) || string.IsNullOrEmpty(adminPass) ||
                adminmail == "Email" || adminPass == "Password")
            {
                MessageBox.Show("Please enter your credentials.");
                return;
            }

            if (adminmail == inputadmin && adminPass == inputpass)
            {
                MessageBox.Show("Admin registered");
                MainWindow mainpage = new MainWindow();
                mainpage.Show();
                
                Loadinginfo();
            }
            else
            {
                MessageBox.Show("Please enter correct information.");
            }
        }

        private void RemoveText(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && (tb.Text == "Email" || tb.Text == "Password"))
            {
                tb.Text = "";
                tb.Foreground = new SolidColorBrush(Colors.Firebrick);
            }
        }

        private void AddText(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb.Name == "Adminemail")
                {
                    tb.Text = "Email";
                    tb.Foreground = new SolidColorBrush(Colors.Firebrick);
                }
                else if (tb.Name == "AdminPass")
                {
                    tb.Text = "Password";
                    tb.Foreground = new SolidColorBrush(Colors.Firebrick);
                }
            }
        }

        private void Admin_Click(object s,KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                Enter_admin(s,e);
            }
        }
    }
}
