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
    /// Логика взаимодействия для GetBook.xaml
    /// </summary>
    public partial class GetBook : Window
    {
        List<ClientBooks> ClientBooksList = new List<ClientBooks>();
        List<string> listSort = new List<string>() { "По фамилии", "По имени", "По книге", "По дате выдачи", "По дате возврата", "По сумме долга" };
        public GetBook()
        {
            InitializeComponent();

            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;

            Filter();
        }

        private void Filter()
        {
            ClientBooksList = AppData.Context.ClientBooks.ToList();
            ClientBooksList = ClientBooksList.
                Where(i =>
                    i.Client.LastName.ToLower().Contains(txtSearch.Text.ToLower()) ||
                    i.Client.FirstName.ToLower().Contains(txtSearch.Text.ToLower()) ||
                    i.Book.Name.ToLower().Contains(txtSearch.Text.ToLower()) ||
                    i.From.ToString().ToLower().Contains(txtSearch.Text.ToLower()) ||
                    i.To.ToString().ToLower().Contains(txtSearch.Text.ToLower()) ||
                    i.Debt.ToString().ToLower().Contains(txtSearch.Text.ToLower())
                ).ToList();

            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    ClientBooksList = ClientBooksList.OrderBy(i => i.Client.LastName).ToList();
                    break;
                case 1:
                    ClientBooksList = ClientBooksList.OrderBy(i => i.Client.FirstName).ToList();
                    break;
                case 2:
                    ClientBooksList = ClientBooksList.OrderBy(i => i.Book.Name).ToList();
                    break;
                case 3:
                    ClientBooksList = ClientBooksList.OrderBy(i => i.From).ToList();
                    break;
                case 4:
                    ClientBooksList = ClientBooksList.OrderBy(i => i.To).ToList();
                    break;
                case 5:
                    ClientBooksList = ClientBooksList.OrderBy(i => i.Debt).ToList();
                    break;
                default:
                    ClientBooksList = ClientBooksList.OrderBy(i => i.From).ToList();
                    break;
            }

            lvReader.ItemsSource = ClientBooksList;
        }

        private void btdAddClient_Click(object sender, RoutedEventArgs e)
        {
            AddGetBook addGetBook = new AddGetBook();
            this.Opacity = 0.2;
            addGetBook.ShowDialog();
            lvReader.ItemsSource = AppData.Context.Client.ToList();
            this.Opacity = 1;
            Filter();
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

        private void lvReader_MouseDoubleGetBook(object sender, MouseButtonEventArgs e)
        {
            var editReader = new ClientBooks();
            if (lvReader.SelectedItem is ClientBooks)
            {
                editReader = lvReader.SelectedItem as ClientBooks;
            }

            UpdateGetBook updateGetBook = new UpdateGetBook();
            this.Opacity = 0.2;
            updateGetBook.ShowDialog();
            lvReader.ItemsSource = AppData.Context.ClientBooks.ToList();
            this.Opacity = 1;
            Filter();
        }

        private void lvReader_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                if (lvReader.SelectedItem is ClientBooks && lvReader.SelectedIndex != -1)
                {
                    try
                    {
                        var item = lvReader.SelectedItem as ClientBooks;
                        var resultClick = MessageBox.Show("Вы уверены?", "Подтверите Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (resultClick == MessageBoxResult.Yes)
                        {
                            AppData.Context.ClientBooks.Remove(item);
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