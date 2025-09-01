using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Orvosi__Idopont
{
    public partial class AdminHistory : Window
    {
        private List<AppointmentInfo> allAppointments = new List<AppointmentInfo>();
        private Serverconnection connection = new Serverconnection();

        public AdminHistory()
        {
            InitializeComponent();
            LoadAppointments();
        }

        private async void LoadAppointments()
        {
            try
            {
              
                List<Appointment> response = await connection.GetAppointments();
                if (response == null || response.Count == 0)
                {
                    MessageBox.Show("No appointments found.");
                    return;
                }

            
                allAppointments = response.Select(a => new AppointmentInfo
                {
                    doctor = a.doctor,
                    name = a.Név,
                    date = a.LétrehozásDátuma.ToString("yyyy-MM-dd"),
                    timeslot = a.TimeslotId.ToString(),
                    Status_Condition = a.Status_Condition
                }).ToList();

                AppointmentsListBox.ItemsSource = allAppointments;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load appointments: " + ex.Message);
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            var filtered = allAppointments;

            if (DoctorFilter.SelectedItem != null)
            {
                string doctor = DoctorFilter.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(doctor))
                {
                    filtered = filtered.FindAll(a => a.doctor == doctor);
                }
            }

            if (StatusFilter.SelectedItem is ComboBoxItem statusItem)
            {
                string status = statusItem.Content.ToString();
                if (status != "All")
                {
                    filtered = filtered.FindAll(a => a.Status_Condition == status);
                }
            }

            if (DateFilter.SelectedDate != null)
            {
                DateTime selectedDate = DateFilter.SelectedDate.Value.Date;
                filtered = filtered.FindAll(a => DateTime.Parse(a.date).Date == selectedDate);
            }

            AppointmentsListBox.ItemsSource = filtered;
        }
    }

    public class AppointmentInfo
    {
        public string doctor { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string timeslot { get; set; }
        public string Status_Condition { get; set; }
    }
}
