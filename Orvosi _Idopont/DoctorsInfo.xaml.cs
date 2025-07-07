using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

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

        public void Start()
        {
            var listofDoctors = new List<DoctorInfo>
            {
                new DoctorInfo
                {
                    Docname = "Dr. Ali Ebtekar",
                    profilKépUrl = "https://cdn.pixabay.com/photo/2015/05/26/09/05/doctor-784329_1280.png",
                    specialty = "Dermatology",
                    treatments = "Offers acne treatment, mole and wart removal, chemical peels, psoriasis and eczema care, as well as Botox and dermal fillers.",
                    Description = "Board-certified dermatologist with over 10 years of experience in treating skin, hair, and nail disorders. He emphasizes preventive care and modern aesthetic treatments."
                },
                new DoctorInfo
                {
                    Docname = "Dr. Mátyás Palánki",
                    profilKépUrl = "https://cdn.pixabay.com/photo/2024/09/03/15/21/ai-generated-9019520_1280.png",
                    specialty = "Cardiology",
                    treatments = "Provides ECG and stress testing, hypertension and cholesterol management, coronary artery disease treatment, and heart failure care.",
                    Description = "Senior cardiologist known for his patient-centered approach. He specializes in managing heart disease, hypertension, and performing stress tests."
                },
                new DoctorInfo
                {
                    Docname = "Dr. Peter Busko",
                    profilKépUrl = "https://cdn.pixabay.com/photo/2023/12/15/18/40/ai-generated-8451277_1280.png",
                    specialty = "Orthopedics",
                    treatments = "Handles joint pain and arthritis care, fracture management, sports injury rehabilitation, spine treatments, and joint replacement consultations.",
                    Description = "Experienced orthopedist offering care for musculoskeletal issues including spine problems, fractures, and joint conditions."
                }
            };

            DoctorsList.ItemsSource = listofDoctors;

     
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }







    }


}
