using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using TrainingCoursesApp.pageApp;

namespace TrainingCoursesApp.pageApp.pageGeneral
{
    /// <summary>
    /// Логика взаимодействия для PageAuthorization.xaml
    /// </summary>
    public partial class PageAuthorization : Page
    {
        public PageAuthorization()
        {
            InitializeComponent();
        }

        private void btnAuthorization_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClassPort.port = txbxPort.Text;
                People peopleFull = JsonConvert.DeserializeObject<People>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/GetPeopleLoginPassword?login={txbxLogin.Text}&password={txbxPassword.Text}"));
                People people = JsonConvert.DeserializeObject<People>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/GetPeopleLogin?login={txbxLogin.Text}"));
                if (peopleFull == null)
                {
                    MessageBox.Show("Данный пользователь отсутствует", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    if (peopleFull != null)
                    {
                        if (peopleFull.IDCategory == 1)
                        {
                            ClassFrame.frame.Navigate(new pageAdmin.PageAdminMain());
                        }
                        else
                        {
                            ClassFrame.frame.Navigate(new pageListener.PageListenerCourse(peopleFull.PeopleID));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message.ToString(), "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }
    }
}
