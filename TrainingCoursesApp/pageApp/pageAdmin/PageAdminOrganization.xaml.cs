using System;
using Newtonsoft.Json;
using System.Collections.Generic;
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
using Excel = Microsoft.Office.Interop.Excel;
using TrainingCoursesApp.pageApp.pageAdmin;
using System.IO;

namespace TrainingCoursesApp.pageApp.pageAdmin
{
    /// <summary>
    /// Логика взаимодействия для PageAdminOrganization.xaml
    /// </summary>
    public partial class PageAdminOrganization : Page
    {
        public PageAdminOrganization()
        {
            InitializeComponent();
            lvOrganization.ItemsSource = JsonConvert.DeserializeObject<List<Organization>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/Organizations"));
        }
        private void btnReportOrganization_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workBook;
            Organization organization = lvOrganization.SelectedItem as Organization;
            List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/Courses?idOrganization={organization.OrganizationID}"));
            List<People> peoples = new List<People> { };
            for (int i = 0; i < courses.Count; i++)
            {
                int courseID = courses[i].CourseID;
                List<int> ids = JsonConvert.DeserializeObject<List<int>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/CoursePeoples?CourseID={courseID}"));
                List<People> peoplesTime = new List<People> { };
                for (int j = 0; j < ids.Count(); j++)
                {
                    peoplesTime.Add(JsonConvert.DeserializeObject<People>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/People/{ids[j]}")));
                }
                for (int j = 0; j < peoplesTime.Count; j++)
                {
                    if (!peoples.Contains(peoplesTime[j]))
                    {
                        peoples.Add(peoplesTime[j]);
                    }
                }
            }
            workBook = excelApp.Workbooks.Add();
            if (peoples.Count == 0)
            {
                MessageBox.Show("В данной организации никто курсы не проходил", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            for (int i = 0; i < peoples.Count; i++)
            {
                int idPeople = peoples[i].PeopleID;
                List<int> ids = JsonConvert.DeserializeObject<List<int>>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/CoursePeoples?PeopleID={idPeople}"));
                List<Course> coursesPeople = new List<Course> { };
                for (int j = 0; j < ids.Count(); j++)
                {
                    coursesPeople.Add(JsonConvert.DeserializeObject<Course>(ClassHelpURL.GetResponse($"http://localhost:{ClassPort.port}/api/Courses?idCourse={ids[j]}")));
                }
                int counter = 0;
                if (coursesPeople.Count == 0)
                    continue;
                Excel.Worksheet workSheet;
                workSheet = workBook.ActiveSheet as Excel.Worksheet;
                workSheet.Name = $"{peoples[i].FirstName} {peoples[i].SecondName} {peoples[i].ThirdName}";
                for (int j = 0; j < coursesPeople.Count; j++)
                {
                    workSheet.Cells[1, counter + 1] = "Название курса";
                    workSheet.Cells[1, counter + 2] = "Организация";
                    workSheet.Cells[1, counter + 3] = "Дата начала курса";
                    workSheet.Cells[1, counter + 4] = "Дата окончания курса";
                    workSheet.Cells[1, counter + 5] = "Количество часов";
                    workSheet.Cells[1, counter + 6] = "Количество участников";

                    workSheet.Cells[2, counter + 1] = coursesPeople[j].Program;
                    workSheet.Cells[2, counter + 2] = organization.Title;
                    workSheet.Cells[2, counter + 3] = coursesPeople[j].PlanStart;
                    workSheet.Cells[2, counter + 4] = coursesPeople[j].PlanEnd;
                    workSheet.Cells[2, counter + 5] = coursesPeople[j].CountHours;
                    workSheet.Cells[2, counter + 6] = coursesPeople[j].CountPeopleNow;

                    int idCourse = coursesPeople[j].CourseID;
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
            }
            workBook.SaveAs($@"{Directory.GetCurrentDirectory()}\Отчёты\По организациям\{(lvOrganization.SelectedItem as Organization).Title}.xlsx");
            MessageBox.Show($@"Файл с именем '{workBook.Name}' сохранён в папку '{Directory.GetCurrentDirectory()}\Отчёты\По организациям'",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            excelApp.Visible = true;
            excelApp.UserControl = true;
        }
    }
}
