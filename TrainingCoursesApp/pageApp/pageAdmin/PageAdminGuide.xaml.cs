using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrainingCoursesApp.data;
using TrainingCoursesApp.pageApp.pageAdmin;

namespace TrainingCoursesApp.pageApp.pageAdmin
{
    /// <summary>
    /// Логика взаимодействия для PageAdminGuide.xaml
    /// </summary>
    public partial class PageAdminGuide : Page
    {
        public int idTopic;
        public int idQualification;
        public PageAdminGuide()
        {
            InitializeComponent();

            lvQualification.ItemsSource = JsonConvert.DeserializeObject<List<Qualification>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/Qualifications"));
            lvTopic.ItemsSource = JsonConvert.DeserializeObject<List<Topic>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/Topics"));
        }
    }
}
