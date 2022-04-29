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
using Word = Microsoft.Office.Interop.Word;
using TrainingCoursesApp.pageApp.pageListener;
using Newtonsoft.Json;

namespace TrainingCoursesApp.pageApp.pageListener
{
    /// <summary>
    /// Логика взаимодействия для PageListenerCourse.xaml
    /// </summary>
    public partial class PageListenerCourse : Page
    {
        public int IDPeople;
        public PageListenerCourse(int idPeople)
        {
            InitializeComponent();
            IDPeople = idPeople;
            lvCourse.ItemsSource = JsonConvert.DeserializeObject<List<SelectCourseCourseID_Result>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/GetSelectCourseCourseID?idPeople={IDPeople}"));
        }

        private void btnMyCourses_Click(object sender, RoutedEventArgs e)
        {
            lvCourse.ItemsSource = JsonConvert.DeserializeObject<List<SelectCourseCourseID_Result>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/GetSelectCourseCourseID?idPeople={IDPeople}"));
        }

        private void btnAllCourses_Click(object sender, RoutedEventArgs e)
        {
            lvCourse.ItemsSource = JsonConvert.DeserializeObject<List<SelectCourse_Result>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/GetSelectCourse"));
        }
    }
}
