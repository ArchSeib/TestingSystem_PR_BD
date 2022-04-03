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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class PanelControl : Window
    {
        private Users _user;
        public PanelControl(Users user)
        {
            InitializeComponent();
            _user = user;
            DataContext = _user;
            DGTestsInfo.ItemsSource = Connect.GetContext().Users_Tests.Where(a=>a.ID_User==_user.ID_User).ToList();
            LBFIO.Text = _user.FIO;
            LBlLogin.Text = _user.Login;
            CBTests.ItemsSource = Connect.GetContext().Tests.ToList();
        }
        private void DataEdit_Click(object sender, RoutedEventArgs e)
        {
            EditDate _editDate = new EditDate(_user);
            _editDate.Show();
            this.Visibility = Visibility.Hidden;
        }

        private void ChouseTest_Click(object sender, RoutedEventArgs e)
        {
            if(CBTests.Text == "")
            {
                MessageBox.Show("Выбирите тест");
            }
            else
            {
            Tests _tests = CBTests.Items.CurrentItem as Tests;
            Testing _testing = new Testing(_tests,_user);
            _testing.Show();
            this.Close();
            }
        }

        private void UpdateHistoryTest_Click(object sender, RoutedEventArgs e)
        {
            DGTestsInfo.ItemsSource = Connect.GetContext().Users_Tests.Where(a => a.ID_User == _user.ID_User).ToList();
        }

        private void Sertificat_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow _mainWindow = new MainWindow();
            _mainWindow.Visibility = Visibility.Visible;
        }
    }
}
