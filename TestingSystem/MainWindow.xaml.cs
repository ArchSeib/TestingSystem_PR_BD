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

namespace TestingSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if(TbLogin.Text == "")
            {
                MessageBox.Show("Вы не ввели логин");
            }
            else
            {
                if (TbPassword.Text == "")
                {
                    MessageBox.Show("Вы не ввели пароль");
                }
                else
                {
                    if (Connect.GetContext().Users.Any(a => a.Login == TbLogin.Text) == true)
                    {
                        var User = Connect.GetContext().Users.Where(a => a.Login == TbLogin.Text).First();
                        if(User.Password==TbPassword.Text)
                        {
                            if(User.ID_Role==1)
                            {
                                PanelControl _panelControl = new PanelControl(User);
                                _panelControl.Show();
                                this.Visibility = Visibility.Collapsed;
                            }
                            if (User.ID_Role == 2)
                            {
                                AdminMenu _adminMenu = new AdminMenu(User);
                                _adminMenu.Show();
                                this.Visibility = Visibility.Collapsed;
                            }
                            MessageBox.Show("Вы авторизированы");
                        }
                        else
                        {
                            MessageBox.Show("Вы ввели неправильный пароль");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Такого пользователя не существует");
                    }
                }
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            TbLogin.Clear();
            TbPassword.Clear();
        }

        private void Registr_Click(object sender, RoutedEventArgs e)
        {
            Registration _registration = new Registration();
            _registration.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); ;
        }
    }
}
