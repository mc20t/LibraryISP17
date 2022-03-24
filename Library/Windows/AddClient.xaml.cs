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
    public partial class AddClient : Window
    {
        DB.Client editReader = new DB.Client();
        bool isEdit = true; // изменяем или добавляем пользователя
        string pathPhoto = null; // Для сохранения пути к изображению

        public AddClient()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите");
            if (resultClick == MessageBoxResult.Yes)
            {
                DB.Client newClient = new DB.Client();
                newClient.LastName = txtLastName.Text;
                newClient.FirstName = txtFirstName.Text;
                newClient.Phone = txtPhone.Text;
                newClient.Password = txtPassword.Text;

                AppData.Context.Client.Add(newClient);
            }
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
