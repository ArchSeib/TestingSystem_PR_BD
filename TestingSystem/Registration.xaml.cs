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
using System.Text.RegularExpressions;

namespace TestingSystem
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private Users _user = new Users();
        public Registration()
        {
            InitializeComponent();
            DataContext = _user;
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Regex PasswordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{6,30}$");
            Regex LoginRegex = new Regex(@"^(([A-Z]+)||([a-z]+)(([0-9]*[a-z]*[A-Z]*)*[_,-]*)+){6,30}$");
            StringBuilder error = new StringBuilder();
            if(TbFIO.Text == "")
            {
                error.AppendLine("Вы не ввели ФИО");
            }
            if (TbLogin.Text == "")
            {
                error.AppendLine("Вы не ввели логин");
            }
            if (TbPassword.Text == "")
            {
                error.AppendLine("Вы не ввели пароль");
            }
            if (LoginRegex.IsMatch(TbLogin.Text) == false)
            {
                error.AppendLine("Логин должен состоять из английских букв.\nОн может содержать большие и маленькие буквы,\nцифры, знаки тире и нижнее подчеркивание");
            }
            if (PasswordRegex.IsMatch(TbPassword.Text) == false)
            {
                error.AppendLine("Пароль должен содержать большую букву, хотя бы одну цифру,\nодин знак, иметь длину больше 6 символов");
            }
            if (TbAgainPassword.Text == "")
            {
                error.AppendLine("Повторите пароль");
            }
            else
            {
                if (TbPassword.Text != TbAgainPassword.Text)
                {
                    error.AppendLine("Пароли не совпадают");
                }
            }
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            else
            {
                try
                {
                    _user.ID_Role = 1;
                    Connect.GetContext().Users.Add(_user);
                    Connect.GetContext().SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {

                    PanelControl _panelControl = new PanelControl(_user);
                    _panelControl.Show();
                    MessageBox.Show("Вы зарегестрированы и авторизированы");
                    this.Close();
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            TbFIO.Clear();
            TbLogin.Clear();
            TbPassword.Clear();
            TbAgainPassword.Clear();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow _mainWindow = new MainWindow();
            _mainWindow.Visibility = Visibility.Visible;
        }
    }
}
