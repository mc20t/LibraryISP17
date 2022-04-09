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
    public partial class AddBook : Window
    {
        DB.Book editBook = new DB.Book();
        bool isEdit = true; // изменяем или добавляем

        public AddBook()
        {
            InitializeComponent();
            isEdit = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Валидация

            // проверка на пустоту
            if (string.IsNullOrWhiteSpace(txtBookName.Text))
            {
                MessageBox.Show("Поле Название не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show("Поле Колличество не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbAuthor.Text))
            {
                MessageBox.Show("Поле Автор не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbGenre.Text))
            {
                MessageBox.Show("Поле Жанр не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // проверка на количество символов

            if (txtBookName.Text.Length > 100)
            {
                MessageBox.Show("Недопустимое количесво символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtQuantity.Text.Length > 3)
            {
                MessageBox.Show("Недопустимое количесво символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (isEdit)
            {
                try
                {
                    editBook.Name = txtBookName.Text;

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
                    var resultClick = MessageBox.Show("Вы уверены?", "Подтвердите добавление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resultClick == MessageBoxResult.Yes)
                    {
                        // Добавление нового читателя
                        DB.Client newClient = new DB.Client();
                        //newClient.LastName = txtBookName.Text;
                        //newClient.FirstName = txtFirstName.Text;
                        //newClient.Phone = txtPhone.Text;
                        //newClient.Password = txtPassword.Text;

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
    }
}
