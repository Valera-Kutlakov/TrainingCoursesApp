using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Shapes;
using TrainingCoursesApp.data;
using TrainingCoursesApp.pageApp.pageGeneral;

namespace TrainingCoursesApp
{
    /// <summary>
    /// Логика взаимодействия для WindowMain.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        public WindowMain()
        {
            InitializeComponent();
            ClassFrame.frame = frameMain;
            ClassFrame.frame.Navigate(new PageAuthorization());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (ClassFrame.frame.CanGoBack)
            {
                ClassFrame.frame.GoBack();
            }
        }
    }
}
