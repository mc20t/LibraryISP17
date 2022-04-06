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
using Library.Windows;
using Library.DB;
using static Library.ClassHelper.AppData;

namespace Library.Windows
{
    /// <summary>
    /// Логика взаимодействия для BookList.xaml
    /// </summary>
    public partial class BookList : Window
    {
        List<Book> bookList = new List<Book>();
        List<string> listSort = new List<string>() { "По умолчанию", "По названию", "По колличеству книг", "По фамилии автора", "По имени автора", "По жанру" };
        public BookList()
        {
            InitializeComponent();

            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;

            Filter();
        }

        private void Filter()
        {
            bookList = AppData.Context.Book.ToList();
            bookList = bookList.
                Where(i =>
                    i.Name.ToLower().Contains(txtSearch.Text.ToLower()) ||
                    i.Quantity.ToString().ToLower().Contains(txtSearch.Text.ToLower()) ||
                    i.Author.LastName.ToLower().Contains(txtSearch.Text.ToLower()) ||
                    i.Author.FirstName.ToLower().Contains(txtSearch.Text.ToLower()) ||
                    i.Genre.Name.ToLower().Contains(txtSearch.Text.ToLower())
                ).ToList();

            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    bookList = bookList.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    bookList = bookList.OrderBy(i => i.Name).ToList();
                    break;
                case 2:
                    bookList = bookList.OrderBy(i => i.Quantity).ToList();
                    break;
                case 3:
                    bookList = bookList.OrderBy(i => i.Author.LastName).ToList();
                    break;
                case 4:
                    bookList = bookList.OrderBy(i => i.Author.FirstName).ToList();
                    break;
                case 5:
                    bookList = bookList.OrderBy(i => i.Genre.Name).ToList();
                    break;
                default:
                    bookList = bookList.OrderBy(i => i.ID).ToList();
                    break;
            }

            lvReader.ItemsSource = bookList;
        }

        private void btdAddClient_Click(object sender, RoutedEventArgs e)
        {
            AddBook addBook = new AddBook();
            this.Opacity = 0.2;
            addBook.ShowDialog();
            lvReader.ItemsSource = AppData.Context.Client.ToList();
            this.Opacity = 1;
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lvReader_MouseDoubleBook(object sender, MouseButtonEventArgs e)
        {
            var editReader = new Book();
            if (lvReader.SelectedItem is Book)
            {
                editReader = lvReader.SelectedItem as Book;
            }

            UpdateBook updateBook = new UpdateBook();
            this.Opacity = 0.2;
            updateBook.ShowDialog();
            lvReader.ItemsSource = AppData.Context.Book.ToList();
            this.Opacity = 1;
        }

        private void lvReader_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                if (lvReader.SelectedItem is Client && lvReader.SelectedIndex != -1)
                {
                    try
                    {
                        var item = lvReader.SelectedItem as Client;
                        var resultClick = MessageBox.Show("Вы уверены?", "Подтверите Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultClick == MessageBoxResult.Yes)
                        {
                            AppData.Context.Client.Remove(item);
                            AppData.Context.SaveChanges();
                            MessageBox.Show("Пользователь успешно удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            Filter();
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
}