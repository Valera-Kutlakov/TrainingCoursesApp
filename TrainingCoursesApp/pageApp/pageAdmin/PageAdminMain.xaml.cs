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
    /// Логика взаимодействия для PageAdminMain.xaml
    /// </summary>
    public partial class PageAdminMain : Page
    {
        public PageAdminMain()
        {
            InitializeComponent();
        }

        private void btnCourse_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frame.Navigate(new PageAdminCourse());
        }

        private void btnOrganization_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frame.Navigate(new PageAdminOrganization());
        }

        private void btnGuide_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frame.Navigate(new PageAdminGuide());
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.frame.Navigate(new PageAdminReport());
        }
    }
}