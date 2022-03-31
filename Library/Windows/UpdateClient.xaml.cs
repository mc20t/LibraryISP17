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
using System.IO;
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

            // вставка изображения
            if (client.Photo != null)
            {
                using (MemoryStream stream = new MemoryStream(client.Photo))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    imgUser.Source = bitmapImage;
                }
            }

            editClient = client;

            txtLastName.Text = editClient.LastName;
            txtFirstName.Text = editClient.FirstName;
            txtPhone.Text = editClient.Phone;
            txtPassword.Text = editClient.Password;

            isEdit = true;

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
                    editClient.LastName = txtLastName.Text;
                    editClient.FirstName = txtFirstName.Text;
                    editClient.Phone = txtPhone.Text;
                    editClient.Password = txtPassword.Text;

                    if (pathPhoto != null)
                    {
                        editClient.Photo = File.ReadAllBytes(pathPhoto);
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
                        DB.Client newReader = new DB.Client();
                        newReader.LastName = txtLastName.Text;
                        newReader.FirstName = txtFirstName.Text;
                        newReader.Phone = txtPhone.Text;
                        newReader.Password = txtPassword.Text;

                        if (pathPhoto != null)
                        {
                            newReader.Photo = File.ReadAllBytes(pathPhoto);
                        }

                        AppData.Context.Client.Add(newReader);

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