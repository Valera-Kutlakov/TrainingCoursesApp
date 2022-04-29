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
    /// Логика взаимодействия для PageAdminCourseDetail.xaml
    /// </summary>
    public partial class PageAdminCourseDetail : Page
    {
        public int idCourse;
        public int idOrganization;
        public PageAdminCourseDetail(int id)
        {
            InitializeComponent();

            idCourse = id;
            Course course = JsonConvert.DeserializeObject<Course>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/Courses?idCourse={idCourse}"));
            idOrganization = course.IDOrganization;

            lvCourseDetail.ItemsSource = JsonConvert.DeserializeObject<List<SelectCourseEducatorTopicIDCourse_Result>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/GetSelectCourseEducatorTopicIDCourse_Result?courseID={idCourse}"));
        }

        private void btnPeople_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frame.Navigate(new PageAdminCoursePeople(idCourse));
        }
    }
}
