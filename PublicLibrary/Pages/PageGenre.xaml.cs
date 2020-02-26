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
    /// Логика взаимодействия для PageGenre.xaml
    /// </summary>
    public partial class PageGenre : Page
    {
        DbContext db = new DbContext(MainWindow.path);

        public PageGenre()
        {
            InitializeComponent();

            gvGenre.DataContext = new Genre();
            lwGenre.ItemsSource = db.GetAllGenres();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Genre genre = (Genre)gvGenre.DataContext;

            if (genre.Id != 0)
            {
                db.EditGenre(genre);
            }
            else
            {
                db.AddGenre(genre);
            }
            lwGenre.ItemsSource = db.GetAllGenres();
        }

        private void lwGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gvGenre.DataContext = (Genre)(((ListView)sender).SelectedItem);
        }

    }
}
