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
using TrainingCoursesApp.pageApp.pageAdmin;
using Newtonsoft.Json;

namespace TrainingCoursesApp.pageApp.pageAdmin
{
    /// <summary>
    /// Логика взаимодействия для PageAdminCoursePeople.xaml
    /// </summary>
    public partial class PageAdminCoursePeople : Page
    {
        public int courseID;
        public int peopleID;
        public PageAdminCoursePeople(int idCourse)
        {
            InitializeComponent();
            courseID = idCourse;
            List<int> ids = JsonConvert.DeserializeObject<List<int>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/CoursePeoples?CourseID={idCourse}"));
            List<People> peoples = new List<People> { };
            for (int j = 0; j < ids.Count(); j++)
            {
                peoples.Add(JsonConvert.DeserializeObject<People>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/People/{ids[j]}")));
            }
            lvPeople.ItemsSource = peoples;
        }
        private void btnCertificate_Click(object sender, RoutedEventArgs e)
        {
            Word.Document doc = null;
            try
            {
                // Создаём объект приложения
                Word.Application app = new Word.Application();
                // Путь до шаблона документа
                string source = $@"{System.IO.Directory.GetCurrentDirectory()}\Certificate.docx";
                // Открываем
                doc = app.Documents.Add(source);
                doc.Activate();

                // Добавляем информацию
                // wBookmarks содержит все закладки
                Word.Bookmarks wBookmarks = doc.Bookmarks;
                Word.Range wRange;
                int i = 0;
                Course course = JsonConvert.DeserializeObject<Course>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/Courses?idCourse={courseID}"));
                People people = lvPeople.SelectedItem as People;
                int organizationID = int.Parse(course.IDOrganization.ToString());
                Organization organization = JsonConvert.DeserializeObject<Organization>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/Organizations/{organizationID}"));
                string[] data = new string[15] {
                    organization.City,
                    DateFormat(course.PlanEnd),
                    DateFormat(course.PlanEnd),
                    DateFormat(course.PlanStart),
                    people.FirstName,
                    course.CountHours.ToString(),
                    organization.Title,
                    organization.Title,
                    course.Program,
                    organization.Rector,
                    organization.Region,
                    JsonConvert.DeserializeObject<string>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/GetSelectRegistrationNumber?CourseID={course.CourseID}&PeopleID={people.PeopleID}")),
                    people.SecondName + " " + people.ThirdName,
                    organization.Secretary,
                    DateTime.Today.Year.ToString()};
                foreach (Word.Bookmark mark in wBookmarks)
                {
                    wRange = mark.Range;
                    wRange.Text = data[i];
                    i++;
                }

                // Закрываем документ
                doc.SaveAs2($@"{System.IO.Directory.GetCurrentDirectory()}\Сертификаты\{people.FirstName} {people.SecondName} {people.ThirdName}");
                MessageBox.Show($@"Файл с именем '{doc.Name}' сохранён в папку '{System.IO.Directory.GetCurrentDirectory()}\Сертификаты'",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                doc.Close();
                doc = null;
            }
            catch (Exception ex)
            {
                // Если произошла ошибка, то закрываем документ и выводим информацию
                MessageBox.Show($"Во время выполнения произошла ошибка: {ex}");
                doc.Close();
                doc = null;
            }
        }
        public string DateFormat(DateTime date)
        {
            string[] monthString = new string[] { "января", "февраля", "марта", "апреля", "мая", "июня", "июля", "августа", "сентября", "октября", "ноября", "декабря" };
            string month = monthString[date.Month - 1];
            return $"{date.Day} {month} {date.Year}";
        }
    }
}
