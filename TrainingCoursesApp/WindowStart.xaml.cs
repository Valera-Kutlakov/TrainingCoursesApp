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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrainingCoursesApp
{
    /// <summary>
    /// Логика взаимодействия для WindowStart.xaml
    /// </summary>
    public partial class WindowStart : Window
    {
        public WindowStart()
        {
            InitializeComponent();
            DoubleAnimation start = new DoubleAnimation();
            start.From = 0;
            start.To = 1;
            start.Duration = TimeSpan.FromSeconds(3);
            start.Completed += start_Completed;
            BeginAnimation(OpacityProperty, start);
        }

        private void start_Completed(object sender, EventArgs e)
        {
            DoubleAnimation start = new DoubleAnimation();
            start.From = 1;
            start.To = 0;
            start.Duration = TimeSpan.FromSeconds(3);
            start.Completed += main_Completed;
            BeginAnimation(OpacityProperty, start);
        }

        private void main_Completed(object sender, EventArgs e)
        {
            WindowMain windowMain = new WindowMain();
            windowMain.Show();
            Close();
        }
    }
}
