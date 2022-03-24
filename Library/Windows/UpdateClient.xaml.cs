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
using Library.ClassHelper;
using Microsoft.Win32;

namespace Library.Windows
{
    /// <summary>
    /// Логика взаимодействия для UpdateClient.xaml
    /// </summary>
    public partial class UpdateClient : Window
    {
        DB.Client editClient = new DB.Client();
        bool isEdit = true; // изменяем или добавляем пользователя
        string pathPhoto = null; // Для сохранения пути к изображению

        public UpdateClient()
        {
            InitializeComponent();
        }

        public UpdateClient(DB.Client client)
        {
            InitializeComponent();

            editClient = client;

            txtLastName.Text = editClient.LastName;
            txtFirstName.Text = editClient.FirstName;
            txtPhone.Text = editClient.Phone;
            txtPassword.Text = editClient.Password;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            editClient.IsDeleted = true;
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                imgUser.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                pathPhoto = openFileDialog.FileName;
            }
        }
    }
}