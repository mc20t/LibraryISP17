using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
            isEdit = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Валидация

            // проверка на пустоту
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Поле Фамилия не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Поле Имя не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Поле Телефон не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // проверка на количество символов

            if (txtLastName.Text.Length > 100)
            {
                MessageBox.Show("Недопустимое количесво символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtFirstName.Text.Length > 100)
            {
                MessageBox.Show("Недопустимое количесво символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtPhone.Text.Length > 12)
            {
                MessageBox.Show("Недопустимое количесво символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (isEdit)
            {
                try
                {
                    editReader.LastName = txtLastName.Text;
                    editReader.FirstName = txtFirstName.Text;
                    editReader.Phone = txtPhone.Text;

                    if (pathPhoto != null)
                    {
                        editReader.Photo = File.ReadAllBytes(pathPhoto);
                    }

                    AppData.Context.SaveChanges();
                    MessageBox.Show("Успех", "Данные читателя успешно изменены", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

            }
            else
            {
                try
                {
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтверите добавление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        // Добавление нового читателя
                        DB.Client newClient = new DB.Client();
                        newClient.LastName = txtLastName.Text;
                        newClient.FirstName = txtFirstName.Text;
                        newClient.Phone = txtPhone.Text;
                        newClient.Password = txtPassword.Text;

                        if (pathPhoto != null)
                        {
                            newClient.Photo = File.ReadAllBytes(pathPhoto);
                        }

                        AppData.Context.Client.Add(newClient);

                        AppData.Context.SaveChanges();
                        MessageBox.Show("Успех", "Пользователь успешно добавлен", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

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
