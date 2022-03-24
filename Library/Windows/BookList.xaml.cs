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
        List<string> listSort = new List<string>() { "По умолчанию", "По названию", "По колличеству книг", "По номеру полки", "По жанру", "По автору" };
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
                    i.Name.ToLower().Contains(txtSearch.Text.ToLower()) /*||
                    i.Quantity.ToLower().Contains(txtSearch.Text.ToLower()) ||
                    i.NumBookshelf.ToLower().Contains(txtSearch.Text.ToLower())*/
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

        private void btdDelClient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btdUpdateClient_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}