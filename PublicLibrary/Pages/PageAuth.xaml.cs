using PublicLibrary.lip;
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

namespace PublicLibrary.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAuth.xaml
    /// </summary>
    public partial class PageAuth : Page
    {
        private DbContext db = new DbContext(MainWindow.path);
        public PageAuth()
        {
            InitializeComponent();
            stUsers.DataContext = MainWindow.user;
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput(InputLogin, validateInputLogin))
            {
                MainWindow.user.Password = InputPassword.Password;
                 db.GetUser(MainWindow.user);

                if (MainWindow.user != null)
                {
                    MainWindow._MainFrame.Navigate(new PageMainAuth());

                    MenuItem miBook = new MenuItem() { Header = "Книги" };

                    MenuItem miListBook = new MenuItem() { Header = "Список" };
                    miListBook.Click += MiListBook_Click;

                    MenuItem miAddBook = new MenuItem() { Header = "Добавить книгу" };
                    miAddBook.Click += MiAddBook_Click;

                    MenuItem miDictionary = new MenuItem() { Header = "Справочник" };

                    MenuItem miPublisher = new MenuItem() { Header = "Издательство" };
                    miPublisher.Click += MiPublisher_Click;
                    miDictionary.Items.Add(miPublisher);

                    MenuItem miGenre = new MenuItem() { Header = "Жанры" };
                    miGenre.Click += MiGenre_Click;
                    miDictionary.Items.Add(miGenre);

                    miBook.Items.Add(miListBook);
                    miBook.Items.Add(new Separator());
                    miBook.Items.Add(miAddBook);


                    //miFile
                    //MenuItem miExit = new MenuItem() { Header = "Выйти" };
                    //MainWindow._MainMenu.Items[0]

                    MainWindow._MainMenu.Items.Add(miBook);
                    MainWindow._MainMenu.Items.Add(miDictionary);
                }
                else
                {
                    MessageBox.Show("Ошибка авторизации");
                }
            }
        }

        private void MiGenre_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._MainFrame.Navigate(new PageGenre());
        }

        private void MiPublisher_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._MainFrame.Navigate(new PagePublisher());
        }

        private void PageAuth_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MiAddBook_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._MainFrame.Navigate(new PageWelcome(null)); 
        }

        private void MiListBook_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._MainFrame.Navigate(new PageBooks());
        }

        private void InputLogin_KeyDown(object sender, KeyEventArgs e)
        {
            CheckInput(InputLogin, validateInputLogin);
        }

        private void InputLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            CheckInput(InputLogin, validateInputLogin);
        }

        private bool CheckInput(TextBox input, Label validLable)
        {
            if (string.IsNullOrWhiteSpace(input.Text))
            {
                validLable.Content = "Поля обязательное для заполнения!";
                validLable.Foreground = new SolidColorBrush(Colors.Red);
                return false;
            }
            else
            {
                validLable.Content = "";
                return true;
            }
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow();
            regWindow.Show();
        }
    }
}
