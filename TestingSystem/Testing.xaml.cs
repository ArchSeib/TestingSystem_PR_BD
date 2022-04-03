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
using System.Windows.Threading;

namespace TestingSystem
{
    /// <summary>
    /// Логика взаимодействия для Testing.xaml
    /// </summary>
   

    public partial class Testing : Window
    {
        private Users _user;
        private Tests _test;
        private List<Questions> QuestionsList = new List<Questions>();
        private int _countQuestions;
        private int _numberQuestions=0;
        private int Point = 0;

        private DispatcherTimer _timer;

        private int AnswerMin;
        private int AnswerSecond = 59;

        private int TestMin;
        private int TestSecond = 59;

        private Users_Tests _users_tests = new Users_Tests();


        public Testing(Tests test, Users user)
        {
            InitializeComponent();
            _user = user;
            _test = test;
            LBFIO.Text = _user.FIO;
            LBNameTest.Text = _test.Name_Test;
            QuestionsList = Connect.GetContext().Questions.Where(a => a.ID_Test == _test.ID_Test).ToList();
            _countQuestions = QuestionsList.Count;
            Fill();


            TestMin = _test.Time_On_Test -1;
            AnswerMin = TestMin / _countQuestions-1;

            TbTimerAnswer.Text = Convert.ToString(AnswerMin + ":" + AnswerSecond);
            TbTimerTest.Text = Convert.ToString(TestMin + ":" + TestSecond);

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Tick;
            _timer.Start();
        }

        public void Tick (Object sender, EventArgs e)
        {
            if(TestMin == 0 && TestSecond==0)
            {
                EndTest_Click(null, null);
                _timer.Stop();
            }
            else
            {
                if(TestSecond == 0)
                {
                    TestSecond = 59;
                    TestMin--;
                }
                else
                {
                    TestSecond--;
                }
            }

            if (AnswerMin == 0 && AnswerSecond == 0)
            {
                Answer_Click(null,null);
                AnswerMin = _test.Time_On_Test / _countQuestions;
                AnswerSecond = 59;
            }
            else
            {
                if (AnswerSecond == 0)
                {
                    AnswerSecond = 59;
                    AnswerMin--;
                }
                else
                {
                    AnswerSecond--;
                }
            }
            TbTimerAnswer.Text = Convert.ToString(AnswerMin + ":" + AnswerSecond);
            TbTimerTest.Text = Convert.ToString(TestMin + ":" + TestSecond);
        }

        public void Fill()
        {
            var _Questuion = QuestionsList[_numberQuestions];
            var ListAnswers = Connect.GetContext().Answers.Where(a => a.ID_Questuion == _Questuion.ID_Questuion).ToList();
            if (_numberQuestions != 0)
            {
                var _ItemQuestionList = QuestionsList[_numberQuestions - 1];
                Answers item = null;
                if (FirstAnswer.IsChecked == true)
                {
                    item = Connect.GetContext().Answers.Where(a => a.ID_Questuion == _ItemQuestionList.ID_Questuion).ToList()[0];
                }
                if (SecondAnswer.IsChecked == true)
                {
                    item = Connect.GetContext().Answers.Where(a => a.ID_Questuion == _ItemQuestionList.ID_Questuion).ToList()[1];
                }
                if (ThirdAnswer.IsChecked == true)
                {
                    item = Connect.GetContext().Answers.Where(a => a.ID_Questuion == _ItemQuestionList.ID_Questuion).ToList()[2];
                }
                if (FouthAnswer.IsChecked == true)
                {
                    item = Connect.GetContext().Answers.Where(a => a.ID_Questuion == _ItemQuestionList.ID_Questuion).ToList()[3];
                }
                if(item != null)
                if (item.Accuracy == "1")
                {
                    Point += 100 / _countQuestions;

                }
            }
            LBPoint.Text = Point.ToString();
            TBQuestion.Text = _Questuion.Text_Question;
            FirstAnswer.Content = ListAnswers[0].Answer;
            SecondAnswer.Content = ListAnswers[1].Answer;
            ThirdAnswer.Content = ListAnswers[2].Answer;
            FouthAnswer.Content = ListAnswers[3].Answer;
            AnswerMin = _test.Time_On_Test / _countQuestions;
            AnswerSecond = 59;
            _numberQuestions++;
            FirstAnswer.IsChecked = false;
            SecondAnswer.IsChecked = false;
            ThirdAnswer.IsChecked = false;
            FouthAnswer.IsChecked = false;
        }

        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            if(_numberQuestions< _countQuestions)
            {
                Fill();
            }
            else
            {
                EndTest_Click(null, null);
            }
        }

        private void EndTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _users_tests.ID_User = _user.ID_User;
                _users_tests.ID_Test = _test.ID_Test;
                _users_tests.Time_Spent = _test.Time_On_Test - TestMin;
                _users_tests.Data_Passing_Test = DateTime.Now;
                _users_tests.Number_Points = Point;
                if (Point >= 70)
                {
                    _users_tests.ID_Result_Test = 1;
                }
                else
                {
                    _users_tests.ID_Result_Test = 2;
                }
                Connect.GetContext().Users_Tests.Add(_users_tests);
                Connect.GetContext().SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Close();
                PanelControl _panelcontrol = new PanelControl(_user);
                _panelcontrol.Show();
            }
        }
    }
}
