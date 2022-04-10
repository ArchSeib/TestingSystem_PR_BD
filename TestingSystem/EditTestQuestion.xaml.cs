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
        private Tests _tests;

        private List<Questions> questionList;
        private Questions _questions;

        private int NumberPageTest = 1;

        private int NumberPageQuestion = 1;

        private Users _users;

        public EditTestQuestion(Users users)
        {
            InitializeComponent();
            _users = users;
            testsList = Connect.GetContext().Tests.ToList();
            _tests = testsList[0];
            TBTest.Text = _tests.Name_Test;
            TbTimeOnTest.Text = _tests.Time_On_Test.ToString();
            TbNumberTest.Text = "1";
            NumberPageTest = 1;

            questionList = Connect.GetContext().Questions.Where(a => a.ID_Test == _tests.ID_Test).ToList();
            if (questionList.Count != 0)
            {
                _questions = questionList[0];
                TBQuestion.Text = _questions.Text_Question;
            }
            else
            {
                TBQuestion.Text = "";
                BtnAddAnswerQuestion.IsEnabled = false;
                BtnDeleteQuestion.IsEnabled = false;
                BtnNextQuestion.IsEnabled = false;
                BtnSaveQuestion.IsEnabled = false;
                BtnPreviousQuestion.IsEnabled = false;
                MessageBox.Show("В текущем тесте нет вопросов.\nПока не будет добавлен хотя бы один вопрос\nвам разрешено изменять дынные лишь текущего теста");

            }
            TbNumberQuestion.Text = "1";
            NumberPageQuestion = 1;
        }

        private void SaveTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _tests.Name_Test = TBTest.Text;
                _tests.Time_On_Test = Convert.ToInt32(TbTimeOnTest.Text);
                Connect.GetContext().SaveChanges();
                MessageBox.Show("Изменения сохранены");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void AddTest_Click(object sender, RoutedEventArgs e)
        {
            if(TBTest.Text!="" && Convert.ToInt32(TbTimeOnTest.Text)>=60)
            {
                if (Connect.GetContext().Tests.Any(a => a.Name_Test == TBTest.Text) == true)
                {
                    MessageBox.Show("Такой тест уже существует");
                }
                else
                {
                    try
                    {
                        _tests.Name_Test = TBTest.Text;
                        _tests.Time_On_Test = Convert.ToInt32(TbTimeOnTest.Text);
                        Connect.GetContext().Tests.Add(_tests);
                        questionList = null;
                        Connect.GetContext().SaveChanges();
                        MessageBox.Show("Тест Добавлен!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                    testsList = Connect.GetContext().Tests.ToList();
                    _tests = testsList[testsList.Count - 1];
                    NumberPageTest = testsList.Count;
                    questionList = Connect.GetContext().Questions.Where(a => a.ID_Test == _tests.ID_Test).ToList();
                    TBQuestion.Text = "";

                    NumberPageQuestion = 1;
                    TbNumberQuestion.Text = "1";


                    MessageBox.Show("Пока не будет добавлен хотя бы один вопрос\nвам разрешено изменять дынные лишь текущего теста");

                }
            }
            else
            {
                MessageBox.Show("Убедитесь ввели название теста,\nа также время на его прохождение");
            }
            BtnAddTest.IsEnabled = false;
            BtnNextTest.IsEnabled = false;
            BtnPreviousTest.IsEnabled = false;

            BtnAddAnswerQuestion.IsEnabled = false;
            BtnDeleteQuestion.IsEnabled = false;
            BtnNextQuestion.IsEnabled = false;
            BtnSaveQuestion.IsEnabled = false;
            BtnPreviousQuestion.IsEnabled = false;
        }

        private void NextTest_Click(object sender, RoutedEventArgs e)
        {
            if(NumberPageTest==testsList.Count)
            {
                NumberPageTest = 1;
                UpdateTestBox(NumberPageTest);
            }
            else
            {
                NumberPageTest ++;
                UpdateTestBox(NumberPageTest);
            }
            TbNumberTest.Text = NumberPageTest.ToString();
        }

        private void PreviousTest_Click(object sender, RoutedEventArgs e)
        {
            if (NumberPageTest == 1)
            {
                NumberPageTest = testsList.Count;
                UpdateTestBox(NumberPageTest);
            }
            else
            {
                NumberPageTest--;
                UpdateTestBox(NumberPageTest);
            }
            TbNumberTest.Text = NumberPageTest.ToString();
        }

        private void UpdateTestBox (int numberpage)
        {
            _tests = testsList[numberpage - 1];
            TBTest.Text = _tests.Name_Test;
            TbTimeOnTest.Text = _tests.Time_On_Test.ToString();
            questionList = Connect.GetContext().Questions.Where(a => a.ID_Test == _tests.ID_Test).ToList();
            if (questionList.Count != 0)
            {
                _questions = questionList[0];
                TBQuestion.Text = _questions.Text_Question;
                AnswerForQuestion(_questions);
            }
            else
            {
                TBQuestion.Text = "";
                MessageBox.Show("Пока не будет добавлен хотя бы один вопрос\nвам разрешено изменять дынные лишь текущего теста");
                BtnAddTest.IsEnabled = false;
                BtnNextTest.IsEnabled = false;
                BtnPreviousTest.IsEnabled = false;

                BtnAddAnswerQuestion.IsEnabled = false;
                BtnDeleteQuestion.IsEnabled = false;
                BtnNextQuestion.IsEnabled = false;
                BtnSaveQuestion.IsEnabled = false;
                BtnPreviousQuestion.IsEnabled = false;
                NumberPageQuestion = 1;
                TbNumberQuestion.Text = "1";
            }
        }

        private void DeleteTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                questionList = null;
                foreach (var item in Connect.GetContext().Users_Tests)
                {
                    Connect.GetContext().Users_Tests.Remove(item);
                }
                var deleteTest = Connect.GetContext().Tests.Where(x => x.ID_Test == _tests.ID_Test).FirstOrDefault();
                var deleteQuestions = Connect.GetContext().Questions.Where(x => x.ID_Test == deleteTest.ID_Test);
                foreach (var itemQuestion in deleteQuestions)
                {
                    var deleteAnswer = Connect.GetContext().Answers.Where(x => x.ID_Questuion == itemQuestion.ID_Questuion);
                    Connect.GetContext().Answers.RemoveRange(deleteAnswer);
                }
                Connect.GetContext().Questions.RemoveRange(deleteQuestions);
                Connect.GetContext().Tests.Remove(deleteTest);
                Connect.GetContext().SaveChanges();

                testsList = Connect.GetContext().Tests.ToList();

                _tests = testsList[0];
                TBTest.Text = _tests.Name_Test;
                TbTimeOnTest.Text = _tests.Time_On_Test.ToString();
                TbNumberTest.Text = "1";
                NumberPageTest = 1;

                questionList = Connect.GetContext().Questions.Where(a => a.ID_Test == _tests.ID_Test).ToList();
                if (questionList.Count != 0)
                {
                    _questions = questionList[0];
                    TBQuestion.Text = _questions.Text_Question;
                    AnswerForQuestion(_questions);
                }
                else
                {
                    TBQuestion.Text = "";
                    BtnAddTest.IsEnabled = false;
                    BtnNextTest.IsEnabled = false;
                    BtnPreviousTest.IsEnabled = false;

                    BtnAddAnswerQuestion.IsEnabled = false;
                    BtnDeleteQuestion.IsEnabled = false;
                    BtnNextQuestion.IsEnabled = false;
                    BtnSaveQuestion.IsEnabled = false;
                    BtnPreviousQuestion.IsEnabled = false;
                    MessageBox.Show("В текущем тесте нет вопросов.\nПока не будет добавлен хотя бы один вопрос\nвам разрешено изменять дынные лишь текущего теста");

                }
                TbNumberQuestion.Text = "1";
                NumberPageQuestion = 1;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveQuestion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _questions.Text_Question = TBQuestion.Text;
                Connect.GetContext().SaveChanges();
                MessageBox.Show("Изменения сохранены");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void OpenAnswer_Click(object sender, RoutedEventArgs e)
        {
            if(questionList.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы один вопрос");
            }
            else
            {
                EditAnswer _editAnswer = new EditAnswer(_questions, _users);
                this.Visibility = Visibility.Hidden;
                _editAnswer.Show();
            }
        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var deleteAnswer = Connect.GetContext().Answers.Where(x => x.ID_Questuion == _questions.ID_Questuion);
                Connect.GetContext().Answers.RemoveRange(deleteAnswer);

                Connect.GetContext().Questions.Remove(_questions);
                Connect.GetContext().SaveChanges();


                questionList = Connect.GetContext().Questions.Where(a => a.ID_Test == _tests.ID_Test).ToList();
                if (questionList.Count == 0)
                {
                    TBQuestion.Text = "";
                    MessageBox.Show("Вы удалили последний вопрос в этом тесте\nПока не будет добавлен хотя бы один вопрос\nвам разрешено изменять дынные лишь текущего теста");
                    BtnAddTest.IsEnabled = false;
                    BtnNextTest.IsEnabled = false;
                    BtnPreviousTest.IsEnabled = false;

                    BtnAddAnswerQuestion.IsEnabled = false;
                    BtnDeleteQuestion.IsEnabled = false;
                    BtnNextQuestion.IsEnabled = false;
                    BtnSaveQuestion.IsEnabled = false;
                    BtnPreviousQuestion.IsEnabled = false;

                }
                else
                {
                    _questions = questionList[0];
                    TBQuestion.Text = _questions.Text_Question;
                    TbNumberQuestion.Text = "1";
                    NumberPageQuestion = 1;
                    AnswerForQuestion(_questions);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PreviousQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (NumberPageQuestion == 1)
            {
                NumberPageQuestion = questionList.Count;
                UpdateQuestionBox(NumberPageQuestion);
            }
            else
            {
                NumberPageQuestion--;
                UpdateQuestionBox(NumberPageQuestion);
            }
            TbNumberQuestion.Text = NumberPageQuestion.ToString();
        }

        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (NumberPageQuestion == questionList.Count)
            {
                NumberPageQuestion = 1;
                UpdateQuestionBox(NumberPageQuestion);
            }
            else
            {
                NumberPageQuestion++;
                UpdateQuestionBox(NumberPageQuestion);
            }
            TbNumberQuestion.Text = NumberPageQuestion.ToString();
        }

        private void UpdateQuestionBox(int numberpage)
        {
            _questions = questionList[numberpage - 1];
            TBQuestion.Text = _questions.Text_Question;
            AnswerForQuestion(_questions);
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            _questions = new Questions();
            if (TBQuestion.Text != "")
            {
                try
                {
                    _questions.Text_Question = TBQuestion.Text;
                    _questions.ID_Test = _tests.ID_Test;
                    Connect.GetContext().Questions.Add(_questions);
                    Connect.GetContext().SaveChanges();
                    MessageBox.Show("Вопрос добавлен!\nДобавьте ответы на вопрос");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                questionList = Connect.GetContext().Questions.Where(a => a.ID_Test == _tests.ID_Test).ToList();
                _questions = questionList[questionList.Count - 1];
                TBQuestion.Text = _questions.Text_Question;
                NumberPageQuestion = questionList.Count;
                TbNumberQuestion.Text = questionList.Count.ToString();
                NumberPageQuestion = questionList.Count;

                EditAnswer _editAnswer = new EditAnswer(_questions, _users);
                NumberPageQuestion = 1;
                TbNumberQuestion.Text = NumberPageQuestion.ToString();

                BtnAddAnswerQuestion.IsEnabled = true;
                BtnDeleteQuestion.IsEnabled = true;
                BtnNextQuestion.IsEnabled = true;
                BtnSaveQuestion.IsEnabled = true;
                BtnPreviousQuestion.IsEnabled = true;

                BtnAddTest.IsEnabled = true;
                BtnNextTest.IsEnabled = true;
                BtnPreviousTest.IsEnabled = true;

                this.Visibility = Visibility.Hidden;
                _editAnswer.Show();
            }
            else
            {
                MessageBox.Show("Введите текст вопроса");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu(_users);
            adminMenu.Show();
            this.Close();
        }
        private void AnswerForQuestion(Questions questions)
        {
            var AnswerList = Connect.GetContext().Answers.Where(a=>a.ID_Questuion == questions.ID_Questuion).ToList();
            if(AnswerList.Count==0)
            {
                MessageBox.Show("Добавьте ответ на вопросы");
                EditAnswer _editAnswer = new EditAnswer(questions, _users);
                this.Visibility = Visibility.Hidden;
                _editAnswer.Show();
            }
        }
    }
}
