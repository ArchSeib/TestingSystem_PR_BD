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
    /// Логика взаимодействия для EditAnswer.xaml
    /// </summary>
    public partial class EditAnswer : Window
    {
        private Questions _questions;
        private List<Answers> answersList;
        private Answers _answers;
        private Users _users;

        public EditAnswer(Questions questions, Users users)
        {
            InitializeComponent();
            _users = users;
            _questions = questions;
            answersList = Connect.GetContext().Answers.Where(a => a.ID_Questuion == _questions.ID_Questuion).ToList();

            if (answersList.Count != 0)
            {
                TBAnswer1.Text = answersList[0].Answer;
                TBAnswer2.Text = answersList[1].Answer;
                TBAnswer3.Text = answersList[2].Answer;
                TBAnswer4.Text = answersList[3].Answer;

                if(answersList[0].Accuracy=="1")
                {
                    RBAnswer1.IsChecked = true;
                }
                if (answersList[1].Accuracy == "1")
                {
                    RBAnswer2.IsChecked = true;
                }
                if (answersList[2].Accuracy == "1")
                {
                    RBAnswer3.IsChecked = true;
                }
                if (answersList[3].Accuracy == "1")
                {
                    RBAnswer4.IsChecked = true;
                }


            }
            else
            {
                BtnDeleteAnswer1.IsEnabled = false;
                BtnDeleteAnswer2.IsEnabled = false;
                BtnDeleteAnswer3.IsEnabled = false;
                BtnDeleteAnswer4.IsEnabled = false;

                TBAnswer1.Text = "";
                TBAnswer2.Text = "";
                TBAnswer3.Text = "";
                TBAnswer4.Text = "";
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            EditTestQuestion _editTestQuestion = new EditTestQuestion(_users);
            _editTestQuestion.Visibility = Visibility.Visible;
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (TBAnswer1.Text == "")
            {
                error.AppendLine("Вы не ввели первый вопрос");
            }
            if (TBAnswer2.Text == "")
            {
                error.AppendLine("Вы не ввели второй вопрос");
            }
            if (TBAnswer3.Text == "")
            {
                error.AppendLine("Вы не ввели третий вопрос");
            }
            if (TBAnswer4.Text == "")
            {
                error.AppendLine("Вы не ввели четвертый вопрос");
            }
            if (RBAnswer1.IsChecked==false && RBAnswer2.IsChecked == false && RBAnswer3.IsChecked == false && RBAnswer4.IsChecked == false)
            {
                error.AppendLine("Вы не выбрали правильный ответ");
            }
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
            }
            else
            {
                try
                {
                    answersList = Connect.GetContext().Answers.Where(a => a.ID_Questuion == _questions.ID_Questuion).ToList();
                    if (answersList.Count < 4 || answersList.Count == 0)
                    {
                        answersList = null;
                        foreach (var item in Connect.GetContext().Answers)
                        {
                            if (item.ID_Questuion == _questions.ID_Questuion)
                            {
                                Connect.GetContext().Answers.Remove(item);
                            }
                        }

                        _answers = new Answers();
                        _answers.Answer = TBAnswer1.Text;
                        _answers.ID_Questuion = _questions.ID_Questuion;
                        if(RBAnswer1.IsChecked==false)
                        {
                            _answers.Accuracy = "0";
                        }
                        else
                        {
                            _answers.Accuracy = "1";
                        }
                        Connect.GetContext().Answers.Add(_answers);

                        _answers = new Answers();
                        _answers.Answer = TBAnswer2.Text;
                        _answers.ID_Questuion = _questions.ID_Questuion;
                        if (RBAnswer2.IsChecked == false)
                        {
                            _answers.Accuracy = "0";
                        }
                        else
                        {
                            _answers.Accuracy = "1";
                        }
                        Connect.GetContext().Answers.Add(_answers);

                        _answers = new Answers();
                        _answers.Answer = TBAnswer3.Text;
                        _answers.ID_Questuion = _questions.ID_Questuion;
                        if (RBAnswer3.IsChecked == false)
                        {
                            _answers.Accuracy = "0";
                        }
                        else
                        {
                            _answers.Accuracy = "1";
                        }
                        Connect.GetContext().Answers.Add(_answers);

                        _answers = new Answers();
                        _answers.Answer = TBAnswer4.Text;
                        _answers.ID_Questuion = _questions.ID_Questuion;
                        if (RBAnswer4.IsChecked == false)
                        {
                            _answers.Accuracy = "0";
                        }
                        else
                        {
                            _answers.Accuracy = "1";
                        }
                        Connect.GetContext().Answers.Add(_answers);

                        Connect.GetContext().SaveChanges();

                        MessageBox.Show("Ответы добавлены");
                    }
                    else
                    {
                        answersList[0].Answer = TBAnswer1.Text;
                        answersList[1].Answer = TBAnswer2.Text;
                        answersList[2].Answer = TBAnswer3.Text;
                        answersList[3].Answer = TBAnswer4.Text;
                        Connect.GetContext().SaveChanges();
                        MessageBox.Show("Ответы сохранены");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                EditTestQuestion _editTestQuestion = new EditTestQuestion(_users);
                _editTestQuestion.Visibility = Visibility.Visible;
                this.Close();
            }
        }

        private void DeleteAnswer4_Click(object sender, RoutedEventArgs e)
        {
            DeleteItem(3);
            TBAnswer4.Text = "";
        }

        private void DeleteAnswer3_Click(object sender, RoutedEventArgs e)
        {
            DeleteItem(2);
            TBAnswer3.Text = "";
        }

        private void DeleteAnswer2_Click(object sender, RoutedEventArgs e)
        {
            DeleteItem(1);
            TBAnswer2.Text = "";
        }

        private void DeleteAnswer1_Click(object sender, RoutedEventArgs e)
        {
            DeleteItem(0);
            TBAnswer1.Text = "";
        }
        private void DeleteItem(int index)
        {
            _answers = answersList[index];
            try
            {
                Connect.GetContext().Answers.Remove(_answers);
                Connect.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            answersList = Connect.GetContext().Answers.Where(a => a.ID_Questuion == _questions.ID_Questuion).ToList();
        }
    }
}
