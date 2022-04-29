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
    /// Логика взаимодействия для PageAdminCourse.xaml
    /// </summary>
    public partial class PageAdminCourse : Page
    {
        public PageAdminCourse()
        {
            InitializeComponent();
            lvCourse.ItemsSource = JsonConvert.DeserializeObject<List<SelectCourse_Result>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/GetSelectCourse"));
        }

        private void btnDetailCourse_Click(object sender, RoutedEventArgs e)
        {
            if (lvCourse.SelectedItems.Count > 1)
            {
                MessageBox.Show("Выделите только один объект из списка", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (lvCourse.SelectedItems.Count == 1)
                ClassFrame.frame.Navigate(new PageAdminCourseDetail((int)(lvCourse.SelectedItem as SelectCourse_Result).CourseID));
        }
    }
}