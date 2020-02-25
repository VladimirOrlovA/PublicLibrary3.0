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
using PublicLibrary.lip;

namespace PublicLibrary.Pages
{
   
    

    /// <summary>
    /// Логика взаимодействия для PageWelcome.xaml
    /// </summary>
    public partial class PageWelcome : Page
    {
        DbContext dbContext = new DbContext(MainWindow.path);
        Book _book = null;
        public PageWelcome(Book book)
        {
            InitializeComponent();

            cbEdition.ItemsSource = new List<Edition>()
            {
                new Edition(1, "Казахстанская правда"),
                new Edition(2, "Егемен Казахстан"),
                new Edition(3, "Литер")
            };

            cbGenre.ItemsSource = new List<Genre>()
            {
                new Genre(1, "Деловая литература"),
                new Genre(2, "Детективы и Триллеры"),
                new Genre(3, "Литература для детей"),
            };

            if (book != null)
            {
                _book = book;

                AddBtn.Content = "Редактировать";
                AddBtn.Click += EditBook_Click;
                AddBtn.Click -= AddBtn_Click;

                #region old
                //TBXname.Text = book.Name;
                //TBXedition.Text = book.Edition;
                //DpDate.SelectedDate = book.IssueDate;
                //// book.Author = ((ComboBoxItem)CBXauthor.SelectedItem).Content.ToString();
                //// book.Genre = ((ComboBoxItem)CBXgenre.SelectedItem).Content.ToString();

                //rbAvaliable.IsChecked = book.IsAvailible;
                //chAfter18.IsChecked = book.IsEighteenPlus;
                //chOld.IsChecked = book.IsRaritet;
                #endregion 
            }
            else
            {
                _book = new Book();

                AddBtn.Content = "Добавить книгу";
                AddBtn.Click -= EditBook_Click;
                AddBtn.Click += AddBtn_Click;
            }

            //Привязка данных
            gridBook.DataContext = _book;
        }

        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            _book = gridBook.DataContext as Book;
            _book.Genre = (Genre)cbGenre.SelectedItem;
            _book.Edition = (Edition)cbEdition.SelectedItem;

            if (dbContext.EditBook(_book))
            {
                MessageBox.Show("Книга изменена успешно!");
                MainWindow._MainFrame.Navigate(new PageBooks());
            }
            else
                MessageBox.Show("Возникли ошибки при изменении книги!");
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            _book = gridBook.DataContext as Book;
            _book.Genre = (Genre)cbGenre.SelectedItem;
            _book.Edition = (Edition)cbEdition.SelectedItem;

            _book.AddedBy = MainWindow.user.Id;
            _book.AddedTime = DateTime.Now;

            if (dbContext.AddBook(_book))
            {
                MessageBox.Show("Книга добавлена успешно!");
                MainWindow._MainFrame.Navigate(new PageBooks());
            }
            else
                MessageBox.Show("Возникли ошибки при добавлении книги!");
        }
    }
}
