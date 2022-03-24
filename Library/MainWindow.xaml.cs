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
using Library.ClassHelper;

namespace Library
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

        private void BTNLogin_Click(object sender, RoutedEventArgs e)
        {
            var UserAuth = AppData.Context.Client.ToList().
                Where(i => i.Phone == TXTLogin.Text && i.Password == TXTPassword.Text).FirstOrDefault();

            if (UserAuth != null)
            {
                Windows.Menu menu = new Windows.Menu();
                menu.Show();
                this.Close();
            }
            else
                MessageBox.Show("Пользователь не найден!");
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RememberMe_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
