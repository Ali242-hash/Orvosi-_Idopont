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
        Serverconnection connection = new Serverconnection();
        List<AppointmentInfo> allappointments = new List<AppointmentInfo>();
       
        public AdminHistory()
        {
            InitializeComponent();
            Loaded += (s, e) => LoadHistroy();
        }

        private async void LoadHistroy()
        {
            try
            {
                List<Appointment> appointments = await connection.GetAppointmentsHistory();

                if(appointments==null || appointments.Count == 0)
                {
                    MessageBox.Show("no appointments found");
                    return;
                }

                allappointments = appointments.Select(a=>new AppointmentInfo
                {
                    Docname = a.doctor,
                    name = a.Név,
                    date = a.LétrehozásDátuma.ToString("yyyy,MM,dd"),
                    timeslot = a.TimeslotId.ToString(),
                    Status_Condition = a.Status_Condition


                }).ToList();

                var doctornames = allappointments.Select(a=>a.Docname).Distinct().ToList();
                doctornames.Insert(0, "All");
                DoctorFilter.ItemsSource = doctornames;
                DoctorFilter.SelectedIndex = 0;

                AppointmentsListBox.ItemsSource = allappointments;
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
           
            string selectedDoctor = (DoctorFilter.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "All";

          
            string selectedStatus = (StatusFilter.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "All";

            DateTime? selectedDate = DateFilter.SelectedDate;

            var filter = allappointments.AsEnumerable();

           
            if (!string.IsNullOrEmpty(selectedDoctor) && selectedDoctor != "All")
            {
                filter = filter.Where(a => a.Docname.Equals(selectedDoctor, StringComparison.OrdinalIgnoreCase));
            }

            
            if (!string.IsNullOrEmpty(selectedStatus) && selectedStatus != "All")
            {
                filter = filter.Where(a => a.Status_Condition.Equals(selectedStatus, StringComparison.OrdinalIgnoreCase));
            }

        
            if (selectedDate.HasValue)
            {
                string dateString = selectedDate.Value.ToString("yyyy-MM-dd");
                filter = filter.Where(a => a.date == dateString);
            }

            AppointmentsListBox.ItemsSource = filter.ToList();
        }










    }


}
