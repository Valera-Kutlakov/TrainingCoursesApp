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
using Excel = Microsoft.Office.Interop.Excel;
using TrainingCoursesApp.pageApp.pageAdmin;
using Newtonsoft.Json;

namespace TrainingCoursesApp.pageApp.pageAdmin
{
    /// <summary>
    /// Логика взаимодействия для PageAdminReport.xaml
    /// </summary>
    public partial class PageAdminReport : Page
    {
        public PageAdminReport()
        {
            InitializeComponent();
            lvPeople.ItemsSource = JsonConvert.DeserializeObject<List<People>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/GetPeopleCategoryID"));
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workBook;
            People people = lvPeople.SelectedItem as People;
            List<int> ids = JsonConvert.DeserializeObject<List<int>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/CoursePeoples?PeopleID={people.PeopleID}"));
            List<Course> courses = new List<Course> { };
            for (int j = 0; j < ids.Count(); j++)
            {
                courses.Add(JsonConvert.DeserializeObject<Course>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/Courses?idCourse={ids[j]}")));
            }
            workBook = excelApp.Workbooks.Add();
            Excel.Worksheet workSheet;
            workSheet = workBook.ActiveSheet as Excel.Worksheet;
            workSheet.Name = $"{people.FirstName} {people.SecondName} {people.ThirdName}";
            int idPeople = people.PeopleID;
            int counter = 0;
            if (courses.Count == 0)
            {
                MessageBox.Show("Данный преподаватель не проходил курсов", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            for (int j = 0; j < courses.Count; j++)
            {
                int idOrganization = courses[j].IDOrganization;

                Organization organization = JsonConvert.DeserializeObject<Organization>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/Organizations/{idOrganization}"));

                workSheet.Cells[1, counter + 1] = "Название курса";
                workSheet.Cells[1, counter + 2] = "Организация";
                workSheet.Cells[1, counter + 3] = "Дата начала курса";
                workSheet.Cells[1, counter + 4] = "Дата окончания курса";
                workSheet.Cells[1, counter + 5] = "Количество часов";
                workSheet.Cells[1, counter + 6] = "Количество участников";

                workSheet.Cells[2, counter + 1] = courses[j].Program;
                workSheet.Cells[2, counter + 2] = organization.Title;
                workSheet.Cells[2, counter + 3] = courses[j].PlanStart;
                workSheet.Cells[2, counter + 4] = courses[j].PlanEnd;
                workSheet.Cells[2, counter + 5] = courses[j].CountHours;
                workSheet.Cells[2, counter + 6] = courses[j].CountPeopleNow;

                int idCourse = courses[j].CourseID;
                List<int> idsEducator = JsonConvert.DeserializeObject<List<int>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/GetCourseEducatorTopicEducatorID?courseID={idCourse}"));
                List<int> idsTopic = JsonConvert.DeserializeObject<List<int>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/GetCourseEducatorTopicTopicID?courseID={idCourse}"));
                List<Topic> topics = new List<Topic> { };
                for (int l = 0; l < idsTopic.Count(); l++)
                {
                    topics.Add(JsonConvert.DeserializeObject<Topic>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/Topics/{idsTopic[l]}")));
                }
                List<Educator> educators = new List<Educator> { };
                for (int l = 0; l < idsEducator.Count(); l++)
                {
                    educators.Add(JsonConvert.DeserializeObject<Educator>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/Educators/{idsEducator[l]}")));
                }

                workSheet.Cells[4, counter + 1] = "Тема курса";
                workSheet.Cells[4, counter + 2] = "Преподаватель";
                workSheet.Cells[4, counter + 3] = "Количество часов";

                int len = 0;

                for (int l = 0; l < topics.Count; l++)
                {
                    workSheet.Cells[5 + l, counter + 1] = topics[l].Title;
                    workSheet.Cells[5 + l, counter + 2] = educators[l].FirstName + " " + educators[l].SecondName + " " + educators[l].ThirdName;
                    workSheet.Cells[5 + l, counter + 3] = topics[l].CountHours;
                    len = 5 + l;
                }

                Excel.Range rng = workSheet.Range[workSheet.Cells[1, counter + 1], workSheet.Cells[2, counter + 6]];
                Excel.Range rng2 = workSheet.Range[workSheet.Cells[4, counter + 1], workSheet.Cells[len, counter + 3]];
                Excel.Borders border = rng.Borders;
                border.LineStyle = Excel.XlLineStyle.xlContinuous;
                border = rng2.Borders;
                border.LineStyle = Excel.XlLineStyle.xlContinuous;
                Excel.Range rng3 = workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[len, counter + 6]];
                rng3.EntireRow.AutoFit();
                rng3.EntireColumn.AutoFit();
                counter += 7;
            }
            workBook.Sheets.Add(workSheet);
            workBook.SaveAs($@"{System.IO.Directory.GetCurrentDirectory()}\Отчёты\По преподавателям\{(lvPeople.SelectedItem as People).FirstName} {(lvPeople.SelectedItem as People).SecondName} {(lvPeople.SelectedItem as People).ThirdName}.xlsx");
            MessageBox.Show($@"Файл с именем '{workBook.Name}' сохранён в папку '{System.IO.Directory.GetCurrentDirectory()}\Отчёты\По преподавателям'",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            excelApp.Visible = true;
            excelApp.UserControl = true;
        }
    }
}
