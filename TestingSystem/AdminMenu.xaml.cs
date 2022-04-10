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
    /// Логика взаимодействия для AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Window
    {
        private Users _user;
        public AdminMenu(Users user)
        {
            InitializeComponent();
            _user = user;
        }

        private void UserDataEdit_Click(object sender, RoutedEventArgs e)
        {
            EditDate _editDate = new EditDate(_user);
            _editDate.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void BdEdit_Click(object sender, RoutedEventArgs e)
        {
            EditTestQuestion _EditTestQuestion = new EditTestQuestion(_user);
            _EditTestQuestion.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void Exitt_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
