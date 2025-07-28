using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;
using System.Windows.Interop;

namespace Orvosi__Idopont
{
    /// <summary>
    /// Interaction logic for DoctorsInfo.xaml
    /// </summary>
    public partial class DoctorsInfo : Window
    {
        public DoctorsInfo()
        {
            InitializeComponent();
            Start();
        }

        public async void Start()
        {
            HttpClient client = new HttpClient();
            string url = "http://127.1.1.1:3000/doctorprofiles";

            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonData = await response.Content.ReadAsStringAsync();

                var Doctors = JsonConvert.DeserializeObject<List<DoctorInfo>>(jsonData);

                DoctorsList.ItemsSource = Doctors;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load: " + ex.Message);
            }
        }

        private List<string> Doctor_appoin(TimeSpan start, TimeSpan end, int minutes)
        {
            var Applist = new List<string>();

            for (TimeSpan time = start; time < end; time = time.Add(TimeSpan.FromMinutes(minutes)))
            {
                DateTime doctimedate = DateTime.Today.Add(time);
                Applist.Add(doctimedate.ToString("hh:mm tt"));
            }
            return Applist;
        }

        private void Morning_Doc(object s, RoutedEventArgs e)
        {
            Afternoondoc.IsChecked = false;
            Docshift.Items.Clear();

            var Docmorn_shift = Doctor_appoin(TimeSpan.FromHours(9), TimeSpan.FromHours(12), 15);
            foreach (var item in Docmorn_shift)
            {
                Docshift.Items.Add(item);
            }
        }

        private void Afternoon_Doc(object s, RoutedEventArgs e)
        {
            Morningdoc.IsChecked = false;
            Docshift.Items.Clear();

            var Doc_shiftafter = Doctor_appoin(TimeSpan.FromHours(13), TimeSpan.FromHours(17), 15);
            foreach (var item in Doc_shiftafter)
            {
                Docshift.Items.Add(item);
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private async void Shift_Confirm(object sender, RoutedEventArgs e)
        {
            if (DoctorDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select either(Morning or Afternoon) Shift");
                return;
            }

            var selectedDoctor = DoctorsList.SelectedItem as DoctorInfo;
            if (selectedDoctor == null)
            {
                MessageBox.Show("Please select a doctor from the list.");
                return;
            }

            var shifttype = Morningdoc.IsChecked == true ? "délelőtt" :
                            Afternoondoc.IsChecked == true ? "délután" : null;

            var selectedDate = DoctorDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd");

            var jsonData = new
            {
                doctorId = selectedDoctor.id,
                dátum = selectedDate,
                típus = shifttype,
                active = true,
            };

            using (HttpClient client = new HttpClient())
            {
                var stringJson = JsonConvert.SerializeObject(jsonData);
                var content = new StringContent(stringJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://127.1.1.1:3000/shifts", content);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Your shift confirmed");
                }
                else
                {
                    string mesg = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Your shift login did not go through, please try again\n{mesg}");
                }
            }
        }


    }
}