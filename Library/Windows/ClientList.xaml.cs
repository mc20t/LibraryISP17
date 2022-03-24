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
    /// Логика взаимодействия для ClientList.xaml
    /// </summary>
    public partial class ClientList : Window
    {
        List<Client> readerList = new List<Client>();
        List<string> listSort = new List<string>() { "По умолчанию", "По фамилии", "По имени", "По телефону", "По паролю" };

        public ClientList()
        {
            InitializeComponent();

            cmbSort.ItemsSource = listSort;
            cmbSort.SelectedIndex = 0;


            Filter();
        }

        private void Filter()
        {
            readerList = AppData.Context.Client.ToList();
            readerList = readerList.
                Where(i => 
                    i.LastName.ToLower().Contains(txtSearch.Text.ToLower()) ||
                    i.FirstName.ToLower().Contains(txtSearch.Text.ToLower()) || 
                    i.Phone.ToLower().Contains(txtSearch.Text.ToLower()) ||
                    i.Password.ToLower().Contains(txtSearch.Text.ToLower())
                ).ToList();

            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    readerList = readerList.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    readerList = readerList.OrderBy(i => i.LastName).ToList();
                    break;
                case 2:
                    readerList = readerList.OrderBy(i => i.FirstName).ToList();
                    break;
                case 3:
                    readerList = readerList.OrderBy(i => i.Phone).ToList();
                    break;
                case 4:
                    readerList = readerList.OrderBy(i => i.Password).ToList();
                    break;
                default:
                    readerList = readerList.OrderBy(i => i.ID).ToList();
                    break;
            }

            lvReader.ItemsSource = readerList;
        }

        private void btdAddClient_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient();
            this.Opacity = 0.2;
            addClient.ShowDialog();
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

        private void lvReader_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var editReader = new  DB.Client();
            if (lvReader.SelectedItem is DB.Client)
            {
                editReader = lvReader.SelectedItem as DB.Client;
            }

            UpdateClient updateClient = new UpdateClient();
            this.Opacity = 0.2;
            updateClient.ShowDialog();
            lvReader.ItemsSource = AppData.Context.Client.ToList();
            this.Opacity = 1;
        }

        private void lvReader_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}