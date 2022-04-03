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
using System.Windows.Shapes;

namespace TestingSystem
{
    /// <summary>
    /// Логика взаимодействия для EditTestQuestion.xaml
    /// </summary>
    public partial class EditTestQuestion : Window
    {
        private List<Tests> testsList;
        private List<Questions> questionList;
        private Tests _tests;

        public EditTestQuestion()
        {
            InitializeComponent();
            testsList = Connect.GetContext().Tests.ToList();
            TBTest.Text = testsList[0].Name_Test;
            TbTimeOnTest.Text = testsList[0].Time_On_Test.ToString();
            questionList = Connect.GetContext().Questions.Where(a => a.ID_Test == testsList[0].ID_Test).ToList();
            _tests = testsList[0];
            TbNumberTest.Text = "1";
        }

        private void SaveTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _tests.Name_Test = TBTest.Text;
                _tests.Time_On_Test = Convert.ToInt32(TbTimeOnTest.Text);
                Connect.GetContext().SaveChanges();
                MessageBox.Show("Изменеия сохранены");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void AddTest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddNext_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PreviousTest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteTest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveQuestion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpenAnswer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PreviousQuestion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
